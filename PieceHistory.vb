Public Class PieceHistory

    Public StartPosition As VectorInt 'Where the piece was placed at the start
    Public PiecesDestroyed As New List(Of BoardPiece) 'All the pieces captured by this piece. Including the last one.
    Public CurrentPosition As VectorInt 'Where the piece is now
    Public PieceType As BoardPiece 'The type of piece it was.

    Sub New(ByRef StartPos As VectorInt, ByRef PieceT As BoardPiece) 'Initialize values
        PieceType = PieceT
        StartPosition = StartPos
        CurrentPosition = StartPos
    End Sub

    Function AmIHere(ByRef PossiblePlace As VectorInt) As Boolean 'Checks if the piece you are looking for is at the specified location
        If PossiblePlace.x = CurrentPosition.x AndAlso PossiblePlace.y = CurrentPosition.y Then
            Return True
        End If
        Return False
    End Function
End Class
