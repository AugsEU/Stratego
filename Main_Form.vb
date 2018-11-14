Imports System.Runtime.InteropServices
Public Class Main_Form
    Const DEVELOPER_MODE As Boolean = False
    Dim PieceFrequencies() As Integer = New Integer() {Piece.Spy, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Sergeant, Piece.Sergeant, Piece.Sergeant, Piece.Sergeant, Piece.Lieutenant, Piece.Lieutenant, Piece.Lieutenant, Piece.Lieutenant, Piece.Captain, Piece.Captain, Piece.Captain, Piece.Captain, Piece.Major, Piece.Major, Piece.Major, Piece.Colonel, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Flag} 'List every piece. This list is then re-used to keep track of the pieces that the player still has hidden.
    Dim CurrentBoard As BoardState
    Dim P1StartVector As VectorInt
    Dim P1SelectedPiece As BoardPiece
    Dim P1RemovingPiece As VectorInt
    Dim PlayerIsRed As Boolean
    Dim GameStage As GameState 'Shows what stage of the game.
    Dim AIThread As System.Threading.Thread
    Dim CurrentInfo As InfoPacket

    Enum GameState As Integer 'Shows what stage of the game.
        Placing = 0
        Playing = 1
        Over = 2
    End Enum

    Private Sub Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim StartPieces(9, 9) As BoardPiece
        CurrentBoard = New BoardState(StartPieces, False)
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        PlayerIsRed = True
        GameStage = GameState.Playing
        CurrentInfo = New InfoPacket(CurrentBoard, Nothing, Nothing)

        'TEST

        Dim BlueFlag As BoardPiece = New BoardPiece(12, False)
        Dim RedFlag As BoardPiece = New BoardPiece(12, True)
        Dim BlueDude As BoardPiece = New BoardPiece(6, False)
        Dim RedDude As BoardPiece = New BoardPiece(5, True)


        'Dim StartPieces(9, 9) As BoardPiece
        'For x = 0 To 9
        '    For y = 0 To 3
        '        StartPieces(x, y) = BlueDude
        '    Next
        'Next 
        StartPieces(8, 8) = RedFlag
        StartPieces(0, 0) = BlueFlag
        StartPieces(4, 4) = BlueDude
        'StartPieces(5, 5) = RedDude
        StartPieces(4, 5) = RedDude

        DrawBoard(CurrentBoard)
        'END TEST

    End Sub

    Private Sub Log(ByVal Message As String)
        DisplayTxb.Text = DisplayTxb.Text + vbNewLine + Message
    End Sub

    Function GetPieceImage(ByVal Value As Integer, ByVal IsRed As Boolean)
        Dim ReturnImage As Bitmap = My.Resources.NA
        If IsRed Then
            Select Case Value 'This selects the correct red image
                Case 1
                    ReturnImage = My.Resources.R1
                Case 2
                    ReturnImage = My.Resources.R2
                Case 3
                    ReturnImage = My.Resources.R3
                Case 4
                    ReturnImage = My.Resources.R4
                Case 5
                    ReturnImage = My.Resources.R5
                Case 6
                    ReturnImage = My.Resources.R6
                Case 7
                    ReturnImage = My.Resources.R7
                Case 8
                    ReturnImage = My.Resources.R8
                Case 9
                    ReturnImage = My.Resources.R9
                Case 10
                    ReturnImage = My.Resources.R10
                Case 11
                    ReturnImage = My.Resources.RB
                Case 12
                    ReturnImage = My.Resources.RF
            End Select
        Else
            Select Case Value 'This selects the correct blue image
                Case 1
                    ReturnImage = My.Resources.B1
                Case 2
                    ReturnImage = My.Resources.B2
                Case 3
                    ReturnImage = My.Resources.B3
                Case 4
                    ReturnImage = My.Resources.B4
                Case 5
                    ReturnImage = My.Resources.B5
                Case 6
                    ReturnImage = My.Resources.B6
                Case 7
                    ReturnImage = My.Resources.B7
                Case 8
                    ReturnImage = My.Resources.B8
                Case 9
                    ReturnImage = My.Resources.B9
                Case 10
                    ReturnImage = My.Resources.B10
                Case 11
                    ReturnImage = My.Resources.BB
                Case 12
                    ReturnImage = My.Resources.BF
            End Select
        End If
        Return ReturnImage
    End Function

    Private Sub DrawBoard(ByVal MyBoard As BoardState)
        Dim NewBackImage As Bitmap = My.Resources.tablero

        For x = 0 To BoardSide - 1 'Go through the board item by item
            For y = 0 To BoardSide - 1
                Dim newImage As Bitmap
                If Not IsNothing(MyBoard.Pieces(x, y)) Then
                    If MyBoard.Pieces(x, y).IsRed Then
                        newImage = GetPieceImage(MyBoard.Pieces(x, y).Value, True)
                        If (MyBoard.Pieces(x, y).Hidden And Not (MyBoard.Pieces(x, y).IsRed = PlayerIsRed)) Then
                            newImage = My.Resources.RH
                        End If
                    Else
                        newImage = GetPieceImage(MyBoard.Pieces(x, y).Value, False)
                        If (MyBoard.Pieces(x, y).Hidden And Not (MyBoard.Pieces(x, y).IsRed = PlayerIsRed)) Then
                            newImage = My.Resources.BH
                        End If
                    End If

                    DrawPiece(newImage, New VectorInt(x, y), NewBackImage)
                End If
            Next
        Next
        BoardPnl.BackgroundImage = NewBackImage
        BoardPnl.Invalidate()
    End Sub

    Private Sub DrawPiece(ByVal MyImg As Image, ByVal Position As VectorInt, ByRef BackImage As Image)
        Dim g As Graphics = Graphics.FromImage(BackImage)
        g.DrawImage(MyImg, New Rectangle(BoardBorder + Position.x * TileSize, -(Position.y * 0.999) * TileSize - BoardBorder + 540, TileSize, TileSize)) 'The numbers used here are arbitrary corrections for this specific size
    End Sub

    Private Sub EndGame(ByVal RedWon As Boolean) 'Handles the end of the game
        'RedWon is the team that should have won.
        Dim ActuallyHappened As Boolean = True 'Did the game really end? Assume true until proven false
        For x = 0 To BoardSide - 1
            For y = 0 To BoardSide - 1
                If Not IsNothing(CurrentBoard.Pieces(x, y)) Then
                    If (CurrentBoard.Pieces(x, y).IsRed = Not RedWon) Then 'Check losing team
                        If (CurrentBoard.Pieces(x, y).Value = 12) Then 'IF there flag is still here
                            ActuallyHappened = False 'Hoax
                        End If
                    End If
                End If
            Next
        Next

        If (ActuallyHappened) Then
            If RedWon Then
                MsgBox("Red wins the game!", MsgBoxStyle.Information)
            Else
                MsgBox("Blue wins the game!", MsgBoxStyle.Information)
            End If
            'The game is over
            GameStage = GameState.Over
            'Call end game analysis
            Dim ModuleName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("AI Module Name") 'Gets module properties
            Dim MethodName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("AI Endgame Analysis")
            If Not MethodName.ToLower() = "null" Then 'To lower is used to get rid of case-sensitivity
                Dim MyMethod As System.Reflection.MethodInfo = Type.GetType(ModuleName).GetMethod(MethodName) 'Gets method from properties
                MyMethod.Invoke(Me, Nothing) 'Call sub
            End If
        End If
    End Sub

    Private Sub BoardPnl_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BoardPnl.MouseDown
        Dim MouseX As Integer = e.X
        Dim MouseY As Integer = e.Y

        Dim XCoordinate As Integer = Math.Floor((MouseX - BoardBorder) / 47.5) 'Convert Mouse click to coordinates
        Dim YCoordinate As Double = Math.Floor((MouseY + BoardBorder - 530) / (-47.4)) - 1

        If InBounds(New VectorInt(XCoordinate, YCoordinate)) Then
            If GameStage = GameState.Playing Then 'If we are playing
                If Not IsNothing(CurrentBoard.Pieces(XCoordinate, YCoordinate)) AndAlso IsNothing(P1StartVector) Then 'Clicked on a piece and no piece selected
                    If CurrentBoard.Pieces(XCoordinate, YCoordinate).IsRed = PlayerIsRed Then 'Only select your own pieces
                        P1StartVector = New VectorInt(XCoordinate, YCoordinate) 'Select current place
                    ElseIf DEVELOPER_MODE Then 'If developer mode is on
                        P1StartVector = New VectorInt(XCoordinate, YCoordinate) 'Select current place
                    End If

                ElseIf Not IsNothing(P1StartVector) Then
                    Dim MyMove As Move = New Move(P1StartVector, New VectorInt(XCoordinate, YCoordinate)) 'Declare the move
                    Dim TempBoard As BoardState = CurrentBoard.Move(MyMove, False)
                    CurrentInfo.PreviousPlayerMove = MyMove
                    If Not IsNothing(TempBoard) Then 'Make sure that the Tempboard isn't nothing
                        If Not IsNothing(CurrentBoard.Pieces(MyMove.EndVector.x, MyMove.EndVector.y)) Then 'Log the event in the list.
                            Log("A " + CurrentBoard.Pieces(MyMove.StartVector.x, MyMove.StartVector.y).Value.ToString + " moved onto a " + CurrentBoard.Pieces(MyMove.EndVector.x, MyMove.EndVector.y).Value.ToString)
                            'Add pieces to graveyard
                            Dim Piece1 As BoardPiece = CurrentBoard.Pieces(MyMove.StartVector.x, MyMove.StartVector.y)
                            Dim Piece2 As BoardPiece = CurrentBoard.Pieces(MyMove.EndVector.x, MyMove.EndVector.y)
                            Dim Winner As BoardPiece = Battle(Piece1, Piece2)
                            If IsNothing(Winner) Then 'Check for tie
                                CurrentInfo.Graveyard.Add(Piece1) 'Both pieces are dead
                                CurrentInfo.Graveyard.Add(Piece2)
                            ElseIf Piece1.EqualTo(Winner) Then 'Piece1 is the winner
                                CurrentInfo.Graveyard.Add(Piece2) 'Piece2 lost so add it to the graveyard
                            ElseIf Piece2.EqualTo(Winner) Then 'Piece2 is the winner
                                CurrentInfo.Graveyard.Add(Piece1) 'Piece1 lost so add it to the graveyard
                            End If

                        End If
                        CurrentInfo.PreviousBoard = CurrentBoard 'Store old board
                        CurrentBoard = TempBoard 'New board
                        DrawBoard(CurrentBoard) 'Redraw the board
                        EndGame(PlayerIsRed) 'When the player moves only the player can win.
                        AITurn()
                    End If
                    P1StartVector = Nothing 'Reset the start vector
                End If
            ElseIf GameStage = GameState.Placing Then 'Placing
                If (YCoordinate < 4) Then 'Must put on bottom half of the screen.
                    If (Not IsNothing(P1SelectedPiece) AndAlso IsNothing(CurrentBoard.Pieces(XCoordinate, YCoordinate))) Then 'Make sure to check the user isn't overwriting a piece.
                        CurrentBoard.Pieces(XCoordinate, YCoordinate) = P1SelectedPiece 'Insert Piece
                        DrawPiece(GetPieceImage(P1SelectedPiece.Value, P1SelectedPiece.IsRed), New VectorInt(XCoordinate, YCoordinate), BoardPnl.BackgroundImage)
                        For i = 0 To PlacingPanel.Controls.Count - 1
                            If (PlacingPanel.Controls(i).Name = P1SelectedPiece.Value.ToString) Then 'Remove index of selected piece.
                                PlacingPanel.Controls.RemoveAt(i)
                                i = PlacingPanel.Controls.Count 'This will break out of the for loop.
                            End If
                        Next
                        P1SelectedPiece = Nothing
                        P1RemovingPiece = Nothing
                        ReAlignPicking()
                        RefreshPlacingPanelImages()
                    ElseIf Not IsNothing(CurrentBoard.Pieces(XCoordinate, YCoordinate)) Then 'If there is nothing selecting the user must want to put back.
                        P1RemovingPiece = New VectorInt(XCoordinate, YCoordinate)
                        P1SelectedPiece = Nothing
                    ElseIf Not IsNothing(P1RemovingPiece) AndAlso IsNothing(CurrentBoard.Pieces(XCoordinate, YCoordinate)) Then 'Moving a piece
                        CurrentBoard.Pieces(XCoordinate, YCoordinate) = CurrentBoard.Pieces(P1RemovingPiece.x, P1RemovingPiece.y) 'Put piece in the clicked place
                        CurrentBoard.Pieces(P1RemovingPiece.x, P1RemovingPiece.y) = Nothing 'Remove piece from where it was
                        P1RemovingPiece = Nothing 'Make sure this isn't still selected.
                        DrawBoard(CurrentBoard) 'Refresh the board
                    End If
                End If

            End If
            PxBSelected.Location = New Point(BoardBorder + (47.5 * XCoordinate), -(YCoordinate * 47.4) - BoardBorder + 437.2) 'Put the picturebox to indicate a click
        End If


    End Sub

    Private Sub ReAlignPicking() 'This re-aligns the picking panel
        'MsgBox(PlacingPanel.AutoScrollPosition.ToString())
        For i = 0 To PlacingPanel.Controls.Count - 1
            PlacingPanel.Controls(i).Location = New Point((i Mod 4) * 70, Math.Floor(i / 4) * 80 + PlacingPanel.AutoScrollPosition.Y)
        Next
    End Sub

    Private Sub PvEBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PvEBtn.Click 'Starts a new PvE game.
        If AIWorker.IsBusy Then
            Log("Wait for AI to stop thinking!")
        Else
            Dim StartPieces(9, 9) As BoardPiece
            Dim rng As Random = New Random()
            Randomize() 'Make sure to get a new random seed
            Dim RandomTeam As Boolean = rng.Next(0, 2) > 0 'This is a 50-50 boolean
            CurrentBoard = New BoardState(StartPieces, RandomTeam) 'A random team starts first
            GameStage = GameState.Placing 'Start with placing the pieces first
            Dim TeamResult As DialogResult
            Dim HasSelectedTeam As Boolean = False
            While (Not HasSelectedTeam)
                TeamResult = TeamPrompt.ShowDialog() 'Do they want to be red?
                If TeamResult = DialogResult.Yes Then
                    PlayerIsRed = True
                    HasSelectedTeam = True
                ElseIf TeamResult = DialogResult.No Then
                    PlayerIsRed = False
                    HasSelectedTeam = True
                Else
                    MsgBox("You must choose a team!", MsgBoxStyle.Exclamation)
                End If
            End While
            FillBox()
            AIWorker.RunWorkerAsync(Not PlayerIsRed) 'Set the AI placing and give the AI it's team.
            DrawBoard(CurrentBoard)
            LockBtn.Show()
        End If
    End Sub

    Private Sub PlacingClick(ByVal sender As PictureBox, ByVal e As System.EventArgs) 'This is the handler for all the pictureboxes.
        RefreshPlacingPanelImages() 'Unhighlight images
        Dim SenderPicture As Bitmap = sender.Image
        Dim PictureDrawer As Graphics = Graphics.FromImage(SenderPicture) 'Copy in the image to the graphics
        PictureDrawer.DrawRectangle(New Pen(Brushes.BurlyWood, 4), New Rectangle(2, 2, 57, 59)) 'Draw rectangle on the senderpicture
        sender.BackgroundImage = SenderPicture
        If GameStage = GameState.Placing Then 'Taking a piece out.
            P1SelectedPiece = New BoardPiece(CInt(sender.Name), PlayerIsRed)
        End If
    End Sub

    Private Sub RefreshPlacingPanelImages() 'Gets rid of all highlighting
        For Each PicBox As PictureBox In PlacingPanel.Controls
            PicBox.Image = GetPieceImage(CInt(PicBox.Name), PlayerIsRed)
        Next
    End Sub

    Private Sub PlacingPanel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PlacingPanel.Click
        If GameStage = GameState.Placing And Not IsNothing(P1RemovingPiece) Then 'Putting a piece back
            Dim PicImage As Image = GetPieceImage(CurrentBoard.Pieces(P1RemovingPiece.x, P1RemovingPiece.y).Value, PlayerIsRed)
            Dim NewPictureBox As PictureBox = New PictureBox()
            NewPictureBox.Image = PicImage 'Set basic properties
            NewPictureBox.BackgroundImageLayout = ImageLayout.Tile
            NewPictureBox.Size = New Size(61, 63)
            NewPictureBox.Location = New Point(0, 0) 'Align them 
            NewPictureBox.Name = CurrentBoard.Pieces(P1RemovingPiece.x, P1RemovingPiece.y).Value
            AddHandler NewPictureBox.Click, AddressOf PlacingClick 'This adds an event handler to our picture box
            PlacingPanel.Controls.Add(NewPictureBox)
            ReAlignPicking() 'Align it all
            CurrentBoard.Pieces(P1RemovingPiece.x, P1RemovingPiece.y) = Nothing 'Remove piece
            P1RemovingPiece = Nothing 'Reset
            DrawBoard(CurrentBoard)
        End If
    End Sub

    Sub FillBox() 'This is the initial fill for the picking panel
        PlacingPanel.Controls.Clear()

        For i = 0 To PieceFrequencies.Length - 1
            Dim PicImage As Image = GetPieceImage(PieceFrequencies(i), PlayerIsRed)
            Dim NewPictureBox As PictureBox = New PictureBox()
            NewPictureBox.Image = PicImage 'Set basic properties
            NewPictureBox.BackgroundImageLayout = ImageLayout.Tile
            NewPictureBox.Size = New Size(61, 63)
            NewPictureBox.Name = PieceFrequencies(i)
            AddHandler NewPictureBox.Click, AddressOf PlacingClick 'This adds an event handler to our picture box
            PlacingPanel.Controls.Add(NewPictureBox)
        Next
        ReAlignPicking()
    End Sub

    Function GetPlayerPieceHidden() As BoardState
        Dim NwPieces(BoardSide - 1, BoardSide - 1) As BoardPiece 'This array stores a copy of the current board positions
        Dim CurPieces As BoardPiece(,) = CurrentBoard.Pieces

        For x = 0 To Utils.BoardSide - 1 'Go through the board item by item
            For y = 0 To Utils.BoardSide - 1
                If Not IsNothing(CurPieces(x, y)) Then 'Check for nothing
                    NwPieces(x, y) = New BoardPiece(CurPieces(x, y).Value, CurPieces(x, y).IsRed) 'Make new objects to ensure the other doesn't change.
                    NwPieces(x, y).Hidden = CurPieces(x, y).Hidden
                    If NwPieces(x, y).Hidden And NwPieces(x, y).IsRed = PlayerIsRed Then 'Piece is hidden and on player's team
                        'NwPieces(x, y) = NormalBoardState.Pieces(x, y).MemberwiseClone()
                        NwPieces(x, y).Value = 0
                    End If
                End If
            Next
        Next
        Return New BoardState(NwPieces, CurrentBoard.IsRedToPlay) 'Return value
    End Function

    Private Sub AIWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles AIWorker.DoWork 'Starts the AI going.
        Dim ModuleName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("AI Module Name") 'Gets module properties
        If (GameStage = GameState.Playing) Then 'When the game is in progress
            Dim MethodName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("AI Main Method")
            Dim MyMethod As System.Reflection.MethodInfo = Type.GetType(ModuleName).GetMethod(MethodName) 'Gets method from properties
            Dim Result As Move = New Move(New VectorInt(0, 0), New VectorInt(0, 0))
            Try
                Result = MyMethod.Invoke(Me, {e.Argument}) 'Starts the AI. The AI must return a move.
            Catch ex As Exception
                MsgBox("AI module problem: " + ex.Message, MsgBoxStyle.Critical)
            End Try
            e.Result = Result
        ElseIf GameStage = GameState.Placing Then
            Dim MethodName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("AI Placing Method")
            Dim MyMethod As System.Reflection.MethodInfo = Type.GetType(ModuleName).GetMethod(MethodName) 'Gets method from properties
            Dim Result(10, 4) As BoardPiece
            Try
                Result = MyMethod.Invoke(Me, {e.Argument}) 'Starts the AI. The AI must return a move.
            Catch ex As Exception
                MsgBox("AI module problem: " + ex.Message, MsgBoxStyle.Critical)
            End Try
            e.Result = Result
            'ElseIf GameStage = GameState.Over Then 'If the game is over
            '    Dim MethodName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("AI Endgame Analysis")
            '    Dim MyMethod As System.Reflection.MethodInfo = Type.GetType(ModuleName).GetMethod(MethodName) 'Gets method from properties
            '    MyMethod.Invoke(Me, Nothing) 'Call sub
        End If
    End Sub

    Private Sub AIWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles AIWorker.RunWorkerCompleted
        If GameStage = GameState.Playing Then
            Dim MyMove As Move = DirectCast(e.Result, Move) 'Declare the move
            Dim TempBoard As BoardState = CurrentBoard.Move(MyMove, False)
            If Not IsNothing(TempBoard) Then 'Make sure that the Tempboard isn't nothing
                If Not IsNothing(CurrentBoard.Pieces(MyMove.EndVector.x, MyMove.EndVector.y)) Then
                    Log("A " + CurrentBoard.Pieces(MyMove.StartVector.x, MyMove.StartVector.y).Value.ToString + " moved onto a " + CurrentBoard.Pieces(MyMove.EndVector.x, MyMove.EndVector.y).Value.ToString)
                    'Add pieces to graveyard
                    Dim Piece1 As BoardPiece = CurrentBoard.Pieces(MyMove.StartVector.x, MyMove.StartVector.y)
                    Dim Piece2 As BoardPiece = CurrentBoard.Pieces(MyMove.EndVector.x, MyMove.EndVector.y)
                    Dim Winner As BoardPiece = Battle(Piece1, Piece2)
                    If IsNothing(Winner) Then 'Check for tie
                        CurrentInfo.Graveyard.Add(Piece1) 'Both pieces are dead
                        CurrentInfo.Graveyard.Add(Piece2)
                    ElseIf Piece1.EqualTo(Winner) Then 'Piece1 is the winner
                        CurrentInfo.Graveyard.Add(Piece2) 'Piece2 lost so add it to the graveyard
                    ElseIf Piece2.EqualTo(Winner) Then 'Piece2 is the winner
                        CurrentInfo.Graveyard.Add(Piece1) 'Piece1 lost so add it to the graveyard
                    End If
                End If
                CurrentInfo.PreviousBoard = CurrentBoard 'Store old board
                CurrentBoard = TempBoard 'New board
                CurrentInfo.PreviousAIMove = MyMove
                DrawBoard(CurrentBoard) 'Redraw the board
                EndGame(Not PlayerIsRed)
            Else 'It is nothing
                MsgBox("AI Module is not following the rules!", MsgBoxStyle.Critical)
            End If
        ElseIf GameStage = GameState.Placing Then
            Dim MyPlacement As BoardPiece(,) = DirectCast(e.Result, BoardPiece(,))
            'Check the board
            If Not (MyPlacement.GetLength(0) = 10 And MyPlacement.GetLength(1) = 4) Then 'The array must be the right size. if not then
                MsgBox("AI Module Problem: Wrong size array, must be (10,4)")
                Return 'Leave the method
            End If

            For x = 0 To 9 'Loop through the arry
                For y = 0 To 3
                    CurrentBoard.Pieces(x, y + 6) = MyPlacement(x, y) '
                Next
            Next
            DrawBoard(CurrentBoard)
        End If
    End Sub

    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        End 'Ends the program.
    End Sub

    Private Sub LockBtn_Click(sender As Object, e As EventArgs) Handles LockBtn.Click
        Dim PieceCount As Integer = 0 'Stores the number of pieces on the board
        For x = 0 To BoardSide - 1
            For y = 0 To BoardSide - 1
                If (Not IsNothing(CurrentBoard.Pieces(x, y))) Then
                    PieceCount += 1 'Increment by 1
                End If
            Next
        Next
        If PieceCount = 80 Then
            GameStage = GameState.Playing 'Switch to playing mode
            If Not CurrentBoard.IsRedToPlay = PlayerIsRed Then
                AITurn()
            End If
            LockBtn.Hide() 'Hide the button.
        Else
            MsgBox("All pieces must be placed first", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub AITurn() 'Gets called
        If Not GameStage = GameState.Over Then 'Only do something if it is not over.
            Dim AIBoard As BoardState = GetPlayerPieceHidden()
            CurrentInfo.MyBoard = AIBoard
            AIWorker.RunWorkerAsync(CurrentInfo)
        End If
    End Sub

    Private Sub FormTimer_Tick(sender As Object, e As EventArgs) Handles FormTimer.Tick 'Happens every 100ms so don't take too much time here.
        UpdateTurnLabel()
        If DEVELOPER_MODE Then
            For x = 0 To BoardSide - 1
                For y = 0 To BoardSide - 1
                    If Not IsNothing(CurrentBoard.Pieces(x, y)) Then
                        CurrentBoard.Pieces(x, y).Hidden = False 'Unhide all pieces
                    End If
                Next
            Next
            DrawBoard(CurrentBoard)
        End If
    End Sub

    Private Sub UpdateTurnLabel()
        If Not IsNothing(CurrentBoard) Then
            If CurrentBoard.IsRedToPlay Then 'Set text and colour
                TurnLbl.Text = "IT IS RED'S TURN"
                TurnLbl.ForeColor = Color.Red
            Else
                TurnLbl.Text = "IT IS BLUE'S TURN"
                TurnLbl.ForeColor = Color.Blue
            End If
        End If
    End Sub

    Private Sub DisplayTxb_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DisplayTxb.KeyDown
        If e.KeyCode = Keys.M And DEVELOPER_MODE Then
            Dim Dude As BoardPiece = New BoardPiece(10, PlayerIsRed)
            For x = 0 To 9
                For y = 0 To 3
                    CurrentBoard.Pieces(x, y) = Dude
                Next
            Next
            CurrentBoard.Pieces(5, 3) = New BoardPiece(12, PlayerIsRed)
        End If
    End Sub

    Private Sub HelpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpBtn.Click
        HelpMenu.Visible = False 'Ensure it's not visible
        HelpMenu.ShowDialog()
    End Sub

    Private Sub EvaluateBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EvaluateBtn.Click
        MsgBox(AIModule.Evaluate(CurrentBoard))
    End Sub

    Private Sub PxBSelected_MouseDown(sender As Object, e As MouseEventArgs) Handles PxBSelected.MouseDown
        Dim NewArgs As MouseEventArgs = New MouseEventArgs(e.Button, e.Clicks, e.X + PxBSelected.Location.X, e.Y + PxBSelected.Location.Y, e.Delta)
        BoardPnl_MouseDown(sender, NewArgs) 'Call the board panel to handle this
    End Sub
End Class