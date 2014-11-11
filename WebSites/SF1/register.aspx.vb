Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Class register
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Private Shared DES As New TripleDESCryptoServiceProvider
    Private Shared MD5 As New MD5CryptoServiceProvider

    Public Shared Function MD5Hash(ByVal value As String) As Byte()
        Return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value))
    End Function

    Protected Sub cmdsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Dim err As String = ""

        'check if user exist
        Dim sql_checkexist As String
        Dim exist As Integer = 0
        conn.Open()
        sql_checkexist = "SELECT COUNT(id) from users where user_name='" + txtuser.Text + "'"
        Dim checkexist As New SqlCommand(sql_checkexist, conn)
        checkexist = New SqlCommand(sql_checkexist, conn)
        exist = checkexist.ExecuteScalar
        conn.Close()

        If cmdsave.Text = "Save" Then
            If exist = 0 Then
                txtuser.BorderColor = Drawing.Color.Empty
            Else
                err = "**Username already Exist"
                txtuser.BorderColor = Drawing.Color.Red
            End If
        End If

        If txtuser.Text = "" Then
            txtuser.BorderColor = Drawing.Color.Red
            err = err & "<br>**Username cannot be blank"
        Else
            txtuser.BorderColor = Drawing.Color.Empty
        End If
        If txtpass.Text = "" Then
            txtpass.BorderColor = Drawing.Color.Red
            err = err & "<br>**Password cannot be blank"
        Else
            txtpass.BorderColor = Drawing.Color.Empty
        End If
        If txtpass.Text = txtretype.Text Then
            txtretype.BorderColor = Drawing.Color.Empty
            txtpass.BorderColor = Drawing.Color.Empty
        Else
            txtretype.BorderColor = Drawing.Color.Red
            txtpass.BorderColor = Drawing.Color.Red
            err = err & "<br>**Password Not Match"
        End If
        If txtdesc.Text = "" Then
            txtdesc.BorderColor = Drawing.Color.Red
            err = err & "<br>**Description/Name cannot be blank"
        Else
            txtdesc.BorderColor = Drawing.Color.Empty
        End If
        If err <> "" Then
            lblerror.Text = err
            lblerror.ForeColor = Drawing.Color.Red
        End If

        If err = "" Then
            'count user
            Dim sql_insertUser As String
            Dim count As Integer
            conn.Open()
            sql_insertUser = "SELECT COUNT(id) from users"
            Dim insertSF As New SqlCommand(sql_insertUser, conn)
            insertSF = New SqlCommand(sql_insertUser, conn)
            count = insertSF.ExecuteScalar
            conn.Close()
            count = count + 1

            'insert user
            Call insertUser()

            'insert profile
            Dim sql_insertprofiles As String = ""
            If cmdsave.Text = "Save" Then
                sql_insertprofiles = "insert into profiles values ('" + count.ToString + "','" + ddlutype.SelectedValue + "','" + txtdesc.Text + "',1,'" + ddloper.SelectedValue + "')"
            Else
                Dim active As Integer
                If chkactive.Checked = True Then
                    active = 1
                Else
                    active = 0
                End If
                sql_insertprofiles = "update profiles set user_type = '" + ddlutype.SelectedValue + "', description = '" + txtdesc.Text + "', active = '" + active.ToString + "', operation_type = '" + ddloper.SelectedValue + "' where id =" & Request.QueryString("uid")

            End If

            Dim cmdinsert_profiles As New SqlCommand(sql_insertprofiles, conn)
            conn.Open()
            Try
                cmdinsert_profiles.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
            conn.Close()
            lblerror.Text = "User has been created"
            lblerror.ForeColor = Drawing.Color.Green

            txtuser.Text = ""
            txtpass.Text = ""
            txtdesc.Text = ""
            If cmdsave.Text = "Save" Then
                'Response.Redirect("register.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&action=saved")
            Else
                'Response.Redirect("register.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&action=updated")
            End If
        End If

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'end getting username and desc

        If Request.QueryString("ut") <> 1 Then
            lblnotaut.Visible = True
            Panel1.Visible = False
        Else
            If Request.QueryString("action") <> "" Then
                lblerror.Text = "User has been " & Request.QueryString("action") & " ."
                lblerror.ForeColor = Drawing.Color.Green
            End If
            If Request.QueryString("uid") <> "" Then
                cmdsave.Text = "Update"
                txtuser.ReadOnly = True
                lblactive.Visible = True
                chkactive.Visible = True
                Dim sql_getuserid As String = "select t1.active, t1.description,t2.user_name, t3.name, t1.user_type from profiles as t1 inner join users as t2 on t1.user_id = t2.id inner join userTypes as t3 on t1.user_type = t3.id where t1.id=" & Request.QueryString("uid")
                Dim readuser As New SqlCommand(sql_getuserid, conn)
                Dim userrows As SqlDataReader

                conn.Open()
                userrows = readuser.ExecuteReader
                While userrows.Read
                    txtuser.Text = userrows(2)
                    txtdesc.Text = userrows(1)
                    Application.Lock()
                    Application("usertype") = userrows(4)
                    Application.UnLock()
                    If userrows(0) = 1 Then
                        chkactive.Checked = True
                    Else
                        chkactive.Checked = False
                    End If
                End While
                conn.Close()

            End If

            'operation
            Dim sql_getoper As String = "select id, description from oper_types where active = 1"
            Dim readoper As New SqlCommand(sql_getoper, conn)
            Dim operrows As SqlDataReader

            conn.Open()
            operrows = readoper.ExecuteReader
            While operrows.Read
                Dim newoper As New ListItem(operrows(1).ToString, operrows(0).ToString)
                ddloper.Items.Add(newoper)
            End While
            conn.Close()

            Dim sql_getutype As String = "select * from userTypes"
            Dim readutype As New SqlCommand(sql_getutype, conn)
            Dim utyperows As SqlDataReader

            conn.Open()
            utyperows = readutype.ExecuteReader
            While utyperows.Read
                Dim newItem As New ListItem(utyperows(1).ToString, utyperows(0).ToString)
                ddlutype.Items.Add(newItem)
            End While
            conn.Close()

            '##downtime table
            Dim cellCtr As Integer
            ' Current cell counter.
            Dim cellCnt As Integer
            '#Header
            Dim heads() As String = {" ", "Name", "User Name", "User Code", "User Type", "Operation"}
            cellCnt = 4 '#Columns

            Dim tRowh As New TableRow()
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                tCell.Text = heads(cellCtr)
                ' Add new TableCell object to row.
                tRowh.Cells.Add(tCell)
            Next
            ' Add new row to table.
            tblregister.Rows.Add(tRowh)

            Dim sql_gettimetype As String = "select t1.id, t1.description, t2.user_name, t3.name, t4.description from profiles as t1 inner join users as t2 on t1.user_id = t2.id inner join userTypes as t3 on t1.user_type = t3.id left join oper_types as t4 on t1.operation_type = t4.id where t1.active = 1"
            Dim readTime As New SqlCommand(sql_gettimetype, conn)
            Dim sfRows As SqlDataReader

            conn.Open()
            sfRows = readTime.ExecuteReader
            While sfRows.Read
                Dim tRow As New TableRow()
                For cellCtr = 1 To cellCnt
                    Dim tCell As New TableCell()
                        Dim link As New HyperLink()
                        link.NavigateUrl = "register.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&uid=" & sfRows(0).ToString & "&ot=" & Request.QueryString("ot")
                        link.Text = sfRows(cellCtr).ToString
                        tCell.Controls.Add(link)

                    ' Add new TableCell object to row.
                    tRow.Cells.Add(tCell)
                Next
                ' Add new row to table.
                tblregister.Rows.Add(tRow)
            End While
            conn.Close()
        End If
    End Sub

    Private Sub insertUser()
        'encryptpassword
        Dim encryptedpass As String

        Dim key As String = "maychell31"
        DES.Key = register.MD5Hash(key)
        DES.Mode = CipherMode.ECB
        Dim Buffer As Byte() = ASCIIEncoding.ASCII.GetBytes(txtpass.Text)
        encryptedpass = Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))

        Dim sql_insertUsers As String = ""
        If cmdsave.Text = "Save" Then
            sql_insertUsers = "insert into users values ('" + txtuser.Text + "','" + encryptedpass + "')"
        Else
            sql_insertUsers = "update users set pass = '" + encryptedpass + "' where id =" & Request.QueryString("uid")
        End If
        Dim cmdinsert_user As New SqlCommand(sql_insertUsers, conn)
        conn.Open()
        Try
            cmdinsert_user.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim sql As String = "select t1.id,t1.description,t2.user_name, t3.name, t4.description, t1.active from profiles as t1 inner join users as t2 on t1.user_id = t2.id inner join userTypes as t3 on t1.user_type = t3.id left join oper_types as t4 on t1.operation_type = t4.id where t1.description like '%" & txtsearch.Text & "%' or t2.user_name like '%" & txtsearch.Text & "%'"
        Call gettable(sql)
    End Sub

    Private Sub gettable(ByVal sql As String)
        tblregister.Rows.Clear()
        '##downtime table
        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        '#Header
        Dim heads() As String = {" ", "Name", "Username", "User Type", "Operation", ""}
        cellCnt = 5 '#Columns

        Dim tRowh As New TableRow()
        For cellCtr = 1 To cellCnt
            Dim tCell As New TableCell()
            tCell.Text = heads(cellCtr)
            ' Add new TableCell object to row.
            tRowh.Cells.Add(tCell)
        Next
        ' Add new row to table.
        tblregister.Rows.Add(tRowh)

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
                    link.NavigateUrl = "register.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&uid=" & sfRows(0).ToString
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
            tblregister.Rows.Add(tRow)
        End While
        conn.Close()
    End Sub

    Protected Sub ddlutype_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlutype.PreRender
        ddlutype.SelectedValue = Application("usertype")
    End Sub

    Protected Sub ddloper_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddloper.PreRender
        ddloper.SelectedValue = Application("ot")
    End Sub

End Class
