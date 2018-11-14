Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class BinaryFileSave

    Shared Sub SaveIntoBinary(ByVal SaveData As Object, ByVal FilePath As String)
        Dim DataInBytes() As Byte = ConvertToBytes(SaveData) 'Convert into bytes
        Try
            My.Computer.FileSystem.WriteAllBytes(FilePath, DataInBytes, False) 'Save into file. The False means it will be overwritten
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation) 'Exception
        End Try
    End Sub

    Private Shared Function ConvertToBytes(ByVal DataObj As Object) As Byte()
        Dim ObjType As Type = DataObj.GetType()
        ' Get the IsSerializable property of myTestClassInstance.
        Dim IsSerializable As Boolean = ObjType.IsSerializable() 'Checks if file can be serialized

        If (IsNothing(DataObj) Or Not IsSerializable) Then
            Throw New Exception("Must be serializable")
        End If

        Dim MyFormatter As BinaryFormatter = New BinaryFormatter() 'Binary formatter can serialize objects
        Dim Ms As MemoryStream = New MemoryStream() 'Stores serialized list of bytes
        MyFormatter.Serialize(Ms, DataObj) 'Serializes object
        Return Ms.ToArray() 'Return it as array.
    End Function

    Shared Function LoadToObject(ByVal FilePath As String) As Object
        Dim Bytes() As Byte = LoadBytes(FilePath) 'Loads it as bytes

        If (IsNothing(Bytes)) Then 'If it is nothing return it
            Return Nothing
        End If

        Dim MyFormatter As BinaryFormatter = New BinaryFormatter() 'Create formatter to de-serialize
        Dim Ms As MemoryStream = New MemoryStream(Bytes) 'Put our bytes into the memory stream
        Return MyFormatter.Deserialize(Ms) 'Deserialize and return the deserialized object. This returns an object
    End Function

    Private Shared Function LoadBytes(ByVal FilePath As String) As Byte()
        Try
            Return My.Computer.FileSystem.ReadAllBytes(FilePath) 'Read all the bytes from a directory
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
        Return Nothing
    End Function

End Class
