﻿Imports System.Data.SqlClient

Partial Class Site
    Inherits System.Web.UI.MasterPage
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)

    Protected Sub menunavi_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles menunavi.MenuItemClick
        Dim menuname As String
        menuname = menunavi.SelectedValue
        If menuname = "home" Then
            Response.Redirect("home.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=home")
        ElseIf menuname = "getmo" Then
            Response.Redirect("getmo_main.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=getmo")
        ElseIf menuname = "viewitems" Then
            'Response.Redirect("About.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=viewitems")
            Response.Write("<script>")
            Response.Write("window.open('About.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=viewitems" & "','_blank')")
            Response.Write("</script>")
        ElseIf menuname = "registration" Then
            Response.Redirect("register.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=registration")
        ElseIf menuname = "downtime" Then
            Response.Redirect("downtime.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot"))
        ElseIf menuname = "logout" Then
            Response.Redirect("Default.aspx")
        ElseIf menuname = "getrecovermo" Then
            Response.Redirect("admingetmo.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=getmo")
        ElseIf menuname = "settings" Then
            Response.Redirect("settings.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=settings")
        ElseIf menuname = "dbasesetup" Then
            Response.Redirect("settings.aspx?set=sh")
        ElseIf menuname = "usertype" Then
            Response.Redirect("admingetmo.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=getmo")
        ElseIf menuname = "manualmo" Then
            Response.Redirect("manualmo.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=manualmo")
        ElseIf menuname = "operatorytypes" Then
        ElseIf menuname = "operationtypes" Then
        ElseIf menuname = "utypes" Then
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '########setting selected tab for dbase settings
        If Request.QueryString("set") = "sap" Then
            menudbasetting.Items(0).Selected = True
        Else
            menudbasetting.Items(1).Selected = True
        End If
        '########end selected tab for dbase settings

        '########SET SETTING PAGE
        Dim pagename = ""
        pagename = Request.ServerVariables("SCRIPT_NAME")
        If InStr(pagename, "/") > 0 Then
            pagename = Right(pagename, Len(pagename) - InStrRev(pagename, "/"))
        End If

        If pagename.ToString = "settings.aspx" Then
            menudbasetting.Visible = True
            menunavi.Visible = False
        End If
        '########END SETTINGS

        '################# Menu for operator set visible
        If Request.QueryString("ut") = 1 Then
            menuoperator.Visible = False
        Else
            menunavi.Visible = False
            menuoperator.Visible = True
        End If
        '################# end setting visible menu operator

        '#################setting selected value for menu admin
        Dim req As String = Request.QueryString("mnu")
        If req = "home" Then
            menunavi.Items(0).Selected = True
        ElseIf req = "getmo" Then
            menunavi.Items(1).Selected = True
        ElseIf req = "manualmo" Then
            menunavi.Items(2).Selected = True
        ElseIf req = "viewitems" Then
            menunavi.Items(3).Selected = True
        ElseIf req = "registration" Then
            menunavi.Items(4).Selected = True

        End If
        '################# end setting selected value for menu admin

        '##########MENU OPERATOR#################
        If Request.QueryString("id") = "" Then
            menuoperator.Visible = False
        End If

        Dim selectedoper As String = ""
        selectedoper = Request.QueryString("mnu")
        If selectedoper = "home" Then
            menuoperator.Items(0).Selected = True
        ElseIf selectedoper = "getmo" Then
            menuoperator.Items(1).Selected = True
        ElseIf selectedoper = "viewitems" Then
            menuoperator.Items(2).Selected = True
        End If
        '########################################

        ''#################get username and description
        If Request.QueryString("id") <> "" Then
            conn.Open()
            Dim sql_getuname As String = "select '<b><u>' + users.user_name +'</u></b><br>'+ profiles.description from users inner join profiles on users.id = profiles.user_id where users.id = " & Request.QueryString("id")
            Dim getuname As New SqlCommand(sql_getuname, conn)
            getuname = New SqlCommand(sql_getuname, conn)
            lblusername.Text = getuname.ExecuteScalar & "<br><font color='#000'>" & Format(Now, "MMMM dd, yyyy") & "</font>"
            conn.Close()
        End If
        '#################end getting username and description

    End Sub

    Protected Sub menuoperator_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles menuoperator.MenuItemClick
        Dim menuoper As String
        menuoper = menuoperator.SelectedValue
        If menuoper = "home" Then
            Response.Redirect("home.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=home")
        ElseIf menuoper = "getmo" Then
            'Response.Redirect("main.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=getmo")
            Response.Redirect("getmo_main.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=getmo")
        ElseIf menuoper = "viewitems" Then
            'Response.Redirect("About.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=viewitems")
            Response.Write("<script>")
            Response.Write("window.open('About.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=viewitems" & "','_blank')")
            Response.Write("</script>")
        ElseIf menuoper = "registration" Then
            Response.Redirect("register.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot") & "&mnu=registration")
        ElseIf menuoper = "downtime" Then
            Response.Redirect("downtime.aspx?id=" & Request.QueryString("id") & "&ut=" & Request.QueryString("ut") & "&ot=" & Request.QueryString("ot"))
        ElseIf menuoper = "logout" Then
            Response.Redirect("Default.aspx")
        Else
        End If
    End Sub

    Protected Sub menudbasetting_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles menudbasetting.MenuItemClick
        Dim menudb As String
        menudb = menudbasetting.SelectedValue
        If menudb = "sap" Then
            Response.Redirect("settings.aspx?set=sap")
        ElseIf menudb = "sh" Then
            Response.Redirect("settings.aspx?set=sh")
        End If
    End Sub
End Class

