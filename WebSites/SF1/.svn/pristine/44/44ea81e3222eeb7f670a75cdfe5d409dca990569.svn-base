Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data

Partial Class settings
    Inherits System.Web.UI.Page
    Dim err As String
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbConnect").ToString)
    Public sapconn As New SqlConnection(ConfigurationManager.ConnectionStrings("sapConnect").ToString)

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles saveset.Click
        Call validateTBox()
        If err = "" Then
            Dim name As String
            If Request.QueryString("set") <> "" Then
                If Request.QueryString("set") = "sh" Then
                    name = "dbConnect"
                Else
                    name = "sapConnect"
                End If
            
                Dim isNew As Boolean = False
                Dim path As String = Server.MapPath("~/Web.Config")
                Dim doc As New XmlDocument()
                doc.Load(path)
                Dim list As XmlNodeList = doc.DocumentElement.SelectNodes(String.Format("connectionStrings/add[@name='{0}']", name))
                Dim node As XmlNode
                isNew = list.Count = 0
                If isNew Then
                    node = doc.CreateNode(XmlNodeType.Element, "add", Nothing)
                    Dim attribute As XmlAttribute = doc.CreateAttribute("name")
                    attribute.Value = name
                    node.Attributes.Append(attribute)

                    attribute = doc.CreateAttribute("connectionString")
                    attribute.Value = ""
                    node.Attributes.Append(attribute)

                    attribute = doc.CreateAttribute("providerName")
                    attribute.Value = "System.Data.SqlClient"
                    node.Attributes.Append(attribute)
            Else
                node = List(0)
            End If
            Dim conString As String = node.Attributes("connectionString").Value
            Dim conStringBuilder As New SqlConnectionStringBuilder(conString)
            conStringBuilder.InitialCatalog = txtdb.Text
            conStringBuilder.DataSource = txtserver.Text
            conStringBuilder.IntegratedSecurity = ddlsec.SelectedValue
            conStringBuilder.UserID = txtuid.Text
            conStringBuilder.Password = txtpass.Text
            node.Attributes("connectionString").Value = conStringBuilder.ConnectionString
            If isNew Then
                doc.DocumentElement.SelectNodes("connectionStrings")(0).AppendChild(node)
            End If
            doc.Save(path)
                Response.Redirect("Default.aspx")
            Else
                lblerrs.Text = "Please Select Configuration to Set"
            End If
        Else
            lblerrs.Text = err
        End If
    End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    Response.Redirect("Default.aspx")
    'End Sub

    Private Sub validateTBox()
        If txtdb.Text = "" Then
            txtdb.BorderColor = Drawing.Color.Red
            err = err + "<br>**Database cannot be blank"
        Else
            txtdb.BorderColor = Drawing.Color.Empty
        End If

        If txtpass.Text = "" Then
            txtpass.BorderColor = Drawing.Color.Red
            err = err + "<br>**Password cannot be blank"
        Else
            txtpass.BorderColor = Drawing.Color.Empty
        End If

        If txtserver.Text = "" Then
            txtserver.BorderColor = Drawing.Color.Red
            err = err + "<br>**Server cannot be blank"
        Else
            txtserver.BorderColor = Drawing.Color.Empty
        End If

        If txtuid.Text = "" Then
            txtuid.BorderColor = Drawing.Color.Red
            err = err + "<br>**User ID cannot be blank"
        Else
            txtuid.BorderColor = Drawing.Color.Empty
        End If

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim err As String = ""
        If Request.QueryString("set") = "sh" Then
            Try
                conn.Open()
                If conn.State = ConnectionState.Open Then

                Else
                    err = err + "<br>Connection for Shopfloor database must be define."
                End If
                conn.Close()
            Catch
                err = err + "<br>Connection for Shopfloor database must be define."
            End Try
        End If
        If Request.QueryString("set") = "sap" Then
            If Request.QueryString("tbl") = 1 Then
                err = err + "<br>Table for SAP must define."
                txtlblname.BorderColor = Drawing.Color.Red
            ElseIf Request.QueryString("tbl") = 2 Then
                err = err + "<br>Table Saved."
                lblerrs.ForeColor = Drawing.Color.Green
            Else
                txtlblname.BorderColor = Drawing.Color.Empty
            End If

            Try
                sapconn.Open()
                If sapconn.State = ConnectionState.Open Then
                    txtlblname.Visible = True
                    lbltblname.Visible = True
                    cmdsavetable.Visible = True
                Else
                    err = err + "<br>Connection for SAP database must be define."
                End If
                sapconn.Close()
            Catch
                err = err + "<br>Connection for SAP database must be define."
            End Try
        End If
        If err = "" Then
            lblerrs.Text = ""

        Else
            lblerrs.Text = err
        End If
        Try
            conn.Open()
            If conn.State = ConnectionState.Open Then
                '##SELECT CURRENT SETTINGS of excel path
                Dim sql_getsettings As String = "select * from path_settings where id = 1"
                Dim readSettings As New SqlCommand(sql_getsettings, conn)
                Dim settingsRow As SqlDataReader

                settingsRow = readSettings.ExecuteReader
                While settingsRow.Read
                    txtpath.Text = settingsRow(1)
                End While
                conn.Close()
            Else

            End If
            conn.Close()
        Catch

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub insertSettings()
        '##UPDATE CURRENT SETTINGS
        Dim inset_settings As String
        inset_settings = "update path_settings set path_name = '" & txtpath.Text & "' where id = 1"

        Dim insertSF As New SqlCommand(inset_settings, conn)
        conn.Open()
        Try
            insertSF.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()

    End Sub

    Protected Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        txtdb.Text = ""
        txtpass.Text = ""
        txtserver.Text = ""
        txtuid.Text = ""
        ddlsec.SelectedValue = "False"
        lblerrs.Text = ""
        txtdb.BorderColor = Drawing.Color.Empty
        txtpass.BorderColor = Drawing.Color.Empty
        txtserver.BorderColor = Drawing.Color.Empty
        txtuid.BorderColor = Drawing.Color.Empty
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        txtpath.Text = ""
        lblerrpath.Text = ""
        txtpath.BorderColor = Drawing.Color.Empty
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        If txtpath.Text = "" Then
            lblerrpath.Text = "**File path cannot be blank"
            txtpath.BorderColor = Drawing.Color.Red
        Else
            Call insertSettings()
            lblerrpath.Text = ""
            txtpath.BorderColor = Drawing.Color.Empty
        End If
    End Sub

    Protected Sub cmdsavetable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsavetable.Click

            Dim set_table As String
            set_table = "update path_settings set path_name = '" & txtlblname.Text & "' where id = 2"

            Dim set_tables As New SqlCommand(set_table, conn)
            conn.Open()
            Try
                set_tables.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
        conn.Close()
        txtlblname.Text = ""
        lblerrs.Text = lblerrs.Text & "<br><font color='green'>Table name Saved</font>"
        Response.Redirect("settings.aspx?set=sap&tbl=2")
    End Sub

    'Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
    '    Response.Redirect("settings.aspx?set=sap")
    'End Sub

    'Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
    '    Response.Redirect("settings.aspx?set=sh")
    'End Sub
End Class
