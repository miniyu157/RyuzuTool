Public Class Donation

    Private Sub Donation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ControlBox = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.Location = New Point(1017, 72)
        Me.BackColor = Color.FromArgb(255, 223, 239)

        PictureBox1.Image = My.Resources.Donation

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class