Public Class InfoPacket 'Stores relavent information that the AI may need to know.
    Public MyBoard As BoardState
    Public PreviousBoard As BoardState
    Public PreviousPlayerMove As Move
    Public PreviousAIMove As Move
    Public Graveyard As New List(Of BoardPiece)

    Sub New(ByVal Board As BoardState, ByVal PrevPlayerMove As Move, ByVal PrevAIMove As Move)
        MyBoard = Board
        PreviousPlayerMove = PrevPlayerMove
        PreviousAIMove = PreviousAIMove
    End Sub

End Class
