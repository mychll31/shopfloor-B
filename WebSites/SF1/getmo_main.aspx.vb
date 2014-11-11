﻿Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO

Partial Class getmo_main
    Inherits System.Web.UI.Page

    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Public sapconn As New SqlConnection(ConfigurationManager.ConnectionStrings("sapconnect").ToString)
    Public sapshop As New SqlConnection(ConfigurationManager.ConnectionStrings("sapshop").ToString)

    '#OMOR
    Dim SAPtable As String

    Protected Sub cmdgetMO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdgetMO.Click
        If Request.QueryString("ut") = 3 Then
            txtaoper.Text = "Labor"
        End If
        If txtoper.Text = "" And txtaoper.Text = "" Then
            If Request.QueryString("ut") = 3 Then
                lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'>Resurce Name must not be empty.</font></div></div>"
            Else
                lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'> Operator or Assistant Operator cannot be empty.</font></div></div>"
            End If
            txtoper.BorderColor = Drawing.Color.Red
        Else
            panelerr.CssClass = ""
            txtmo.Focus()
            txtoper.BorderColor = Drawing.Color.Empty
            lblerror.Text = ""
            Panel1.Visible = True
            cmdgetMO.Visible = False
            btnnomo.Visible = False
        End If
    End Sub

    Protected Sub txtmo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmo.TextChanged
        If IsNumeric(txtmo.Text) Then
            lblerror.ForeColor = Drawing.Color.Empty
            lblerror.Text = ""
            txtmo.BorderColor = Drawing.Color.Empty
            '###check if there is a recover mo then, sap details
            checkrecovermo()
            'get sap details

            '###end sap details
            
        Else
            lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'>MO IS NOT DEFINED. <b>" & UCase(txtmo.Text) & "</b> </font></div></div>"
            lblerror.ForeColor = Drawing.Color.Red
            txtmo.BorderColor = Drawing.Color.Red
            txtmo.Text = ""
            txtmo.Focus()
        End If
    End Sub

    Private Sub getSAPtable()
        '######GET TABLE NAME
        conn.Open()
        Dim gettable As New SqlCommand("select path_name from path_settings where id = 2", conn)
        SAPtable = gettable.ExecuteScalar
        conn.Close()
        '######END GET TABLE NAME
    End Sub

    Private Sub getSAPdetails()
        getSAPtable()
        Dim goodsin As String = ""

        '#####GET SAP DETAILS
        Dim sql_getsapDetails As String = "select top 1 U_ItemCode,U_RtgCode,U_Description,U_Warehouse,U_Quantity,U_ActualQty,U_RequiredDate,U_PlannedStartDate,U_PlannedStartTime,U_PlannedEndDate,U_PlannedStartTime,'',0,0,U_Status from [" + SAPtable + "] where DocNum = '" + txtmo.Text + "'"
        Dim readsapdetails As New SqlCommand(sql_getsapDetails, sapconn)
        Dim saprows As SqlDataReader

        sapconn.Open()
        saprows = readsapdetails.ExecuteReader
        If saprows.HasRows Then
            'clearfix
            lblerror.Text = ""

            While saprows.Read
                If IsDBNull(saprows(14)) Then
                Else
                    '###########GET OMORCODE FOR MO STATUS
                    Dim omorstatus As String = ""
                        conn.Open()
                        Dim get_omorcode As New SqlCommand("select name from omor_status where code = '" & saprows(14).ToString & "'", conn)
                        omorstatus = get_omorcode.ExecuteScalar
                        conn.Close()
                    If omorstatus = "" Then
                        omorstatus = saprows(14).ToString
                    End If
                    '###########END GETTING OMORCODE
                    If ((saprows(14)).ToString = "RL" Or (saprows(14)).ToString = "ST") Then
                        If IsDBNull(saprows(0)) Then lblfgcode.Text = "" Else lblfgcode.Text = saprows(0).ToString
                        If IsDBNull(saprows(1)) Then lblrouting.Text = "" Else lblrouting.Text = saprows(1).ToString
                        If IsDBNull(saprows(2)) Then lbldesc.Text = "" Else lbldesc.Text = saprows(2).ToString
                        If IsDBNull(saprows(3)) Then lblwarehouse.Text = "" Else lblwarehouse.Text = saprows(3).ToString
                        If IsDBNull(saprows(4)) Then plnqty.Text = 0 Else plnqty.Text = Math.Round(saprows(4), 2)
                        If IsDBNull(saprows(5)) Then actualqty.Text = 0 Else actualqty.Text = Math.Round(saprows(5), 2)
                        If IsDBNull(saprows(6)) Then lblreqdate.Text = "" Else lblreqdate.Text = Format(saprows(6), "MMM. dd, yyyy")
                        If IsDBNull(saprows(7)) Then lblplnstart.Text = "" Else lblplnstart.Text = Format(saprows(7), "MMM. dd, yyyy") & " " & saprows(8).ToString().Substring(0, saprows(8).ToString().Length - 2) & ":" & (Right(saprows(8).ToString, 2))
                        If IsDBNull(saprows(9)) Then lblplnend.Text = "" Else lblplnend.Text = Format(saprows(9), "MMM. dd, yyyy") & " " & saprows(10).ToString().Substring(0, saprows(10).ToString().Length - 2) & ":" & (Right(saprows(10).ToString(), 2))
                        If IsDBNull(saprows(11)) Then lblperpiece.Text = "" Else lblperpiece.Text = saprows(11).ToString
                        If IsDBNull(saprows(12)) Then lblsubsplanqty.Text = 0 Else lblsubsplanqty.Text = Math.Truncate(saprows(12)).ToString
                        If IsDBNull(saprows(13)) Then lblsubsqtyperpiece.Text = 0 Else lblsubsqtyperpiece.Text = Math.Truncate(saprows(13)).ToString
                        If IsDBNull(saprows(14)) Then lblomorcode.Text = "" Else lblomorcode.Text = saprows(14).ToString
                        lblstatus.Text = omorstatus
                        lbluom.Text = lblperpiece.Text
                        uommajor.Text = lblperpiece.Text
                        '###OUTPUT TABLE
                        paneltable.Visible = True
                        Panel2.Visible = True
                        Panel3.Visible = True
                        cmdcancel.Enabled = True
                        '###END OUTPUT
                        'setting barcode as readonly and cancel as visible
                        txtmo.ReadOnly = True
                        cmdcancel.Visible = True
                    Else
                        lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'> MO cannot be process. <b>STATUS : " & omorstatus.ToString & "</b></font></div></div>"
                        txtmo.Text = ""
                        txtmo.Focus()
                    End If
                End If
            End While
        Else
            lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'>NO MO FOUND. <b>" & txtmo.Text & "</b></font></div></div>"
            txtmo.Text = ""
            txtmo.Focus()

            'clearfix
            Panel3.Dispose()
            Panel2.Dispose()
            paneltable.Dispose()
            paneltable.Visible = False
            Panel3.Visible = False
            Panel2.Visible = False
        End If
        sapconn.Close()

        '#####END SAP DETAILS

        '#########Get quantity from the previous MO
        conn.Open()
        Dim prev_quantity As Integer = 0
        Dim getquantity As New SqlCommand("select top 1 quantity from shopfloors where mo='" & txtmo.Text & "' order by id desc", conn)
        prev_quantity = getquantity.ExecuteScalar
        conn.Close()

        If prev_quantity = 0 Then
            txtg.Text = lblsubsplanqty.Text
            txtgoods.Text = lblsubsplanqty.Text
        Else
            txtg.Text = prev_quantity.ToString
            txtgoods.Text = prev_quantity.ToString
        End If

        If txtg.Text <> "" And lblsubsqtyperpiece.ToString <> "" Then
            'lblgoodsin.Text = (Integer.Parse(txtg.Text) * Integer.Parse(lblsubsqtyperpiece.Text)).ToString
            lblgoodsprpieces.Text = (Double.Parse(txtg.Text) * Double.Parse(lblsubsqtyperpiece.Text)).ToString
            'lbloriginal.Text = (Double.Parse(txtg.Text) * Double.Parse(lblsubsqtyperpiece.Text)).ToString
        End If
        '#########End get quantity

        Dim g1 As Integer = 0
        Dim r1 As Integer = 0
        Dim getgood As New SqlCommand("select quantity,rejects from shopfloors where mo = '" & txtmo.Text & "'", conn)
        Dim rowgood As SqlDataReader

        conn.Open()
        rowgood = getgood.ExecuteReader
        If rowgood.HasRows Then
            While rowgood.Read
                g1 = g1 + rowgood(0)
                r1 = r1 + rowgood(1)
            End While
        End If
        conn.Close()
        lblgoodqty.Text = g1.ToString
        lblbadqty.Text = r1.ToString
        lblbalance.Text = Math.Round((CInt(plnqty.Text) - (g1 + r1)), 2).ToString

    End Sub

    Protected Sub cmdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        lblerror.Text = ""
        Panel3.Dispose()
        Panel2.Dispose()
        paneltable.Dispose()
        paneltable.Visible = False
        Panel3.Visible = False
        Panel2.Visible = False
        cmdcancel.Enabled = False

        'setting barcode as readonly and cancel as invisible
        txtmo.ReadOnly = False
        cmdcancel.Visible = False

        lblhms.Text = "00:00:00"
        echoed.Text = ""
        echosd.Text = ""
        lblstartTime.Text = ""
        lblendTime.Text = ""
        txtg1.Text = ""
        txtr1.Text = ""

        txtmo.Text = ""
        txtmo.Focus()
    End Sub

    Protected Sub Panel2_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel2.Init
        ddltype.Items.Add("SELECT PROCESS")
    End Sub

    Protected Sub cmddirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddirect.Click
        Call checkoperation(1)
        titleremarks.Text = "DIRECT"
        titletimetype.Text = ""
        cmddirect.CssClass = "curr"
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = ""
    End Sub

    Protected Sub cmdindirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdindirect.Click
        Call checkoperation(2)
        titleremarks.Text = "INDIRECT"
        titletimetype.Text = ""
        cmddirect.CssClass = ""
        cmdindirect.CssClass = "curr"
        cmdprodd.CssClass = ""
    End Sub

    Protected Sub cmdprodd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdprodd.Click
        Call checkoperation(3)
        titleremarks.Text = "PROD. DOWNTIME"
        titletimetype.Text = ""
        cmddirect.CssClass = ""
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = "curr"
    End Sub

    Private Sub checkoperation(ByVal oper As Integer)
        '#Set time type in dropdownlist according to operation
        ddltype.Items.Clear()
        ddltype.Items.Add("SELECT PROCESS")
        Dim sql_getopers As String
        sql_getopers = "select name,id from timeTypes where active = '1' and type = '" & oper

        If lblnomo.Text = "" Then
            sql_getopers = sql_getopers & "' and code <> 'NJR'"
        Else
            sql_getopers = sql_getopers & "' and code = 'NJR'"
        End If

        Dim readoper As New SqlCommand(sql_getopers, conn)
        Dim operRows As SqlDataReader

        conn.Open()
        operRows = readoper.ExecuteReader
        While operRows.Read
            Dim newItem As New ListItem(operRows(0).ToString, operRows(1).ToString)
            ddltype.Items.Add(newItem)
        End While
        conn.Close()
    End Sub

    Private Sub cmdstart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdstart.Click

        '#cssclass settings
        cmdstart.CssClass = "curr"
        cmdcanc.CssClass = ""
        cmdstop.CssClass = ""
        cmdcontinue.CssClass = ""
        cmdok.CssClass = ""

        'setting operators readonly
        txtaoper.ReadOnly = True
        txtoper.ReadOnly = True
        txthelper.ReadOnly = True

        '#Validating PROCESS
        lblhms.Text = "00:00:00"

        '#Cleaning End Time
        echoed.Text = ""
        lblendTime.Text = ""

        If ddltype.Text = "SELECT PROCESS" Then
            '##if user doesnt select process yet
            lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'> PROCESS NOT DEFINED.</font></div></div>"
            ddltype.BorderColor = Drawing.Color.Red
        Else
            'enable timer
            Timer1.Enabled = True

            'disabling buttons
            cmdstart.Enabled = False
            cmdcanc.Enabled = True
            cmdstop.Enabled = True

            'enabling changeoperator buttons
            changeop1.Visible = True

            'setting 1 second to balance the time
            lblhms.Text = "00:00:01"
            'setting start time
            echosd.Text = Format(Now, "yyyy-MM-dd")
            'setting end time
            lblstartTime.Text = Format(Now, "HH:mm:ss")

            '##########insert partial data of shopfloor########################################################################################
            '#get machinename
            conn.Open()
            Dim getmachinename As New SqlCommand("select user_name from users where id = '" & Request.QueryString("id") & "'", conn)
            Dim machinename = getmachinename.ExecuteScalar
            conn.Close()
            '#end machinename

            '#get timetype and remarks
            Dim tt_desc As String = ""
            Dim tt_code As String = ""
            Dim gettimetype_remarks As New SqlCommand("select description,code from timeTypes where id = '" & ddltype.SelectedValue & "'", conn)
            Dim rowstimetype_remarks As SqlDataReader

            conn.Open()
            rowstimetype_remarks = gettimetype_remarks.ExecuteReader
            If rowstimetype_remarks.HasRows Then
                While rowstimetype_remarks.Read
                    tt_desc = rowstimetype_remarks(0).ToString
                    tt_code = rowstimetype_remarks(1).ToString
                End While
            End If
            conn.Close()
            '#end get time type and remarks
            If titleremarks.Text = "DIRECT" Then
                If tt_code = "L" And lblsecondsinchangeover.Text <> "" Then
                    'if change over then
                    insertinShopfloors("insert into shopfloors values('" & txtmo.Text & "','','','" & lblrouting.Text & "', '" & Request.QueryString("ot") & "','" & machinename.ToString & "','" & tt_code & "','" & (echosd.Text & " " & lblstartTime.Text) & "','','1','In excess of two hours','','','','" & txtoper.Text & "','" & txtaoper.Text & "','" & txthelper.Text & "','','" & Request.QueryString("id") & "','" & lblfgcode.Text & "','" & txtheadcount.Text & "',0)")
                Else
                    insertinShopfloors("insert into shopfloors values('" & txtmo.Text & "','','','" & lblrouting.Text & "', '" & Request.QueryString("ot") & "','" & machinename.ToString & "','" & tt_code & "','" & (echosd.Text & " " & lblstartTime.Text) & "','','1','" & tt_desc & "','','','','" & txtoper.Text & "','" & txtaoper.Text & "','" & txthelper.Text & "','','" & Request.QueryString("id") & "','" & lblfgcode.Text & "','" & txtheadcount.Text & "',0)")
                End If
            ElseIf titleremarks.Text = "INDIRECT" Or titleremarks.Text = "PROD. DOWNTIME" Then
                '##because the process is indirect we need to get the time type from the previous record of mo
                conn.Open()
                Dim getprev_timetype As New SqlCommand("select  top 1 time_type from shopfloors where mo = '" & txtmo.Text & "' and resource = '" & machinename.ToString & "' and operation='" & Request.QueryString("ot") & "' order by id desc", conn)
                Dim prev_timetype = getprev_timetype.ExecuteScalar
                conn.Close()

                If lblnomo.Text = "" Then
                    '#then insert to shopfloor with the previous MO time type
                    insertinShopfloors("insert into shopfloors values('" & txtmo.Text & "','','','" & lblrouting.Text & "', '" & Request.QueryString("ot") & "','" & machinename.ToString & "','" & prev_timetype.ToString & "','" & (echosd.Text & " " & lblstartTime.Text) & "','','1','" & tt_desc & "','','','','" & txtoper.Text & "','" & txtaoper.Text & "','" & txthelper.Text & "','','" & Request.QueryString("id") & "','" & lblfgcode.Text & "','" & txtheadcount.Text & "',0)")
                Else
                    insertinShopfloors("insert into shopfloors values('" & txtmo.Text & "','','','','" & Request.QueryString("ot") & "','" & machinename & "','','" & (echosd.Text & " " & lblstartTime.Text) & "','','1','" & tt_desc & "','','','" & lbloper_remarks.Text & "','" & txtoper.Text & "','" & txtaoper.Text & "','" & txthelper.Text & "','','" & Request.QueryString("id") & "','','" & txtheadcount.Text & "',0)")
                End If
            Else
                'no statement for the condition yet
            End If
            getlastinsertedID()
            '##########end inserting data#######################################################################################################

            'clearfix
            lblerror.Text = ""
            ddltype.BorderColor = Drawing.Color.Empty

            'hide start and unhide stop
            cmdstart.Visible = False
            cmdstop.Visible = True
        End If
        cmdcancel.Visible = False

        Dim g1 As Integer = 0
        Dim r1 As Integer = 0
        Dim getgood As New SqlCommand("select quantity,rejects from shopfloors where mo = '" & txtmo.Text & "'", conn)
        Dim rowgood As SqlDataReader

        conn.Open()
        rowgood = getgood.ExecuteReader
        If rowgood.HasRows Then
            While rowgood.Read
                g1 = g1 + rowgood(0)
                r1 = r1 + rowgood(1)
            End While
        End If
        conn.Close()
        lblgoodqty.Text = g1.ToString
        lblbadqty.Text = r1.ToString
        lblbalance.Text = Math.Round((CInt(plnqty.Text) - (g1 + r1)), 2).ToString

    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblhms.Text = DateAdd("s", 1, lblhms.Text)
        lblhms.Text = Format(CDate(lblhms.Text), "HH:mm:ss")
    End Sub

    Protected Sub cmdstop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdstop.Click
        '#cssclass settings
        cmdstart.CssClass = ""
        cmdcanc.CssClass = ""
        cmdstop.CssClass = "curr"
        cmdcontinue.CssClass = ""
        cmdok.CssClass = ""

        'enabling buttons
        cmdstart.Enabled = False
        cmdcanc.Enabled = True
        cmdstop.Enabled = False
        cmdcontinue.Enabled = True
        cmdok.Enabled = True

        Timer1.Enabled = False
        echoed.Text = Format(Now, "yyyy-MM-dd")
        lblendTime.Text = Format(Now, "HH:mm:ss")

        cmdstart.Enabled = False
        cmdcontinue.Enabled = True
        cmdok.Enabled = True
        cmdstop.Enabled = False

        lblerror.Text = "<div class='divwarning'><div class='labelerror'><br><font color='red'>Stop is Clicked! Please Confirmed Data by Clicking SUBMIT. Thank you</font></div></div>"

        'hide stop and unhide submit and cancel
        cmdstop.Visible = False
        cmdcontinue.Visible = True
        cmdok.Visible = True
    End Sub

    Private Sub insertinShopfloors(ByVal sql As String)
        Dim insertSF As New SqlCommand(sql, conn)
        conn.Open()
        Try
            insertSF.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub
    '#########################TEMPORARY EXECUTE NON QUERY
    Private Sub insertinShopfloors1(ByVal sql As String)
        Dim insertSF As New SqlCommand(sql, sapshop)
        sapshop.Open()
        Try
            insertSF.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        sapshop.Close()
    End Sub
    '#########################TEMPORARY EXECUTE NON QUERY

    Protected Sub cmdcanc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcanc.Click
        cmdstart.CssClass = ""
        cmdcanc.CssClass = "curr"
        cmdstop.CssClass = ""
        cmdcontinue.CssClass = ""
        cmdok.CssClass = ""

        echoed.Text = ""
        lblendTime.Text = ""
    End Sub

    Protected Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        lblerror.Text = ""

        '######################check if there are some changes made in the Good value
        conn.Open()
        Dim agoods As Integer
        Dim getgoods As New SqlCommand("select quantity,mo,* from shopfloors where mo = '" & txtmo.Text & "' and id <> '" & lbloutmo.Text & "' order by end_date_time desc", conn)
        agoods = getgoods.ExecuteScalar
        lblnewgood.Text = agoods.ToString
        conn.Close()
        '############################################################################


        If lblnewgood.Text = txtg.Text Then
            '#################UPDATE Current MO
            Dim duration = (CDate(echoed.Text & " " & lblendTime.Text) - CDate(echosd.Text & " " & lblstartTime.Text))
            insertinShopfloors("update shopfloors set posting_date = '" & echoed.Text & "', doc_date = '" & echoed.Text & "', end_date_time = '" & (echoed.Text & " " & lblendTime.Text) & "', quantity = '" & txtg1.Text & "', rejects = '" & txtr1.Text & "', oper_remarks = '" & lbloper_remarks.Text & "', duration = '" & duration.ToString & "', headcount = '" & txtheadcount.Text & "' where id = '" & lbloutmo.Text & "'")
        Else
            'stored original Good
            txtg.Text = lblnewgood.Text
            'lbloriginal.Text = (Double.Parse(txtg.Text) * Double.Parse(lblsubsqtyperpiece.Text)).ToString

            'reject in major uom
            txtgoods.Text = (Double.Parse(txtg.Text) - Double.Parse(txtrejects.Text)).ToString
            If txtrejects.Text <> 0 Then
                lblgoodsprpieces.Text = (Double.Parse(txtrejects.Text) * Double.Parse(lblsubsqtyperpiece.Text)).ToString
                'lblrejectsinpieces.Text = (Double.Parse(lbloriginal.Text) - Double.Parse(lblgoodsprpieces.Text))
            End If


            Dim duration = (CDate(echoed.Text & " " & lblendTime.Text) - CDate(echosd.Text & " " & lblstartTime.Text))
            insertinShopfloors("update shopfloors set posting_date = '" & echoed.Text & "', doc_date = '" & echoed.Text & "', end_date_time = '" & (echoed.Text & " " & lblendTime.Text) & "', quantity = '" & txtg1.Text & "', rejects = '" & txtr1.Text & "', oper_remarks = '" & lbloper_remarks.Text & "', duration = '" & duration.ToString & "', headcount = '" & txtheadcount.Text & "' where id = '" & lbloutmo.Text & "'")
            End If

        If lblnomo.Text = "" Then
            'Create Excel
            createExcel()
            '#################
        End If
            '#cssclass of buttons
            cmdstart.CssClass = ""
            cmdcanc.CssClass = ""
            cmdstop.CssClass = ""
            cmdcontinue.CssClass = ""
            cmdok.CssClass = "curr"

            'clearfix
            cmdstart.Enabled = True
            cmdcanc.Enabled = False
            cmdstop.Enabled = False
            cmdcontinue.Enabled = False
            cmdok.Enabled = False

            'hide cancel and submit. unhide start
            cmdcontinue.Visible = False
            cmdok.Visible = False
        cmdstart.Visible = True

        cmdcancel.Visible = True

        Dim g1 As Integer = 0
        Dim r1 As Integer = 0
        Dim getgood As New SqlCommand("select quantity,rejects from shopfloors where mo = '" & txtmo.Text & "'", conn)
        Dim rowgood As SqlDataReader

        conn.Open()
        rowgood = getgood.ExecuteReader
        If rowgood.HasRows Then
            While rowgood.Read
                g1 = g1 + rowgood(0)
                r1 = r1 + rowgood(1)
            End While
        End If
        conn.Close()
        lblgoodqty.Text = g1.ToString
        lblbadqty.Text = r1.ToString
        lblbalance.Text = Math.Round((CInt(plnqty.Text) - (g1 + r1)), 2).ToString

    End Sub

    Protected Sub cmdcontinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcontinue.Click
        'Timer Continued
        lblerror.Text = ""
        Timer1.Enabled = True

        '#cssclass of buttons
        cmdstart.CssClass = ""
        cmdcanc.CssClass = ""
        cmdstop.CssClass = ""
        cmdcontinue.CssClass = "curr"
        cmdok.CssClass = ""

        'enabling buttons
        cmdstart.Enabled = False
        cmdcanc.Enabled = True
        cmdstop.Enabled = True
        cmdcontinue.Enabled = False
        cmdok.Enabled = False

        'hide continue and submit. unhide stop
        cmdcontinue.Visible = False
        cmdstop.Visible = True
        cmdok.Visible = False

        echoed.Text = ""
        lblendTime.Text = ""
    End Sub

    Protected Sub txtrejects_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrejects.TextChanged
        Dim totalrejects As Double
        totalrejects = Double.Parse(txtg.Text) - Double.Parse(txtrejects.Text)
        txtgoods.Text = totalrejects

        lblgoodsprpieces.Text = (Double.Parse(totalrejects) * Double.Parse(lblsubsqtyperpiece.Text)).ToString
        'lblrejectsinpieces.Text = (Double.Parse(lbloriginal.Text) - Double.Parse(lblgoodsprpieces.Text))
    End Sub

    Protected Sub btnnomo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnomo.Click
        If txtoper.Text = "" And txtaoper.Text = "" Then
            lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'> Operator or Assistant Operator cannot be empty.</font></div></div>"
            txtoper.BorderColor = Drawing.Color.Red
        Else
            'clearfix
            lblerror.Text = ""
            txtoper.BorderColor = Drawing.Color.Empty
            cmdgetMO.Visible = False
            btnnomo.Visible = False

            'hide direct and prod downtime
            cmddirect.Visible = False
            cmdprodd.Visible = False

            lblnomo.Text = "YOU ARE PROCESSING A NON-RELATED JOB DOWNTIME"
            Panel3.Visible = True

            lblgood.Visible = False
            txtgoods.Visible = False
            lblrejects1.Visible = False
            txtrejects.Visible = False
        End If
    End Sub

    Protected Sub ddltype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltype.SelectedIndexChanged
        If ddltype.SelectedItem.ToString <> "SELECT PROCESS" Then
            titletimetype.Text = " - " & ddltype.SelectedItem.ToString
        Else
            titletimetype.Text = ""
        End If
        If ddltype.SelectedItem.ToString = "Change Over" Then
            Call totalChangeOver()
        Else
            lblsecondsinchangeover.Text = ""
        End If
    End Sub

    Private Sub getlastinsertedID()
        '#get last inserted id
        conn.Open()
        Dim getlastinserted_id As New SqlCommand("select top 1 id from shopfloors order by id desc", conn)
        lbloutmo.Text = (getlastinserted_id.ExecuteScalar).ToString
        conn.Close()
    End Sub

    Private Sub totalChangeOver()
        '#get machinename
        conn.Open()
        Dim getmachinename As New SqlCommand("select user_name from users where id = '" & Request.QueryString("id") & "'", conn)
        Dim machinename = getmachinename.ExecuteScalar
        conn.Close()
        '#end machinename

        '####################GET TOTAL CHANGE OVER##################################################
        Dim gettotalL As New SqlCommand("select duration from shopfloors where time_type = 'L' and mo = '" & txtmo.Text & "' and resource='" & machinename & "' and operation='" & Request.QueryString("ot") & "' and remarks='Productive' and end_date_time <> ''", conn)
        Dim totalL As SqlDataReader

        Dim totalSeconds As Integer = 0
        Dim dura As TimeSpan
        conn.Open()
        totalL = gettotalL.ExecuteReader
        If totalL.HasRows Then
            While totalL.Read
                dura = TimeSpan.Parse(totalL(0).ToString)
                totalSeconds = totalSeconds + dura.TotalSeconds
            End While
        End If
        conn.Close()

        If totalSeconds >= 7200 Then
            lblsecondsinchangeover.Text = "YOU ALREADY CONSUMED THE TWO HOURS FOR CHANGE OVER. DOWNTIME IS APPLIED."
        Else
            '##display the remaing Seconds
            Dim tSeconds1 = 7200 - totalSeconds
            Dim tSeconds2 = DateAdd("s", tSeconds1, "00:00:00")
            lblremainingseconds.Text = Format(CDate(tSeconds2.ToString), "HH:mm:ss")
            lblsecondsinchangeover.Text = ""
        End If

        '####################END TOTAL CHANGE OVER##################################################
    End Sub
    
    Protected Sub changeop1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles changeop1.Click
        If changeop1.Text <> "Save" Then
            oper2.Text = txtoper.Text
            aoper2.Text = txtaoper.Text
            helper2.Text = txthelper.Text

            'setting operators readonly
            txtaoper.ReadOnly = False
            txtoper.ReadOnly = False
            txthelper.ReadOnly = False

            txtaoper.Text = ""
            txtoper.Text = ""
            txthelper.Text = ""

            changeop1.Text = "Save"
            cancelchangeop.Visible = True
        Else
            '##############Transition of Operator###############################################################################################
            If txtoper.Text = "" And txtaoper.Text = "" Then
                lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'>Operator or Assistant Operator must not be empty.</font></div></div>"
            Else
                lblerror.Text = ""
                '##update the current mo
                Dim changeover_end = Format(Now, "yyyy-MM-dd")
                Dim changeover_timeend = Format(Now, "HH:mm:ss")
                Dim duration = CDate(changeover_end.ToString & " " & changeover_timeend) - CDate(echosd.Text & " " & lblstartTime.Text) 'duration
                insertinShopfloors("update shopfloors set posting_date = '" & changeover_end & "', doc_date = '" & changeover_end & "', end_date_time = '" & changeover_end & " " & changeover_timeend & "', quantity = '" & txtg1.Text & "', rejects = '" & txtr1.Text & "', oper_remarks = '" & lbloper_remarks.Text & "', duration = '" & duration.ToString & "', headcount = '" & txtheadcount.Text & "', operator='" & oper2.Text & "', ass_operator='" & aoper2.Text & "', helper='" & helper2.Text & "'  where id = '" & lbloutmo.Text & "'")
                'create excel
                echoed.Text = changeover_end.ToString
                lblendTime.Text = changeover_timeend.ToString
                createExcel()

                echoed.Text = ""
                lblendTime.Text = ""
                '##insert another mo with excess of twor hours tag
                '#get machinename
                conn.Open()
                Dim getmachinename As New SqlCommand("select user_name from users where id = '" & Request.QueryString("id") & "'", conn)
                Dim machinename = getmachinename.ExecuteScalar
                conn.Close()
                '#end machinename

                '#get timetype and remarks
                Dim tt_desc As String = ""
                Dim tt_code As String = ""
                Dim gettimetype_remarks As New SqlCommand("select description,code from timeTypes where id = '" & ddltype.SelectedValue & "'", conn)
                Dim rowstimetype_remarks As SqlDataReader

                conn.Open()
                rowstimetype_remarks = gettimetype_remarks.ExecuteReader
                If rowstimetype_remarks.HasRows Then
                    While rowstimetype_remarks.Read
                        tt_desc = rowstimetype_remarks(0).ToString
                        tt_code = rowstimetype_remarks(1).ToString
                    End While
                End If
                conn.Close()
                '#end get time type and remarks
                insertinShopfloors("insert into shopfloors values('" & txtmo.Text & "','','','" & lblrouting.Text & "', '" & Request.QueryString("ot") & "','" & machinename.ToString & "','" & tt_code & "','" & (changeover_end & " " & changeover_timeend) & "','','1','" & tt_desc & "','" & txtg1.Text & "','" & txtr1.Text & "','','" & txtoper.Text & "','" & txtaoper.Text & "','" & txthelper.Text & "','','" & Request.QueryString("id") & "','" & lblfgcode.Text & "','" & txtheadcount.Text & "',0)")
                getlastinsertedID()
            End If

            '###################################################################################################################################
            'clearfix.
            'setting operators readonly
            txtaoper.ReadOnly = True
            txtoper.ReadOnly = True
            txthelper.ReadOnly = True

            changeop1.Text = "Change Operator"
            cancelchangeop.Visible = False
        End If
    End Sub

    Protected Sub cancelchangeop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelchangeop.Click
        txtaoper.Text = aoper2.Text
        txtoper.Text = oper2.Text
        txthelper.Text = helper2.Text

        'setting operators readonly
        txtaoper.ReadOnly = True
        txtoper.ReadOnly = True
        txthelper.ReadOnly = True

        aoper2.Text = ""
        oper2.Text = ""
        helper2.Text = ""

        cancelchangeop.Visible = False
        changeop1.Text = "Change Operator"
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.QueryString("ut") = 3 Then 'user type 3 is for labor
            'hiding operators
            lbloperatorname.Text = "Resource"
            lblassopername.Visible = False
            lblhelpername.Visible = False
            txtaoper.Visible = False
            txthelper.Visible = False
            txtoper.Text = "Labor"
            txtoper.ReadOnly = True
            aoper2.Visible = False
            changeop1.Visible = False

            'unhide headcount
            lblheadcount.Visible = True
            txtheadcount.Visible = True
        End If
        Call checkoperation(1)
        titleremarks.Text = "DIRECT"
        titletimetype.Text = ""
        cmddirect.CssClass = "curr"
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = ""
    End Sub

    Protected Sub txtheadcount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtheadcount.TextChanged
        If IsNumeric(txtheadcount.Text) Then
            Dim headcount As Integer = Double.Parse(txtheadcount.Text)
            If headcount <= 0 Then
                lblerrinhead.Text = "<br>Head Count must be greater then 0."
            Else
                lblerrinhead.Text = ""
            End If
        Else
            lblerrinhead.Text = "<br>Invalid Head Count"
        End If
    End Sub

    Private Sub createExcel()
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

        '#get machinename
        conn.Open()
        Dim getmachinename As New SqlCommand("select user_name from users where id = '" & Request.QueryString("id") & "'", conn)
        Dim machinename = getmachinename.ExecuteScalar
        conn.Close()
        '#end machinename

        '#get timetype and remarks
        Dim tt_desc As String = ""
        Dim tt_code As String = ""
        Dim gettimetype_remarks As New SqlCommand("select description,code from timeTypes where id = '" & ddltype.SelectedValue & "'", conn)
        Dim rowstimetype_remarks As SqlDataReader
        conn.Open()
        rowstimetype_remarks = gettimetype_remarks.ExecuteReader
        If rowstimetype_remarks.HasRows Then
            While rowstimetype_remarks.Read
                tt_desc = rowstimetype_remarks(0).ToString
                tt_code = rowstimetype_remarks(1).ToString
            End While
        End If
        conn.Close()
        '#end get time type and remarks

        'Dim body() As String = {"", txtmo.Text, Format(CDate(echoed.Text), "yyyyMMdd").ToString, Format(CDate(echoed.Text), "yyyyMMdd").ToString, lblrouting.Text, UCase(Request.QueryString("ot")), machinename, UCase(tt_code), Format(CDate(echosd.Text), "yyyyMMdd").ToString, Format(CDate(lblstartTime.Text), "HH:mm").ToString, Format(CDate(echoed.Text), "yyyyMMdd").ToString, Format(CDate(lblendTime.Text), "HH:mm").ToString, "1", tt_desc, txtgoods.Text, txtrejects.Text}
        Dim body() As String = {"", txtmo.Text, Format(CDate(echoed.Text), "yyyyMMdd").ToString, Format(CDate(echoed.Text), "yyyyMMdd").ToString, lblrouting.Text, UCase(Request.QueryString("ot")), machinename, UCase(tt_code), Format(CDate(echosd.Text), "yyyyMMdd").ToString, Format(CDate(lblstartTime.Text), "HH:mm").ToString, Format(CDate(echoed.Text), "yyyyMMdd").ToString, Format(CDate(lblendTime.Text), "HH:mm").ToString, "1", tt_desc, 0, 0}

        For body1 As Integer = 1 To 15
            shXL.Cells(2, body1).Value = body(body1)
        Next
        appXL.Visible = True
        appXL.UserControl = True
        ' Release object references.

        '#####GET PATH SETTINGS
        conn.Open()
        Dim gettable As New SqlCommand("select path_name from path_settings where id = 1", conn)
        Dim strSetting = gettable.ExecuteScalar
        conn.Close()
        '#####END GETIING PATH SETTINGS

        Dim myXlsFileName As String = ""
        Dim howmayexcel As Integer = Integer.Parse(txtheadcount.Text)
        For value As Integer = 1 To howmayexcel
            myXlsFileName = "MO" & txtmo.Text & Format(Now, "yyyyMMdd") & Format(Now, "hhmmss") & "_" & value.ToString
            wbXl.SaveAs(Filename:=strSetting & myXlsFileName & ".xls")
        Next

        raXL = Nothing
        shXL = Nothing
        wbXl = Nothing
        appXL.Quit()
        appXL = Nothing
        Exit Sub
