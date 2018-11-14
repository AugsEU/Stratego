Imports Microsoft.VisualBasic
Public Class Move
    Private strt As VectorInt 'The start
    Private ed As VectorInt 'The end

    Sub New(ByVal StrtVector As VectorInt, ByVal EdVector As VectorInt)
        strt = StrtVector
        ed = EdVector
    End Sub

    Property StartVector As VectorInt 'When changing x, it will be refered to as i.
        Get
            Return strt
        End Get

        Set(ByVal value As VectorInt)
            strt = value
        End Set
    End Property
    Property EndVector As VectorInt 'When changing y, it will be refered to as j.
        Get
            Return ed
        End Get

        Set(ByVal value As VectorInt)
            ed = value
        End Set
    End Property
End Class
