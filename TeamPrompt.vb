Public Class TeamPrompt

    Private Sub BlueBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlueBtn.Click
        Me.DialogResult = Windows.Forms.DialogResult.No 'No the player doesn't want to play red
        Me.Close()
    End Sub

    Private Sub RedBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedBtn.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes 'Yes the player wants to play red
        Me.Close()
    End Sub
End Class