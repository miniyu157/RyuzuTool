Public Class Announcement
    Private Sub Announcement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select(0, 0)
        Me.TopMost = True
        Me.TopLevel = True
        Me.Focus()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.CheckState = CheckState.Checked Then
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).SetValue("An", Me.Tag)
        End If

        Me.Close()
    End Sub
    Private Sub Announcement_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If CheckBox1.CheckState = CheckState.Checked Then
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).SetValue("An", Me.Tag)
        End If
    End Sub
End Class