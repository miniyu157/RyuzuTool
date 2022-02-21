Public Class ChangeLogDisplay
    Private Sub ChangeLogDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = System.IO.File.ReadAllText(Application.StartupPath & "\ChangeLog - 更新日志.md")
        TextBox1.Select(0, 0)
    End Sub
End Class