Public Class PieceLocation 'Class to store a piece + location combo
    Public MyPiece As BoardPiece
    Public MyLocation As VectorInt

    Sub New(ByVal Piece As BoardPiece, ByVal MyLocal As VectorInt)
        MyPiece = Piece
        MyLocation = MyLocal
    End Sub
End Class
