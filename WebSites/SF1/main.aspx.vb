Imports System.Diagnostics
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO

Partial Class main
    Inherits System.Web.UI.Page
    Dim insertSQL As String
    Dim sap_table As String
    Dim totalduration As Integer = 0
    Dim no_ofres, quantity, rejects, sec, i, opers As Integer
    Dim goodsqty, Sap_plan_Goods, sap_convperpiece, sap_convuom As Long
    Dim Sap_plannedstime, Sap_plannedetime, Sap_itemcode, Sap_itemname, Sap_routing, Sap_plannedqty, Sap_actualqty, Sap_requireddate, Sap_plannedsdate, Sap_plannededate, Sap_schedulingmethod, Sap_batchsize, Sap_warehouse, Sap_factor, Sap_revision, Sap_uom, Sap_status, Sap_rej_uom As String
    Dim strtimename, sql_insertSF, strSetting, Operation, startdateTime, enddateTime, machinename, remarks As String
    Dim sql_getTimetype, gettimetype, gettypeno, gettypecode As String
    Dim timeType As String = ""
    Dim hrs As Double = 0
    Dim mins As Double = 0
    Dim secs As Double = 0
    Dim remar As Integer = 0
    '##Details for MO (SAP DBase)
    Dim checkoper As Boolean
    Dim total As Double
    Dim arr_reco(26)
    Dim arr_reco_ctr As Integer = 0
    Dim rec_stat_type, rec_endtime, rec_id As String

    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Public sapconn As New SqlConnection(ConfigurationManager.ConnectionStrings("sapconnect").ToString)
    Public sapshop As New SqlConnection(ConfigurationManager.ConnectionStrings("sapshop").ToString)

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtaddsubs.Visible = False

        'Dim usertype As String
        'conn.Open()
        'Dim sql_getusertype As String = "select user_type from profiles where user_id = '" + Request.QueryString("id") + "'"
        'Dim getusertype As New SqlCommand(sql_getusertype, conn)
        'getusertype = New SqlCommand(sql_getusertype, conn)
        'usertype = getusertype.ExecuteScalar
        'conn.Close()

        'If usertype = "PR" Then
        If Request.QueryString("ut") = "3" Then
            txtaoper.Visible = False
            txthelper.Visible = False
            lblheadcount.Visible = True
            txtheadcount.Visible = True
            lbloperatorname.Text = "Name :"
            lblassopername.Visible = False
            lblhelpername.Visible = False
        End If

        If Request.QueryString("ot") = "IL" Then
            Response.Redirect("logistics.aspx")
        End If
        'get username and description
        'conn.Open()
        'Dim sql_getuname As String = "select '<div class = username><b><u>' + users.user_name +'</u></b><br>'+ profiles.description + '</div>' from users inner join profiles on users.id = profiles.user_id where users.id = " & Request.QueryString("id")
        'Dim getuname As New SqlCommand(sql_getuname, conn)
        'getuname = New SqlCommand(sql_getuname, conn)
        'lbluser.Text = getuname.ExecuteScalar
        'conn.Close()
        'end getting username and desc

        ddltype.Items.Add("SELECT PROCESS")
        lblhms.Text = hrs.ToString + ":" + mins.ToString + ":" + secs.ToString
        lbldatenow.Text = Format(Now, "MMM. dd, yyyy")
        paneltable.Visible = False
        checkoper = False

        If Request.QueryString("id") = "" Then
            Response.Redirect("Default.aspx")
        Else
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False

        End If
    End Sub

    Protected Sub txtmo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmo.TextChanged
        If IsNumeric(txtmo.Text) Then
            lblerror.ForeColor = Drawing.Color.Empty
            lblerror.Text = ""
            txtmo.BorderColor = Drawing.Color.Empty

            Dim checkopen As Boolean = False

            temporaryValue()

            If Sap_itemcode <> "" Then
                'txtgoods.Text = goodsqty

                ''get the last inserted id with the mo
                'conn.Open()
                'Dim gD As Double
                'Dim sql_getgoods As String = "select top 1 quantity from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
                'Dim getgoods As New SqlCommand(sql_getgoods, conn)
                'getgoods = New SqlCommand(sql_getgoods, conn)
                'gD = getgoods.ExecuteScalar
                'conn.Close()

                ''if gD is null, means mo is not existing
                'If IsDBNull(gD) Then
                '    txtg.Text = goodsqty
                'Else
                '    txtg.Text = gD
                '    txtgoods.Text = gD
                'End If

                conn.Open()
                Dim sql_getallmo As String = "select top 1 id from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
                Dim getallmo As New SqlCommand(sql_getallmo, conn)
                getallmo = New SqlCommand(sql_getallmo, conn)
                lblcountmo.Text = getallmo.ExecuteScalar
                conn.Close()

                displayValue()

                Panel2.Visible = True
                Panel3.Visible = True
                txtmo.ReadOnly = True

            '################################CHECKRECOVERY#####################################################
            Dim sql_getrecovery As String
            'If Request.QueryString("ot") = "ADMIN" Then
            'sql_getrecovery = "select top 1 * from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
            sql_getrecovery = "select top 1 * from shopfloors where mo = '" & txtmo.Text & "' and resource='" & machinename & "' and operation='" & Request.QueryString("ot") & "' and end_date_time = '' order by id desc"
            'Else
            '    sql_getrecovery = "select top 1 * from shopfloors where userid = '" + Request.QueryString("id") + "' and mo = '" + txtmo.Text + "' order by id desc"
            'End If
            Dim readrecovery As New SqlCommand(sql_getrecovery, conn)
            Dim recoveryrows As SqlDataReader

            conn.Open()
            recoveryrows = readrecovery.ExecuteReader
            While recoveryrows.Read
                For value As Integer = 0 To 20
                    arr_reco(value) = recoveryrows(value).ToString
                    If value = 9 Then
                        If arr_reco(value) = "" Then
                            checkopen = True
                        End If
                    End If
                Next
            End While
            conn.Close()
            If checkopen = True Then
                lblerror.Text = "System has recovery file for this MO from an unexpected shutdown. Click Recover, to load the MO."
                panelerr.CssClass = "recoverdiv"
                If Request.QueryString("ot") = "ADMIN" Then
                    cmdrecover.Visible = True
                Else
                    lblerror.Text = lblerror.Text & " <b>PLEASE CONTACT ADMIN / SUPERVISOR</b>"

                End If
                'lblddltype.Text = arr_reco(7)
                Application.Lock()
                Application("rec_id") = arr_reco(0).ToString
                Application("rec_mo") = arr_reco(1).ToString
                Application("rec_posting_date") = arr_reco(2).ToString
                Application("rec_doc_date") = arr_reco(3).ToString
                Application("rec_sbu") = arr_reco(4).ToString
                Application("rec_operation") = arr_reco(5).ToString
                Application("rec_resource") = arr_reco(6).ToString
                Application("rec_time_type") = arr_reco(7).ToString
                Application("rec_start_date_time") = arr_reco(8).ToString
                Application("rec_end_date_time") = arr_reco(9).ToString
                Application("rec_no_ofres") = arr_reco(10).ToString
                Application("rec_remarks") = arr_reco(11).ToString
                Application("rec_quatity") = arr_reco(12).ToString
                Application("rec_rejects") = arr_reco(13).ToString
                Application("rec_oper_remarks") = arr_reco(14).ToString
                Application("rec_operator") = arr_reco(15).ToString
                Application("rec_ass_operator") = arr_reco(16).ToString
                Application("rec_helper") = arr_reco(17).ToString
                Application("rec_duration") = arr_reco(18).ToString
                Application("rec_userid") = arr_reco(19).ToString
                Application("rec_fg") = arr_reco(20).ToString
                Application.UnLock()
                Sap_routing = Application("rec_sbu")
            Else
                panelerr.CssClass = ""
            End If

            '################################ENDRECOVERY#####################################################
        Else
            lblerror.Text = "MO IS NOT DEFINED"
            lblerror.ForeColor = Drawing.Color.Red
            txtmo.BorderColor = Drawing.Color.Red
            txtmo.Focus()
        End If
        Else
        lblerror.Text = "MO IS NOT DEFINED"
        lblerror.ForeColor = Drawing.Color.Red
        txtmo.BorderColor = Drawing.Color.Red
        txtmo.Focus()
        End If
    End Sub

    Protected Sub cmdgetMO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdgetMO.Click
        If txtoper.Text = "" And txtaoper.Text = "" Then
            lblerror.Text = "Operator or Assistant Operator cannot be empty."
            panelerr.CssClass = "mainerror"
            txtoper.BorderColor = Drawing.Color.Red
        Else
            panelerr.CssClass = ""
            txtmo.Focus()
            txtoper.BorderColor = Drawing.Color.Empty
            lblerror.Text = ""
            Panel1.Visible = True
            cmdgetMO.Visible = False
            btnnomo.Visible = False
            txtoper.ReadOnly = True
            txtaoper.ReadOnly = True
            txthelper.ReadOnly = True
        End If
    End Sub

    Protected Sub cmdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Panel2.Visible = False
        Panel3.Visible = False
        txtmo.Text = ""
        txtmo.Enabled = True
        txtmo.ReadOnly = False
        txtmo.Focus()
    End Sub

    Protected Sub cmdstart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdstart.Click
        If lblop.Text <> "" And ddltype.Text <> "SELECT PROCESS" Then
            'changeoperator can be active

            If lbld.Text = "L" Then
                '#########CHECK IF CHANGE OVER, then remaining time in 2hrs must be consumed before record as downtime
                Dim timetype99 = ""
                Dim sduration, eduration As DateTime

                Dim sql_checkifchangeover As String = "select start_date_time,end_date_time,duration from shopfloors where mo='" & txtmo.Text & "' and resource='" & machine_name.Text & "' and time_type='L' and remarks='Productive' order by id desc"
                Dim checkifchangeover As New SqlCommand(sql_checkifchangeover, conn)
                Dim changeoverrows As SqlDataReader
                conn.Open()
                changeoverrows = checkifchangeover.ExecuteReader
                While changeoverrows.Read
                    sduration = CDate(changeoverrows(0).ToString)
                    eduration = CDate(changeoverrows(1).ToString)
                    totalduration = totalduration + ((eduration - sduration).TotalSeconds)
                End While
                conn.Close()
                If totalduration >= 7200 Then
                    lblerr.Text = "<br>TWO HOURS ALREADY CONSUMED"
                    lbld.Text = "IN EXCESS OF TWO HOURS"
                End If
                lblerr.Text = sql_checkifchangeover 'totalduration.ToString
                '#########END CHECKING CHANGE OVER

            End If
            changeop1.Visible = True

            ddltype.BorderColor = Drawing.Color.Empty
            lbltimeCount.Text = ""
            hrs = 0
            mins = 0
            sec = 0

            cmdstart.CssClass = "curr"
            cmdcanc.CssClass = ""
            cmdstop.CssClass = ""

            ddlmm.Visible = False
            txtdd.Visible = False
            txtyy.Visible = False
            txth.Visible = False
            txtm.Visible = False
            txts.Visible = False
            Button2.Visible = False


            If lblrem2.Text = "" Then
                lblrem2.Text = (ddltype.SelectedValue).ToString
            End If

            txtrejects.BorderColor = Drawing.Color.Empty
            txtgoods.BorderColor = Drawing.Color.Empty

            lblstartTime.Text = Format(Now, "hh:mm:ss")
            lblsTime.Text = Format(Now, "HH:mm:ss")
            lblsDate.Text = Format(Now, "yyyyMMdd")
            echosd.Text = Format(Now, "yyyy-MM-dd")
            startdateTime = Format(Now, "yyyy-MM-dd hh:mm:ss")

            cmdcancel.Visible = False
            Timer1.Enabled = True
            lblendTime.Text = ""

            cmddirect.Enabled = False
            cmdindirect.Enabled = False
            cmdprodd.Enabled = False
            ddltype.Enabled = False

            Call temporaryValue()
            If lblrecover.Text = "" Then
                Call insertShopfloor()
            End If
        Else
            lblerr.Text = "Please select Process"
            ddltype.BorderColor = Drawing.Color.Red
        End If

    End Sub

    Protected Sub cmdcanc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcanc.Click
        If Timer1.Enabled = True Then
            cmdstart.CssClass = ""
            cmdcanc.CssClass = "curr"
            cmdstop.CssClass = ""

            Timer1.Enabled = False
            cmddirect.Enabled = True
            cmdindirect.Enabled = True
            cmdprodd.Enabled = True
            ddltype.Enabled = True
        Else
            lblerr.Text = "Process Not yet Started"
        End If

    End Sub

    Protected Sub cmdstop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdstop.Click

        If lblstartTime.Text <> "" Then
            txtaoper.Visible = True
            txtoper.Visible = True
            txthelper.Visible = True
            oper2.Visible = False
            aoper2.Visible = False
            helper2.Visible = False

            cmdstart.CssClass = ""
            cmdcanc.CssClass = ""
            cmdstop.CssClass = "curr"

            Timer1.Enabled = False

            lblendTime.Text = lbltimeNow.Text
            enddateTime = Format(Now, "yyyy-MM-dd hh:mm:ss")
            lbleDate.Text = Format(Now, "yyyyMMdd")
            echoed.Text = Format(Now, "yyyy-MM-dd")
            lbleTime.Text = Format(Now, "HH:mm:ss")

            Call temporaryValue()
            Call updateShopfloor()
            If lblnomo.Text = "" Then
                Call createExcel()
            End If
            ddltype.Enabled = True
            cmddirect.Enabled = True
            cmdindirect.Enabled = True
            cmdprodd.Enabled = True

            paneltable.Visible = True
            lblprorem.Text = strtimename & " - " & lblrem.Text
            lblmo.Text = txtmo.Text
            lblfgdesc.Text = Sap_itemcode
            lbloperator.Text = txtoper.Text
            lblassoper.Text = txtaoper.Text
            lblhelper.Text = txthelper.Text
            lblstart.Text = lblstartTime.Text
            lblend.Text = lblendTime.Text
            lblgoods.Text = txtgoods.Text
            lblrejects.Text = txtrejects.Text
            lblremarks.Text = lbloper_remarks.Text

            '############Edit time if headcount is exist
            Dim startdatetimes = CDate(echosd.Text & " " & lblstartTime.Text)
            Dim enddatetimes = CDate(echoed.Text & " " & lblendTime.Text)
            Dim total = enddatetimes - startdatetimes
            Dim headcount = Integer.Parse(txtheadcount.Text)

            Dim totalseconds = total.TotalSeconds * headcount
            Dim over = DateAdd("s", totalseconds, enddateTime)
            lblendTime.Text = Format(CDate(over.ToString), "HH:mm:ss")
            '############end headcount

            'clear everything
            cmdindirect.CssClass = ""
            cmddirect.CssClass = ""
            cmdprodd.CssClass = ""

            ddltype.Items.Clear()
            ddltype.Items.Add("SELECT PROCESS")

            cmdstop.CssClass = ""

            lblhms.Text = "00:00:00"
            echosd.Text = ""
            echoed.Text = ""
            lblstartTime.Text = ""
            lblendTime.Text = ""

            lbloper_remarks.Text = ""
            txtrejects.Text = ""
            txtaddsubs.Text = ""
        Else
            lblerr.Text = "Process Not yet Started"
        End If
    End Sub

    Private Sub cmddirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddirect.Click
        opers = 1
        Call checkOperation()

        cmddirect.CssClass = "curr"
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = ""

        lblerr.Text = ""
        ddltype.BorderColor = Drawing.Color.Empty
        lblop.Text = opers

    End Sub

    Protected Sub cmdindirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdindirect.Click
        opers = 2
        Call checkOperation()

        cmddirect.CssClass = ""
        cmdindirect.CssClass = "curr"
        cmdprodd.CssClass = ""

        lblerr.Text = ""
        ddltype.BorderColor = Drawing.Color.Empty
        lblop.Text = opers
    End Sub

    Protected Sub cmdprodd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdprodd.Click
        opers = 3
        Call checkOperation()

        cmddirect.CssClass = ""
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = "curr"

        lblerr.Text = ""
        ddltype.BorderColor = Drawing.Color.Empty
        lblop.Text = opers
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Timer1.Enabled = True Then
            If lbltimeCount.Text = "" Then
                sec = Integer.TryParse("2", i)
                lbltimeCount.Text = "2"
            Else
                sec = Integer.TryParse(lbltimeCount.Text, i)
            End If
            lbltimeCount.Text = (i + 1).ToString
            hrs = Math.Truncate(i / 3600)
            mins = Math.Truncate((i / 60) - (hrs * 60))
            secs = Math.Truncate(((i / 60) - (hrs * 60) - mins) * 60)
            lblhms.Text = hrs.ToString + ":" + mins.ToString + ":" + secs.ToString
            lbltimeNow.Text = Format(Now, "hh:mm:ss")
            If lbld.Text = "L" Then
                If secs = 10 Then
                    'start change over
                    lbler.Text = "<br>TWO HOURS ALREADY CONSUMED"
                    lblendTime.Text = lbltimeNow.Text
                    enddateTime = Format(Now, "yyyy-MM-dd hh:mm:ss")
                    lbleDate.Text = Format(Now, "yyyyMMdd")
                    echoed.Text = Format(Now, "yyyy-MM-dd")
                    lbleTime.Text = Format(Now, "HH:mm:ss")

                    Call temporaryValue()
                    Call updateShopfloor()
                    Call createExcel()

                    ddltype.Enabled = True
                    cmddirect.Enabled = True
                    cmdindirect.Enabled = True
                    cmdprodd.Enabled = True

                    paneltable.Visible = True
                    lbld.Text = "IN EXCESS OF TWO HOURS"
                    lblprorem.Text = strtimename & " - " & lblrem.Text
                    lblmo.Text = txtmo.Text
                    lblfgdesc.Text = Sap_itemcode
                    lbloperator.Text = txtoper.Text
                    lblassoper.Text = txtaoper.Text
                    lblhelper.Text = txthelper.Text
                    lblstart.Text = lblstartTime.Text
                    lblend.Text = lblendTime.Text
                    lblgoods.Text = txtgoods.Text
                    lblrejects.Text = txtrejects.Text
                    lblremarks.Text = lbloper_remarks.Text
                    'change over end

                    'echosd.Text = echoed.Text
                    'lblsDate.Text = lbleDate.Text
                    'lblstartTime.Text = lblendTime.Text
                    'lblsTime.Text = lbleTime.Text
                    'echosd.Text = ""
                    'lblrem.Text = "IN EXCESS OF TWO HOURS"
                    'lbleDate.Text = ""
                    'lblendTime.Text = ""
                    'lbleTime.Text = ""
                    Call insertShopfloor()
                End If
            End If
        End If
    End Sub

    Private Sub temporaryValue()

        conn.Open()
        Dim table As String
        Dim sql_gettableName As String = "select path_name from path_settings where id = 2"
        Dim gettable As New SqlCommand(sql_gettableName, conn)
        gettable = New SqlCommand(sql_gettableName, conn)
        table = gettable.ExecuteScalar
        conn.Close()

        Dim sql_getsapDetails As String = "select top 1 U_ItemCode,U_Description,U_RevisionName,U_RtgCode,U_Quantity,U_Factor,U_ActualQty, U_Warehouse,U_BatchSize,U_Status,U_RequiredDate,U_PlannedStartDate,U_PlannedStartTime,U_PlannedEndDate,U_PlannedEndTime,U_SchedulingMtd,U_InventoryUoM,U_Status, U_SubsPlanQty, U_SubsUOM,U_SubsQtyPerPcs from [" + table + "] where DocNum = '" + txtmo.Text + "'"
        Dim readsapdetails As New SqlCommand(sql_getsapDetails, sapconn)
        Dim saprows As SqlDataReader

        sapconn.Open()
        saprows = readsapdetails.ExecuteReader
        If saprows.HasRows Then

            While saprows.Read
                If IsDBNull(saprows(0)) Then
                    Sap_itemcode = ""
                Else
                    Sap_itemcode = If(Not IsDBNull(Sap_itemcode), saprows(0).ToString, Sap_itemcode)
                End If

                If IsDBNull(saprows(1)) Then
                    Sap_itemname = ""
                Else
                    Sap_itemname = saprows(1).ToString
                End If

                If IsDBNull(saprows(2)) Then
                    Sap_revision = ""
                Else
                    Sap_revision = saprows(2).ToString
                End If

                If IsDBNull(saprows(3)) Then
                    Sap_routing = ""
                Else
                    Sap_routing = saprows(3).ToString
                End If

                If IsDBNull(saprows(4)) Then
                    Sap_plannedqty = 0
                Else
                    Sap_plannedqty = saprows(4)
                End If

                If IsDBNull(saprows(5)) Then
                    Sap_factor = ""
                Else
                    Sap_factor = saprows(5).ToString
                End If

                If IsDBNull(saprows(6)) Then
                    Sap_actualqty = ""
                Else
                    Sap_actualqty = saprows(6).ToString
                End If

                If IsDBNull(saprows(7)) Then
                    Sap_warehouse = ""
                Else
                    Sap_warehouse = saprows(7).ToString
                End If

                If IsDBNull(saprows(8)) Then
                    Sap_batchsize = ""
                Else
                    Sap_batchsize = saprows(8).ToString
                End If

                If IsDBNull(saprows(10)) Then
                    Sap_requireddate = ""
                Else
                    Sap_requireddate = FormatDateTime(CDate(saprows(10).ToString), 1)
                End If

                If IsDBNull(saprows(11)) Then
                    Sap_plannedsdate = ""
                Else
                    Sap_plannedsdate = FormatDateTime(CDate(saprows(11).ToString), 1)
                End If

                If IsDBNull(saprows(12)) Then
                    Sap_plannedstime = ""
                Else
                    Sap_plannedstime = saprows(12).ToString().Substring(0, saprows(12).ToString().Length - 2) & ":" & (Right(saprows(12).ToString, 2))
                End If

                If IsDBNull(saprows(13)) Then
                    Sap_plannededate = ""
                Else
                    Sap_plannededate = FormatDateTime(CDate(saprows(13).ToString), 1)
                End If

                If IsDBNull(saprows(14)) Or saprows(14) = 0 Then
                    Sap_plannedetime = ""
                Else
                    Sap_plannedetime = saprows(14).ToString().Substring(0, saprows(14).ToString().Length - 2) & ":" & (Right(saprows(14).ToString(), 2))
                End If

                If IsDBNull(saprows(16)) Then
                    Sap_uom = ""
                Else
                    Sap_uom = saprows(16).ToString
                End If

                If IsDBNull(saprows(17)) Then
                    Sap_status = ""
                Else
                    Sap_status = saprows(17).ToString
                End If


                If IsDBNull(saprows(18)) Then
                    Sap_plan_Goods = 0
                Else
                    Sap_plan_Goods = saprows(18)
                End If

                If IsDBNull(saprows(19)) Then
                    Sap_rej_uom = ""
                Else

                    Sap_rej_uom = saprows(19).ToString
                End If

                If IsDBNull(saprows(20)) Then
                    sap_convperpiece = 0
                Else
                    sap_convperpiece = Math.Floor(saprows(20)).ToString
                End If

            End While
        sapconn.Close()

        Operation = Request.QueryString("ot")
        no_ofres = 1
        goodsqty = Sap_plan_Goods

        Dim sql_getuname As String = "select user_name from users where id = '" + Request.QueryString("id") + "'"
        Dim reduname As New SqlCommand(sql_getuname, conn)
        Dim unamerows As SqlDataReader

        conn.Open()
        unamerows = reduname.ExecuteReader
        While unamerows.Read
            machinename = unamerows(0)
        End While
        conn.Close()
            machine_name.Text = machinename
        'machinename = Session("user")
        no_ofres = 1
        quantity = Integer.TryParse(txtgoods.Text, quantity)
        rejects = Integer.TryParse(txtrejects.Text, rejects)
        remarks = lbloper_remarks.Text
        Else
        If lblnomo.Text = "" Then
            lblerror.Text = "MO IS NOT CREATED YET"
        End If
        End If
    End Sub

    Private Sub displayValue()

        'get the last inserted id with the mo
        txtg.Text = Sap_plan_Goods

        conn.Open()
        Dim gD As Integer
        Dim sql_getgoods As String = "select top 1 quantity from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
        Dim getgoods As New SqlCommand(sql_getgoods, conn)
        getgoods = New SqlCommand(sql_getgoods, conn)
        gD = getgoods.ExecuteScalar
        conn.Close()

        If gD = 0 Then
            txtg.Text = Sap_plan_Goods
            txtgoods.Text = Sap_plan_Goods
        Else
            txtg.Text = gD
            txtgoods.Text = gD
        End If

        lbluom.Text = Sap_rej_uom

        Dim omorcode As String
        conn.Open()
        Dim sql_getomorcode As String = "select name from omor_status where code = '" & Sap_status & "'"
        Dim get_omorcode As New SqlCommand(sql_getomorcode, conn)
        get_omorcode = New SqlCommand(sql_getomorcode, conn)
        omorcode = get_omorcode.ExecuteScalar
        conn.Close()

        Dim goodsin As String = ""
        If txtgoods.Text <> "" And sap_convperpiece.ToString <> "" Then
            goodsin = (Integer.Parse(txtgoods.Text) * Integer.Parse(sap_convperpiece.ToString))
        End If
        output.Text = "<br><table border=1 class='sap_details'>" & _
        "<tr class='head'><td>FG Code</td><td>Status</td><td>Routing</td></tr>" & _
        "<tr><td>" & Sap_itemcode & "</td><td>" & omorcode & "</td><td>" & Sap_routing & "</td></tr>" & _
        "<tr class='head'><td colspan = 3>Description</td></tr>" & _
        "<tr><td colspan = 3>" & Sap_itemname & "</td></tr>" & _
        "<tr class='head'><td>Warehouse</td><td>Planned Qty</td><td>Actual Qty</td></tr>" & _
        "<tr><td>" & Sap_warehouse & "</td><td>" & Sap_plannedqty & "</td><td>" & Sap_actualqty & "</td></tr>" & _
        "<tr class='head'><td>Req. Date</td><td>Planned Start Date</td><td>Planned End Date</td></tr>" & _
        "<tr><td>" & Sap_requireddate & "</td><td>" & Sap_plannedsdate & " " & Sap_plannedstime & "</td><td>" & Sap_plannededate & " " & Sap_plannedetime & "</td></tr>" & _
        "<tr class='head'><td>Pc per " & Sap_rej_uom & "</td><td colspan=2>Goods in " & Sap_rej_uom & "</td></tr>" & _
        "<tr><td> " & sap_convperpiece & " </td><td colspan=2>" & goodsin & "</td></tr>" & _
        "</table>"
    End Sub

    Private Sub checkOperation()
        ddltype.Items.Clear()
        ddltype.Items.Add("SELECT PROCESS")
        '1=direct 2 =indirect 3=prod.dowtime
        Dim sql_getopers As String
        If lblnomo.Text = "" Then
            sql_getopers = "select code,name from timeTypes where type='" + opers.ToString + "'"
        Else
            sql_getopers = "select code,name from timeTypes where type='2' and code = 'NJR'"
        End If

        Dim readoper As New SqlCommand(sql_getopers, conn)
        Dim operRows As SqlDataReader

        conn.Open()
        operRows = readoper.ExecuteReader
        While operRows.Read
            Dim newItem As New ListItem(operRows(1).ToString, operRows(0).ToString)
            ddltype.Items.Add(newItem)
        End While
        conn.Close()
    End Sub

    Private Sub insertShopfloor()

        Dim type_desc As String = ""
        conn.Open()
        Dim sql_gettype_desc As String = "select description from timeTypes where code = '" & ddltype.SelectedValue & "'"
        Dim gettype_desc As New SqlCommand(sql_gettype_desc, conn)
        gettype_desc = New SqlCommand(sql_gettype_desc, conn)
        type_desc = gettype_desc.ExecuteScalar
        conn.Close()

            Dim insert_timetype As String = ""
            If lblop.Text = "1" Then
                insert_timetype = ddltype.SelectedValue
            Else
                conn.Open()
                Dim sql_get1 As String = "select top 1 time_type from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
                Dim get1 As New SqlCommand(sql_get1, conn)
                get1 = New SqlCommand(sql_get1, conn)
                insert_timetype = get1.ExecuteScalar
                conn.Close()
            End If

        If lbld.Text = "IN EXCESS OF TWO HOURS" Then
            sql_insertSF = "insert into shopfloors values ('" + txtmo.Text + "', '" + lbleDate.Text + "', '" + lbleDate.Text + "', '" + Sap_routing + "', '" + Operation + "', '" + machinename + "', '" + insert_timetype + "', '" + enddateTime + "', '', '" + no_ofres.ToString + "', '" + lbld.Text + "','" + txtgoods.Text + "','" + txtrejects.Text + "','" + lbloper_remarks.Text + "','" + txtoper.Text + "','" + txtaoper.Text + "','" + txthelper.Text + "','" + lblhms.Text + "','" + Request.QueryString("id") + "','" + Sap_itemcode + " - " + Sap_itemname + "', '" + txtheadcount.Text + "','0')"
        Else
            sql_insertSF = "insert into shopfloors values ('" + txtmo.Text + "', '" + lbleDate.Text + "', '" + lbleDate.Text + "', '" + Sap_routing + "', '" + Operation + "', '" + machinename + "', '" + insert_timetype + "', '" + startdateTime + "', '" + enddateTime + "', '" + no_ofres.ToString + "', '" + type_desc + "','" + txtgoods.Text + "','" + txtrejects.Text + "','" + lbloper_remarks.Text + "','" + txtoper.Text + "','" + txtaoper.Text + "','" + txthelper.Text + "','" + lblhms.Text + "','" + Request.QueryString("id") + "','" + Sap_itemcode + " - " + Sap_itemname + "', '" + txtheadcount.Text + "','0')"
        End If
        Dim insertSF As New SqlCommand(sql_insertSF, conn)

        conn.Open()
        Try
            insertSF.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()

        'get id
        Dim sql_getlastInsertedID As String
        Dim lastInsertedID As Integer

        conn.Open()
        'sql_getlastInsertedID = "SELECT IDENT_CURRENT('shopfloors')"
        sql_getlastInsertedID = "select top 1 id from shopfloors where userid = '" + Request.QueryString("id") + "' and mo = '" + txtmo.Text + "' order by id desc"
        Dim getLastID As New SqlCommand(sql_getlastInsertedID, conn)
        getLastID = New SqlCommand(sql_getlastInsertedID, conn)
        lastInsertedID = getLastID.ExecuteScalar
        conn.Close()
        lbloutmo.Text = lastInsertedID.ToString
    End Sub

    Public Sub createExcel()
        Dim appXL As Excel.Application
        Dim wbXl As Excel.Workbook
        Dim shXL As Excel.Worksheet
        Dim raXL As Excel.Range

        ' Start Excel and get Application object.
        appXL = CreateObject("Excel.Application")
        appXL.Visible = True
        ' Add a new workbook.
        'wbXl = appXL.Workbooks.Add
        Dim strdir As String = Server.MapPath("~/")
        wbXl = appXL.Workbooks.Open(strdir & "timebookingtemplate.xls")
        shXL = wbXl.ActiveSheet
        'Add table headers going cell by cell.
        'Dim heads() As String = {"", "MONo", "PostingDate", "DocDate", "SBU", "Operation", "Resource", "TimeType", "StartDate", "StartTime", "EndDate", "EndTime", "NoofResources", "Remarks", "Quantity", "RejectQty"}
        'For head1 As Integer = 1 To 15
        '    shXL.Cells(1, head1).Value = heads(head1)
        'Next
        Dim remarks1 As String = ""
        If lblrem.Text = "" Then
            remarks1 = Application("rec_remarks")
        Else
            remarks1 = lblrem.Text
        End If
        If lbld.Text = "IN EXCESS OF TWO HOURS" Then
            remarks1 = lbld.Text
        End If


        Dim insert_timetype As String = ""
        If lblop.Text = "1" Then
            insert_timetype = ddltype.SelectedValue
        Else
            conn.Open()
            Dim sql_get1 As String = "select top 1 time_type from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
            Dim get1 As New SqlCommand(sql_get1, conn)
            get1 = New SqlCommand(sql_get1, conn)
            insert_timetype = get1.ExecuteScalar
            conn.Close()
        End If

        Dim body() As String = {"", txtmo.Text, lbleDate.Text, lbleDate.Text, Sap_routing, Operation, machinename, UCase(insert_timetype), lblsDate.Text, lblsTime.Text, lbleDate.Text, lbleTime.Text, no_ofres, remarks1, txtgoods.Text, txtrejects.Text}

        For body1 As Integer = 1 To 15
            shXL.Cells(2, body1).Value = body(body1)
        Next
        appXL.Visible = True
        appXL.UserControl = True
        ' Release object references.

        Dim sql_getsettings As String = "select * from path_settings where id = 1"
        Dim readSettings As New SqlCommand(sql_getsettings, conn)
        Dim settingsRow As SqlDataReader

        conn.Open()
        settingsRow = readSettings.ExecuteReader
        While settingsRow.Read
            strSetting = settingsRow(1)
        End While
        conn.Close()

        Dim myXlsFileName As String = "MO" & txtmo.Text & Format(Now, "yyyyMMdd") & Format(Now, "hhmmss")
        If Not File.Exists("C:\Documents and Settings\All Users\Desktop\" & myXlsFileName & ".xls") Then
            wbXl.SaveAs(Filename:=strSetting & myXlsFileName & ".xls")
        End If

        raXL = Nothing
        shXL = Nothing
        wbXl = Nothing
        appXL.Quit()
        appXL = Nothing
        Exit Sub
Err_Handler:
        MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)

    End Sub

    Protected Sub txtrejects_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrejects.TextChanged
        If txtaddsubs.Text <> "" Then
            total = (txtg.Text + Integer.Parse(txtaddsubs.Text)) - txtrejects.Text
        Else
            total = txtg.Text - txtrejects.Text
        End If

        txtgoods.Text = total
        If total <= 0 Then
            lblerr.Text = "Reject must be less than Good"
            txtrejects.BorderColor = Drawing.Color.Red
        Else
            txtrejects.BorderColor = Drawing.Color.Empty
            lblerr.Text = ""
        End If
    End Sub

    Private Sub updateShopfloor()
        'Dim sql_getlastInsertedID As String
        'Dim lastInsertedID As Integer

        'If Application("rec_id") = "" Then
        '    conn.Open()
        '    'sql_getlastInsertedID = "SELECT IDENT_CURRENT('shopfloors')"
        '    sql_getlastInsertedID = "select top 1 id from shopfloors where userid = '" + Request.QueryString("id") + "' and mo = '" + txtmo.Text + "' order by id desc"
        '    Dim getLastID As New SqlCommand(sql_getlastInsertedID, conn)
        '    getLastID = New SqlCommand(sql_getlastInsertedID, conn)
        '    lastInsertedID = getLastID.ExecuteScalar
        '    conn.Close()
        'Else
        '    lastInsertedID = Application("rec_id")
        '    lbloutmo.Text = Application("rec_id")
        'End If

        Dim sql_gettimeType As String = "select description,name from timeTypes where code = '" + ddltype.SelectedValue + "'"
        Dim readtype As New SqlCommand(sql_gettimeType, conn)
        Dim typerows As SqlDataReader

        conn.Open()
        typerows = readtype.ExecuteReader
        While typerows.Read
            timeType = typerows(0)
            strtimename = typerows(1)
        End While
        conn.Close()
        lblrem.Text = timeType
        'lble.Text = ddltype.SelectedValue
        'direct
        Dim strdirect As String
        Dim ddl_type As String = ""
        If lblop.Text = "1" Then
            strdirect = ddltype.SelectedValue
            ddl_type = timeType
        Else
            strdirect = lblddltype.Text
            ddl_type = ddltype.SelectedItem.ToString
        End If
        'Dim rid As String
        'If lblrecover.Text = "" Then
        '    rid = lastInsertedID.ToString
        'Else
        '    rid = Application("rec_id")
        'End If
        Dim sstartdatetime = CDate(echosd.Text & " " & lblstartTime.Text)
        Dim senddatetime = CDate(echoed.Text & " " & lblendTime.Text)
        Dim hms = senddatetime - sstartdatetime

        lblhms.Text = hms.ToString()

        sql_insertSF = "UPDATE shopfloors set posting_date = '" + echoed.Text + "', doc_date = '" + echoed.Text + "', end_date_time='" + echoed.Text + " " + lblendTime.Text + "', quantity='" + txtgoods.Text + "', rejects='" + txtrejects.Text + "', oper_remarks='" + lbloper_remarks.Text + "', duration='" + lblhms.Text + "', userid=" + Request.QueryString("id") + ", headcount = '" + txtheadcount.Text + "', addsub = '" + lblsubsid.Text + "' where id = '" + lbloutmo.Text + "'"

            Dim insertSF As New SqlCommand(sql_insertSF, conn)
            conn.Open()
            Try
                insertSF.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
        conn.Close()
    End Sub

    Protected Sub cmdrecover_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdrecover.Click
        If Request.QueryString("ot") = "ADMIN" Then
            ddlmm.Visible = True
            txtdd.Visible = True
            txtyy.Visible = True
            txth.Visible = True
            txtm.Visible = True
            txts.Visible = True

            Button2.Visible = True
            cmdstart.Visible = False
            cmdcanc.Visible = False
            cmdstop.Visible = False

            'lblerr.Text = Application("rec_id")

            gettimetype = ""
            gettypeno = ""
            gettypecode = ""

            sql_getTimetype = "select name,type, code from timeTypes where code = '" + Application("rec_time_type") + "'"
            Dim readtimetype As New SqlCommand(sql_getTimetype, conn)
            Dim ttyperows As SqlDataReader

            conn.Open()
            ttyperows = readtimetype.ExecuteReader
            While ttyperows.Read
                gettimetype = ttyperows(0)
                gettypeno = ttyperows(1)
                gettypecode = ttyperows(2)
            End While
            conn.Close()

            ddltype.Items.Clear()
            ddltype.Items.Add(gettimetype)
            lblgoods.Text = Application("rec_goods")
            txtrejects.Text = Application("rec_rejects")
            lblhms.Text = Application("rec_duration")
            lblstartTime.Text = Hour(Application("rec_start_date_time")) & ":" & Minute(Application("rec_start_date_time")) & ":" & Second(Application("rec_start_date_time"))
            lblsDate.Text = Format(CDate(Application("rec_start_date_time")), "yyyyMMdd")
            echosd.Text = Format(CDate(Application("rec_start_date_time")), "yyyy-MM-dd")
            lbloper_remarks.Text = Application("rec_oper_remarks")
            lbltimeCount.Text = Application("rec_duration")
            lblop.Text = gettypeno
            lblddltype.Text = gettypecode
            lblrem2.Text = Application("rec_time_type")
            lblrem.Text = Application("rec_remarks")
            cmdrecover.Visible = False
            lblerror.Text = "You are processing a recovered MO."
            lblrecover.Text = "recover"
            lbloutmo.Text = Application("rec_id")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim shour = txth.Text
        Dim smin = txtm.Text
        Dim ssec = txts.Text
        Dim dd = txtdd.Text
        Dim yy = txtyy.Text
        Dim er As String

        If IsNumeric(shour) And IsNumeric(smin) And IsNumeric(ssec) And IsNumeric(dd) And IsNumeric(yy) Then
            If shour <> "" And smin <> "" And ssec <> "" And dd <> "" And yy <> "" Then
                txth.BorderColor = Drawing.Color.Empty
                txtm.BorderColor = Drawing.Color.Empty
                txts.BorderColor = Drawing.Color.Empty
                If shour > 24 Then
                    er = er + "<br>Error in Hour"
                    txth.BorderColor = Drawing.Color.Red
                Else
                    txth.BorderColor = Drawing.Color.Empty
                End If
                If smin > 60 Then
                    er = er + "<br>Erron in Minute"
                    txtm.BorderColor = Drawing.Color.Red
                Else
                    txtm.BorderColor = Drawing.Color.Empty
                End If
                If ssec > 60 Then
                    er = er + "<br>Error in Seconds"
                    txts.BorderColor = Drawing.Color.Red
                Else
                    txts.BorderColor = Drawing.Color.Empty
                End If
                If dd > 31 Then
                    txtdd.BorderColor = Drawing.Color.Red
                    er = er + "<br>Error in date"
                Else
                    txtdd.BorderColor = Drawing.Color.Empty
                End If
            Else
                er = er + "<br>Time cannot be Blank"
                txth.BorderColor = Drawing.Color.Red
                txtm.BorderColor = Drawing.Color.Red
                txts.BorderColor = Drawing.Color.Red
            End If
        Else
            er = er + "<br>Invalid time"
        End If
        If er <> "" Then
            lble.Text = er
        Else
            lble.Text = ""
            'stop

            Dim recdate As String = ddlmm.SelectedValue & " " & dd.ToString & ", " & yy.ToString & " " & shour.ToString & ":" & smin.ToString & ":" & ssec.ToString

            enddateTime = Format(CDate(recdate), "yyyy-MM-dd hh:mm:ss")
            lbleDate.Text = Format(CDate(recdate), "yyyyMMdd")
            echoed.Text = Format(CDate(recdate), "yyyy-MM-dd")
            lbleTime.Text = Format(CDate(recdate), "HH:mm:ss")

            Call temporaryValue()
            Call updateShopfloor()
            Call createExcel()

            paneltable.Visible = True
            lblprorem.Text = strtimename
            lblmo.Text = txtmo.Text
            lblfgdesc.Text = Sap_itemcode
            lbloperator.Text = txtoper.Text
            lblassoper.Text = txtaoper.Text
            lblhelper.Text = txthelper.Text
            lblstart.Text = lblstartTime.Text
            lblend.Text = lblendTime.Text
            lblgoods.Text = txtgoods.Text
            lblrejects.Text = txtrejects.Text
            lblremarks.Text = lbloper_remarks.Text
        End If
    End Sub

    Protected Sub ddltype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltype.SelectedIndexChanged
        lbld.Text = ddltype.SelectedValue
        lblrem2.Text = ddltype.SelectedValue
        'get time type
        conn.Open()
        Dim sql_gettimetypes As String = "select description from timeTypes where code = '" + ddltype.SelectedValue + "'"
        Dim gettimetype As New SqlCommand(sql_gettimetypes, conn)
        gettimetype = New SqlCommand(sql_gettimetypes, conn)
        lblrem.Text = gettimetype.ExecuteScalar
        conn.Close()

    End Sub

    'Protected Sub btnaddsubs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddsubs.Click

    '    'select all from history with the given mo and used must be 0
    '    Dim qtySA As Double
    '    Dim historid As New List(Of Integer)
    '    qtySA = 0
    '    Dim sql_gethistorytable As String = "select id,qty from history_addsubs where mo = '" & txtmo.Text & "' and used = 0"
    '    Dim gethistorytable As New SqlCommand(sql_gethistorytable, sapshop)
    '    Dim rowgethistorytable As SqlDataReader

    '    sapshop.Open()
    '    rowgethistorytable = gethistorytable.ExecuteReader
    '    While rowgethistorytable.Read
    '        qtySA = qtySA + rowgethistorytable(1)
    '        historid.Add(rowgethistorytable(0))
    '        lblerinadd.Text = lblerinadd.Text & rowgethistorytable(0).ToString
    '    End While
    '    sapshop.Close()
    '    If btnaddsubs.Text = "Search Additional Substrate" Then
    '        If historid Is Nothing Then
    '            'if history is null
    '            btnaddsubs.Text = "No Additional Substrate"
    '            btnaddsubs.ToolTip = "If you have already requested Substrate Click Again"
    '            btnaddsubs.CssClass = "noadditionalsubs"
    '        Else
    '            For Each x In historid
    '                lblerinadd.Text = x.ToString
    '                Dim sql_udpdatehistory = "update history_addsubs set used=1 where id=" & x
    '                Dim udpdatehistory As New SqlCommand(sql_udpdatehistory, sapshop)
    '                sapshop.Open()
    '                Try
    '                    udpdatehistory.ExecuteNonQuery()
    '                Catch ex As System.Data.SqlClient.SqlException
    '                    MsgBox(ex.Message)
    '                End Try
    '                sapshop.Close()
    '            Next
    '            If qtySA <> 0 Then
    '                btnaddsubs.Text = "You have Additional " & qtySA.ToString & ". Click to set Additional Substrate"
    '                btnaddsubs.CssClass = "additionalTrue"
    '            End If
    '        End If
    '    ElseIf btnaddsubs.Text = "No Additional Substrate" Then
    '        btnaddsubs.CssClass = ""
    '        btnaddsubs.Text = "Search Additional Substrate"
    '    Else
    '        txtaddsubs.Text = Math.Floor(qtySA).ToString
    '        txtgoods.Text = Integer.Parse(txtg.Text) + Integer.Parse(txtaddsubs.Text)
    '        btnaddsubs.CssClass = ""
    '        btnaddsubs.Text = "Search Additional Substrate"
    '    End If

    'End Sub

    Private Sub cleareverything()
        lblhms.Text = "00:00:00"
        echosd.Text = ""
        lblstartTime.Text = ""
        echosd.Text = ""
        lblendTime.Text = ""
        lbloper_remarks.Text = ""
        txtgoods.Text = ""
        txtrejects.Text = ""
        txtaddsubs.Text = ""
    End Sub

    Protected Sub changeop1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles changeop1.Click
        If changeop1.Text <> "Save" Then
            txtoper.Enabled = True
            txtaoper.Enabled = True
            txthelper.Enabled = True

            txtoper.Visible = False
            txtaoper.Visible = False
            txthelper.Visible = False
            oper2.Visible = True
            aoper2.Visible = True
            helper2.Visible = True

            changeop1.Text = "Save"
        Else
            txtoper.Enabled = False
            txtaoper.Enabled = False
            txthelper.Enabled = False
            txtoper.ReadOnly = False
            txtaoper.ReadOnly = False
            txthelper.ReadOnly = False

            changeop1.Text = "Change Operator"
            echoed.Text = Format(Now, "yyyy-MM-dd")
            lbleDate.Text = Format(Now, "yyyyMMdd")
            lblendTime.Text = Format(Now, "hh:mm:ss")
            Call updateShopfloor()
            Call temporaryValue()

            'insertShopfloor()
            Dim type_desc As String = ""
            txtaoper.Visible = False
            txtoper.Visible = False
            txthelper.Visible = False
            oper2.Visible = True
            aoper2.Visible = True
            helper2.Visible = True

            conn.Open()
            Dim sql_gettype_desc As String = "select description from timeTypes where code = '" & ddltype.SelectedValue & "'"
            Dim gettype_desc As New SqlCommand(sql_gettype_desc, conn)
            gettype_desc = New SqlCommand(sql_gettype_desc, conn)
            type_desc = gettype_desc.ExecuteScalar
            conn.Close()

            Dim insert_timetype As String = ""
            If lblop.Text = "1" Then
                insert_timetype = ddltype.SelectedValue
            Else
                conn.Open()
                Dim sql_get1 As String = "select top 1 time_type from shopfloors where mo = '" + txtmo.Text + "' order by id desc"
                Dim get1 As New SqlCommand(sql_get1, conn)
                get1 = New SqlCommand(sql_get1, conn)
                insert_timetype = get1.ExecuteScalar
                conn.Close()
            End If
            sql_insertSF = "insert into shopfloors values ('" + txtmo.Text + "', '" + lbleDate.Text + "', '" + lbleDate.Text + "', '" + Sap_routing + "', '" + Operation + "', '" + machinename + "', '" + insert_timetype + "', '" + echoed.Text + " " + lblendTime.Text + "', '', '" + no_ofres.ToString + "', '" + type_desc + "','" + txtgoods.Text + "','" + txtrejects.Text + "','" + lbloper_remarks.Text + "','" + oper2.Text + "','" + aoper2.Text + "','" + helper2.Text + "','" + lblhms.Text + "','" + Request.QueryString("id") + "','" + Sap_itemcode + " - " + Sap_itemname + "','" + Integer.Parse(txtheadcount.Text) + "','0')"
            Dim insertSF As New SqlCommand(sql_insertSF, conn)
            conn.Open()
            Try
                insertSF.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
            conn.Close()

            conn.Open()
            Dim sql_getlastInsertedID As String
            sql_getlastInsertedID = "select top 1 id from shopfloors where userid = '" + Request.QueryString("id") + "' and mo = '" + txtmo.Text + "' order by id desc"
            Dim getLastID As New SqlCommand(sql_getlastInsertedID, conn)
            getLastID = New SqlCommand(sql_getlastInsertedID, conn)
            Dim lastInsertedID = getLastID.ExecuteScalar
            conn.Close()
            lbloutmo.Text = lastInsertedID.ToString
        End If
    End Sub

    Protected Sub btnnomo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnomo.Click
        If txtoper.Text = "" And txtaoper.Text = "" Then
            lblerror.Text = "Operator or Assistant Operator cannot be empty."
            panelerr.CssClass = "mainerror"
            txtoper.BorderColor = Drawing.Color.Red
        Else
            lblgood.Visible = False
            txtgoods.Visible = False
            lblrejects1.Visible = False
            txtrejects.Visible = False
            lbladdsubs.Visible = False
            txtaddsubs.Visible = False
            cmdprodd.Visible = False

            cmdgetMO.Visible = False
            btnnomo.Visible = False
            lblerror.Text = ""
            panelerr.CssClass = "mainerror"
            txtrejects.ReadOnly = True
            txtoper.BorderColor = Drawing.Color.Empty
            'btnaddsubs.Visible = False
            lblnomo.Text = "YOU ARE PROCESSING NON-RELATED JOB DOWNTIME"
            cmddirect.Visible = False

            Panel2.Visible = True
            Panel3.Visible = True
            txtmo.ReadOnly = True
        End If
    End Sub

    Private Sub getSAPTable()
        conn.Open()
        Dim sql_gettableName As String = "select path_name from path_settings where id = 2"
        Dim gettable As New SqlCommand(sql_gettableName, conn)
        gettable = New SqlCommand(sql_gettableName, conn)
        sap_table = gettable.ExecuteScalar
        conn.Close()
    End Sub

    Private Sub insertsubstrate(ByVal totalSA As Double, ByVal qtySA As Double)
        Dim sql_insertsubstrate As String
        sql_insertsubstrate = "insert into history_adds values('" & txtmo.Text & "','" & Request.QueryString("id") & "','" & qtySA & "','" & totalSA & "','" & Request.QueryString("ot") & "')"
        Dim insertsubstrate As New SqlCommand(sql_insertsubstrate, conn)
        conn.Open()
        Try
            insertsubstrate.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()

    End Sub

    Protected Sub txtheadcount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtheadcount.TextChanged
        If IsNumeric(txtheadcount.Text) Then
            lblerrinhead.Text = ""
            lblerrinhead.ForeColor = Drawing.Color.Empty
            If txtheadcount.Text <= 0 Then
                lblerrinhead.Text = "Invalid Head Count. Head Count must be greater than Zero(0)."
                lblerrinhead.ForeColor = Drawing.Color.Red
            End If
        Else
            lblerrinhead.Text = "Invalid Head Count."
            lblerrinhead.ForeColor = Drawing.Color.Red
        End If
    End Sub
End Class
