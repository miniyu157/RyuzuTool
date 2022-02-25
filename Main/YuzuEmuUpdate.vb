
Imports System.IO
Public Class YuzuEmuUpdate

    Private Sub EmuUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, Me.Height - 1), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(Me.Width - 1, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})

        Me.Top += 15
        Dim OpT As New Threading.Thread(AddressOf Op)
        OpT.Start()
        Transitions.Transition.run(Me, "Top", Me.Top - 15, New Transitions.TransitionType_Deceleration(250))

        AddHandler MyBase.MouseDown, AddressOf 鼠标按下
        AddHandler MyBase.MouseMove, AddressOf 鼠标移动
        AddHandler MyBase.MouseUp, AddressOf 鼠标弹起
    End Sub
#Region "整个窗体都可以拖动"
    Dim 是否按下 As Boolean
    Dim 鼠标在窗体中的X As Integer
    Dim 鼠标在窗体中的Y As Integer
    Private Sub 鼠标按下(sender As Object, e As MouseEventArgs)
        鼠标在窗体中的X = e.X
        鼠标在窗体中的Y = e.Y
        是否按下 = True
    End Sub
    Private Sub 鼠标移动(sender As Object, e As MouseEventArgs)
        If 是否按下 = True Then
            Me.Location = New Point(Cursor.Position.X - 鼠标在窗体中的X, Cursor.Position.Y - 鼠标在窗体中的Y)
        End If
    End Sub
    Private Sub 鼠标弹起(sender As Object, e As MouseEventArgs)
        是否按下 = False
    End Sub
#End Region
    Private Sub Op()
        Me.Opacity = 0
        For i = 1 To 50
            Threading.Thread.Sleep(10)
            Me.Opacity += (i / 100) * 2
            Application.DoEvents()
        Next
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
        'Update.7z
        Dim DownRyujinx = New Process
        DownRyujinx.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownRyujinx.StartInfo.Arguments = "(URL)" & Label1.Tag & "(SavePath)" & Application.StartupPath & "(SaveName)Update.7z(Job)Yuzu 更新(x)" & Me.Left & "(y)" & Me.Top & "(End)"
        DownRyujinx.Start()
        DownRyujinx.WaitForExit()

        Me.Show()
        Button1.Enabled = False
        Button2.Enabled = False
        ProgressBar1.Visible = True
        Label7.Visible = True

        Dim InstFile As New Threading.Thread(AddressOf C7zAndFile)
        InstFile.Start()
    End Sub

    Private Sub C7zAndFile()
        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)


        Dim JieYuzu = New Process
        JieYuzu.StartInfo.FileName = Application.StartupPath & "\Bin\7z.exe"
        JieYuzu.StartInfo.Arguments = "x """ & Application.StartupPath & "\Update.7z"" -o""" & OpenYuzu.GetValue("InstallFolder") & "\" & """"
        JieYuzu.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        JieYuzu.Start()
        JieYuzu.WaitForExit()

        If OpenYuzu.GetValue("EA") = "True" Then
            My.Computer.FileSystem.RenameDirectory(OpenYuzu.GetValue("InstallFolder") & "\yuzu-windows-msvc-early-access", "Yuzu.New")
        Else
            My.Computer.FileSystem.RenameDirectory(OpenYuzu.GetValue("InstallFolder") & "\yuzu-windows-msvc", "Yuzu.New")
        End If

        '删除所有Old文件
        For Each DeleteOldFile In My.Computer.FileSystem.GetFiles(OpenYuzu.GetValue("InstallFolder") & "\")
            File.Delete(DeleteOldFile)
        Next

        Directory.Delete(OpenYuzu.GetValue("InstallFolder") & "\plugins\", True)

        '复制新的文件
        For Each CopyNewFile In My.Computer.FileSystem.GetFiles(OpenYuzu.GetValue("InstallFolder") & "\Yuzu.New\")
            File.Copy(CopyNewFile, OpenYuzu.GetValue("InstallFolder") & "\" & My.Computer.FileSystem.GetName(CopyNewFile))
        Next

        '复制新的文件夹
        For Each CopyNewDirectory In My.Computer.FileSystem.GetDirectories(OpenYuzu.GetValue("InstallFolder") & "\Yuzu.New\")
            My.Computer.FileSystem.CopyDirectory(CopyNewDirectory, OpenYuzu.GetValue("InstallFolder") & "\" & My.Computer.FileSystem.GetName(CopyNewDirectory))
        Next

        Directory.Delete(OpenYuzu.GetValue("InstallFolder") & "\Yuzu.New\", True)

        OpenYuzu.SetValue("Version", Label8.Text)

        MsgBox("更新完成，重启程序生效", 4096)

        Shell(Application.StartupPath & "\Bin\ResetApp.exe ", Style:=AppWinStyle.Hide)
    End Sub

End Class