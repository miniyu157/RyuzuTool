Public Class AboutMe
    Private Sub AboutMe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Text = " "
        Me.ControlBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedDialog

        '窗体设计器中的属性
        'Me.ShowInTaskbar = False  （当用户从主窗体中打开"关于"对话框时不应显示任务栏中的图标）
        'Me.Size = New Size(340, 588)
        'PictureBox1.Location = New Point(98,20) (所有控件的位置随PictureBox1的改变而改变)


        '设置所有图片框的显示方式
        For Each SetAllSize In Me.Controls
            If TypeOf SetAllSize Is PictureBox Then
                SetAllSize.SizeMode = PictureBoxSizeMode.Zoom
            End If
        Next

        '设置所有的图标
        PictureBox1.Image = My.Resources.QF
        PictureBox2.Image = My.Resources.Mail
        PictureBox4.Image = My.Resources.QQ
        PictureBox5.Image = My.Resources.WeChat

        Button1.BackgroundImage = My.Resources.Back
        Button1.BackgroundImageLayout = ImageLayout.Zoom
    End Sub


    '***************************************QQ和Mail的点击事件
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
        Label3.Font = New Font("微软雅黑 Light", Label3.Font.Size, FontStyle.Underline)
    End Sub
    Private Sub Label3_MouseLeave(sender As Object, e As EventArgs) Handles Label3.MouseLeave
        Label3.Font = New Font("微软雅黑 Light", Label3.Font.Size)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Shell（"explorer ""https://ryujinx.org/""")
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Shell（"explorer ""https://yuzu-emu.org/""")
    End Sub
End Class