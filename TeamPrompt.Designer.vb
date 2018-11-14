<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TeamPrompt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PromptLbl = New System.Windows.Forms.Label()
        Me.RedBtn = New System.Windows.Forms.Button()
        Me.BlueBtn = New System.Windows.Forms.Button()
        Me.OrLbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'PromptLbl
        '
        Me.PromptLbl.BackColor = System.Drawing.Color.Transparent
        Me.PromptLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PromptLbl.Location = New System.Drawing.Point(12, 9)
        Me.PromptLbl.Name = "PromptLbl"
        Me.PromptLbl.Size = New System.Drawing.Size(349, 31)
        Me.PromptLbl.TabIndex = 0
        Me.PromptLbl.Text = "Which team do you want to play?"
        '
        'RedBtn
        '
        Me.RedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RedBtn.Font = New System.Drawing.Font("Modern No. 20", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RedBtn.Image = Global.StrategoMMXVI.My.Resources.Resources.RH
        Me.RedBtn.Location = New System.Drawing.Point(192, 43)
        Me.RedBtn.Name = "RedBtn"
        Me.RedBtn.Size = New System.Drawing.Size(62, 68)
        Me.RedBtn.TabIndex = 1
        Me.RedBtn.Text = "Red"
        Me.RedBtn.UseVisualStyleBackColor = True
        '
        'BlueBtn
        '
        Me.BlueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BlueBtn.Font = New System.Drawing.Font("Modern No. 20", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlueBtn.Image = Global.StrategoMMXVI.My.Resources.Resources.BH
        Me.BlueBtn.Location = New System.Drawing.Point(100, 43)
        Me.BlueBtn.Name = "BlueBtn"
        Me.BlueBtn.Size = New System.Drawing.Size(62, 68)
        Me.BlueBtn.TabIndex = 2
        Me.BlueBtn.Text = "Blue"
        Me.BlueBtn.UseVisualStyleBackColor = True
        '
        'OrLbl
        '
        Me.OrLbl.AutoSize = True
        Me.OrLbl.BackColor = System.Drawing.Color.Transparent
        Me.OrLbl.Location = New System.Drawing.Point(168, 72)
        Me.OrLbl.Name = "OrLbl"
        Me.OrLbl.Size = New System.Drawing.Size(18, 13)
        Me.OrLbl.TabIndex = 3
        Me.OrLbl.Text = "Or"
        '
        'TeamPrompt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.ClientSize = New System.Drawing.Size(373, 121)
        Me.Controls.Add(Me.OrLbl)
        Me.Controls.Add(Me.BlueBtn)
        Me.Controls.Add(Me.RedBtn)
        Me.Controls.Add(Me.PromptLbl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "TeamPrompt"
        Me.Text = "Choose a team"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PromptLbl As System.Windows.Forms.Label
    Friend WithEvents RedBtn As System.Windows.Forms.Button
    Friend WithEvents BlueBtn As System.Windows.Forms.Button
    Friend WithEvents OrLbl As System.Windows.Forms.Label
End Class
