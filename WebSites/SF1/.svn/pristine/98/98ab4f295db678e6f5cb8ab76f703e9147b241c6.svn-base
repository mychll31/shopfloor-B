Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Partial Class manualmo
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Public sapconn As New SqlConnection(ConfigurationManager.ConnectionStrings("sapconnect").ToString)
    Public sapshop As New SqlConnection(ConfigurationManager.ConnectionStrings("sapshop").ToString)

    '#OMOR
    Dim SAPtable As String

    Private Sub checkrecovermo()
        getSAPtable()
        '#get machinename
        conn.Open()
        Dim getmachinename As New SqlCommand("select user_name from users where id = '" & Request.QueryString("id") & "'", conn)
        Dim machinename = getmachinename.ExecuteScalar
        conn.Close()
        '#end machinename

        '##CHECK RECOVER
        Dim ifrecover As Integer = 0
        conn.Open()
        Dim getrecover As New SqlCommand("select top 1 id from shopfloors where end_date_time='' and mo='" & txtmo.Text & "' and operation='" & Request.QueryString("ot") & "' and resource='" & machinename & "' order by id desc", conn)
        ifrecover = getrecover.ExecuteScalar
        conn.Close()

        If ifrecover > 0 Then
            lblerror.Text = "<div class='divwarning'><div class='labelerror'><br><font color='red'>System has recovery file for this MO from an unexpected shutdown. Contact Administrator/Authorize Person to Recover MO. Use <b>REFERENCE NO.  " & ifrecover & " </b></font></div></div>"
        Else
            getSAPdetails()
        End If
        '##END RECOVER

    End Sub

    Private Sub getSAPdetails()
        Dim goodsin As String = ""
        '#####GET SAP DETAILS
        Dim sql_getsapDetails As String = "select top 1 U_ItemCode,U_RtgCode,U_Description,U_Warehouse,U_Quantity,U_ActualQty,U_RequiredDate,U_PlannedStartDate,U_PlannedStartTime,U_PlannedEndDate,U_PlannedStartTime,U_SubsUom,U_SubsPlanQty,U_SubsQtyPerPcs,U_Status from [" + SAPtable + "] where DocNum = '" + txtmo.Text + "'"
        Dim readsapdetails As New SqlCommand(sql_getsapDetails, sapconn)
        Dim saprows As SqlDataReader

        sapconn.Open()
        saprows = readsapdetails.ExecuteReader
        If saprows.HasRows Then
            'clearfix
            lblerror.Text = ""

            While saprows.Read
                If IsDBNull(saprows(0)) Then lblfgcode.Text = "" Else lblfgcode.Text = saprows(0).ToString
                If IsDBNull(saprows(1)) Then lblrouting.Text = "" Else lblrouting.Text = saprows(1).ToString
                If IsDBNull(saprows(2)) Then lbldesc.Text = "" Else lbldesc.Text = saprows(2).ToString
                If IsDBNull(saprows(3)) Then lblwarehouse.Text = "" Else lblwarehouse.Text = saprows(3).ToString
                If IsDBNull(saprows(4)) Then plnqty.Text = 0 Else plnqty.Text = saprows(4).ToString
                If IsDBNull(saprows(5)) Then actualqty.Text = 0 Else actualqty.Text = saprows(5).ToString
                If IsDBNull(saprows(6)) Then lblreqdate.Text = "" Else lblreqdate.Text = saprows(6).ToString
                If IsDBNull(saprows(7)) Then lblplnstart.Text = "" Else lblplnstart.Text = FormatDateTime(CDate(saprows(7).ToString), 1) & " " & saprows(8).ToString().Substring(0, saprows(8).ToString().Length - 2) & ":" & (Right(saprows(8).ToString, 2))
                If IsDBNull(saprows(9)) Then lblplnend.Text = "" Else lblplnend.Text = FormatDateTime(CDate(saprows(9).ToString), 1) & " " & saprows(10).ToString().Substring(0, saprows(10).ToString().Length - 2) & ":" & (Right(saprows(10).ToString(), 2))
                If IsDBNull(saprows(11)) Then lblperpiece.Text = "" Else lblperpiece.Text = saprows(11).ToString
                If IsDBNull(saprows(12)) Then lblsubsplanqty.Text = 0 Else lblsubsplanqty.Text = Math.Truncate(saprows(12)).ToString
                If IsDBNull(saprows(13)) Then lblsubsqtyperpiece.Text = 0 Else lblsubsqtyperpiece.Text = Math.Truncate(saprows(13)).ToString
                If IsDBNull(saprows(14)) Then lblomorcode.Text = "" Else lblomorcode.Text = saprows(14).ToString
            End While
            lbluom.Text = lblperpiece.Text
            '###OUTPUT TABLE
            Panel1.Visible = True
            cmdcancel.Visible = True
            '###END OUTPUT
        Else
            lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'> NO MO FOUND. <b>" & txtmo.Text & "</b></font></div></div>"
            lblerror.ForeColor = Drawing.Color.Red
            txtmo.BorderColor = Drawing.Color.Red
            txtmo.Text = ""
            txtmo.Focus()
            'clearfix
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
            lblgoodsin.Text = (Integer.Parse(txtg.Text) * Integer.Parse(lblsubsqtyperpiece.Text)).ToString
        End If
        '#########End get quantity

        '###########GET OMORCODE FOR MO STATUS
        Dim omorstatus As String = ""
        If lblomorcode.Text <> "" Then
            conn.Open()
            Dim get_omorcode As New SqlCommand("select name from omor_status where code = '" & lblomorcode.Text & "'", conn)
            omorstatus = get_omorcode.ExecuteScalar
            conn.Close()
        End If
        If omorstatus <> "" Then
            lblstatus.Text = omorstatus
        End If
        '###########END GETTING OMORCODE
    End Sub

    Private Sub getSAPtable()
        '######GET TABLE NAME
        conn.Open()
        Dim gettable As New SqlCommand("select path_name from path_settings where id = 2", conn)
        SAPtable = gettable.ExecuteScalar
        conn.Close()
        '######END GET TABLE NAME
    End Sub

    Protected Sub txtreject_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrejects.TextChanged
        Dim totalrejects As Integer
        totalrejects = Integer.Parse(txtg.Text) - Integer.Parse(txtrejects.Text)
        txtgoods.Text = totalrejects
    End Sub

    Protected Sub cmdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Response.Redirect("manualmo.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=manualmo")
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '############# INITIALIZING DATES
        txtstartmm.Text = Format(Now, "MM")
        txtendmm.Text = Format(Now, "MM")
        txtstartd.Text = Format(Now, "dd")
        txtendd.Text = Format(Now, "dd")
        txtstarty.Text = Format(Now, "yyyy")
        txtendy.Text = Format(Now, "yyyy")

        lblpostingdate.Text = Format(Now, "MM-dd-yyyy").ToString
        lbldocdate.Text = Format(Now, "MM-dd-yyyy").ToString
        '#############

        ddlsbu.Items.Clear()
        ddlsbu.Items.Add("")
        Dim readsbu As New SqlCommand("select name,name from sbu", conn)
        Dim sbuRows As SqlDataReader

        conn.Open()
        sbuRows = readsbu.ExecuteReader
        While sbuRows.Read
            Dim newItem As New ListItem(sbuRows(0).ToString, sbuRows(1).ToString)
            ddlsbu.Items.Add(newItem)
        End While
        conn.Close()

        ddloper.Items.Clear()
        ddloper.Items.Add("")
        Dim readoper As New SqlCommand("select code,code from oper_types where code <> 'ADMIN'", conn)
        Dim operRows As SqlDataReader

        conn.Open()
        operRows = readoper.ExecuteReader
        While operRows.Read
            Dim newItem As New ListItem(operRows(0).ToString, operRows(1).ToString)
            ddloper.Items.Add(newItem)
        End While
        conn.Close()

        ddlprocess.Items.Clear()
        ddlprocess.Items.Add("")
        Dim readprocess As New SqlCommand("select name,id from timeTypes", conn)
        Dim processRows As SqlDataReader

        conn.Open()
        processRows = readprocess.ExecuteReader
        While processRows.Read
            Dim newItem As New ListItem(processRows(0).ToString, processRows(1).ToString)
            ddlprocess.Items.Add(newItem)
        End While
        conn.Close()
    End Sub

    Protected Sub txtmo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmo.TextChanged
        If IsNumeric(txtmo.Text) Then
            lblerror.ForeColor = Drawing.Color.Empty
            lblerror.Text = ""
            txtmo.BorderColor = Drawing.Color.Empty
            '###check if there is a recover mo then, sap details
            checkrecovermo()

        Else
            lblerror.Text = "<div class='diverror'><div class='labelerror'><br><font color='red'>MO IS NOT DEFINED. <b>" & UCase(txtmo.Text) & "</b> </font></div></div>"
            lblerror.ForeColor = Drawing.Color.Red
            txtmo.BorderColor = Drawing.Color.Red
            txtmo.Text = ""
            txtmo.Focus()
        End If
    End Sub

    Protected Sub txtresource_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtresource.TextChanged
        Dim checkresource As Integer = 0
        conn.Open()
        Dim gettable As New SqlCommand("select id from users where user_name = '" & txtresource.Text & "'", conn)
        checkresource = gettable.ExecuteScalar
        conn.Close()

        If checkresource = 0 Then
            lblerrinres.Text = "Resource name is not Exist."
        Else
            lblerrinres.Text = ""
        End If

    End Sub

    Protected Sub ddlprocess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlprocess.SelectedIndexChanged
        Dim remarks As String = ""
        conn.Open()
        Dim readremarks As New SqlCommand("select description,type,code from timeTypes where id = '" & ddlprocess.SelectedValue & "'", conn)
        Dim remarksRows As SqlDataReader
        remarksRows = readremarks.ExecuteReader
        If remarksRows.HasRows Then
            While remarksRows.Read
                lblremarks.Text = remarksRows(0).ToString
                If remarksRows(1) = 1 Then
                    lblrem.Text = remarksRows(2).ToString
                Else
                    lblrem.Text = ""
                End If
            End While
        End If
        conn.Close()
    End Sub

    Protected Sub txtendmm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtendmm.TextChanged
        Try
            If IsNumeric(txtendmm.Text) Then
                If txtendmm.Text > 12 Then
                    lblerrorindate.Text = "Invalid Month"
                    txtendmm.Focus()
                Else
                    lblerrorindate.Text = ""
                    Functionpostingdate()
                    txtendd.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtendmm.Focus()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub txtendd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtendd.TextChanged
        Try
            If IsNumeric(txtendd.Text) Then
                If txtendd.Text > 31 Then
                    lblerrorindate.Text = "Invalid Days"
                    txtendd.Focus()
                Else
                    lblerrorindate.Text = ""
                    Functionpostingdate()
                    txtendy.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtendd.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtendy_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtendy.TextChanged
        Try
            If IsNumeric(txtendy.Text) Then
                If txtendy.Text > 2021 Then
                    lblerrorindate.Text = "Invalid Year"
                    txtendy.Focus()
                Else
                    lblerrorindate.Text = ""
                    Functionpostingdate()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtendy.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Functionpostingdate()
        Dim endmm, enddd, endyy As String
        endmm = ""
        enddd = ""
        endyy = ""

        If txtendmm.Text.Length = 1 Then
            endmm = "0" & txtendmm.Text
        Else
            endmm = txtendmm.Text
        End If
        If txtendd.Text.Length = 1 Then
            enddd = "0" & txtendd.Text
        Else
            enddd = txtendd.Text
        End If
        If txtendy.Text.Length = 1 Then
            endyy = "0" & txtendy.Text
        Else
            endyy = txtendy.Text
        End If
        lblpostingdate.Text = endmm & "-" & enddd & "-" & endyy
        lbldocdate.Text = endmm & "-" & enddd & "-" & endyy
    End Sub

    Protected Sub txtstartmm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstartmm.TextChanged
        Try
            If IsNumeric(txtstartmm.Text) Then
                If txtstartmm.Text > 12 Then
                    lblerrorindate.Text = "Invalid Month"
                    txtstartmm.Focus()
                Else
                    lblerrorindate.Text = ""
                    txtstartd.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtstartmm.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtstartd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstartd.TextChanged
        Try
            If IsNumeric(txtstartd.Text) Then
                If txtstartd.Text > 12 Then
                    lblerrorindate.Text = "Invalid Day"
                    txtstartd.Focus()
                Else
                    lblerrorindate.Text = ""
                    txtstarty.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtstartd.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtstarty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstarty.TextChanged
        Try
            If IsNumeric(txtstarty.Text) Then
                If txtstarty.Text > 2021 Then
                    lblerrorindate.Text = "Invalid Year"
                    txtstarty.Focus()
                Else
                    lblerrorindate.Text = ""
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtstarty.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtstarth_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstarth.TextChanged
        Try
            If IsNumeric(txtstarth.Text) Then
                If txtstarth.Text > 12 Then
                    lblerrorindate.Text = "Invalid Hour"
                    txtstarth.Focus()
                Else
                    lblerrorindate.Text = ""
                    txtstartm.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtstarth.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtstartm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstartm.TextChanged
        Try
            If IsNumeric(txtstartm.Text) Then
                If txtstartm.Text > 59 Then
                    lblerrorindate.Text = "Invalid Minute"
                    txtstartm.Focus()
                Else
                    lblerrorindate.Text = ""
                    txtstarts.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtstartm.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtstarts_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstarts.TextChanged
        Try
            If IsNumeric(txtstarts.Text) Then
                If txtstarts.Text > 12 Then
                    lblerrorindate.Text = "Invalid Minute"
                    txtstarts.Focus()
                Else
                    lblerrorindate.Text = ""
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtstarts.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtendh_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtendh.TextChanged
        Try
            If IsNumeric(txtendh.Text) Then
                If txtendh.Text > 12 Then
                    lblerrorindate.Text = "Invalid Minute"
                    txtendh.Focus()
                Else
                    lblerrorindate.Text = ""
                    txtendm.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtendh.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtendm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtendm.TextChanged
        Try
            If IsNumeric(txtendm.Text) Then
                If txtendm.Text > 59 Then
                    lblerrorindate.Text = "Invalid Minute"
                    txtendm.Focus()
                Else
                    lblerrorindate.Text = ""
                    txtends.Focus()
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtendm.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtends_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtends.TextChanged
        Try
            If IsNumeric(txtends.Text) Then
                If txtends.Text > 59 Then
                    lblerrorindate.Text = "Invalid Minute"
                    txtends.Focus()
                Else
                    lblerrorindate.Text = ""
                End If
            Else
                lblerrorindate.Text = "Alphanumeric Invalid"
                txtends.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmdsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsubmit.Click
        Dim sqlinsert As String = ""
        Dim postingdate As String = Format(CDate(lblpostingdate.Text), "yyyyMMdd")
        Dim docdate As String = Format(CDate(lbldocdate.Text), "yyyyMMdd")
        Dim startdate As String = txtstarty.Text & "-" & txtstartmm.Text & "-" & txtstartd.Text
        Dim starttime As String = txtstarth.Text & ":" & txtstartm.Text & ":" & txtstarts.Text
        Dim endtime As String = txtendh.Text & ":" & txtendm.Text & ":" & txtends.Text
        Dim enddate As String = txtendy.Text & "-" & txtendmm.Text & "-" & txtendd.Text
        sqlinsert = "insert into shopfloors values('" & txtmo.Text & "','" & postingdate.ToString & "','" & docdate.ToString & "', '" & ddlsbu.Text & "','" & ddloper.Text & "','" & txtresource.Text & "','" & lblrem.Text & "','" & startdate.ToString & " " & starttime.ToString & "', '" & enddate.ToString & " " & endtime.ToString & "','" & lblnoresource.Text & "','" & lblremarks.Text & "','" & txtgoods.Text & "','" & txtrejects.Text & "','','" & txtoper.Text & "','" & txtaoper.Text & "','" & txthelper.Text & "','','','" & lblfgcode.Text & "','" & txtheadcount.Text & "','')"

        Dim insertSF As New SqlCommand(sqlinsert, conn)
        conn.Open()
        Try
            insertSF.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()

        createexcel()
    End Sub


    Private Sub createexcel()
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
        '#end get time type and remarks

        Dim body() As String = {"", txtmo.Text, Format(CDate(lblpostingdate.Text), "yyyyMMdd").ToString, Format(CDate(lbldocdate.Text), "yyyyMMdd").ToString, lblrouting.Text, ddloper.Text, txtresource.Text, lblrem.Text, txtstarty.Text & " " & txtstartmm.Text, txtstarth.Text & " " & txtstartm.Text, txtendy.Text & " " & txtendmm.Text, txtendh.Text & "  " & txtendm.Text, lblremarks.Text, txtgoods.Text, txtrejects.Text}

        For body1 As Integer = 1 To 14
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

End Class
