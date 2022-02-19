Imports HtmlAgilityPack

Public Class Main
    Dim Prog As Boolean

    Dim Change As String
    Dim Ver As String
    Dim Link As String
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Prog = False

        CheckForIllegalCrossThreadCalls = False

        Dim 更新日志线程 As New Threading.Thread(AddressOf 更新日志)
        更新日志线程.Start()

        Dim 检查更新线程 As New Threading.Thread(AddressOf 检查更新)
        检查更新线程.Start()
    End Sub
    Private Sub 检查更新()
        Do Until Prog = True
            Threading.Thread.Sleep(50)
            Application.DoEvents()
        Loop

        If Command() = Ver Then
            MsgBox("暂无更新")
            End
        Else
            Me.Hide()

            Form1.TextBox1.Text = Change
            Form1.Label2.Text = Command()
            Form1.Label4.Text = Ver
            Form1.Label1.Tag = Link
            Form1.ShowDialog()
        End If

    End Sub
    Private Sub 更新日志()
        Change = GetChange()
        Ver = GetVer()
        Link = GetLink()

        Prog = True
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub


#Region "获取版本信息"
    Function GetChange()
        Dim wc As New HtmlWeb
        Dim doc As HtmlDocument = wc.Load("https://gitee.com/QianTime/software-update/blob/master/UpdateInfo.txt")
        Dim rootNode As HtmlNode = doc.DocumentNode
        Dim xPathStr As String = "/"
        Dim a As String

        Dim Test As HtmlNodeCollection = rootNode.SelectNodes(xPathStr)
        For Each Node As HtmlNode In Test
            a = Node.InnerHtml
        Next

#Disable Warning BC42104 ' 在为变量赋值之前，变量已被使用
        Dim StartNum = InStr(a, "Ry更新日志开始") + 7
        Dim StrLen = InStr(a, "Ry更新日志结束") - StartNum - 1

        GetChange = " - " & a.Substring(StartNum, StrLen).Replace("(crlf)", vbCrLf & " - ")
#Enable Warning BC42104 ' 在为变量赋值之前，变量已被使用
    End Function

    Function GetVer()
        Dim wc As New HtmlWeb
        Dim doc As HtmlDocument = wc.Load("https://gitee.com/QianTime/software-update/blob/master/UpdateInfo.txt")
        Dim rootNode As HtmlNode = doc.DocumentNode
        Dim xPathStr As String = "/"
        Dim a As String

        Dim Test As HtmlNodeCollection = rootNode.SelectNodes(xPathStr)
        For Each Node As HtmlNode In Test
            a = Node.InnerHtml
        Next

#Disable Warning BC42104 ' 在为变量赋值之前，变量已被使用
        Dim StartNum = InStr(a, "Ry爬取版本号开始") + 8
        Dim StrLen = InStr(a, "Ry爬取版本号结束") - StartNum - 1
        GetVer = a.Substring(StartNum, StrLen)
#Enable Warning BC42104 ' 在为变量赋值之前，变量已被使用
    End Function

    Function GetLink()
        Dim wc As New HtmlWeb
        Dim doc As HtmlDocument = wc.Load("https://gitee.com/QianTime/software-update/blob/master/UpdateInfo.txt")
        Dim rootNode As HtmlNode = doc.DocumentNode
        Dim xPathStr As String = "/"
        Dim a As String

        Dim Test As HtmlNodeCollection = rootNode.SelectNodes(xPathStr)
        For Each Node As HtmlNode In Test
            a = Node.InnerHtml
        Next

#Disable Warning BC42104 ' 在为变量赋值之前，变量已被使用
        Dim StartNum = InStr(a, "Ry更新链接开始") + 7
        Dim StrLen = InStr(a, "Ry更新链接结束") - StartNum - 1
        GetLink = a.Substring(StartNum, StrLen).Replace(";", "&")
#Enable Warning BC42104 ' 在为变量赋值之前，变量已被使用

    End Function

#End Region
End Class