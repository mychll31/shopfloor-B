Imports System.Data.SqlClient

Partial Class downtime
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        lblnonjob.Text = "<br>For NON-JOB Related MO, use code <b>NJR</b>."

        If Request.QueryString("dtid") <> "" Then
            txtcode.Text = Request.QueryString("dtid")
            Button2.Text = "Update"
            Label1.Visible = True
            chkactive.Visible = True
            Dim sql_getiddt As String = "select * from timeTypes where id=" & Request.QueryString("dtid")
            Dim readdt As New SqlCommand(sql_getiddt, conn)
            Dim dtrows As SqlDataReader

            conn.Open()
            dtrows = readdt.ExecuteReader
            While dtrows.Read
                txtcode.Text = dtrows(1).ToString
                txtreason.Text = dtrows(2).ToString
                Application.Lock()
                Application("process") = dtrows(3)
                Application.UnLock()
                txtdesc.Text = dtrows(4).ToString
                If dtrows(5) = 1 Then
                    chkactive.Checked = True
                Else
                    chkactive.Checked = False
                End If
            End While
            conn.Close()
        End If

        If Request.QueryString("action") <> "" Then
            lblerror.Text = "Downtime has been " & Request.QueryString("action") & "."
        End If

        Dim sql_getutype As String = "select * from types"
        Dim readutype As New SqlCommand(sql_getutype, conn)
        Dim utyperows As SqlDataReader

        conn.Open()
        utyperows = readutype.ExecuteReader
        While utyperows.Read
            Dim newItem As New ListItem(utyperows(1).ToString, utyperows(0).ToString)
            ddltype.Items.Add(newItem)
            'ddlsearch.Items.Add(newItem)
        End While
        conn.Close()

        '##downtime table
        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        '#Header
        Dim heads() As String = {" ", "Code", "Name", "Type", "Description", ""}
        cellCnt = 4 '#Columns

        Dim tRowh As New TableRow()
        For cellCtr = 1 To cellCnt
            Dim tCell As New TableCell()
            tCell.Text = heads(cellCtr)
            ' Add new TableCell object to row.
            tRowh.Cells.Add(tCell)
        Next
        ' Add new row to table.
        tbldt.Rows.Add(tRowh)

        Dim sql_gettimetype As String = "select t1.id, t1.code, t1.name, t2.names, t1.description, t1.active, t1.type from timeTypes as t1 inner join types as t2 on t1.type = t2.id where t1.active = 1"
        Dim readTime As New SqlCommand(sql_gettimetype, conn)
        Dim sfRows As SqlDataReader

        conn.Open()
        sfRows = readTime.ExecuteReader
        While sfRows.Read
            Dim tRow As New TableRow()
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                Dim link As New HyperLink()
                link.NavigateUrl = "downtime.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&dtid=" & sfRows(0).ToString & "&ot=" & Request.QueryString("ot")
                link.Text = sfRows(cellCtr).ToString
                tCell.Controls.Add(link)

                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            tbldt.Rows.Add(tRow)
        End While
        conn.Close()

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim err As String = ""

        If txtcode.Text = "" Then
            err = err + "<br>**Code cannot be blank"
            txtcode.BorderColor = Drawing.Color.Red
        Else
            txtcode.BorderColor = Drawing.Color.Empty
        End If
        If txtreason.Text = "" Then
            err = err + "<br>**Reason cannot be blank"
            txtreason.BorderColor = Drawing.Color.Red
        Else
            txtreason.BorderColor = Drawing.Color.Empty
        End If
        If txtdesc.Text = "" Then
            err = err + "<br>**Description cannot be blank"
            txtdesc.BorderColor = Drawing.Color.Red
        Else
            txtdesc.BorderColor = Drawing.Color.Empty
        End If
        If ddltype.Text = "" Then
            err = err + "<br>**Process cannot be blank"
            ddltype.BorderColor = Drawing.Color.Red
        Else
            ddltype.BorderColor = Drawing.Color.Empty
        End If
        If err = "" Then
            Dim active As Integer = 1
            If chkactive.Checked = True Then
                active = 1
            Else
                active = 0
            End If
            Dim sql_insertUsers As String
            If Button2.Text = "Save" Then
                sql_insertUsers = "insert into timeTypes values ('" + txtcode.Text + "','" + txtreason.Text + "','" + ddltype.SelectedValue + "','" + txtdesc.Text + "', 1)"
            Else
                sql_insertUsers = "update timeTypes set code ='" + txtcode.Text + "', name = '" + txtreason.Text + "', type='" + ddltype.SelectedValue + "', description='" + txtdesc.Text + "', active = " + active.ToString + " where id = " & Request.QueryString("dtid")
            End If
            Dim cmdinsert_user As New SqlCommand(sql_insertUsers, conn)
            conn.Open()
            Try
                cmdinsert_user.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
            conn.Close()

            If Button2.Text = "Save" Then
                Response.Redirect("downtime.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&action=added")
            Else
                Response.Redirect("downtime.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&action=updated")
            End If
            Button2.Text = "Save"
            txtcode.Text = ""
            txtreason.Text = ""
            txtdesc.Text = ""
        Else
            lblerror.Text = err
        End If
    End Sub

    Protected Sub cmdsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsearch.Click
        Dim sql As String = "select t1.id, t1.code, t1.name, t2.names, t1.description, t1.active, t1.type from timeTypes as t1 inner join types as t2 on t1.type = t2.id where (t1.code like '%" + txtsearch.Text + "%' or t1.name like '%" + txtsearch.Text + "%')"
        Call gettable(sql)
    End Sub

    Private Sub gettable(ByVal sql As String)
        tbldt.Rows.Clear()
        '##downtime table
        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        '#Header
        Dim heads() As String = {" ", "Code", "Name", "Type", "Description", ""}
        cellCnt = 5 '#Columns

        Dim tRowh As New TableRow()
        For cellCtr = 1 To cellCnt
            Dim tCell As New TableCell()
            tCell.Text = heads(cellCtr)
            ' Add new TableCell object to row.
            tRowh.Cells.Add(tCell)
        Next
        ' Add new row to table.
        tbldt.Rows.Add(tRowh)

        Dim sql_gettimetype As String = sql
        Dim readTime As New SqlCommand(sql_gettimetype, conn)
        Dim sfRows As SqlDataReader

        conn.Open()
        sfRows = readTime.ExecuteReader
        While sfRows.Read
            Dim tRow As New TableRow()
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                If cellCtr = 2 Then
                    Dim link As New HyperLink()
                    link.NavigateUrl = "downtime.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&dtid=" & sfRows(0).ToString
                    link.Text = sfRows(cellCtr).ToString
                    tCell.Controls.Add(link)
                ElseIf cellCtr = 5 Then
                    If sfRows(5) = 1 Then
                        tCell.Text = "Active"
                    Else
                        tCell.Text = "Inactive"
                    End If
                Else
                    tCell.Text = sfRows(cellCtr).ToString()
                End If
                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            tbldt.Rows.Add(tRow)
        End While
        conn.Close()
    End Sub

End Class
