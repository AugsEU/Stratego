Public MustInherit Class Node
    Public MyBoardState As BoardState
    Public ChildNodes As List(Of Node) = New List(Of Node)

    Public Sub AddChild(ByRef ChildNode As Node)
        ChildNodes.Add(ChildNode)
    End Sub

End Class
