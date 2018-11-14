<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Form
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Form))
        Me.PvEBtn = New System.Windows.Forms.Button()
        Me.ExitBtn = New System.Windows.Forms.Button()
        Me.DisplayTxb = New System.Windows.Forms.TextBox()
        Me.PlacingPanel = New System.Windows.Forms.Panel()
        Me.AIWorker = New System.ComponentModel.BackgroundWorker()
        Me.LockBtn = New System.Windows.Forms.Button()
        Me.FormTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TurnLbl = New System.Windows.Forms.Label()
        Me.TurnPnl = New System.Windows.Forms.Panel()
        Me.BoardPnl = New System.Windows.Forms.Panel()
        Me.PxBSelected = New System.Windows.Forms.PictureBox()
        Me.HelpBtn = New System.Windows.Forms.Button()
        Me.EvaluateBtn = New System.Windows.Forms.Button()
        Me.TurnPnl.SuspendLayout()
        Me.BoardPnl.SuspendLayout()
        CType(Me.PxBSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PvEBtn
        '
        Me.PvEBtn.Location = New System.Drawing.Point(801, 12)
        Me.PvEBtn.Name = "PvEBtn"
        Me.PvEBtn.Size = New System.Drawing.Size(75, 50)
        Me.PvEBtn.TabIndex = 2
        Me.PvEBtn.Text = "Player Vs AI"
        Me.PvEBtn.UseVisualStyleBackColor = True
        '
        'ExitBtn
        '
        Me.ExitBtn.Location = New System.Drawing.Point(12, 460)
        Me.ExitBtn.Name = "ExitBtn"
        Me.ExitBtn.Size = New System.Drawing.Size(292, 37)
        Me.ExitBtn.TabIndex = 5
        Me.ExitBtn.Text = "Exit"
        Me.ExitBtn.UseVisualStyleBackColor = True
        '
        'DisplayTxb
        '
        Me.DisplayTxb.Location = New System.Drawing.Point(12, 12)
        Me.DisplayTxb.Multiline = True
        Me.DisplayTxb.Name = "DisplayTxb"
        Me.DisplayTxb.ReadOnly = True
        Me.DisplayTxb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DisplayTxb.Size = New System.Drawing.Size(205, 106)
        Me.DisplayTxb.TabIndex = 6
        Me.DisplayTxb.Text = "Hello user!"
        '
        'PlacingPanel
        '
        Me.PlacingPanel.AutoScroll = True
        Me.PlacingPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.PlacingPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PlacingPanel.Location = New System.Drawing.Point(12, 124)
        Me.PlacingPanel.Name = "PlacingPanel"
        Me.PlacingPanel.Size = New System.Drawing.Size(292, 330)
        Me.PlacingPanel.TabIndex = 7
        '
        'AIWorker
        '
        '
        'LockBtn
        '
        Me.LockBtn.Location = New System.Drawing.Point(801, 447)
        Me.LockBtn.Name = "LockBtn"
        Me.LockBtn.Size = New System.Drawing.Size(75, 50)
        Me.LockBtn.TabIndex = 8
        Me.LockBtn.Text = "Confirm Layout"
        Me.LockBtn.UseVisualStyleBackColor = True
        Me.LockBtn.Visible = False
        '
        'FormTimer
        '
        Me.FormTimer.Enabled = True
        Me.FormTimer.Interval = 200
        '
        'TurnLbl
        '
        Me.TurnLbl.BackColor = System.Drawing.Color.Transparent
        Me.TurnLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TurnLbl.ForeColor = System.Drawing.Color.Blue
        Me.TurnLbl.Location = New System.Drawing.Point(3, 5)
        Me.TurnLbl.Name = "TurnLbl"
        Me.TurnLbl.Size = New System.Drawing.Size(71, 58)
        Me.TurnLbl.TabIndex = 1
        Me.TurnLbl.Text = "IT IS BLUE'S TURN"
        '
        'TurnPnl
        '
        Me.TurnPnl.BackColor = System.Drawing.Color.Silver
        Me.TurnPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TurnPnl.Controls.Add(Me.TurnLbl)
        Me.TurnPnl.Location = New System.Drawing.Point(223, 12)
        Me.TurnPnl.Name = "TurnPnl"
        Me.TurnPnl.Size = New System.Drawing.Size(81, 68)
        Me.TurnPnl.TabIndex = 9
        '
        'BoardPnl
        '
        Me.BoardPnl.BackgroundImage = Global.StrategoMMXVI.My.Resources.Resources.tablero
        Me.BoardPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BoardPnl.Controls.Add(Me.PxBSelected)
        Me.BoardPnl.Location = New System.Drawing.Point(310, 12)
        Me.BoardPnl.Name = "BoardPnl"
        Me.BoardPnl.Size = New System.Drawing.Size(485, 485)
        Me.BoardPnl.TabIndex = 0
        '
        'PxBSelected
        '
        Me.PxBSelected.BackColor = System.Drawing.Color.Transparent
        Me.PxBSelected.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PxBSelected.Location = New System.Drawing.Point(242, 290)
        Me.PxBSelected.Name = "PxBSelected"
        Me.PxBSelected.Size = New System.Drawing.Size(48, 47)
        Me.PxBSelected.TabIndex = 0
        Me.PxBSelected.TabStop = False
        '
        'HelpBtn
        '
        Me.HelpBtn.Location = New System.Drawing.Point(801, 392)
        Me.HelpBtn.Name = "HelpBtn"
        Me.HelpBtn.Size = New System.Drawing.Size(75, 49)
        Me.HelpBtn.TabIndex = 10
        Me.HelpBtn.Text = "Help"
        Me.HelpBtn.UseVisualStyleBackColor = True
        '
        'EvaluateBtn
        '
        Me.EvaluateBtn.Location = New System.Drawing.Point(223, 86)
        Me.EvaluateBtn.Name = "EvaluateBtn"
        Me.EvaluateBtn.Size = New System.Drawing.Size(76, 32)
        Me.EvaluateBtn.TabIndex = 11
        Me.EvaluateBtn.Text = "Evaluate"
        Me.EvaluateBtn.UseVisualStyleBackColor = True
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(888, 509)
        Me.Controls.Add(Me.EvaluateBtn)
        Me.Controls.Add(Me.HelpBtn)
        Me.Controls.Add(Me.TurnPnl)
        Me.Controls.Add(Me.LockBtn)
        Me.Controls.Add(Me.PlacingPanel)
        Me.Controls.Add(Me.DisplayTxb)
        Me.Controls.Add(Me.ExitBtn)
        Me.Controls.Add(Me.PvEBtn)
        Me.Controls.Add(Me.BoardPnl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main_Form"
        Me.Text = "Stratego MMXVI"
        Me.TurnPnl.ResumeLayout(False)
        Me.BoardPnl.ResumeLayout(False)
        CType(Me.PxBSelected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BoardPnl As System.Windows.Forms.Panel
    Friend WithEvents PvEBtn As System.Windows.Forms.Button
    Friend WithEvents ExitBtn As System.Windows.Forms.Button
    Friend WithEvents PxBSelected As System.Windows.Forms.PictureBox
    Friend WithEvents DisplayTxb As System.Windows.Forms.TextBox
    Friend WithEvents PlacingPanel As System.Windows.Forms.Panel
    Friend WithEvents AIWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents LockBtn As Button
    Friend WithEvents FormTimer As Timer
    Friend WithEvents TurnLbl As Label
    Friend WithEvents TurnPnl As Panel
    Friend WithEvents HelpBtn As System.Windows.Forms.Button
    Friend WithEvents EvaluateBtn As System.Windows.Forms.Button
End Class
