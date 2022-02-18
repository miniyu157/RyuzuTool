Public Class ChangeLogDisplay
    Private Sub ChangeLogDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.White
        Me.Text = ""
        Me.FormBorderStyle = FormBorderStyle.FixedDialog

        TextBox1.Text = System.IO.File.ReadAllText(Application.StartupPath & "\ChangeLog - 更新日志.md")
        Label1.Focus()
    End Sub
End Class