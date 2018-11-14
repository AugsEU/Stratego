Public Class BoardState
    Public Pieces(BoardSide - 1, BoardSide - 1) As BoardPiece '10 by 10 array to store all the pieces and their position.
    Public IsRedToPlay As Boolean 'Stores whose turn it is.
    Sub New(ByVal StartPieces(,) As BoardPiece, ByVal IsItRedTurn As Boolean)
        IsRedToPlay = IsItRedTurn
        Pieces = StartPieces
    End Sub

    Function CheckMove(ByVal MyMove As Move) As Boolean
        If Not InBounds(MyMove.EndVector) Then 'Check for out of bounds
            Return False
        End If

        If IsNothing(Pieces(MyMove.StartVector.x, MyMove.StartVector.y)) Then 'You can't move an empty space
            Return False
        End If

        If (Not IsNothing(Pieces(MyMove.EndVector.x, MyMove.EndVector.y))) Then
            If Pieces(MyMove.EndVector.x, MyMove.EndVector.y).IsRed = Pieces(MyMove.StartVector.x, MyMove.StartVector.y).IsRed Then 'You can't move onto your own piece
                Return False
            End If
        End If

        If Not (Pieces(MyMove.StartVector.x, MyMove.StartVector.y).IsRed = IsRedToPlay) Then 'Can't move when it's not your turn
            Return False
        End If

        Dim MyPiece As BoardPiece = Pieces(MyMove.StartVector.x, MyMove.StartVector.y) 'This is the piece we are moving
        If (MyPiece.Value = Piece.Bomb Or MyPiece.Value = Piece.Flag) Then 'Bombs and flags should not be moving
            Return False
        End If

        Dim dy As Integer = MyMove.EndVector.y - MyMove.StartVector.y 'Get the difference in coordinates from start to end
        Dim dx As Integer = MyMove.EndVector.x - MyMove.StartVector.x 'Distance in each direction
        If Not (dy * dx = 0) Then 'If neither is 0 then there has been diagonal movement which is not allowed
            Return False
        End If
        If (dy = 0 And dx = 0) Then 'You can't move nowhere
            Return False
        End If

        If (MyPiece.Value = Piece.Scout) Then 'If the piece is the scout(2) it has different rules
            Dim xStep As Integer = If(dx < 0, -1, 1) 'If dx is negative there should be a negative step
            Dim yStep As Integer = If(dy < 0, -1, 1) 'If dy is negative yStep is assinged to -1, else 1.
            If (Math.Abs(dx) > 0) Then 'Either dy or dx is bigger than 0.
                For x = xStep To dx - xStep Step xStep 'We want to avoid checking the first and last parts of the route as it's not against the rules to have a piece there. The 2 is at the start
                    If Not IsNothing(Pieces(MyMove.StartVector.x + x, MyMove.StartVector.y)) Then 'Check for pieces
                        Return False 'You can't jump over pieces
                    ElseIf Utils.OneVectorOfMany(New VectorInt(MyMove.StartVector.x + x, MyMove.StartVector.y), New VectorInt(2, 4), New VectorInt(2, 5), New VectorInt(3, 4), New VectorInt(3, 5), New VectorInt(6, 4), New VectorInt(6, 5), New VectorInt(7, 4), New VectorInt(7, 5)) Then 'Check for ponds
                        Return False
                    End If
                Next
            ElseIf (Math.Abs(dy) > 0) Then
                For y = yStep To dy - yStep Step yStep 'We want to avoid checking the first and last parts of the route as it's not against the rules to have a piece there. The 2 is at the start
                    If Not IsNothing(Pieces(MyMove.StartVector.x, MyMove.StartVector.y + y)) Then 'Check for pieces
                        Return False 'You can't jump over pieces
                    ElseIf Utils.OneVectorOfMany(New VectorInt(MyMove.StartVector.x, MyMove.StartVector.y + y), New VectorInt(2, 4), New VectorInt(2, 5), New VectorInt(3, 4), New VectorInt(3, 5), New VectorInt(6, 4), New VectorInt(6, 5), New VectorInt(7, 4), New VectorInt(7, 5)) Then 'Check for ponds
                        Return False
                    End If
                Next
            End If
        Else
            If (Math.Abs(dy) > 1) Then 'Non-scouts can't move more than 1 piece at a time.
                Return False
            End If
            If (Math.Abs(dx) > 1) Then
                Return False
            End If
        End If
        Return True
    End Function

    Function Move(ByVal MyMove As Move, ByVal SkipCheck As Boolean) As BoardState
        Dim NewPieces(BoardSide - 1, BoardSide - 1) As BoardPiece 'This array stores a copy of the current board positions
        Array.Copy(Pieces, NewPieces, 100) 'This copies all the values over from our Pieces array.
        If Not SkipCheck Then
            If CheckMove(MyMove) = False Then
                Return Nothing
            End If
        End If

        If Not IsNothing(Pieces(MyMove.EndVector.x, MyMove.EndVector.y)) Then 'Reveal pieces but clone them aswell
            NewPieces(MyMove.StartVector.x, MyMove.StartVector.y) = New BoardPiece(NewPieces(MyMove.StartVector.x, MyMove.StartVector.y).Value, NewPieces(MyMove.StartVector.x, MyMove.StartVector.y).IsRed) 'Cloning pieces
            NewPieces(MyMove.EndVector.x, MyMove.EndVector.y) = New BoardPiece(NewPieces(MyMove.EndVector.x, MyMove.EndVector.y).Value, NewPieces(MyMove.EndVector.x, MyMove.EndVector.y).IsRed)

            NewPieces(MyMove.EndVector.x, MyMove.EndVector.y).Hidden = False
            NewPieces(MyMove.StartVector.x, MyMove.StartVector.y).Hidden = False
            NewPieces(MyMove.EndVector.x, MyMove.EndVector.y) = Utils.Battle(NewPieces(MyMove.StartVector.x, MyMove.StartVector.y), NewPieces(MyMove.EndVector.x, MyMove.EndVector.y)) 'Fight it out
        Else
            NewPieces(MyMove.EndVector.x, MyMove.EndVector.y) = Pieces(MyMove.StartVector.x, MyMove.StartVector.y)
        End If

        NewPieces(MyMove.StartVector.x, MyMove.StartVector.y) = Nothing 'Leave nothing behind
        If IsRedToPlay = True Then 'Once you moved change whose turn it is
            Return New BoardState(NewPieces, False)
        Else
            Return New BoardState(NewPieces, True)
        End If

    End Function
End Class
