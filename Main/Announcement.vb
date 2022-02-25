Public Class Announcement
    Private Sub Announcement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select(0, 0)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class