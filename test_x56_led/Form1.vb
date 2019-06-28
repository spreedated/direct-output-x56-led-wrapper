Imports X56_Wrapper_x64

Public Class Form1
    Dim i As X56_Wrapper = New X56_Wrapper

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dcvs As Array
        dcvs = System.Enum.GetValues(GetType(X56_Wrapper.DeviceSelection))
        For Each dcv In dcvs
            cmbDevices.Items.Add(dcv)
        Next
        cmbDevices.SelectedIndex = 2

        i.Open()
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        i.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        i.SetLed(cmbDevices.SelectedIndex, CShort(txt_Blue.Text), CShort(txt_Green.Text), CShort(txt_Red.Text), CShort(txtBrightness.Text))
    End Sub

    Private Sub TextBoxesOnlyDigits_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Red.KeyPress, txt_Green.KeyPress, txt_Blue.KeyPress, txtBrightness.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    Private Sub ColorTextBoxesLimit_TextChanged(sender As Object, e As EventArgs) Handles txt_Red.TextChanged, txt_Green.TextChanged, txt_Blue.TextChanged
        Dim txtBox As TextBox = sender
        If txtBox.Text.Length >= 1 Then
            If CShort(txtBox.Text) >= 256 Then
                txtBox.Text = "255"
            End If
        Else
            txtBox.Text = "0"
        End If
    End Sub

    Private Sub TxtBrightness_TextChanged(sender As Object, e As EventArgs) Handles txtBrightness.TextChanged
        Dim txtBox As TextBox = sender
        If txtBox.Text.Length >= 1 Then
            If CShort(txtBox.Text) >= 101 Then
                txtBox.Text = "100"
            End If
        Else
            txtBox.Text = "0"
        End If
    End Sub
End Class
