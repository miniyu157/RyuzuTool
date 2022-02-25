Imports System.IO
Imports DSAPI.神扩展
Imports System.Net

Public Class Form1
    Dim LoadVerlist '防止检测那些Change Items是空的
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        '
        '
        '------------------------------------ Qianshiguang° Triste All Rights Reserved ----------------------------------------------------------------
        '
        '
        '            ██████╗ ██╗   ██╗██╗   ██╗███████╗██╗   ██╗    ████████╗ ██████╗  ██████╗ ██╗     
        '            ██╔══██╗╚██╗ ██╔╝██║   ██║╚══███╔╝██║   ██║    ╚══██╔══╝██╔═══██╗██╔═══██╗██║     
        '            ██████╔╝ ╚████╔╝ ██║   ██║  ███╔╝ ██║   ██║       ██║   ██║   ██║██║   ██║██║     
        '            ██╔══██╗  ╚██╔╝  ██║   ██║ ███╔╝  ██║   ██║       ██║   ██║   ██║██║   ██║██║     
        '            ██║  ██║   ██║   ╚██████╔╝███████╗╚██████╔╝       ██║   ╚██████╔╝╚██████╔╝███████╗
        '            ╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚══════╝ ╚═════╝        ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
        '                                                                                    
        '
        '---------------------------------------- 浅时光° Triste All Rights Reserved -------------------------------------------------------------------
        '-----------QQ群（按 Ctrl 键并单击可转到链接）：https://qm.qq.com/cgi-bin/qm/qr?k=CYpbVCqv2xdQaRck4IMIuzsZHPYEtN5-&jump_from=webapi---------------
        '
        '



        '---------------------------------   未完成：DownloadManage引用的DSAPI.DLL路径

        Dim TempFile1 = Split("EN1,LDN1,CN1,EN,LDN,CN,Firm", ",")
        For Each DeleteTemp1 In TempFile1
            If File.Exists(Application.StartupPath & "\" & DeleteTemp1 & ".json") Then
                File.Delete(Application.StartupPath & "\" & DeleteTemp1 & ".json")
            End If
        Next

        Label3.Text = Application.ProductVersion
        Label4.Font = New Font("Century Gothic", 24)


        Panel1.Location = New Point(0, 0) '重设蒙层位, 24pt置

#Region "设置 Me And Picturebox的属性 以及删除临时文件"
        With Me
            .MaximizeBox = False
            .MinimizeBox = False
            .FormBorderStyle = FormBorderStyle.FixedDialog
        End With

        '失落的代码
        'Dim 图片列表 As String() = "QF.png,Install.png,Tool.png,Back2.png,Yuzu.png,Next.png,Done.png,Next.png,Next.png,Done.png".分割(",")
        '图片列表.遍历(Sub(图片文件名, 序号) If 存在文件(图片文件名) Then Me.找到控件(Of PictureBox)($"PictureBox{序号 + 1}").ImageLocation = 图片文件名.补全当前路径)

        'Dim 要删除的文件列表 As String() = "EA.json,EALinks.txt,EAVers.txt,RB.json,RBLinks.txt,RBVers.txt,vc_redist.x64.exe".分割(",")
        '要删除的文件列表.遍历(Sub(文件名) If 存在文件(文件名) Then IO.File.Delete(文件名.补全当前路径))
        PictureBox1.Image = My.Resources.QF
        PictureBox2.Image = My.Resources.Install
        PictureBox3.Image = My.Resources.Tool
        PictureBox4.Image = My.Resources.Back2
        PictureBox5.Image = My.Resources.Yuzu
        PictureBox6.Image = My.Resources._Next
        PictureBox7.Image = My.Resources.Done
        PictureBox8.Image = My.Resources._Next
        PictureBox9.Image = My.Resources._Next
        PictureBox10.Image = My.Resources.Done
        PictureBox11.Image = My.Resources._Next
        PictureBox12.Image = My.Resources.Yuzu
        DeleteTemp()
#End Region

#Region "注册表"

        Dim key1 = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True)
        '不存在Tool则创建
        If key1.OpenSubKey("RyuzuTool") Is Nothing Then
            key1.CreateSubKey("RyuzuTool")

            key1.OpenSubKey("RyuzuTool", True).CreateSubKey("Yuzu")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("InstallFolder", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("Version", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("Firmver", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("Insted", "None")

            key1.OpenSubKey("RyuzuTool", True).CreateSubKey("Ryujinx")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("InstallFolder", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("Version", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("Firmver", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("Insted", "None")

            '判断Yuzu
        ElseIf key1.OpenSubKey("RyuzuTool").OpenSubKey("Yuzu") Is Nothing Then
            key1.OpenSubKey("RyuzuTool", True).CreateSubKey("Yuzu")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("InstallFolder", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("Version", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("Firmver", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).SetValue("Insted", "None")
            '判断Ryujinx
        ElseIf key1.OpenSubKey("RyuzuTool").OpenSubKey("Ryujinx") Is Nothing Then
            key1.OpenSubKey("RyuzuTool", True).CreateSubKey("Ryujinx")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("InstallFolder", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("Version", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("Firmver", "None")
            key1.OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).SetValue("Insted", "None")
        End If

#End Region
        LoadVerlist = "No" '表示没有下载json，用于下一行代码
        MaterialCheckBox1.CheckState = CheckState.Checked

        Dim i As Integer
        Dim proc As Process()
        If System.Diagnostics.Process.GetProcessesByName("yuzu").Length > 0 Then
            proc = Process.GetProcessesByName("yuzu")
            For i = 0 To proc.Length - 1
                proc(i).Kill()
            Next
        End If


        If Command() = "manage" Then
            ReleaseManage.Show()
            Me.Size = New Size(0, 0)
            Me.FormBorderStyle = FormBorderStyle.None
            Me.ShowInTaskbar = False
            Me.Location = New Point(-100, -100)
        End If

        CheckForIllegalCrossThreadCalls = False
        Button17.Enabled = False
        Dim 检测更新线程 As New Threading.Thread(AddressOf 检测更新)
        检测更新线程.Start()

#Region "公告系统部分"
        CheckForIllegalCrossThreadCalls = False

        Dim 下载An线程 As New Threading.Thread(AddressOf 下载An)
        下载An线程.Start()
    End Sub
    Dim An正文
    Dim An序号
    Private Sub 下载An()
        If File.Exists(Application.StartupPath & "\An.txt") Then File.Delete(Application.StartupPath & "\An.txt")
        Try
            My.Computer.Network.DownloadFile(My.Resources.Source, Application.StartupPath & "\An.txt")
        Catch ex As Exception
            MsgBox("网络链接失败，但这并不影响使用")
        End Try


        Dim 全部内容 As String = File.ReadAllText(Application.StartupPath & "\An.txt")
        '获取正文
        An正文 = 提取中间文本(全部内容, "Ry公告获取开始", "Ry公告获取结束").Replace("(crlf)", vbCrLf)
        '获取序号
        An序号 = 提取中间文本(全部内容, "Ry公告序号获取开始", "Ry公告序号获取结束")

        Dim 本地序号 = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).GetValue("An")

        If 本地序号 <> An序号 Then
            Announcement.TextBox1.Text = An正文
            Announcement.Tag = An序号
            Announcement.Label1.Text = "公告 " & An序号
            Announcement.ShowDialog()
        End If

        If File.Exists(Application.StartupPath & "\An.txt") Then File.Delete(Application.StartupPath & "\An.txt")
    End Sub
    Function 提取中间文本(ByVal 源文本 As String, ByVal 前导文本 As String, 结束文本 As String) As String
        Dim 起始位置 = InStr(源文本, 前导文本) + Len(前导文本) - 1
        Dim 目标文本长度 = InStr(源文本, 结束文本) - 起始位置 - 1
        提取中间文本 = 源文本.Substring(起始位置, 目标文本长度)
    End Function
#End Region

    Private Sub 检测更新()
        Dim 检测更新 As New Process
        检测更新.StartInfo.FileName = Application.StartupPath & "\Update.exe"
        检测更新.StartInfo.Arguments = Application.ProductVersion & "-one"
        检测更新.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        检测更新.Start()
        检测更新.WaitForExit()
        Button17.Enabled = True
    End Sub

#Region "主界面(包括下载json/解析)(TapPage1)"
    '捐赠
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Donation.ShowDialog()
    End Sub
    '启动更新
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Process.Start(Application.StartupPath & "\Update.exe", Application.ProductVersion)
        End
    End Sub
    '关于
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        AboutMe.Show()
        Me.Hide()
    End Sub
    '打开管理界面
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        ReleaseManage.Show()
    End Sub

    '切换到"版本选择"界面(加载版本链接列表RB/EA)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Visible = True '打开蒙层

        TabControl1.SelectedIndex = 1

        '恢复默认
        MaterialCheckBox1.CheckState = CheckState.Checked

        '清除列表
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Loading...")
        ComboBox1.SelectedIndex = 0

        '删除临时文件
        DeleteTemp()

        '下载json
        CheckForIllegalCrossThreadCalls = False
        Dim DownData = New Threading.Thread(AddressOf DownloadJson)
        DownData.Start()
        'DownData.Join()
    End Sub
    '多线程获取配置文件并给默认项(EA)加载版本列表
    Private Sub DownloadJson()
        ComboBox1.Items.Add("Loading...")
        ComboBox1.SelectedIndex = 0
        Try
            '获取EA
            My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/YuzuEAMirror/?json", Application.StartupPath & "\EA.json")
            '获取RB
            My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/YuzuMainlineMirror/?json", Application.StartupPath & "\RB.json")
        Catch ex As Exception
            ShowMessage("网络连接失败，请重试")
            Shell(Application.StartupPath & "\Bin\ResetApp.exe", Style:=AppWinStyle.Hide)
        End Try
        ComboBox1.Items.Clear()


        '提取EA版本
        Dim EAVer = 读取文本文件(Application.StartupPath & "\EA.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(EAVer) '最新的在最上面
        Dim WriteEAVerList = File.CreateText(Application.StartupPath & "\EAVers.txt")
        For Each a In EAVer
            If Microsoft.VisualBasic.Right(a, 3) = ".7z" Then
                WriteEAVerList.WriteLine(a.短文件名(True).提取所有数值(0) * -1)
            End If
        Next
        WriteEAVerList.Close()

        '提取EA链接
        Dim EALink = 读取文本文件(Application.StartupPath & "\EA.json").提取所有中间文本("url"":""", """,")
        Array.Reverse(EALink) '最新的在最上面
        Dim WriteEALinkList = File.CreateText(Application.StartupPath & "\EALinks.txt")
        For Each a In EALink
            WriteEALinkList.WriteLine(a.替换("\/", "/"))
        Next
        WriteEALinkList.Close()

        '提取RB版本
        Dim RBVer = 读取文本文件(Application.StartupPath & "\RB.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(RBVer) '最新的在最上面
        Dim WriteRBVerList = File.CreateText(Application.StartupPath & "\RBVers.txt")
        For Each a In RBVer
            If Microsoft.VisualBasic.Right(a, 3) = ".7z" Then
                WriteRBVerList.WriteLine(a.短文件名(True).提取所有数值(0) * -1)
            End If
        Next
        WriteRBVerList.Close()

        '提取RB链接
        Dim RBLink = 读取文本文件(Application.StartupPath & "\RB.json").提取所有中间文本("url"":""", """,")
        Array.Reverse(RBLink) '最新的在最上面
        Dim WriteRBLinkList = File.CreateText(Application.StartupPath & "\RBLinks.txt")
        For Each a In RBLink
            WriteRBLinkList.WriteLine(a.替换("\/", "/"))
        Next
        WriteRBLinkList.Close()

        LoadVerlist = "Yes" '表示已经加载完成

        '-------------------默认的：EA （此段为加载版本列表）-------------------
        Dim OutputVer = File.ReadLines(Application.StartupPath & "\EAVers.txt")
        For Each Setlist In OutputVer
            ComboBox1.Items.Add(Setlist)
        Next
        ComboBox1.SelectedIndex = 0

        Panel1.Visible = False '关闭加载蒙层
    End Sub
#End Region

#Region "选择Yuzu版本 (TapPage2)"

#Region "复选框的关节炎"
    Private Sub MaterialCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialCheckBox1.CheckedChanged
        If MaterialCheckBox1.CheckState = CheckState.Checked Then
            If LoadVerlist = "Yes" Then
                '加载版本列表(EA)
                ComboBox1.Items.Clear()
                Dim OutputVer = File.ReadLines(Application.StartupPath & "\EAVers.txt")
                For Each Setlist In OutputVer
                    ComboBox1.Items.Add(Setlist)
                Next
                ComboBox1.SelectedIndex = 0
            End If

            MaterialCheckBox2.CheckState = CheckState.Unchecked
        ElseIf MaterialCheckBox1.CheckState = CheckState.Unchecked Then
            MaterialCheckBox2.CheckState = CheckState.Checked
        End If
    End Sub
    Private Sub MaterialCheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialCheckBox2.CheckedChanged
        If MaterialCheckBox2.CheckState = CheckState.Checked Then
            If LoadVerlist = "Yes" Then
                '加载版本列表(RB)
                ComboBox1.Items.Clear()
                Dim OutputVer = File.ReadLines(Application.StartupPath & "\RBVers.txt")
                For Each Setlist In OutputVer
                    ComboBox1.Items.Add(Setlist)
                Next
                ComboBox1.SelectedIndex = 0
            End If

            MaterialCheckBox1.CheckState = CheckState.Unchecked
        ElseIf MaterialCheckBox2.CheckState = CheckState.Unchecked Then
            MaterialCheckBox1.CheckState = CheckState.Checked
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MaterialCheckBox1.CheckState = CheckState.Checked Then
            MaterialCheckBox1.CheckState = CheckState.Unchecked
            MaterialCheckBox2.CheckState = CheckState.Checked
        ElseIf MaterialCheckBox2.CheckState = CheckState.Checked Then
            MaterialCheckBox2.CheckState = CheckState.Unchecked
            MaterialCheckBox1.CheckState = CheckState.Checked
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If MaterialCheckBox1.CheckState = CheckState.Checked Then
            MaterialCheckBox1.CheckState = CheckState.Unchecked
            MaterialCheckBox2.CheckState = CheckState.Checked
        ElseIf MaterialCheckBox2.CheckState = CheckState.Checked Then
            MaterialCheckBox2.CheckState = CheckState.Unchecked
            MaterialCheckBox1.CheckState = CheckState.Checked
        End If
    End Sub
#End Region
    '返回上的图片
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Button3.PerformClick()
    End Sub
    '返回
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ResetTool = New Process
        ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
        ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ResetTool.Start()
    End Sub
    '继续上的图片
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Button4.PerformClick()
    End Sub

    'ComboBox切换时设置Label9.Tag为链接(用于后来在线下载)
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If LoadVerlist = "Yes" Then
            If MaterialCheckBox1.CheckState = CheckState.Checked Then

                Dim ReadLink = File.ReadLines(Application.StartupPath & "\EALinks.txt")
                Label9.Tag = ReadLink(ComboBox1.SelectedIndex)
            ElseIf MaterialCheckBox2.CheckState = CheckState.Checked Then

                Dim ReadLink = File.ReadLines(Application.StartupPath & "\RBLinks.txt")
                Label9.Tag = ReadLink(ComboBox1.SelectedIndex)
            End If
        End If
    End Sub
    '下一步
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectedIndex = 2
    End Sub

#End Region
    Dim prod_key '密钥文件位置
#Region "选择密钥,包括VC检测(TapPage3)"
    '选择密钥文件 Prod.keys
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Label6.Focus() '防止出现内侧线条

        Dim SelectKey = New OpenFileDialog With {
            .Filter = "密钥文件|prod.keys"
        }
        If SelectKey.ShowDialog = 1 Then
            prod_key = SelectKey.FileName

            '告知用户完成
            PictureBox7.Visible = True '显示Done.png
            Button7.TextAlign = ContentAlignment.MiddleLeft
            Button7.Text = "          点击选择 Prod.keys"
        End If
    End Sub
    '选择密钥的 "√" 点击事件
    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Button7.PerformClick()
    End Sub
    '如何获取 Prod.keys
    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Try
            Process.Start("https://yuzu-emu.org/help/quickstart/")
        Catch ex As Exception '用户没有默认浏览器
            Shell("explorer https://yuzu-emu.org/help/quickstart/")
        End Try
    End Sub
    'Next上的图片
    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Button8.PerformClick()
    End Sub
    'Next（并检查VC）
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If PictureBox7.Visible = True Then '用户选择了Prod.keys 就会显示 PictureBox7
            Dim SystemFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.System)
            If File.Exists(SystemFolder & "\MSVCP140_ATOMIC_WAIT.DLL") And
                File.Exists(SystemFolder & "\msvcp140_atomic_wait.dll") Then
                '有VC，打开文件夹选择界面
                TabControl1.SelectedIndex = 4
            Else
                '无VC， 打开VC界面(并开启下载)
                TabControl1.SelectedIndex = 3

                If File.Exists(Application.StartupPath & "\vc_redist.x64.exe") Then
                    File.Delete(Application.StartupPath & "\vc_redist.x64.exe")
                End If

                CheckForIllegalCrossThreadCalls = False
                Dim DownData = New Threading.Thread(AddressOf DownVC)
                DownData.Start()

                Dim GetProg = New Threading.Thread(AddressOf DownVCProgress)
                GetProg.Start()
            End If
        End If
    End Sub
#End Region

#Region "VC下载、安装(TapPage4)"
    Dim DownVCComplete '指示是否完成下载VC
    Dim GetLineLink = "No" '指示是否已经获取直链
    Dim downloadLink '解析后的直链
    '下载VC++
    Private Sub DownVC()
        Dim http As HttpWebRequest
        http = WebRequest.Create("https://aka.ms/vs/16/release/vc_redist.x64.exe")
        http.Method = "GET"
        http.AllowAutoRedirect = False
        downloadLink = Replace(Filter(Split(http.GetResponse.Headers.ToString(), vbCrLf), "Location")(0), "Location: ", "")
        GetLineLink = "Yes" '已经获取直链
        '-------------------------------
        DownVCComplete = "No" '下载未完成
        My.Computer.Network.DownloadFile(downloadLink, Application.StartupPath & "\vc_redist.x64.exe")
        DownVCComplete = "Yes" '--------------已经完成
        Label15.Text = "100 %"

        Label14.Text = "正在安装 VC++"
        ProgressBar1.Visible = False

        Dim InstallVC = New Process
        InstallVC.StartInfo.FileName = Application.StartupPath & "\vc_redist.x64.exe"
        InstallVC.StartInfo.Arguments = " /install /quiet"
        InstallVC.Start()
        InstallVC.WaitForExit()
        '-----------VC安装完成------------------------
        ProgressBar2.Visible = False
        ProgressBar1.Visible = True
        Label14.Text = "安装完成，请单击""Next"""
    End Sub
    '获取下载VC++的进度
    Private Sub DownVCProgress()
        Label14.Text = "获取链接中..."
        Do Until GetLineLink = "Yes" '若直链未获取，则不断等待500ms
            Threading.Thread.Sleep(500)
        Loop

        Label14.Text = "正在下载 VC++"

        Dim IntLeng = System.Net.WebRequest.Create(downloadLink).GetResponse.Headers.Get("Content-Length")
        Do Until DownVCComplete = "Yes"
            Threading.Thread.Sleep(100) '进度更新速度100ms
            Application.DoEvents()
            Dim fi = New System.IO.FileInfo(Application.StartupPath & "\vc_redist.x64.exe")
            ProgressBar1.Value = Math.Round((fi.Length / IntLeng) * 100)
            Label15.Text = Math.Round((fi.Length / IntLeng) * 100, 2) & " %"
            '00.00Mb / 99.00Mb
            Label16.Text = Math.Round(fi.Length / 1024 / 1024, 2) & "MB / " & Math.Round(IntLeng / 1024 / 1024, 2) & "MB"
        Loop

        Label16.Text = Math.Round(IntLeng / 1024 / 1024, 2) & "MB / " & Math.Round(IntLeng / 1024 / 1024, 2) & "MB"
    End Sub
    '图片上的按钮
    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Button9.PerformClick()
    End Sub
    '"Next"
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Label14.Text = "安装完成，请单击""Next""" Then
            TabControl1.SelectedIndex = 4
        End If
    End Sub
#End Region

#Region "选择安装文件夹并安装(TapPage5)"
    '选择安装文件夹
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Label13.Focus() '防止出现内侧线条
        Dim SelectYuzuInst = New FolderBrowserDialog With {
            .Description = "我们将会把 Yuzu 安装到此文件夹"
        }
        If SelectYuzuInst.ShowDialog = 1 Then
            If Not Microsoft.VisualBasic.Right(SelectYuzuInst.SelectedPath, 1) = "\" Then
                Label6.Tag = SelectYuzuInst.SelectedPath

                '告知用户完成
                PictureBox10.Visible = True '显示Done.png
                Button10.TextAlign = ContentAlignment.MiddleLeft
                Button10.Text = "          点击选择安装文件夹"

                Label19.Text = "我们将会把 Yuzu 安装到此文件夹 : "
                Label20.Text = SelectYuzuInst.SelectedPath
                If Microsoft.VisualBasic.Right(SelectYuzuInst.SelectedPath, 1) <> "\" Then
                    Label20.Text = SelectYuzuInst.SelectedPath & "\"
                End If
                Label20.Visible = True

                ToolTip1.SetToolTip(Me.Label20, SelectYuzuInst.SelectedPath) '设置路径气泡
            Else
                ShowMessage("选择的路径，不能是根目录")
            End If
        End If
    End Sub
    '选择安装文件夹上的图片
    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        Button10.PerformClick()
    End Sub
    '打开选择的文件夹
    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Process.Start(Label20.Text)
    End Sub

    '--------------------Next(安装)---------------------------
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If Label20.Visible = True Then
            TabControl1.SelectedIndex = 5
            '
            '启动下载
            Me.Hide()
            DownloadData()

        End If
    End Sub
    '---------------------------------------------------------
    'Next上的图片
    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        Button11.PerformClick()
    End Sub
#End Region

#Region "在线下载Yuzu(TapPage6)"
    Private Sub DownloadData()
        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/NSFirmwareMirror/?json", Application.StartupPath & "\Firm.json")
        Dim FirmLink = 读取文本文件(Application.StartupPath & "\Firm.json").提取所有中间文本("url"":""", """,")
        Array.Reverse(FirmLink)

        '下载Yuzu
        Dim DownYuzu = New Process
        DownYuzu.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownYuzu.StartInfo.Arguments = "(URL)" & Label9.Tag & "(SavePath)" & Application.StartupPath & "(SaveName)Yuzu.7z(Job)Yuzu(x)" & Me.Left & "(y)" & Me.Top & "(End)"
        DownYuzu.Start()
        DownYuzu.WaitForExit()

        '下载固件
        Dim DownFirm = New Process
        DownFirm.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownFirm.StartInfo.Arguments = "(URL)" & FirmLink(0).Replace("\/", "/") & "(SavePath)" & Application.StartupPath & "(SaveName)Firm.7z(Job)Firmware(x)" & Me.Left & "(y)" & Me.Top & "(End)"
        DownFirm.Start()
        DownFirm.WaitForExit()

        Dim InstFile = New Threading.Thread(AddressOf InstYuzu)
        InstFile.Start()
        Me.Show()
    End Sub
#End Region

#Region "安装文件（TabPage7）"
    Private Sub InstYuzu()
        'Shell(Application.StartupPath & "\Bin\7z.exe x """ & Application.StartupPath & "\Yuzu.7z" & """ -o" & Label20.Text, Style:=AppWinStyle.Hide)
        Dim JieYuzu = New Process
        JieYuzu.StartInfo.FileName = Application.StartupPath & "\Bin\7z.exe"
        JieYuzu.StartInfo.Arguments = "x """ & Application.StartupPath & "\Yuzu.7z"" -o""" & Label20.Text & """"
        JieYuzu.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        JieYuzu.Start()
        JieYuzu.WaitForExit()

        Threading.Thread.Sleep(1000)
        Try
            My.Computer.FileSystem.RenameDirectory(Label20.Text & "yuzu-windows-msvc-early-access", "Yuzu")
        Catch ex As Exception
            My.Computer.FileSystem.RenameDirectory(Label20.Text & "yuzu-windows-msvc", "Yuzu")
        End Try
        Directory.CreateDirectory(Label20.Text & "Yuzu\user")

        Threading.Thread.Sleep(1000)

        '让Yuzu自动补全user
        Dim Loaduser = New Process
        Loaduser.StartInfo.FileName = Label20.Text & "Yuzu\yuzu-cmd.exe"
        Loaduser.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Loaduser.Start()
        Loaduser.WaitForExit()

        Threading.Thread.Sleep(1000)

        File.Copy(prod_key, Label20.Text & "Yuzu\user\keys\prod.keys")

        Directory.CreateDirectory(Label20.Text & "Yuzu\user\nand\system")
        Directory.CreateDirectory(Label20.Text & "Yuzu\user\nand\system\Contents")
        Directory.CreateDirectory(Label20.Text & "Yuzu\user\nand\system\Contents\registered")

        Dim JieFirm = New Process
        JieFirm.StartInfo.FileName = Application.StartupPath & "\Bin\7z.exe"
        JieFirm.StartInfo.Arguments = "x """ & Application.StartupPath & "\Firm.7z"" -o""" & Label20.Text & "Yuzu\user\nand\system\Contents\registered\" & """"
        JieFirm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        JieFirm.Start()
        JieFirm.WaitForExit()

        InstallFirmware（Label20.Text & "Yuzu\user\nand\system\Contents\registered\"）

        Dim FirmLink = 读取文本文件(Application.StartupPath & "\Firm.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(FirmLink)

        Dim FirmVer = FirmLink(0).Replace("Firmware_", "").Replace(".zip", "")

        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
        OpenYuzu.SetValue("Firmver", FirmVer)
        If MaterialCheckBox1.CheckState = CheckState.Checked Then
            OpenYuzu.SetValue("EA", "True")
        Else
            OpenYuzu.SetValue("EA", "False")
        End If
        OpenYuzu.SetValue("Version", ComboBox1.SelectedItem.ToString)
        OpenYuzu.SetValue("Insted", "True")
        OpenYuzu.SetValue("InstallFolder", Label20.Text & "Yuzu")

        ShowMessage("安装已完成，重启程序生效")
        End
    End Sub
#End Region



    '自定义对话框
    Private Sub ShowMessage(ByVal Text As String)
        ShowMessageBox.Label1.Text = Text
        ShowMessageBox.ShowDialog()
    End Sub
    '删除临时文件
    Private Sub DeleteTemp()
        If File.Exists(Application.StartupPath & "\EA.json") Then
            File.Delete(Application.StartupPath & "\EA.json")
        End If
        If File.Exists(Application.StartupPath & "\EALinks.txt") Then
            File.Delete(Application.StartupPath & "\EALinks.txt")
        End If
        If File.Exists(Application.StartupPath & "\EAVers.txt") Then
            File.Delete(Application.StartupPath & "\EAVers.txt")
        End If
        If File.Exists(Application.StartupPath & "\RB.json") Then
            File.Delete(Application.StartupPath & "\RB.json")
        End If
        If File.Exists(Application.StartupPath & "\RBLinks.txt") Then
            File.Delete(Application.StartupPath & "\RBLinks.txt")
        End If
        If File.Exists(Application.StartupPath & "\RBVers.txt") Then
            File.Delete(Application.StartupPath & "\RBVers.txt")
        End If
        If File.Exists(Application.StartupPath & "\Firm.json") Then
            File.Delete(Application.StartupPath & "\Firm.json")
        End If
        If File.Exists(Application.StartupPath & "\FirmLinks.txt") Then
            File.Delete(Application.StartupPath & "\FirmLinks.txt")
        End If
        If File.Exists(Application.StartupPath & "\Firm.7z") Then
            File.Delete(Application.StartupPath & "\Firm.7z")
        End If
        If File.Exists(Application.StartupPath & "\Yuzu.7z") Then
            File.Delete(Application.StartupPath & "\Yuzu.7z")
        End If
        If File.Exists(Application.StartupPath & "\vc_redist.x64.exe") Then
            File.Delete(Application.StartupPath & "\vc_redist.x64.exe")
        End If
        If File.Exists(Application.StartupPath & "\Update.7z") Then
            File.Delete(Application.StartupPath & "\Update.7z")
        End If
        If File.Exists(Application.StartupPath & "\FirmUpdate.7z") Then
            File.Delete(Application.StartupPath & "\FirmUpdate.7z")
        End If
        If File.Exists(Application.StartupPath & "\Ryu.7z") Then
            File.Delete(Application.StartupPath & "\Ryu.7z")
        End If
        If File.Exists(Application.StartupPath & "\Firm1.json") Then
            File.Delete(Application.StartupPath & "\Firm1.json")
        End If
    End Sub
    '关闭事件
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub
    '改装固件
    Private Sub InstallFirmware(Path As String)
        If Microsoft.VisualBasic.Right(Path, 1) <> "\" Then
            Path &= "\"
        End If
        For Each RecnmtInNca In My.Computer.FileSystem.GetFiles(Path)
            If Microsoft.VisualBasic.Right(My.Computer.FileSystem.GetName(RecnmtInNca), Len(".cnmt.nca")) = ".cnmt.nca" Then
                My.Computer.FileSystem.RenameFile(RecnmtInNca, My.Computer.FileSystem.GetName(RecnmtInNca).Replace(".cnmt.nca", ".nca"))
            End If
        Next
        For Each AddDir In My.Computer.FileSystem.GetFiles(Path)
            System.IO.Directory.CreateDirectory(Path & My.Computer.FileSystem.GetName(AddDir) & ".New")
        Next
        For Each CopyFirmFile In My.Computer.FileSystem.GetFiles(Path)
            System.IO.File.Copy(CopyFirmFile, CopyFirmFile & ".New\00")
        Next
        For Each DeleteOldFile In My.Computer.FileSystem.GetFiles(Path)
            System.IO.File.Delete(DeleteOldFile)
        Next
        For Each ReDir In My.Computer.FileSystem.GetDirectories(Path)
            My.Computer.FileSystem.RenameDirectory(ReDir, My.Computer.FileSystem.GetName(ReDir).Replace(".nca.New", ".nca"))
        Next
    End Sub





    'Ryujinx
    Private Sub Button13_Click_1(sender As Object, e As EventArgs) Handles Button13.Click
        Ryu.Show()
        Me.Hide()
    End Sub

    '助手
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Shell("explorer ""http://tieba.baidu.com/p/7712446946""")
    End Sub

    Private Sub Label5_MouseEnter(sender As Object, e As EventArgs) Handles Label5.MouseEnter
        Label5.Font = New Font(Label5.Font.Name, Label5.Font.Size, style:=FontStyle.Underline)
    End Sub

    Private Sub Label5_MouseLeave(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        Label5.Font = New Font(Label5.Font.Name, Label5.Font.Size, style:=FontStyle.Regular)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Shell("explorer ""https://yuzu-emu.org/""")
    End Sub
    '更改QQ头像
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ChangeQQHand.ShowDialog()
    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click
        Dim i As Integer
        Dim proc As Process()
        If System.Diagnostics.Process.GetProcessesByName("7z").Length > 0 Then
            proc = Process.GetProcessesByName("7z")
            For i = 0 To proc.Length - 1
                proc(i).Kill()
            Next
        End If
    End Sub
End Class
