Public Class BoardPiece
    Public Value As Integer 'Stores the value of the piece. 1=1,2=2...10=10,B=11,F=12
    Public IsRed As Boolean
    Public Hidden As Boolean

    Sub New(ByVal PieceValue As Integer, ByVal RedTeam As Boolean) 'Subroutine to create the class
        If Not InRangeInc(0, 12, PieceValue) Then
            Throw New Exception("Your piece value must be between 0 and 12")
        End If
        Value = PieceValue
        IsRed = RedTeam
        Hidden = True 'The piece always starts hidden
    End Sub

    Function EqualTo(ByRef AnotherBoardPiece As BoardPiece) As Boolean
        If AnotherBoardPiece.Value = Value AndAlso AnotherBoardPiece.IsRed = IsRed AndAlso AnotherBoardPiece.Hidden = Hidden Then 'All things are equal
            Return True
        End If
        Return False
    End Function

End Class
