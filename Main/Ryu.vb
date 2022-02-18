Imports DSAPI.神扩展
Imports System.IO

Public Class Ryu
    Dim CompleteDownJson As Boolean
    Private Sub Ryu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim TempFile = Split("EN1,LDN1,CN1,EN,LDN,CN,Firm", ",")
        For Each DeleteTemp In TempFile
            If File.Exists(Application.StartupPath & "\" & DeleteTemp & ".json") Then
                File.Delete(Application.StartupPath & "\" & DeleteTemp & ".json")
            End If
        Next
        If File.Exists(Application.StartupPath & "\Ryu.7z") Then
            File.Delete(Application.StartupPath & "\Ryu.7z")
        End If

        Panel1.Location = New Point(0, 0)
        '下载Json
        CheckForIllegalCrossThreadCalls = False
        Dim DownJson As New Threading.Thread(AddressOf JsonDown)
        DownJson.Start()
        Dim CheckDownComplete As New Threading.Thread(AddressOf Downc)
        CheckDownComplete.Start()
    End Sub
    '指示下载json完成
    Private Sub Downc()
        Do Until CompleteDownJson = True
            Application.DoEvents()
            Threading.Thread.Sleep(100)
        Loop

        Panel1.Location = New Point(547, 413)
    End Sub
    Private Sub JsonDown()
        CompleteDownJson = False

        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxMainlineMirror/?json", Application.StartupPath & "\EN1.json")
        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxCNBuilds/?json", Application.StartupPath & "\CN1.json")
        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxLDNMirror/?json", Application.StartupPath & "\LDN1.json")

        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/NSFirmwareMirror/?json", Application.StartupPath & "\Firm.json")

        Dim WriteEN = File.CreateText(Application.StartupPath & "\EN.json")
        Dim AllNameEN = 读取文本文件(Application.StartupPath & "\EN1.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(AllNameEN)
        For Each a In AllNameEN
            If Microsoft.VisualBasic.Right(a, 3) = "zip" Then
                WriteEN.WriteLine(a)
            End If
        Next
        WriteEN.Close()

        Dim WriteCN = File.CreateText(Application.StartupPath & "\CN.json")
        Dim AllNameCN = 读取文本文件(Application.StartupPath & "\CN1.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(AllNameCN)

        For Each a In AllNameCN
            If Microsoft.VisualBasic.Right(a, 3) = "zip" Then
                WriteCN.WriteLine(a)
            End If
        Next
        WriteCN.Close()

        Dim WriteLDN = File.CreateText(Application.StartupPath & "\LDN.json")
        Dim AllNameLDN = 读取文本文件(Application.StartupPath & "\LDN1.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(AllNameLDN)
        For Each a In AllNameLDN
            If Microsoft.VisualBasic.Right(a, 3) = "zip" Then
                WriteLDN.WriteLine(a)
            End If
        Next
        WriteLDN.Close()

        CompleteDownJson = True
    End Sub

    Dim RyuLink As String '
    Dim RyuVer As String
    Dim RyuMCL As String '= EN CN LDN

    Dim FirmLink As String
    Dim FirmVer As String

    Dim prod_key As String
    Dim InstallFolder As String
    'Next（赋值）
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click, PictureBox6.Click
        If MaterialRadioButton1.Checked = True Then
            Dim a = File.ReadLines(Application.StartupPath & "\EN.json")
            RyuLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxMainlineMirror/" & a(ComboBox1.SelectedIndex)
            RyuVer = ComboBox1.SelectedItem.ToString
            RyuMCL = "EN"
        ElseIf MaterialRadioButton2.Checked = True Then
            Dim a = File.ReadLines(Application.StartupPath & "\CN.json")
            RyuLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxCNBuilds/" & a(ComboBox1.SelectedIndex)
            RyuVer = ComboBox1.SelectedItem.ToString
            RyuMCL = "CN"
        ElseIf MaterialRadioButton3.Checked = True Then
            Dim a = File.ReadLines(Application.StartupPath & "\LDN.json")
            RyuLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxLDNMirror/" & a(ComboBox1.SelectedIndex)
            RyuVer = ComboBox1.SelectedItem.ToString
            RyuMCL = "LDN"
        End If

        If RyuLink <> "" Then
            Dim AllFirmVer = 读取文本文件(Application.StartupPath & "\Firm.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllFirmVer)
            FirmLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/NSFirmwareMirror/" & AllFirmVer(0)
            FirmVer = AllFirmVer(0).Replace("Firmware_", "").Replace(".zip", "")

            TabControl1.SelectedIndex = 1
        End If
    End Sub

    'Back(重启软件)
    Private Sub ResetAppOrCloseWindow(sender As Object, e As EventArgs) Handles Button5.Click, PictureBox4.Click, MyBase.FormClosing
        Shell(Application.StartupPath & "\Bin\ResetApp.exe", Style:=AppWinStyle.Hide)
    End Sub

#Region "加载版本列表"
    Private Sub MaterialRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialRadioButton1.CheckedChanged
        ComboBox1.Items.Clear()
        For Each a In File.ReadLines(Application.StartupPath & "\EN.json")
            ComboBox1.Items.Add(a.Replace("ryujinx-", "").Replace("-win_x64.zip", ""))
        Next

        ComboBox1.SelectedIndex = 0
        ComboBox1.Location = New Point(409, 150)
    End Sub
    Private Sub MaterialRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialRadioButton2.CheckedChanged
        ComboBox1.Items.Clear()
        For Each a In File.ReadLines(Application.StartupPath & "\CN.json")
            ComboBox1.Items.Add(a.Replace("ryujinx-", "").Replace("-win_x64.zip", ""))
        Next
        ComboBox1.SelectedIndex = 0
        ComboBox1.Location = New Point(409, 217)
    End Sub
    Private Sub MaterialRadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialRadioButton3.CheckedChanged
        ComboBox1.Items.Clear()
        For Each a In File.ReadLines(Application.StartupPath & "\LDN.json")
            ComboBox1.Items.Add(a.Replace("ryujinx-", "").Replace("-win_x64.zip", ""))
        Next
        ComboBox1.SelectedIndex = 0
        ComboBox1.Location = New Point(409, 285)
    End Sub
#End Region

#Region "Label3 - Ryujinx官网"
    Private Sub Label3_MouseEnter(sender As Object, e As EventArgs) Handles Label3.MouseEnter
        Label3.ForeColor = Color.White
        Label3.Font = New Font(Label3.Font.Name, Label3.Font.Size, style:=FontStyle.Underline)
    End Sub

    Private Sub Label3_MouseLeave(sender As Object, e As EventArgs) Handles Label3.MouseLeave
        Label3.ForeColor = Color.FromArgb(47, 143, 240)
        Label3.Font = New Font(Label3.Font.Name, Label3.Font.Size, style:=FontStyle.Regular)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Shell("explorer https://ryujinx.org/")
    End Sub
#End Region

#Region "版本选择的按钮"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MaterialRadioButton1.Checked = True
        Label1.Text = My.Resources.官方版
        ComboBox1.Location = New Point(409, 150)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MaterialRadioButton2.Checked = True
        Label1.Text = My.Resources.汉化版
        ComboBox1.Location = New Point(409, 217)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MaterialRadioButton3.Checked = True
        Label1.Text = My.Resources.联机版
        ComboBox1.Location = New Point(409, 285)
    End Sub
#End Region

    '-------------------------------------------   密钥选择TabPage2
    '选择密钥
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click, PictureBox7.Click
        Label6.Focus()
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
    '打开Ryujinx指南
    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Try
            Process.Start("https://github.com/Ryujinx/Ryujinx/wiki/Ryujinx-Setup-&-Configuration-Guide#initial-setup---placement-of-prodkeys")
        Catch ex As Exception '用户没有默认浏览器
            Shell("explorer https://github.com/Ryujinx/Ryujinx/wiki/Ryujinx-Setup-&-Configuration-Guide#initial-setup---placement-of-prodkeys")
        End Try
    End Sub
    '下一步
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click, PictureBox8.Click
        Label6.Focus()

        If PictureBox7.Visible = True Then
            TabControl1.SelectedIndex = 2
        End If
    End Sub
    '选择安装文件夹
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click， PictureBox10.Click
        Label13.Focus() '防止出现内侧线条
        Dim SelectYuzuInst = New FolderBrowserDialog With {
            .Description = "我们将会把 Ryujinx 安装到此文件夹"
        }
        If SelectYuzuInst.ShowDialog = 1 Then
            If Not Microsoft.VisualBasic.Right(SelectYuzuInst.SelectedPath, 1) = "\" Then
                Label6.Tag = SelectYuzuInst.SelectedPath

                '告知用户完成
                PictureBox10.Visible = True '显示Done.png
                Button10.TextAlign = ContentAlignment.MiddleLeft
                Button10.Text = "          点击选择安装文件夹"

                Label19.Text = "我们将会把 Ryujinx 安装到此文件夹 : "
                Label20.Text = SelectYuzuInst.SelectedPath
                If Microsoft.VisualBasic.Right(SelectYuzuInst.SelectedPath, 1) <> "\" Then
                    Label20.Text = SelectYuzuInst.SelectedPath & "\"
                    InstallFolder = SelectYuzuInst.SelectedPath & "\"
                End If
                Label20.Visible = True

                ToolTip1.SetToolTip(Me.Label20, SelectYuzuInst.SelectedPath) '设置路径气泡
            Else
                ShowMessage("选择的路径，不能是根目录")
            End If
        End If
    End Sub

    Private Sub ShowMessage(ByVal Text As String)
        ShowMessageBox.Label1.Text = Text
        ShowMessageBox.ShowDialog()
    End Sub
    '打开用户所选定文件夹
    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Process.Start(Label20.Text)
    End Sub
    '下一步
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click, PictureBox11.Click
        'Dim CreateLog = File.CreateText(Application.StartupPath & "\Ryu.log")
        'CreateLog.WriteLine("RyuLink：" & RyuLink & vbCrLf)
        'CreateLog.WriteLine("RyuVer：" & RyuVer & vbCrLf)
        'CreateLog.WriteLine("RyuMCL：" & RyuMCL & vbCrLf)
        'CreateLog.WriteLine("FirmLink：" & FirmLink & vbCrLf)
        'CreateLog.WriteLine("FirmVer：" & FirmVer & vbCrLf)
        'CreateLog.WriteLine("key：" & prod_key & vbCrLf)
        'CreateLog.WriteLine("InstFolder：" & InstallFolder & vbCrLf)
        'CreateLog.Close()

        Me.Hide()

        '下载Ryu.7z
        Dim DownRyujinx = New Process
        DownRyujinx.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownRyujinx.StartInfo.Arguments = "(URL)" & RyuLink & "(SavePath)" & Application.StartupPath & "(SaveName)Ryu.7z(Job)Ryujinx(x)" & Me.Left & "(y)" & Me.Top & "(End)"
        DownRyujinx.Start()
        DownRyujinx.WaitForExit()
        '下载Firm.7z
        Dim DownFirm = New Process
        DownFirm.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownFirm.StartInfo.Arguments = "(URL)" & FirmLink & "(SavePath)" & Application.StartupPath & "(SaveName)Firm.7z(Job)Firmware(x)" & Me.Left & "(y)" & Me.Top & "(End)"
        DownFirm.Start()
        DownFirm.WaitForExit()

        Me.Show()
        TabControl1.SelectedIndex = 3

        Dim InstallFile As New Threading.Thread(AddressOf InstallRyu)
        InstallFile.Start()
    End Sub

    Private Sub InstallRyu()
        Dim JieRyu = New Process
        JieRyu.StartInfo.FileName = Application.StartupPath & "\Bin\7z.exe"
        JieRyu.StartInfo.Arguments = "x """ & Application.StartupPath & "\Ryu.7z"" -o""" & InstallFolder & """"
        JieRyu.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        JieRyu.Start()
        JieRyu.WaitForExit()

        My.Computer.FileSystem.RenameDirectory(InstallFolder & "publish", "Ryujinx")

        If Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\bis\system\Contents\registered\") Then
            For Each SF In My.Computer.FileSystem.GetFiles(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\bis\system\Contents\registered\")
                File.Delete(SF)
            Next

            For Each SD In My.Computer.FileSystem.GetDirectories(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\bis\system\Contents\registered\")
                Directory.Delete(SD, True)
            Next
        End If

        Dim JieFirm = New Process
        JieFirm.StartInfo.FileName = Application.StartupPath & "\Bin\7z.exe"
        JieFirm.StartInfo.Arguments = "x """ & Application.StartupPath & "\Firm.7z"" -o""" & System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\bis\system\Contents\registered\" & """"
        JieFirm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        JieFirm.Start()
        JieFirm.WaitForExit()

        InstallFirmware(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\bis\system\Contents\registered\")
        If Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system") = False Then
            Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system")
        End If
        If File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system\prod.keys") Then
            File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system\prod.keys")
        End If

        File.Copy(prod_key, System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system\prod.keys")

        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        OpenRyu.SetValue("Firmver", FirmVer)
        OpenRyu.SetValue("InstallFolder", InstallFolder & "Ryujinx")
        OpenRyu.SetValue("Version", RyuVer)
        OpenRyu.SetValue("MCL", RyuMCL)

        ShowMessage("安装已完成，重启程序生效")

        Shell(Application.StartupPath & "\Bin\ResetApp.exe", Style:=AppWinStyle.Hide)
    End Sub

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
End Class