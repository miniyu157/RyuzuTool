Imports DSAPI.神扩展

Public Class Contributors
    Private Sub Contributors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        TextBox1.Text = "正在加载..."
        Dim 加载名单线程 As New Threading.Thread(AddressOf 加载名单)
        加载名单线程.Start()
        TextBox1.Select(0, 0)
    End Sub

    Private Sub 加载名单()
        Try
            If IO.File.Exists(Application.StartupPath & "\Contributors.txt") Then
                IO.File.Delete(Application.StartupPath & "\Contributors.txt")
            End If
            My.Computer.Network.DownloadFile(My.Resources.Source, Application.StartupPath & "\Contributors.txt")
            TextBox1.Text = 读取文本文件(Application.StartupPath & "\Contributors.txt").提取中间文本("Ry贡献人员名单开始", "Ry贡献人员名单结束").Replace("(crlf)", vbCrLf)
            If IO.File.Exists(Application.StartupPath & "\Contributors.txt") Then
                IO.File.Delete(Application.StartupPath & "\Contributors.txt")
            End If
            TextBox1.Select(0, 0)
        Catch ex As Exception
            TextBox1.Text = "获取失败"
        End Try
    End Sub
    '申诉
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Try
            Process.Start("tencent://AddContact/?fromId=50&fromSubId=1&subcmd=all&uin=1979287894")
        Catch
            MsgBox("似乎没有安装QQ", 0, "提示")
        End Try
    End Sub
End Class