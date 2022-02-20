Imports System.IO

Public Class Main
    Dim Change As String
    Dim Ver As String
    Dim Link As String
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False

        Dim 下载配置文件线程 As New Threading.Thread(AddressOf 下载配置文件)
        下载配置文件线程.Start()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub


    Private Sub 下载配置文件()
        If File.Exists(Application.StartupPath & "\UpdateInfo.txt") Then File.Delete(Application.StartupPath & "\UpdateInfo.txt")
        My.Computer.Network.DownloadFile(My.Resources.Source, Application.StartupPath & "\UpdateInfo.txt")


        Dim 全部内容 As String = File.ReadAllText(Application.StartupPath & "\UpdateInfo.txt")
        '获取更新日志
        Change = "- " & 提取中间文本(全部内容, "Ry更新日志开始", "Ry更新日志结束").Replace("(crlf)", vbCrLf & "- ")
        '获取更新链接
        Link = 提取中间文本(全部内容, "Ry更新链接开始", "Ry更新链接结束").Replace(";", "&")
        '获取新版本号
        Ver = 提取中间文本(全部内容, "Ry爬取版本号开始", "Ry爬取版本号结束")


        If File.Exists(Application.StartupPath & "\UpdateInfo.txt") Then File.Delete(Application.StartupPath & "\UpdateInfo.txt")

        If Command().Replace("-one", "") = Ver Then
            If InStr(Command, "-one") = 0 Then
                MsgBox("暂无更新")
            End If

            End
        Else
            Me.Hide()

            Form1.TextBox1.Text = Change
            Form1.Label2.Text = Command().Replace("-one", "")
            Form1.Label4.Text = Ver
            Form1.Label1.Tag = Link
            Form1.ShowDialog()
        End If
    End Sub

    Function 提取中间文本(ByVal 源文本 As String, ByVal 前导文本 As String, 结束文本 As String) As String
        Dim 起始位置 = InStr(源文本, 前导文本) + Len(前导文本) - 1
        Dim 目标文本长度 = InStr(源文本, 结束文本) - 起始位置 - 1
        提取中间文本 = 源文本.Substring(起始位置, 目标文本长度)
    End Function
End Class