Imports System.IO
Imports DSAPI.神扩展

Public Class RyuUpdate
    Dim NewRyuLink
    Private Sub RyuUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, Me.Height - 1), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(Me.Width - 1, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})

        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)

        Label5.Text = OpenRyu.GetValue("Version")

        If OpenRyu.GetValue("MCL") = "EN" Then
            Dim AllNameEN = 读取文本文件(Application.StartupPath & "\EN1.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllNameEN)
            Dim NewRyuVer = AllNameEN(0)
            NewRyuLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxMainlineMirror/" & NewRyuVer
        ElseIf OpenRyu.GetValue("MCL") = "CN" Then
            Dim AllNameEN = 读取文本文件(Application.StartupPath & "\CN1.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllNameEN)
            Dim NewRyuVer = AllNameEN(0)
            NewRyuLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxCNBuilds/" & NewRyuVer
        ElseIf OpenRyu.GetValue("MCL") = "LDN" Then
            Dim AllNameEN = 读取文本文件(Application.StartupPath & "\LDN1.json").提取所有中间文本("name"":""", """,")
            Array.Reverse(AllNameEN)
            Dim NewRyuVer = AllNameEN(0)
            NewRyuLink = "https://" & My.Resources.MirrorSource & "/ns_emu_helper/RyujinxLDNMirror/" & NewRyuVer
        End If

        Me.Top += 15
        Dim OpT As New Threading.Thread(AddressOf Op)
        OpT.Start()
        Transitions.Transition.run(Me, "Top", Me.Top - 15, New Transitions.TransitionType_Deceleration(250))
    End Sub
    Private Sub Op()
        Me.Opacity = 0
        For i = 1 To 50
            Threading.Thread.Sleep(10)
            Me.Opacity += (i / 100) * 2
            Application.DoEvents()
        Next
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim 处理位置 As New Threading.Thread(AddressOf 位置)
        Dim 处理透明度 As New Threading.Thread(AddressOf 透明)
        处理透明度.Start()
        处理位置.Start()
    End Sub
    Private Sub 透明()
        For i = 1 To 50
            Threading.Thread.Sleep(10)
            Me.Opacity -= (i / 100) * 2
            Application.DoEvents()
        Next
        ReleaseManage.Button10.Enabled = True
        ReleaseManage.Button10.Text = "检查更新"
        Me.Close()
    End Sub
    Private Sub 位置()
        Transitions.Transition.run(Me, "Top", Me.Top + 15, New Transitions.TransitionType_Deceleration(250))
    End Sub

    '确定
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        'Update.7z
        Dim DownRyujinx = New Process
        DownRyujinx.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownRyujinx.StartInfo.Arguments = "(URL)" & NewRyuLink & "(SavePath)" & Application.StartupPath & "(SaveName)Update.7z(Job)Ryujinx 更新(x)" & Me.Left & "(y)" & Me.Top & "(End)"
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
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        Directory.Delete(OpenRyu.GetValue("InstallFolder") & "\"， True)


        Dim JieRyu = New Process
        JieRyu.StartInfo.FileName = Application.StartupPath & "\Bin\7z.exe"
        JieRyu.StartInfo.Arguments = "x """ & Application.StartupPath & "\Update.7z"" -o""" & OpenRyu.GetValue("InstallFolder") & "\"""
        JieRyu.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        JieRyu.Start()
        JieRyu.WaitForExit()

        My.Computer.FileSystem.RenameDirectory(OpenRyu.GetValue("InstallFolder") & "\publish", "New.Ryujinx")

        For Each CopyDir In My.Computer.FileSystem.GetDirectories(OpenRyu.GetValue("InstallFolder") & "\New.Ryujinx\")
            My.Computer.FileSystem.CopyDirectory(CopyDir, OpenRyu.GetValue("InstallFolder") & "\" & My.Computer.FileSystem.GetName(CopyDir))
        Next

        For Each CopyFile1 In My.Computer.FileSystem.GetFiles(OpenRyu.GetValue("InstallFolder") & "\New.Ryujinx\")
            My.Computer.FileSystem.CopyFile(CopyFile1, OpenRyu.GetValue("InstallFolder") & "\" & My.Computer.FileSystem.GetName(CopyFile1))
        Next

        Directory.Delete(OpenRyu.GetValue("InstallFolder") & "\New.Ryujinx\", True)

        OpenRyu.SetValue("Version", Label8.Text)

        MsgBox("更新完成，重启程序生效", 4096)

        Shell(Application.StartupPath & "\Bin\ResetApp.exe ", Style:=AppWinStyle.Hide)
    End Sub
End Class