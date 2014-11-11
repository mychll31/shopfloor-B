Imports System.Data
Imports System.Data.SqlClient

Partial Class About
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnect").ToString)
    Dim shoprows() As String
    Dim shoprowsCtr As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        '#Header
        Dim heads() As String = {" ", "Process", "MO", "Description", "Operator", "Assistant Operator", "Helper", "Start", "End", "Good Qty", "Bad Qty", "Operator Remarks"}
        cellCnt = 11 '#Columns

        Dim tRowh As New TableRow()
        For cellCtr = 1 To cellCnt
            Dim tCell As New TableCell()
            tCell.Text = "<center>" & heads(cellCtr) & "</center>"
            ' Add new TableCell object to row.
            tRowh.Cells.Add(tCell)
        Next
        ' Add new row to table.
        Table1.Rows.Add(tRowh)

        Dim sql_getShopfloors As String
        If Request.QueryString("ut") = 1 Then
            sql_getShopfloors = "select '','SALE_PRICE' = CASE WHEN time_type = 'R' THEN ('Running') WHEN time_type = 'S' THEN ('Make Ready') WHEN time_type = 'L' THEN ('Change Over') END,mo,resource,operator,ass_operator,helper,start_date_time,end_date_time,quantity,rejects,oper_remarks from shopfloors order by id desc"
        Else
            sql_getShopfloors = "select '','SALE_PRICE' = CASE WHEN time_type = 'R' THEN ('Running') WHEN time_type = 'S' THEN ('Make Ready') WHEN time_type = 'L' THEN ('Change Over') END,mo,resource,operator,ass_operator,helper,start_date_time,end_date_time,quantity,rejects,oper_remarks from shopfloors where userid=" & Request.QueryString("id") & " order by id desc"
        End If
        Dim readsf As New SqlCommand(sql_getShopfloors, conn)
        Dim sfRows As SqlDataReader

        conn.Open()
        sfRows = readsf.ExecuteReader
        While sfRows.Read
            Dim tRow As New TableRow()
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                tCell.Text = sfRows(cellCtr).ToString
                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            Table1.Rows.Add(tRow)
            'Else
            'Call shopfloor()
            'End If
        End While
        conn.Close()

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Table1.Rows.Clear()
        Dim cellCtr As Integer
        ' Current cell counter.
        Dim cellCnt As Integer
        '#Header
        Dim heads() As String = {" ", "Process", "MO", "Description", "Operator", "Assistant Operator", "Helper", "Start", "End", "Good Qty", "Bad Qty", "Operator Remarks"}
        cellCnt = 11 '#Columns

        Dim tRowh As New TableRow()
        For cellCtr = 1 To cellCnt
            Dim tCell As New TableCell()
            tCell.Text = "<center>" & heads(cellCtr) & "</center>"
            ' Add new TableCell object to row.
            tRowh.Cells.Add(tCell)
        Next
        ' Add new row to table.
        Table1.Rows.Add(tRowh)

        Dim sql_getShopfloors As String
        Dim thisprocess As String
        If txtprocess.Text.ToLower = "make ready" Then
            thisprocess = "S"
        ElseIf txtprocess.Text.ToLower = "running" Then
            thisprocess = "R"
        ElseIf txtprocess.Text.ToLower = "change over" Then
            thisprocess = "L"
        Else
            thisprocess = ""
        End If

        Dim thisdates1a, thisdates1b As String
        Dim thisdates2a, thisdates2b As String

        If lblfrom.Text = "" Then
            thisdates1a = "1970-01-01"
            thisdates1b = "3000-12-30"
        Else
            thisdates1a = lblfrom.Text
            thisdates1b = lblfrom.Text
        End If
        If lblto.Text = "" Then
            thisdates2a = "1970-01-01"
            thisdates2b = "3000-12-30"
        Else
            thisdates2a = lblto.Text
            thisdates2b = lblto.Text
        End If

        If Request.QueryString("ut") = 1 Then
            sql_getShopfloors = "select '','SALE_PRICE' = CASE WHEN time_type = 'R' THEN ('Running') WHEN time_type = 'S' THEN ('Make Ready') WHEN time_type = 'L' THEN ('Change Over') END,mo,resource,operator,ass_operator,helper,start_date_time,end_date_time,quantity,rejects,oper_remarks from shopfloors where time_type LIKE '%" & thisprocess & "' and mo LIKE '%" & txtmo.Text & "%' and resource LIKE '%" & txtres.Text & "%' and operator LIKE '%" & txtoper.Text & "%' and ass_operator LIKE '%" & txtassistant.Text & "%' and helper LIKE '%" & txthelper.Text & "%' and start_date_time >= '" & thisdates1a & "' and start_date_time < '" & thisdates1b & " 24:00:00' and end_date_time >= '" & thisdates2a & "' and end_date_time < '" & thisdates2b & " 24:00:00' order by id desc"
        Else
            sql_getShopfloors = "select '','SALE_PRICE' = CASE WHEN time_type = 'R' THEN ('Running') WHEN time_type = 'S' THEN ('Make Ready') WHEN time_type = 'L' THEN ('Change Over') END,mo,resource,operator,ass_operator,helper,start_date_time,end_date_time,quantity,rejects,oper_remarks from shopfloors where time_type LIKE '%" & thisprocess & "' and mo LIKE '%" & txtmo.Text & "%' and resource LIKE '%" & txtres.Text & "%' and operator LIKE '%" & txtoper.Text & "%' and ass_operator LIKE '%" & txtassistant.Text & "%' and helper LIKE '%" & txthelper.Text & "%' and start_date_time >= '" & thisdates1a & "' and start_date_time < '" & thisdates1b & " 24:00:00' and end_date_time >= '" & thisdates2a & "' and end_date_time < '" & thisdates2b & " 24:00:00' and userid = " & Request.QueryString("id") & " order by id desc"
        End If

        Dim readsf As New SqlCommand(sql_getShopfloors, conn)
        Dim sfRows As SqlDataReader

        conn.Open()
        sfRows = readsf.ExecuteReader
        While sfRows.Read
            Dim tRow As New TableRow()
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                tCell.Text = sfRows(cellCtr).ToString

                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            Table1.Rows.Add(tRow)
            'Else
            'Call shopfloor()
            'End If
        End While
        conn.Close()
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        calfrom.Visible = True
        calto.Visible = False
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        calto.Visible = True
        calfrom.Visible = False
    End Sub

    Protected Sub calfrom_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calfrom.SelectionChanged
        lblfrom.Text = Format(calfrom.SelectedDate, "yyyy-MM-dd")
        calfrom.Visible = False
    End Sub

    Protected Sub calto_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calto.SelectionChanged
        lblto.Text = Format(calto.SelectedDate, "yyyy-MM-dd")
        calto.Visible = False
    End Sub

    Protected Sub clearsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles clearsearch.Click
        txtmo.Text = ""
        txtprocess.Text = ""
        txtres.Text = ""
        txtoperator.Text = ""
        txtassistant.Text = ""
        txthelper.Text = ""
        lblto.Text = ""
        lblfrom.Text = ""
        txtoper.Text = ""
        calfrom.Visible = False
        calto.Visible = False
    End Sub
End Class
