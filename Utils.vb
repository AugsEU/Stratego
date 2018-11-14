Module Utils
    Public Const TileSize As Integer = 59 'Size of each square
    Public Const BoardBorder As Integer = 5 'Pixels
    Public Const BoardSide As Integer = 10 'The board is 10 by 10
    Public PieceFrequencies() As Integer = New Integer() {Piece.Spy, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Sergeant, Piece.Sergeant, Piece.Sergeant, Piece.Sergeant, Piece.Lieutenant, Piece.Lieutenant, Piece.Lieutenant, Piece.Lieutenant, Piece.Captain, Piece.Captain, Piece.Captain, Piece.Captain, Piece.Major, Piece.Major, Piece.Major, Piece.Colonel, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Flag} 'List every piece. This list is then re-used to keep track of the pieces that the player still has hidden.

    Public Enum Piece As Integer
        Spy = 1
        Scout = 2
        Miner = 3
        Sergeant = 4
        Lieutenant = 5
        Captain = 6
        Major = 7
        Colonel = 8
        General = 9
        Marshall = 10
        Bomb = 11
        Flag = 12
    End Enum

    Function InRangeInc(ByVal LowerBound As Double, ByVal UpperBound As Double, ByVal Value As Double) As Boolean 'Returns true if the value given in between the bounds(Inc=inclusive)
        If (Value >= LowerBound And Value <= UpperBound) Then
            Return True
        End If
        Return False
    End Function

    Function OneVectorOfMany(ByVal Input As VectorInt, ByVal ParamArray ToCheck() As VectorInt) As Boolean
        For i = 0 To ToCheck.Length - 1
            If Input.x = ToCheck(i).x And Input.y = ToCheck(i).y Then 'If they are the same
                Return True
            End If
        Next
        Return False
    End Function


    Function OnLake(ByVal Input As VectorInt) As Boolean 'Checks if a piece is on the lake tiles
        Return Utils.OneVectorOfMany(Input, New VectorInt(2, 4), New VectorInt(2, 5), New VectorInt(3, 4), New VectorInt(3, 5), New VectorInt(6, 4), New VectorInt(6, 5), New VectorInt(7, 4), New VectorInt(7, 5))
    End Function

    Function InBounds(ByVal Input As VectorInt) As Boolean
        Return Not OnLake(Input) AndAlso InRangeInc(0, 9, Input.x) AndAlso InRangeInc(0, 9, Input.y)
    End Function

    Function Min(ByVal Value1 As Double, ByVal Value2 As Double) As Double 'Returns the lower of the two numbers.
        If Value1 < Value2 Then
            Return Value1
        Else
            Return Value2 'it is possible that they are equal, in this case it doesn't matter which should be returned.
        End If
    End Function

    Function Max(ByVal Value1 As Double, ByVal Value2 As Double) As Double 'Returns the higher of the two numbers.
        If Value1 > Value2 Then
            Return Value1
        Else
            Return Value2 'it is possible that they are equal, in this case it doesn't matter which should be returned.
        End If
    End Function
    Function Battle(ByRef AttackingPiece As BoardPiece, ByRef DefendingPiece As BoardPiece) As BoardPiece 'Takes in two pieces and returns the winner
        If (AttackingPiece.Value = DefendingPiece.Value) Then 'Two pieces cancel out
            Return Nothing 'No victor
        End If
        If (DefendingPiece.Value = Piece.Bomb) Then 'Bomb 
            If (AttackingPiece.Value <> Piece.Miner) Then
                Return Nothing 'Return nothing as there is no winner
            Else
                Return AttackingPiece 'Miner defuses bomb
            End If
        End If

        If (InRangeInc(1, 10, AttackingPiece.Value)) And (InRangeInc(1, 10, DefendingPiece.Value)) Then
            If (AttackingPiece.Value = Piece.Spy And DefendingPiece.Value = Piece.Marshall) Then '1 beats a 10 rule
                Return AttackingPiece
            Else
                If (AttackingPiece.Value > DefendingPiece.Value) Then 'Normal pecking order
                    Return AttackingPiece
                Else
                    Return DefendingPiece
                End If
            End If
        End If
        If (DefendingPiece.Value = Piece.Flag And Not (AttackingPiece.Value = Piece.Flag)) Then
            Return AttackingPiece
        End If
        'At this point there should be a return value. Throw an exception if this failed.
        Throw New Exception("No victor after checking all rules. Are you sure that the values of FirstPiece and SecondPiece are within 1 and 12?")
    End Function
End Module
