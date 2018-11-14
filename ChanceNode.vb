Public Class ChanceNode
    Inherits Node

    Public MyProbabilities As List(Of Double) = New List(Of Double) 'List of corresponding probabilities

    Sub New() 'Chance nodes are intermediate, so they don't need to hold a board state

    End Sub

    Public Overloads Sub AddChild(ByVal ChildNode As Node, ByVal Probability As Double)
        If Not Utils.InRangeInc(0, 1, Probability) Then
            Throw New Exception("Probability must be within 0 and 1")
        End If
        ChildNodes.Add(ChildNode)
        MyProbabilities.Add(Probability)
    End Sub

End Class
