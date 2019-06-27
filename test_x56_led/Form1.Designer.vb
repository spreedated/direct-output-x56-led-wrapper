<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_Red = New System.Windows.Forms.TextBox()
        Me.txt_Green = New System.Windows.Forms.TextBox()
        Me.txt_Blue = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl_Blue = New System.Windows.Forms.Label()
        Me.lbl_Green = New System.Windows.Forms.Label()
        Me.lbl_Red = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lbl_Brightness = New System.Windows.Forms.Label()
        Me.txtBrightness = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbDevices = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(301, 92)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Set"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txt_Red
        '
        Me.txt_Red.Location = New System.Drawing.Point(70, 19)
        Me.txt_Red.MaxLength = 3
        Me.txt_Red.Name = "txt_Red"
        Me.txt_Red.Size = New System.Drawing.Size(45, 20)
        Me.txt_Red.TabIndex = 1
        Me.txt_Red.Text = "0"
        '
        'txt_Green
        '
        Me.txt_Green.Location = New System.Drawing.Point(70, 45)
        Me.txt_Green.MaxLength = 3
        Me.txt_Green.Name = "txt_Green"
        Me.txt_Green.Size = New System.Drawing.Size(45, 20)
        Me.txt_Green.TabIndex = 2
        Me.txt_Green.Text = "0"
        '
        'txt_Blue
        '
        Me.txt_Blue.Location = New System.Drawing.Point(70, 71)
        Me.txt_Blue.MaxLength = 3
        Me.txt_Blue.Name = "txt_Blue"
        Me.txt_Blue.Size = New System.Drawing.Size(45, 20)
        Me.txt_Blue.TabIndex = 3
        Me.txt_Blue.Text = "0"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbl_Blue)
        Me.GroupBox1.Controls.Add(Me.lbl_Green)
        Me.GroupBox1.Controls.Add(Me.lbl_Red)
        Me.GroupBox1.Controls.Add(Me.txt_Red)
        Me.GroupBox1.Controls.Add(Me.txt_Blue)
        Me.GroupBox1.Controls.Add(Me.txt_Green)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(125, 103)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Color"
        '
        'lbl_Blue
        '
        Me.lbl_Blue.Location = New System.Drawing.Point(6, 73)
        Me.lbl_Blue.Name = "lbl_Blue"
        Me.lbl_Blue.Size = New System.Drawing.Size(58, 14)
        Me.lbl_Blue.TabIndex = 7
        Me.lbl_Blue.Text = "Blue:"
        Me.lbl_Blue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Green
        '
        Me.lbl_Green.Location = New System.Drawing.Point(6, 47)
        Me.lbl_Green.Name = "lbl_Green"
        Me.lbl_Green.Size = New System.Drawing.Size(58, 14)
        Me.lbl_Green.TabIndex = 6
        Me.lbl_Green.Text = "Green:"
        Me.lbl_Green.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Red
        '
        Me.lbl_Red.Location = New System.Drawing.Point(6, 22)
        Me.lbl_Red.Name = "lbl_Red"
        Me.lbl_Red.Size = New System.Drawing.Size(58, 14)
        Me.lbl_Red.TabIndex = 5
        Me.lbl_Red.Text = "Red:"
        Me.lbl_Red.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbl_Brightness)
        Me.GroupBox2.Controls.Add(Me.txtBrightness)
        Me.GroupBox2.Location = New System.Drawing.Point(143, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(138, 51)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Brightness"
        '
        'lbl_Brightness
        '
        Me.lbl_Brightness.Location = New System.Drawing.Point(6, 25)
        Me.lbl_Brightness.Name = "lbl_Brightness"
        Me.lbl_Brightness.Size = New System.Drawing.Size(75, 14)
        Me.lbl_Brightness.TabIndex = 7
        Me.lbl_Brightness.Text = "Percentage:"
        Me.lbl_Brightness.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBrightness
        '
        Me.txtBrightness.Location = New System.Drawing.Point(87, 22)
        Me.txtBrightness.MaxLength = 3
        Me.txtBrightness.Name = "txtBrightness"
        Me.txtBrightness.Size = New System.Drawing.Size(45, 20)
        Me.txtBrightness.TabIndex = 6
        Me.txtBrightness.Text = "100"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbDevices)
        Me.GroupBox3.Location = New System.Drawing.Point(287, 21)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(97, 51)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Push to device"
        '
        'cmbDevices
        '
        Me.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDevices.FormattingEnabled = True
        Me.cmbDevices.Location = New System.Drawing.Point(6, 21)
        Me.cmbDevices.Name = "cmbDevices"
        Me.cmbDevices.Size = New System.Drawing.Size(81, 21)
        Me.cmbDevices.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 134)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Testingform"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents txt_Red As TextBox
    Friend WithEvents txt_Green As TextBox
    Friend WithEvents txt_Blue As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lbl_Blue As Label
    Friend WithEvents lbl_Green As Label
    Friend WithEvents lbl_Red As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lbl_Brightness As Label
    Friend WithEvents txtBrightness As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents cmbDevices As ComboBox
End Class
