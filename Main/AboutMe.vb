Public Class AboutMe
    Private Sub AboutMe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label11.Text = Application.ProductVersion
    End Sub

#Region "QQ和Mail的点击事件"
    'QQ
    Private Sub Label5_MouseEnter(sender As Object, e As EventArgs) Handles Label5.MouseEnter
        Label5.Font = New Font(Label5.Font.Name, Label5.Font.Size, FontStyle.Underline)
    End Sub
    Private Sub Label5_MouseLeave(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        Label5.Font = New Font(Label5.Font.Name, Label5.Font.Size)
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Try
            Process.Start("tencent://AddContact/?fromId=50&fromSubId=1&subcmd=all&uin=1979287894")
        Catch
            MsgBox("似乎没有安装QQ", 0, "提示")
        End Try
    End Sub
    'Mail
    Private Sub Label3_MouseEnter(sender As Object, e As EventArgs) Handles Label3.MouseEnter
        Label3.Font = New Font("微软雅黑", Label3.Font.Size, FontStyle.Underline)
    End Sub
    Private Sub Label3_MouseLeave(sender As Object, e As EventArgs) Handles Label3.MouseLeave
        Label3.Font = New Font("微软雅黑", Label3.Font.Size, FontStyle.Regular)
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Try
            Process.Start("mailto:miniyu157@163.com")
        Catch
            MsgBox("似乎没有安装相关邮箱软件", 0, "提示")
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ChangeLogDisplay.ShowDialog()
    End Sub

    Private Sub Form1_Closeing(sender As Object, e As EventArgs) Handles MyBase.Closing
        ResetApp("")
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Shell（"explorer ""https://ryujinx.org/""")
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Shell（"explorer ""https://yuzu-emu.org/""")
    End Sub
    Private Sub ResetApp(ByVal Arguments As String)
        Me.Hide()
        ReleaseManage.Hide()
        Shell(Application.StartupPath & "\Bin\ResetApp.exe " & Arguments, Style:=AppWinStyle.Hide)
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click, Label12.Click
        Shell("explorer ""https://github.com/miniyu157/RyuzuTool""")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click, Label13.Click
        Shell("explorer ""https://space.bilibili.com/1650726013""")
    End Sub

    '贡献人员名单
    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Contributors.ShowDialog()
    End Sub

#End Region
End Class