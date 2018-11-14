<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HelpMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HelpMenu))
        Me.HelpText1 = New System.Windows.Forms.Label()
        Me.HelpText2 = New System.Windows.Forms.Label()
        Me.HelpPic1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.HelpText3 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HelpTabCtrl = New System.Windows.Forms.TabControl()
        Me.RulesPage = New System.Windows.Forms.TabPage()
        Me.GUIPage = New System.Windows.Forms.TabPage()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.HelpLbl1 = New System.Windows.Forms.Label()
        Me.TipsTab = New System.Windows.Forms.TabPage()
        Me.RefreshBtn = New System.Windows.Forms.Button()
        Me.TipTxB = New System.Windows.Forms.TextBox()
        Me.NickNameTxB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NickNameLbl = New System.Windows.Forms.Label()
        Me.SendBtn = New System.Windows.Forms.Button()
        Me.TipsGrid = New System.Windows.Forms.DataGridView()
        CType(Me.HelpPic1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HelpTabCtrl.SuspendLayout()
        Me.RulesPage.SuspendLayout()
        Me.GUIPage.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TipsTab.SuspendLayout()
        CType(Me.TipsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HelpText1
        '
        Me.HelpText1.Location = New System.Drawing.Point(0, 3)
        Me.HelpText1.Name = "HelpText1"
        Me.HelpText1.Size = New System.Drawing.Size(351, 98)
        Me.HelpText1.TabIndex = 0
        Me.HelpText1.Text = resources.GetString("HelpText1.Text")
        '
        'HelpText2
        '
        Me.HelpText2.Location = New System.Drawing.Point(3, 452)
        Me.HelpText2.Name = "HelpText2"
        Me.HelpText2.Size = New System.Drawing.Size(347, 56)
        Me.HelpText2.TabIndex = 2
        Me.HelpText2.Text = resources.GetString("HelpText2.Text")
        '
        'HelpPic1
        '
        Me.HelpPic1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.HelpPic1.Image = CType(resources.GetObject("HelpPic1.Image"), System.Drawing.Image)
        Me.HelpPic1.Location = New System.Drawing.Point(0, 104)
        Me.HelpPic1.Name = "HelpPic1"
        Me.HelpPic1.Size = New System.Drawing.Size(350, 345)
        Me.HelpPic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.HelpPic1.TabIndex = 1
        Me.HelpPic1.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(101, 511)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(174, 103)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'HelpText3
        '
        Me.HelpText3.Location = New System.Drawing.Point(0, 617)
        Me.HelpText3.Name = "HelpText3"
        Me.HelpText3.Size = New System.Drawing.Size(350, 120)
        Me.HelpText3.TabIndex = 4
        Me.HelpText3.Text = resources.GetString("HelpText3.Text")
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 740)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(347, 56)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 5
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 799)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(350, 47)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "The pieces are Flag (1x), Bomb (6x), Spy (1x), Scout (8x), Miner (5x), Sergeant (" & _
            "4x), Lieutenant (4x), Captain (4x), Major (3x), Colonel (2x), General (1x), and " & _
            "the Marshall (1x)"
        '
        'HelpTabCtrl
        '
        Me.HelpTabCtrl.Controls.Add(Me.RulesPage)
        Me.HelpTabCtrl.Controls.Add(Me.GUIPage)
        Me.HelpTabCtrl.Controls.Add(Me.TipsTab)
        Me.HelpTabCtrl.Location = New System.Drawing.Point(0, 0)
        Me.HelpTabCtrl.Name = "HelpTabCtrl"
        Me.HelpTabCtrl.SelectedIndex = 0
        Me.HelpTabCtrl.Size = New System.Drawing.Size(398, 331)
        Me.HelpTabCtrl.TabIndex = 1
        '
        'RulesPage
        '
        Me.RulesPage.AutoScroll = True
        Me.RulesPage.Controls.Add(Me.HelpText1)
        Me.RulesPage.Controls.Add(Me.Label1)
        Me.RulesPage.Controls.Add(Me.HelpPic1)
        Me.RulesPage.Controls.Add(Me.PictureBox2)
        Me.RulesPage.Controls.Add(Me.HelpText2)
        Me.RulesPage.Controls.Add(Me.HelpText3)
        Me.RulesPage.Controls.Add(Me.PictureBox1)
        Me.RulesPage.Location = New System.Drawing.Point(4, 22)
        Me.RulesPage.Name = "RulesPage"
        Me.RulesPage.Padding = New System.Windows.Forms.Padding(3)
        Me.RulesPage.Size = New System.Drawing.Size(385, 262)
        Me.RulesPage.TabIndex = 0
        Me.RulesPage.Text = "Rules"
        Me.RulesPage.UseVisualStyleBackColor = True
        '
        'GUIPage
        '
        Me.GUIPage.AutoScroll = True
        Me.GUIPage.Controls.Add(Me.PictureBox5)
        Me.GUIPage.Controls.Add(Me.Label3)
        Me.GUIPage.Controls.Add(Me.PictureBox4)
        Me.GUIPage.Controls.Add(Me.Label2)
        Me.GUIPage.Controls.Add(Me.PictureBox3)
        Me.GUIPage.Controls.Add(Me.HelpLbl1)
        Me.GUIPage.Location = New System.Drawing.Point(4, 22)
        Me.GUIPage.Name = "GUIPage"
        Me.GUIPage.Padding = New System.Windows.Forms.Padding(3)
        Me.GUIPage.Size = New System.Drawing.Size(385, 295)
        Me.GUIPage.TabIndex = 1
        Me.GUIPage.Text = "How to use this program"
        Me.GUIPage.UseVisualStyleBackColor = True
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(101, 456)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(145, 383)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox5.TabIndex = 5
        Me.PictureBox5.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 390)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(336, 63)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(10, 187)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(333, 199)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 3
        Me.PictureBox4.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(7, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(354, 31)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Then setup your pieces by first clicking on the piece on the side menu, then clic" & _
            "king the square you want it to go:"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(10, 23)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(249, 127)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 1
        Me.PictureBox3.TabStop = False
        '
        'HelpLbl1
        '
        Me.HelpLbl1.AutoSize = True
        Me.HelpLbl1.Location = New System.Drawing.Point(7, 7)
        Me.HelpLbl1.Name = "HelpLbl1"
        Me.HelpLbl1.Size = New System.Drawing.Size(206, 13)
        Me.HelpLbl1.TabIndex = 0
        Me.HelpLbl1.Text = "First, to start the game click on this button:"
        '
        'TipsTab
        '
        Me.TipsTab.Controls.Add(Me.RefreshBtn)
        Me.TipsTab.Controls.Add(Me.TipTxB)
        Me.TipsTab.Controls.Add(Me.NickNameTxB)
        Me.TipsTab.Controls.Add(Me.Label4)
        Me.TipsTab.Controls.Add(Me.NickNameLbl)
        Me.TipsTab.Controls.Add(Me.SendBtn)
        Me.TipsTab.Controls.Add(Me.TipsGrid)
        Me.TipsTab.Location = New System.Drawing.Point(4, 22)
        Me.TipsTab.Name = "TipsTab"
        Me.TipsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.TipsTab.Size = New System.Drawing.Size(390, 305)
        Me.TipsTab.TabIndex = 2
        Me.TipsTab.Text = "User tips"
        Me.TipsTab.UseVisualStyleBackColor = True
        '
        'RefreshBtn
        '
        Me.RefreshBtn.Location = New System.Drawing.Point(180, 190)
        Me.RefreshBtn.Name = "RefreshBtn"
        Me.RefreshBtn.Size = New System.Drawing.Size(27, 23)
        Me.RefreshBtn.TabIndex = 6
        Me.RefreshBtn.Text = " ↻"
        Me.RefreshBtn.UseVisualStyleBackColor = True
        '
        'TipTxB
        '
        Me.TipTxB.Location = New System.Drawing.Point(74, 216)
        Me.TipTxB.Multiline = True
        Me.TipTxB.Name = "TipTxB"
        Me.TipTxB.Size = New System.Drawing.Size(308, 81)
        Me.TipTxB.TabIndex = 5
        '
        'NickNameTxB
        '
        Me.NickNameTxB.Location = New System.Drawing.Point(74, 190)
        Me.NickNameTxB.Name = "NickNameTxB"
        Me.NickNameTxB.Size = New System.Drawing.Size(100, 20)
        Me.NickNameTxB.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(43, 219)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Tip:"
        '
        'NickNameLbl
        '
        Me.NickNameLbl.AutoSize = True
        Me.NickNameLbl.Location = New System.Drawing.Point(10, 193)
        Me.NickNameLbl.Name = "NickNameLbl"
        Me.NickNameLbl.Size = New System.Drawing.Size(58, 13)
        Me.NickNameLbl.TabIndex = 2
        Me.NickNameLbl.Text = "Nickname:"
        '
        'SendBtn
        '
        Me.SendBtn.Location = New System.Drawing.Point(213, 190)
        Me.SendBtn.Name = "SendBtn"
        Me.SendBtn.Size = New System.Drawing.Size(169, 23)
        Me.SendBtn.TabIndex = 1
        Me.SendBtn.Text = "Send tip"
        Me.SendBtn.UseVisualStyleBackColor = True
        '
        'TipsGrid
        '
        Me.TipsGrid.AllowUserToAddRows = False
        Me.TipsGrid.AllowUserToDeleteRows = False
        Me.TipsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TipsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TipsGrid.Location = New System.Drawing.Point(6, 6)
        Me.TipsGrid.Name = "TipsGrid"
        Me.TipsGrid.ReadOnly = True
        Me.TipsGrid.Size = New System.Drawing.Size(381, 178)
        Me.TipsGrid.TabIndex = 0
        '
        'HelpMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(398, 331)
        Me.Controls.Add(Me.HelpTabCtrl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HelpMenu"
        Me.Text = "Help"
        CType(Me.HelpPic1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HelpTabCtrl.ResumeLayout(False)
        Me.RulesPage.ResumeLayout(False)
        Me.GUIPage.ResumeLayout(False)
        Me.GUIPage.PerformLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TipsTab.ResumeLayout(False)
        Me.TipsTab.PerformLayout()
        CType(Me.TipsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HelpText1 As System.Windows.Forms.Label
    Friend WithEvents HelpPic1 As System.Windows.Forms.PictureBox
    Friend WithEvents HelpText2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents HelpText3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents HelpTabCtrl As System.Windows.Forms.TabControl
    Friend WithEvents RulesPage As System.Windows.Forms.TabPage
    Friend WithEvents GUIPage As System.Windows.Forms.TabPage
    Friend WithEvents HelpLbl1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents TipsTab As System.Windows.Forms.TabPage
    Friend WithEvents TipsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents RefreshBtn As System.Windows.Forms.Button
    Friend WithEvents TipTxB As System.Windows.Forms.TextBox
    Friend WithEvents NickNameTxB As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NickNameLbl As System.Windows.Forms.Label
    Friend WithEvents SendBtn As System.Windows.Forms.Button
End Class
