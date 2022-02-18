Imports MaterialSkin
Public Class ShowMessageBox
    Private Sub ShowMessageBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Left = 71
        Me.Width = Label1.Width + 71 * 2
        MaterialRaisedButton1.Left = (Me.Width - MaterialRaisedButton1.Width) / 2 - 6
        Me.Text = Application.ProductName & " - Message"

        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.BlueGrey600, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE)

        Timer1.Interval = 150 '延时关闭窗体
    End Sub
    Private Sub MaterialRaisedButton1_Click(sender As Object, e As EventArgs) Handles MaterialRaisedButton1.Click
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Close()
        Timer1.Stop()
    End Sub
End Class