Err_Handler:
        MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)

    End Sub

    Private Sub checkrecovermo()
        '#get machinename
        conn.Open()
        Dim getmachinename As New SqlCommand("select user_name from users where id = '" & Request.QueryString("id") & "'", conn)
        Dim machinename = getmachinename.ExecuteScalar
        conn.Close()
        '#end machinename

        '##CHECK RECOVER
        Dim ifrecover As Integer = 0
        '' ''conn.Open()
        '' ''Dim getrecover As New SqlCommand("select top 1 id from shopfloors where end_date_time='' and mo='" & txtmo.Text & "' and operation='" & Request.QueryString("ot") & "' and resource='" & machinename & "' order by id desc", conn)
        '' ''ifrecover = getrecover.ExecuteScalar
        '' ''conn.Close()

        If ifrecover > 0 Then
            lblerror.Text = "<div class='divwarning'><div class='labelerror'><br><font color='red'>System has recovery file for this MO from an unexpected shutdown. Contact Administrator/Authorize Person to Recover MO. Use <b>REFERENCE NO.  " & ifrecover & " </b></font></div></div>"
        Else
            getSAPdetails()
        End If
        '##END RECOVER

    End Sub

    Protected Sub lblrejectsinpieces_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblrejectsinpieces.TextChanged
        'lblgoodsprpieces.Text = Double.Parse(lbloriginal.Text) - Double.Parse(lblrejectsinpieces.Text)
        txtrejects.Text = Double.Parse(lblgoodsprpieces.Text) / Double.Parse(txtgoods.Text)
        txtgoods.Text = Double.Parse(txtg.Text) - Double.Parse(txtrejects.Text)
    End Sub

End Class
