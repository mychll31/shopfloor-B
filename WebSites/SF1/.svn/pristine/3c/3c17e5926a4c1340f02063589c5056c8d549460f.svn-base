Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Partial Class admingetmo
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Public sapconn As New SqlConnection(ConfigurationManager.ConnectionStrings("sapconnect").ToString)
    Dim sqlcheckoperation, timetype2, recdate As String
    Dim mo, postingdate, docdate, sbu, operation, resource, timetype, startdatetime, enddatetime, noofres, remarks, quantity, rejects, operremarks, oper, assoperator, helper, userid, fg, headcount, addsub As String
    Dim goodsqty, Sap_plan_Goods, sap_convperpiece, sap_convuom As Long
    Dim Sap_plannedstime, Sap_plannedetime, Sap_itemcode, Sap_itemname, Sap_routing, Sap_plannedqty, Sap_actualqty, Sap_requireddate, Sap_plannedsdate, Sap_plannededate, Sap_schedulingmethod, Sap_batchsize, Sap_warehouse, Sap_factor, Sap_revision, Sap_uom, Sap_status, Sap_rej_uom As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtyy.Text = Format(Now, "yyyy")
        ddlmm.SelectedValue = Format(Now, "MM")
        txtdd.Text = Format(Now, "dd")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ddlrecoper.Items.Add("Select Operation")
        ddlrecresource.Items.Add("Select Resource Name")
        Dim sql_resource As String = "select t1.user_id,t2.user_name from profiles as t1 inner join users as t2 on t1.user_id = t2.id where t1.active=1 and t1.user_type <> '1'"
        Dim readresource As New SqlCommand(sql_resource, conn)
        Dim rowresource As SqlDataReader

        conn.Open()
        rowresource = readresource.ExecuteReader
        While rowresource.Read
            Dim newItem As New ListItem(rowresource(1).ToString, rowresource(0).ToString)
            ddlrecresource.Items.Add(newItem)
        End While
        conn.Close()

        Dim sql_opertypes As String = "select code,description from oper_types where active = 1"
        Dim readopertypes As New SqlCommand(sql_opertypes, conn)
        Dim rowopertypes As SqlDataReader

        conn.Open()
        rowopertypes = readopertypes.ExecuteReader
        While rowopertypes.Read
            Dim newItem As New ListItem(rowopertypes(1).ToString, rowopertypes(0).ToString)
            ddlrecoper.Items.Add(newItem)
        End While
        conn.Close()
    End Sub

    Protected Sub cmdgetmo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdgetmo.Click
        Dim err As String
        If ddlrecoper.Text = "Select Operation" Then
            ddlrecoper.BorderColor = Drawing.Color.Red
            err = err & "Operation Name must not be empty."
        Else
            ddlrecoper.BorderColor = Drawing.Color.Empty
        End If

        If ddlrecresource.Text = "Select Resource Name" Then
            ddlrecresource.BorderColor = Drawing.Color.Red
            err = err & "<br>Resource Name must not be empty."
        Else
            ddlrecresource.BorderColor = Drawing.Color.Empty
        End If

        If err <> "" Then
            lblerr.Text = "<div class='diverror'><div class='labelerror'>" + err + "</div></div>"
        Else
            lblerr.Text = ""
            cmdgetmo.Enabled = False
            ddlrecoper.Enabled = False
            ddlrecresource.Enabled = False
            lblmono.Visible = True
            txtrecmo.Visible = True
        End If
    End Sub

    Protected Sub cmdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Response.Redirect("admingetmo.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=getmo")
    End Sub

    Protected Sub txtrecmo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrecmo.TextChanged
        Dim sql_recmo As String = "select top 1 * from shopfloors where end_date_time='' and mo='" & txtrecmo.Text & "' and operation='" & ddlrecoper.SelectedValue & "' and resource='" & ddlrecresource.SelectedItem.ToString & "' order by id desc"
        getthismo(sql_recmo)
    End Sub

    Private Sub checkOperation(ByVal oper As Integer)
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

    Protected Sub cmddirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddirect.Click
        Call checkOperation(1)
        cmddirect.CssClass = "curr"
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = ""
    End Sub

    Protected Sub cmdindirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdindirect.Click
        Call checkOperation(2)
        cmddirect.CssClass = ""
        cmdindirect.CssClass = "curr"
        cmdprodd.CssClass = ""
    End Sub

    Protected Sub cmdprodd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdprodd.Click
        Call checkOperation(3)
        cmddirect.CssClass = ""
        cmdindirect.CssClass = ""
        cmdprodd.CssClass = "curr"
    End Sub

    Protected Sub btnupdatetime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdatetime.Click
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
            '##Get the value of time type depend on the operation
            If lblop.Text = "1" Then
                timetype2 = lblrem.Text
            Else
                timetype2 = lblrem2.Text
            End If

            '##Get date for enddate
            recdate = yy.ToString & "-" & ddlmm.SelectedValue & "-" & dd.ToString & " " & shour.ToString & ":" & smin.ToString & ":" & ssec.ToString

            '##update recover MO
            Dim sql_updaterecMo As String
            sql_updaterecMo = "update shopfloors set operator='" & txtoper.Text & "', ass_operator='" & txtaoper.Text & "', helper='" & txthelper.Text & "', time_type='" & lblrem.Text & "', end_date_time='" & recdate & "', oper_remarks='" & lbloper_remarks.Text & "', quantity='" & txtgoods.Text & "', rejects='" & txtrejects.Text & "', posting_date='" & (yy.ToString & "-" & ddlmm.SelectedValue & "-" & dd.ToString) & "', doc_date='" & (yy.ToString & "-" & ddlmm.SelectedValue & "-" & dd.ToString) & "' where id = '" & lbloutmo.Text & "'"

            Dim insertSF As New SqlCommand(sql_updaterecMo, conn)
            conn.Open()
            Try
                insertSF.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
            conn.Close()
            Call createExcel()
        End If
    End Sub

    Protected Sub btnaddsubs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddsubs.Click

    End Sub

    Protected Sub ddltype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltype.SelectedIndexChanged

        If ddltype.SelectedItem.ToString <> "SELECT PROCESS" Then
            conn.Open()
            Dim gettable As New SqlCommand("select code from timeTypes where id = '" & ddltype.SelectedValue & "'", conn)
            Dim selectedrem = (gettable.ExecuteScalar).ToString
            conn.Close()
            lblrem.Text = selectedrem
        Else
            lblrem.Text = ""
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
        '####GET REMARKS
        conn.Open()
        Dim sql_getddlremarks As String = "select description from timeTypes where code = '" & lblrem.Text & "'"
        Dim getddlremarks As New SqlCommand(sql_getddlremarks, conn)
        Dim ddlremarks = getddlremarks.ExecuteScalar
        conn.Close()

        Dim body() As String = {"", txtmo.Text, Format(CDate(recdate), "yyyyMMdd"), Format(CDate(recdate), "yyyyMMdd"), lblsbu.Text, lbloperation.Text, lblresource.Text, UCase(lblrem.Text), Format(CDate(echosd.Text), "yyyyMMdd"), Format(CDate(lblstartTime.Text), "HH:mm"), Format(CDate(recdate), "yyyyMMdd"), Format(CDate(recdate), "HH:mm"), "1", ddlremarks, txtgoods.Text, txtrejects.Text}

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

        Dim myXlsFileName As String = "MO" & txtmo.Text & Format(Now, "yyyyMMdd") & Format(Now, "hhmmss")
        wbXl.SaveAs(Filename:=strSetting & myXlsFileName & ".xls")

        raXL = Nothing
        shXL = Nothing
        wbXl = Nothing
        appXL.Quit()
        appXL = Nothing
        Exit Sub
