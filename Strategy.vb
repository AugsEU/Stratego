<Serializable()>
Public Class Strategy
    Dim RegretAndProb(,) As Double = {{1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}, {1, 0.025}}
    'So RegretAndProb (index,0) is for the regret
    '   RegretAndProb (index,1) is for the probability

    Public Function GetProbability(ByVal index As Integer) As Double 'Gets a probability from the array.
        Return RegretAndProb(index, 1)
    End Function

    Public Sub AddRegret(ByVal Postion As VectorInt, ByVal Regret As Double) 'This is for adding regret.
        Dim index As Integer = Postion.y * 10 + Postion.x
        If Regret < 0 Then 'Ensure Regret isn't negative
            Throw New Exception("Regret Value Cannot Be Negative!") 'This can cause trouble if it is allowed to be.
        ElseIf Double.IsPositiveInfinity(Regret) Then
            Throw New Exception("Regret value is too big to be stored as a double.", New Exception("Try using different units for utility."))
        End If
        RegretAndProb(index, 0) += Regret 'AddRegret
        Normalize() 'Since regret has changed, the probabilities need to be re-calculated
    End Sub

    Public Function PickOne() As VectorInt 'This is used to return a random choice(Random Index).
        Dim RangeLimit As Double = 0 'This should add to 1
        Dim BoundsList As List(Of Double) = New List(Of Double)
        For i = 0 To 39
            RangeLimit += RegretAndProb(i, 1)
            BoundsList.Add(RangeLimit)
        Next
        If Not Math.Round(RangeLimit, 6, System.MidpointRounding.AwayFromZero) = 1 Then 'The sum of a random distribution should sum to 1. If it doesn't something is wrong.
            Throw New Exception("Random distribution doesn't sum to 1. It sums to " + RangeLimit.ToString() + ".")
        End If
        Randomize()
        Dim RandomChoice As Double = Convert.ToDouble(Rnd())
        For i = 0 To 39 'Find where the RandomChoice fits into our distribution
            If BoundsList(i) > RandomChoice Then
                Return NumberToVector(i)
            End If
        Next
        Return Nothing
    End Function

    Public Sub Normalize() 'This normalises the regret values, and re-calculates the probabilities.
        Dim RegretSum As Double = 0 'This is the sum of all the regrets.
        For i = 0 To 39 'There should always only be 40 values in RegretAndProb
            RegretSum += RegretAndProb(i, 0) 'Get the sum of the regrets
        Next
        For i = 0 To 39 'Change all probabilities
            Dim MyRegretValue As Double = RegretAndProb(i, 0)
            RegretAndProb(i, 1) = MyRegretValue / RegretSum 'This is less than 1 because there are no negative or zero values so RegretSum is bigger than all regret values.
        Next
    End Sub


End Class
