Imports DSAPI.神扩展
Imports System.IO
Imports DSAPI
Imports System.Runtime.InteropServices
Imports MaterialSkin
Imports Transitions
Public Class ReleaseManage
    Dim 窗体是否可以拖动 As Boolean
    Dim CloseButton As Button
    Dim MinButton As Button

    Private Const CS_DropSHADOW As Integer = &H20000
    Private Const GCL_STYLE As Integer = (-26)
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function SetClassLong(hwnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetClassLong(hwnd As IntPtr, nIndex As Integer) As Integer
    End Function

    Dim LoadCheckRyu As Boolean
    Private Sub ReleaseManage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width -= 30
        Me.Height -= 30
        Me.Top += 15
        Me.Left += 15

        Dim TopBorder As Control = New Panel With {.Location = New Point(0, 0), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle}
        Dim BottomBorder As Control = New Panel With {.Location = New Point(0, Me.Height - 1), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle}
        Dim LeftBorder As Control = New Panel With {.Location = New Point(0, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle}
        Dim RightBorder As Control = New Panel With {.Location = New Point(Me.Width - 1, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle}
        Me.Controls.Add(TopBorder)
        Me.Controls.Add(BottomBorder)
        Me.Controls.Add(LeftBorder)
        Me.Controls.Add(RightBorder)

        CloseButton = New Button With {.Text = "x", .Size = New Drawing.Size(30, 30), .Location = New Point(Me.Width - 42, -3), .Font = New Font("微软雅黑", 10.5), .ForeColor = Color.White, .BackColor = Me.BackColor, .FlatStyle = FlatStyle.Flat}
        MinButton = New Button With {.Text = "_", .Size = New Drawing.Size(30, 30), .Location = New Point(Me.Width - 78, -3), .Font = New Font("微软雅黑", 10.5), .ForeColor = Color.White, .BackColor = Me.BackColor, .FlatStyle = FlatStyle.Flat, .TextAlign = ContentAlignment.TopCenter}

        If Me.ControlBox = True Then
            CloseButton.FlatAppearance.BorderSize = 0
            CloseButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(Me.BackColor.R + 20, Me.BackColor.G + 17, Me.BackColor.B + 21)
            Me.Controls.Add(CloseButton)
            AddHandler CloseButton.Click, AddressOf Closewindow
            If Me.MinimizeBox = True Then
                MinButton.FlatAppearance.BorderSize = 0
                MinButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(Me.BackColor.R + 20, Me.BackColor.G + 17, Me.BackColor.B + 21)
                Me.Controls.Add(MinButton)
                AddHandler MinButton.Click, AddressOf Minwindow
            End If
        End If

        窗体是否可以拖动 = True

        窗体是否可以拖动 = True
        Me.Text = "Switch-emu管理器"

        If 窗体是否可以拖动 = True Then
            AddHandler MyBase.MouseDown, AddressOf 鼠标按下
            AddHandler MyBase.MouseMove, AddressOf 鼠标移动
            AddHandler MyBase.MouseUp, AddressOf 鼠标弹起
        End If

        CheckForIllegalCrossThreadCalls = False

        Dim 启动透明线程 As New Threading.Thread(AddressOf 启动透明)
        启动透明线程.Start()

        Transition.run(Me, "Width", Me.Width + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(Me, "Height", Me.Height + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(Me, "Top", Me.Top - 15, New Transitions.TransitionType_Deceleration(250))
        Transition.run(Me, "Left", Me.Left - 15, New Transitions.TransitionType_Deceleration(250))
        Transition.run(TopBorder, "Width", TopBorder.Width + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(BottomBorder, "Width", BottomBorder.Width + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(LeftBorder, "Height", LeftBorder.Height + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(RightBorder, "Height", RightBorder.Height + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(BottomBorder, "Top", BottomBorder.Top + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(RightBorder, "Left", RightBorder.Left + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(CloseButton, "Left", CloseButton.Left + 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(MinButton, "Left", MinButton.Left + 30, New Transitions.TransitionType_Deceleration(250))

        SetClassLong(Me.Handle, GCL_STYLE, GetClassLong(Me.Handle, GCL_STYLE) Or CS_DropSHADOW)

        Label1.Font = New Font("Century Gothic", 18)

        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE)

        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)

        If OpenYuzu.GetValue("Insted") = "True" And Directory.Exists(OpenYuzu.GetValue("InstallFolder")) Then
            If OpenYuzu.GetValue("EA") = "True" Then
                Label3.Text = "测试版"
                Button3.Text = "转稳定版"
            Else
                Label3.Text = "稳定版"
                Button3.Text = "转测试版"
            End If

            Label4.Text = OpenYuzu.GetValue("Version")
            Label6.Text = OpenYuzu.GetValue("InstallFolder")
            Label8.Text = OpenYuzu.GetValue("Firmver")

            GroupBox1.Text = "安装状态：已安装"

            CheckForIllegalCrossThreadCalls = False

            Button1.Enabled = False
            Button1.Text = "正在连接"
            Dim CheckUpdate As New Threading.Thread(AddressOf CheckYuzuUpdate)
            CheckUpdate.Start()

            '固件
            Dim FirmUpdate As New Threading.Thread(AddressOf CheckFirmUpdate)
            FirmUpdate.Start()
        Else
            GroupBox1.Text = "安装状态：未安装"
            Panel2.Visible = False
        End If

        'Ryujinx
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        If Directory.Exists(OpenRyu.GetValue("InstallFolder")) Then '是否已安装
            Panel3.Visible = True
            GroupBox2.Text = "安装状态：已安装"

            If OpenRyu.GetValue("MCL") = "EN" Then
                Label15.Text = "官方版"
            ElseIf OpenRyu.GetValue("MCL") = "CN" Then
                Label15.Text = "中文版"
            ElseIf OpenRyu.GetValue("MCL") = "LDN" Then
                Label15.Text = "联机版"
            End If

            Label14.Text = OpenRyu.GetValue("Version")
            Label9.Text = OpenRyu.GetValue("Firmver")
            Label11.Text = OpenRyu.GetValue("InstallFolder")

            Dim CheckRyuUpdate As New Threading.Thread(AddressOf RyuUpdateCheck)
            CheckRyuUpdate.Start()

            Dim RyuFirmCheck As New Threading.Thread(AddressOf RyuFirmCheckThread)
            RyuFirmCheck.Start()
        Else
            Panel3.Visible = False
            GroupBox2.Text = "安装状态：未安装"
        End If

        If Directory.Exists(OpenYuzu.GetValue("InstallFolder")) = False Then
            MaterialTabControl1.SelectedIndex = 1
            If Directory.Exists(OpenRyu.GetValue("InstallFolder")) = False Then
                MaterialTabControl1.SelectedIndex = 0
            End If
        Else
            If Directory.Exists(OpenRyu.GetValue("InstallFolder")) = False Then
                MaterialTabControl1.SelectedIndex = 0
            End If
        End If
    End Sub

#Region "Yuzu"
    Dim CheckedYuzuUpdate = "No"

    '检查更新
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button1.Text = "正在连接"
        Dim CheckUpdate As New Threading.Thread(AddressOf CheckYuzuUpdate)
        CheckUpdate.Start()
    End Sub
    '打开文件夹
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start(Label6.Text)
    End Sub
    Private Sub CheckYuzuUpdate()

        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/YuzuEAMirror/?json", Application.StartupPath & "\EA.json")
        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/YuzuMainlineMirror/?json", Application.StartupPath & "\RB.json")


        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
        Dim OldVer = OpenYuzu.GetValue("Version")

        If OpenYuzu.GetValue("EA") = True Then
            If OldVer = GetNewEAVer() Then
                Button1.Text = "检查更新"
                If CheckedYuzuUpdate = "Yes" Then
                    MsgBox("已是最新版本", 4096, Application.ProductName & " - Message")
                End If
                Button1.Enabled = True
            Else
                If File.Exists(Application.StartupPath & "\EA.json") Then
                    File.Delete(Application.StartupPath & "\EA.json")
                End If
                My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/YuzuEAMirror/?json", Application.StartupPath & "\EA.json")
                If OldVer <> GetNewEAVer() Then
                    YuzuEmuUpdate.Label5.Text = OldVer
                    YuzuEmuUpdate.Label8.Text = GetNewEAVer()
                    YuzuEmuUpdate.Label1.Tag = GetNewEALink()

                    Button1.Text = "检查更新"
                    YuzuEmuUpdate.ShowDialog()
                    Button1.Enabled = True
                End If
            End If
        Else
            If OldVer = GetNewRBVer() Then
                Button1.Text = "检查更新"
                If CheckedYuzuUpdate = "Yes" Then
                    MsgBox("已是最新版本", 4096, Application.ProductName & " - Message")
                End If
                Button1.Enabled = True
            Else
                If File.Exists(Application.StartupPath & "\RB.json") Then
                    File.Delete(Application.StartupPath & "\RB.json")
                End If
                My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/YuzuMainlineMirror/?json", Application.StartupPath & "\RB.json")
                If OldVer <> GetNewRBVer() Then
                    YuzuEmuUpdate.Label5.Text = OldVer
                    YuzuEmuUpdate.Label8.Text = GetNewRBVer()
                    YuzuEmuUpdate.Label1.Tag = GetNewRBLInk()

                    Button1.Text = "检查更新"
                    YuzuEmuUpdate.ShowDialog()
                    Button1.Enabled = True
                End If
            End If
        End If

        If File.Exists(Application.StartupPath & "\EA.json") Then
            File.Delete(Application.StartupPath & "\EA.json")
        End If
        If File.Exists(Application.StartupPath & "\RB.json") Then
            File.Delete(Application.StartupPath & "\RB.json")
        End If

        CheckedYuzuUpdate = "Yes"
    End Sub

    Private Sub CheckFirmUpdate()
        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/NSFirmwareMirror/?json", Application.StartupPath & "\Firm.json")

        If OpenYuzu.GetValue("Firmver") <> GetNewFirmVer() Then
            FirmUpdate.Label5.Text = OpenYuzu.GetValue("FirmVer")
            FirmUpdate.Label8.Text = GetNewFirmVer()
            FirmUpdate.Label1.Tag = GetNewFirmLink()
            FirmUpdate.ShowDialog()
        End If
    End Sub


    '转EA/RB
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button1.Enabled = True Then
            Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
            If OpenYuzu.GetValue("EA") = True Then
                OpenYuzu.SetValue("EA", "False")
            Else
                OpenYuzu.SetValue("EA", "True")
            End If

            MsgBox("转换成功，再次重新检查更新", 4096, Application.ProductName & " - Message")
            Dim ResetTool = New Process
            ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
            ResetTool.StartInfo.Arguments = "manage"
            ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ResetTool.Start()
        End If
    End Sub

#Region "一些不重要的代码"
    Private Sub ReleaseManage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim ResetTool = New Process
        ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
        ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ResetTool.Start()
    End Sub
    Private Sub ShowMessage(ByVal Text As String)
        ShowMessageBox.Label1.Text = Text
        ShowMessageBox.ShowDialog()
    End Sub
    Private Sub MaterialTabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MaterialTabControl1.SelectedIndexChanged
        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE)
    End Sub


    '获取最新信息
    Function GetNewEAVer()
        Dim EAVer = 读取文本文件(Application.StartupPath & "\EA.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(EAVer)
        GetNewEAVer = EAVer(0).Replace("Windows-Yuzu-EA-", "").Replace(".7z", "")
    End Function
    Function GetNewRBVer()
        Dim RBVer = 读取文本文件(Application.StartupPath & "\RB.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(RBVer)
        GetNewRBVer = RBVer(0).Replace("yuzu-windows-msvc-", "").Replace(".7z", "")
    End Function
    Function GetNewEALink()
        Dim EAVer = 读取文本文件(Application.StartupPath & "\EA.json").提取所有中间文本("url"":""", """,")
        Array.Reverse(EAVer)
        GetNewEALink = EAVer(0).Replace("\/", "/")
    End Function
    Function GetNewRBLInk()
        Dim RBVer = 读取文本文件(Application.StartupPath & "\RB.json").提取所有中间文本("url"":""", """,")
        Array.Reverse(RBVer)
        GetNewRBLInk = RBVer(0).Replace("\/", "/")
    End Function
    Function GetNewFirmVer()
        Dim FirmVer = 读取文本文件(Application.StartupPath & "\Firm.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(FirmVer)
        GetNewFirmVer = FirmVer(0).Replace("Firmware_", "").Replace(".zip", "")
    End Function
    Function GetNewFirmLink()
        Dim FirmLink = 读取文本文件(Application.StartupPath & "\Firm.json").提取所有中间文本("url"":""", """,")
        Array.Reverse(FirmLink)
        GetNewFirmLink = FirmLink(0).Replace("\/", "/")
    End Function

    '设置yuzu文件夹
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button1.Enabled = True Then
            SetYuzuFolder.ShowDialog()
        End If
    End Sub
    '创建快捷方式
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim ShortCut1 = CreateObject("WScript.Shell").CreateShortcut(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Yuzu.lnk")

        With ShortCut1
            .TargetPath = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).GetValue("InstallFolder") & "\yuzu.exe"  '可执行文件路径
            .Save
        End With

        Const SHCNE_ASSOCCHANGED As Integer = &H8000000
        Const HCNF_FLUSH As Integer = &H1000
        SHChangeNotify(SHCNE_ASSOCCHANGED, HCNF_FLUSH, IntPtr.Zero, IntPtr.Zero)
    End Sub
    '更改密钥
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim UpdateKey = New OpenFileDialog With {
            .Filter = "密钥文件|prod.keys"
        }
        If UpdateKey.ShowDialog = 1 Then
            File.Copy(UpdateKey.FileName, My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True).GetValue("InstallFolder") & "\user\keys\prod.keys", True)
            MsgBox("成功"， 4096)
        End If
    End Sub

#End Region

#End Region


#Region "Ryujinx"
    Dim NewRyuVer
    'CRyu
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Button10.Enabled = True Then
            CRyu.ShowDialog()
        End If
    End Sub
    '龙神更新
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim TempFile = Split("EN1,LDN1,CN1,EN,LDN,CN,Firm", ",")
        For Each DeleteTemp In TempFile
            If File.Exists(Application.StartupPath & "\" & DeleteTemp & ".json") Then
                File.Delete(Application.StartupPath & "\" & DeleteTemp & ".json")
            End If
        Next
        RyuUpdateCheck()
    End Sub
    Private Sub RyuUpdateCheck()

        Button10.Enabled = False
        Button10.Text = "正在连接"
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        If OpenRyu.GetValue("MCL") = "EN" Then
            My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxMainlineMirror/?json", Application.StartupPath & "\EN1.json")

            Dim AllNameEN = 读取文本文件(Application.StartupPath & "\EN1.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllNameEN)
            NewRyuVer = AllNameEN(0).Replace("ryujinx-", "").Replace("-win_x64.zip", "")
        ElseIf OpenRyu.GetValue("MCL") = "CN" Then
            My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxCNBuilds/?json", Application.StartupPath & "\CN1.json")

            Dim AllNameEN = 读取文本文件(Application.StartupPath & "\CN1.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllNameEN)
            NewRyuVer = AllNameEN(0).Replace("ryujinx-", "").Replace("-win_x64.zip", "")
        ElseIf OpenRyu.GetValue("MCL") = "LDN" Then
            My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxLDNMirror/?json", Application.StartupPath & "\LDN1.json")

            Dim AllNameEN = 读取文本文件(Application.StartupPath & "\LDN1.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllNameEN)
            NewRyuVer = AllNameEN(0).Replace("ryujinx-", "").Replace("-win_x64.zip", "")
        End If



        If OpenRyu.GetValue("Version") <> NewRyuVer Then
            RyuUpdate.Label8.Text = NewRyuVer
            RyuUpdate.ShowDialog()
            Me.Button10.Enabled = True
            Me.Button10.Text = "检查更新"
        Else
            Button10.Enabled = True
            Button10.Text = "检查更新"
            If LoadCheckRyu = True Then
                MsgBox("已是最新版本")
            End If
            LoadCheckRyu = True
        End If


    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Process.Start(My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).GetValue("InstallFolder"))
    End Sub
    '创建快捷方式
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Dim ShortCut1 = CreateObject("WScript.Shell").CreateShortcut(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Ryujinx.lnk")

        With ShortCut1
            .TargetPath = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True).GetValue("InstallFolder") & "\Ryujinx.exe"
            .Save
        End With

        '刷新
        Const SHCNE_ASSOCCHANGED As Integer = &H8000000
        Const HCNF_FLUSH As Integer = &H1000
        SHChangeNotify(SHCNE_ASSOCCHANGED, HCNF_FLUSH, IntPtr.Zero, IntPtr.Zero)
    End Sub
    <System.Runtime.InteropServices.DllImport("shell32.dll")>
    Private Shared Sub SHChangeNotify(eventId As Integer, flags As Integer, item1 As IntPtr, item2 As IntPtr)
    End Sub
    '密钥
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim UpdateKey = New OpenFileDialog With {
             .Filter = "密钥文件|prod.keys"
        }
        If UpdateKey.ShowDialog = 1 Then
            If File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system\prod.keys") Then
                File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system\prod.keys")
            End If

            File.Copy(UpdateKey.FileName, System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx\system\prod.keys")
            MsgBox("成功"， 4096)
        End If
    End Sub
    Private Sub RyuFirmCheckThread()
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)

        My.Computer.Network.DownloadFile("https://" & My.Resources.MirrorSource & "/ns_emu_helper/NSFirmwareMirror/?json", Application.StartupPath & "\Firm1.json")

        Dim FirmVer = 读取文本文件(Application.StartupPath & "\Firm1.json").提取所有中间文本("name"":""", """,")
        Array.Reverse(FirmVer)
        Dim GetNewFirmVer = FirmVer(0).Replace("Firmware_", "").Replace(".zip", "")

        If OpenRyu.GetValue("Firmver") <> GetNewFirmVer Then
            RyuFirmUpdate.Label5.Text = OpenRyu.GetValue("Firmver")
            RyuFirmUpdate.Label8.Text = GetNewFirmVer
            RyuFirmUpdate.Label1.Tag = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/NSFirmwareMirror/" & FirmVer(0)
            RyuFirmUpdate.ShowDialog()

        End If
    End Sub
    '设置Ryujinx路径等
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        SetRyuFolder.ShowDialog()
    End Sub
    '下载mod
    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        开网页("https://yuzu-emu.org/wiki/switch-mods/")
    End Sub
#End Region
    Private Sub 开网页(ByVal URL As String)
        Shell("explorer """ & URL & """")
    End Sub

#Region "整个窗体都可以拖动"
    Dim 是否按下 As Boolean
    Dim 鼠标在窗体中的X As Integer
    Dim 鼠标在窗体中的Y As Integer
    Private Sub 鼠标按下(sender As Object, e As MouseEventArgs) Handles MaterialTabSelector1.MouseDown
        鼠标在窗体中的X = e.X
        鼠标在窗体中的Y = e.Y
        是否按下 = True
    End Sub
    Private Sub 鼠标移动(sender As Object, e As MouseEventArgs)
        If 是否按下 = True Then
            Me.Location = New Point(Cursor.Position.X - 鼠标在窗体中的X, Cursor.Position.Y - 鼠标在窗体中的Y)
        End If
    End Sub
    Private Sub 鼠标移动2(sender As Object, e As MouseEventArgs) Handles MaterialTabSelector1.MouseMove
        If 是否按下 = True Then
            Me.Location = New Point(Cursor.Position.X - 鼠标在窗体中的X - 32, Cursor.Position.Y - 鼠标在窗体中的Y - 31)
        End If
    End Sub

    Private Sub 鼠标弹起(sender As Object, e As MouseEventArgs) Handles MaterialTabSelector1.MouseUp
        是否按下 = False
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Dim 处理位置 As New Threading.Thread(AddressOf 位置)
        Dim 处理透明度 As New Threading.Thread(AddressOf 透明)
        处理透明度.Start()
        处理位置.Start()
    End Sub

#Region "一些按钮的功能"
    Private Sub 启动透明()
        For i = 1 To 25
            Threading.Thread.Sleep(10)
            Me.Opacity += (i / 100) * 4
            Application.DoEvents()
        Next
    End Sub
    Private Sub 位置()
        Transition.run(Me, "Top", Me.Top + 15, New Transitions.TransitionType_Deceleration(250))
        Transition.run(Me, "Left", Me.Left + 15, New Transitions.TransitionType_Deceleration(250))
        Transition.run(Me, "Height", Me.Height - 30, New Transitions.TransitionType_Deceleration(250))
        Transition.run(Me, "Width", Me.Width - 30, New Transitions.TransitionType_Deceleration(250))
    End Sub
    Private Sub Closewindow(sender As Object, e As EventArgs)
        CloseButton.Visible = False
        MinButton.Visible = False

        Dim 处理位置 As New Threading.Thread(AddressOf 位置)
        Dim 处理透明度 As New Threading.Thread(AddressOf 透明)
        处理透明度.Start()
        处理位置.Start()
    End Sub
    Private Sub 透明()
        For i = 1 To 25
            Threading.Thread.Sleep(10)
            Me.Opacity -= (i / 100) * 4
            Application.DoEvents()
        Next

        Dim ResetTool = New Process
        ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
        ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ResetTool.Start()
    End Sub
    Private Sub Minwindow(sender As Object, e As EventArgs)
        Dim 最小化透明线程 As New Threading.Thread(AddressOf 最小化透明)
        最小化透明线程.Start()
    End Sub

    Private Sub 最小化透明()
        For i = 1 To 5
            Threading.Thread.Sleep(10)
            Me.Opacity -= 0.2
        Next
        Me.WindowState = FormWindowState.Minimized
        Me.Opacity = 1
    End Sub
#End Region
#End Region
End Class