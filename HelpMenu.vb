Imports System.Data.OleDb
Public Class HelpMenu

    Private Sub OKBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub HelpMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AutoScrollPosition = New Point(0, 0)
    End Sub

    Private Sub OKBtn_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub TipsTab_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TipsTab.Enter
        FillGrid()
        TipsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        TipsGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True
    End Sub

    Private Sub FillGrid()
        TipsGrid.DataSource = GetDataSet("SELECT Users.Nickname, Tips.Tip  FROM Tips, Users WHERE Users.UserID = Tips.UserID").Tables(0)
    End Sub

    Function GetDataSet(ByVal Query As String) As DataSet
        Dim ConnectionWorks As Boolean = False
        Dim ReturnSet As DataSet = New DataSet()
        Do
            Try
                Dim MyConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=N:\UserTipsDatabase.accdb")
                MyConnection.Open()
                Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter(Query, MyConnection)
                ReturnSet = New DataSet()
                Adapter.Fill(ReturnSet)

                ConnectionWorks = True 'Go through
                MyConnection.Close()
            Catch ex As Exception
                If (MsgBox("Connection failed. Do you want to try again?", MsgBoxStyle.YesNo) = MsgBoxResult.No) Then
                    ConnectionWorks = True 'Go through anyway
                End If
            End Try

        Loop Until ConnectionWorks

        Return ReturnSet
    End Function

    Private Sub RefreshBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshBtn.Click
        FillGrid()
    End Sub

    Private Sub SendBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendBtn.Click
        'See if nickname exists
        Dim UserData As DataSet = GetDataSet("SELECT UserID, Nickname FROM Users WHERE Nickname = '" & NickNameTxB.Text & "'") 'Fetch data from database

        If Not UserData.Tables(0).Rows.Count >= 1 Then 'User doesn't exist
            'Create user
            Dim ConnectionWorks As Boolean = False
            SqlCommand("INSERT INTO Users (NickName) VALUES ('" & NickNameTxB.Text & "')")
        End If
        'Get user ID
        UserData = GetDataSet("SELECT UserID, Nickname FROM Users WHERE Nickname = '" & NickNameTxB.Text & "'") 'Refresh user data
        Dim UserID As Integer = UserData.Tables(0).Rows(0).ItemArray(0)
        SqlCommand("INSERT INTO Tips (Tip, UserID) VALUES ('" & TipTxB.Text & "', " & UserID & ")")
        FillGrid()
        TipTxB.Text = ""
    End Sub

    Sub SqlCommand(ByVal SqlCommand As String)
        Dim ConnectionWorks As Boolean = False 'set to false until the connection works
        Do
            Try
                Dim MyConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=N:\UserTipsDatabase.accdb")
                MyConnection.Open()
                Dim Adapter As OleDbCommand = New OleDbCommand(SqlCommand, MyConnection)
                Adapter.ExecuteNonQuery()
                MyConnection.Close()

                ConnectionWorks = True 'Go through
            Catch ex As Exception
                If (MsgBox("Connection failed. Do you want to try again?", MsgBoxStyle.YesNo) = MsgBoxResult.No) Then
                    Exit Sub 'Stop this sub if the user says no
                End If
            End Try
        Loop Until ConnectionWorks
    End Sub

End Class