Err_Handler:
        MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)
    End Sub

    Private Sub getthismo(ByVal sql_recmo As String)
        Dim readrecmo As New SqlCommand(sql_recmo, conn)
        Dim rowrecmo As SqlDataReader
        conn.Open()

        rowrecmo = readrecmo.ExecuteReader
        If rowrecmo.HasRows Then
            Panel4.Visible = True
            Panel5.Visible = False
            While rowrecmo.Read
                lbloutmo.Text = rowrecmo(0).ToString
                mo = rowrecmo(1).ToString
                postingdate = rowrecmo(2).ToString
                docdate = rowrecmo(3).ToString
                sbu = rowrecmo(4).ToString
                operation = rowrecmo(5).ToString
                resource = rowrecmo(6).ToString
                timetype = rowrecmo(7).ToString
                startdatetime = rowrecmo(8).ToString
                enddatetime = rowrecmo(9).ToString
                noofres = rowrecmo(10).ToString
                remarks = rowrecmo(11).ToString
                quantity = rowrecmo(12).ToString
                rejects = rowrecmo(13).ToString
                operremarks = rowrecmo(14).ToString
                oper = rowrecmo(15).ToString
                assoperator = rowrecmo(16).ToString
                helper = rowrecmo(17).ToString
                userid = rowrecmo(19).ToString
                fg = rowrecmo(20).ToString
                headcount = rowrecmo(21).ToString
                addsub = rowrecmo(22).ToString
            End While

            txtmo.Text = mo
            txtoper.Text = oper
            txtaoper.Text = assoperator
            txthelper.Text = helper
            txtrejects.Text = rejects
            lbloper_remarks.Text = operremarks
            echosd.Text = Format(CDate(startdatetime), "MM/dd/yyyy")
            lblstartTime.Text = Format(CDate(startdatetime), "hh:mm:ss")
            lblrem.Text = timetype
            lblsbu.Text = sbu
            lbloperation.Text = operation
            lblresource.Text = resource

        Else
            lblerr.Text = "<div class='diverror'><div class='labelerror'><br>MO TO RECOVER IS NOT EXIST.</div></div>"
        End If
        conn.Close()

        '########GET TOTAL OF GOOD FROM THE PREVIOUS MO
        Dim sql_getgood As String = "select  top 1 quantity from shopfloors where mo='" & txtmo.Text & "' order by id desc"
        Dim getgood As New SqlCommand(sql_getgood, conn)
        Dim rowgetgood As SqlDataReader
        conn.Open()

        rowgetgood = getgood.ExecuteReader
        If rowgetgood.HasRows Then
            While rowgetgood.Read
                txtgoods.Text = rowgetgood(0).ToString
            End While
        End If
        conn.Close()
        '########SAP Details

        Dim goodsin As String = ""
        If txtgoods.Text <> "" And sap_convperpiece.ToString <> "" Then
            goodsin = (Integer.Parse(txtgoods.Text) * Integer.Parse(sap_convperpiece.ToString))
        End If

        conn.Open()
        Dim table As String
        Dim sql_gettableName As String = "select path_name from path_settings where id = 2"
        Dim gettable As New SqlCommand(sql_gettableName, conn)
        gettable = New SqlCommand(sql_gettableName, conn)
        table = gettable.ExecuteScalar
        conn.Close()

        '###################################RECOVERING SAP DETAILS FROM MO
        If txtmo.Text = 0 Then
            lblnomo.Text = "YOU ARE RECOVERING A NON JOB RELATED MO"
            cmddirect.Visible = False
            cmdprodd.Visible = False
            ddltype.Items.Clear()
            ddltype.Items.Add("SELECT PROCESS")
            Else
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
                End If

           
        Dim omorcode As String
        conn.Open()
        Dim sql_getomorcode As String = "select name from omor_status where code = '" & Sap_status & "'"
        Dim get_omorcode As New SqlCommand(sql_getomorcode, conn)
        get_omorcode = New SqlCommand(sql_getomorcode, conn)
        omorcode = get_omorcode.ExecuteScalar
        conn.Close()

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

        '########TimeType
        If timetype <> "" Then
            conn.Open()
            Dim sql_gettimetype As String = "select name from timeTypes where code = '" & timetype & "'"
            Dim gettimetype As New SqlCommand(sql_gettimetype, conn)
            gettimetype = New SqlCommand(sql_gettimetype, conn)
            Dim timetype1 = gettimetype.ExecuteScalar
            conn.Close()

            If timetype1 <> "" Then
                ddltype.Items.Add(timetype1)
            End If
            End If
        End If

        '###################################RECOVERING SAP DETAILS FROM MO
    End Sub

    Protected Sub cmdgetref_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdgetref.Click
        Dim sql_recmo As String = "select top 1 * from shopfloors where id='" & txtrefno.Text & "'"
        getthismo(sql_recmo)
    End Sub
End Class
