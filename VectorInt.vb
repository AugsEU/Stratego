Public Class VectorInt
    Private i As Integer = 0 'The x coordinate
    Private j As Integer = 0 'The y coordinate

    Sub New(ByRef InitX As Integer, ByRef InitY As Integer)
        i = InitX
        j = InitY
    End Sub

    Property x As Integer 'When changing x, it will be refered to as i.
        Get
            Return i
        End Get

        Set(value As Integer)
            i = value
        End Set
    End Property

    Property y As Integer 'When changing y, it will be refered to as j.
        Get
            Return j
        End Get

        Set(value As Integer)
            j = value
        End Set
    End Property

    Function Add(ByVal MyVector As VectorInt) As VectorInt 'Adds two vectors and returns the result
        Dim ReturnVector As VectorInt = New VectorInt(i + MyVector.x, j + MyVector.y)
        Return ReturnVector
    End Function

End Class
