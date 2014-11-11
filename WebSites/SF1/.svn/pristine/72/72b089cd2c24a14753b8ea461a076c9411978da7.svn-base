Imports System.Data.SqlClient

Partial Class logistics
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Public sapconn As New SqlConnection(ConfigurationManager.ConnectionStrings("sapconnect").ToString)

    Protected Sub btnaddsubs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddsubs.Click
        If IsNumeric(txtaddsubs.Text) Then
            lblerror.Text = ""
            Dim sql_addsubs As String
            sql_addsubs = "insert into additional_subs values('" + Request.QueryString("id") + "','" + Request.QueryString("mo") + "','', '" + txtaddsubs.Text + "')"

            Dim insertSF As New SqlCommand(sql_addsubs, conn)
            conn.Open()
            Try
                insertSF.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
            conn.Close()
            Response.Redirect("logistics.aspx")
        Else
            lblerror.Text = "Invalid Substrate<br>"
        End If
    End Sub

    Protected Sub cmdsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsearch.Click

        Dim sql_getshopfloors As String
        sql_getshopfloors = "select id,mo,sbu,operation,resource,start_date_time,end_date_time,quantity,rejects,operator,ass_operator,helper,fg from shopfloors where mo = '" + txtmono.Text + "' and resource = '" + ddlresource.Text + "' order by id desc"
        Dim readshopfloors As New SqlCommand(sql_getshopfloors, conn)
        Dim shopfloorsrows As SqlDataReader

        conn.Open()
        shopfloorsrows = readshopfloors.ExecuteReader

        '##downtime table
        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        '#Header
        Dim heads() As String = {" ", "MO", "SBU", "Operation", "Resource", "Start Date and Time", "End Date and Time", "Good", "Rejects", "Operator", "Assistant Operator", "Helper", "FG"}
        cellCnt = 12 '#Columns

        Dim tRowh As New TableRow()
        For cellCtr = 1 To cellCnt
            Dim tCell As New TableCell()
            tCell.Text = heads(cellCtr)
            ' Add new TableCell object to row.
            tRowh.Cells.Add(tCell)
        Next
        ' Add new row to table.
        Table1.Rows.Add(tRowh)

        While shopfloorsrows.Read
            Dim tRow As New TableRow()
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                Dim link As New HyperLink()
                link.NavigateUrl = "logistics.aspx?id=" & shopfloorsrows(0) & "&mo=" & shopfloorsrows(1)
                link.Text = shopfloorsrows(cellCtr).ToString
                tCell.Controls.Add(link)

                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            Table1.Rows.Add(tRow)
        End While

        If shopfloorsrows.HasRows Then
            txtfound.Text = ""
        Else
            txtfound.Text = "<center>Nothing Found **</center>"
        End If
        conn.Close()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim sql_getusers As String
        sql_getusers = "select user_name from users order by user_name asc"
        Dim readusers As New SqlCommand(sql_getusers, conn)
        Dim userrows As SqlDataReader

        conn.Open()
        userrows = readusers.ExecuteReader
        While userrows.Read
            ddlresource.Items.Add(userrows(0))
        End While
        conn.Close()
        If Request.QueryString("id") <> "" Then
            Panel2.Visible = True
            conn.Open()
            Dim table As String
            Dim sql_gettableName As String = "select path_name from path_settings where id = 2"
            Dim gettable As New SqlCommand(sql_gettableName, conn)
            gettable = New SqlCommand(sql_gettableName, conn)
            table = gettable.ExecuteScalar
            conn.Close()

            'sap uom
            sapconn.Open()
            Dim sql_getsapDetails As String = "select U_SubsUom from [" + table + "] where DocNum = '" & Request.QueryString("mo") & "'"
            Dim get_omorcode As New SqlCommand(sql_getsapDetails, sapconn)
            get_omorcode = New SqlCommand(sql_getsapDetails, sapconn)
            Dim omorcode = get_omorcode.ExecuteScalar
            If Not IsDBNull(omorcode) Then
                lbluomsubs.Text = omorcode
            End If
            sapconn.Close()

            'display the value to add
            Dim sql_getdetailstoadd As String = "select mo,sbu,operation,resource,quantity,rejects,operator,ass_operator,helper,fg from shopfloors where id = '" + Request.QueryString("id") + "' order by id desc"
            Dim readdetails As New SqlCommand(sql_getdetailstoadd, conn)
            Dim detailsrows As SqlDataReader

            conn.Open()
            detailsrows = readdetails.ExecuteReader
            While detailsrows.Read
                lbldetails.Text =
                    "<table id='logistics'><tr><th>MO Number</th><td>" + detailsrows(0).ToString + "</td><th>SBU</th><td>" + detailsrows(1).ToString + "</td><th>Operation</th><td>" + detailsrows(2).ToString + "</td></tr>" & _
                    "<tr><th>Machine Name</th><td>" + detailsrows(3).ToString + "</td><th>Good</th><td>" + detailsrows(4).ToString + "<th>Rejects</th><td>" + detailsrows(5).ToString + "</td></tr>" & _
                    "<tr><th>Operator</th><td>" + detailsrows(6).ToString + "<th>Assistant Operator</th><td>" + detailsrows(7).ToString + "</td><th>Helper</th><td>" + detailsrows(8).ToString + "</td></tr>" & _
                    "<tr><th>FG</th><td colspan=5>" + detailsrows(9).ToString + "</td></tr>" & _
                    "</table>"
            End While
            conn.Close()
        End If
    End Sub

    Protected Sub btnout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnout.Click
        Response.Redirect("Default.aspx")
    End Sub
End Class
