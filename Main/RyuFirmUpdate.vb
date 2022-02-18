Imports System.IO
Public Class RyuFirmUpdate
    Private Sub RyuFirmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, Me.Height - 1), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(Me.Width - 1, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})

        Me.Top += 15
        Dim OpT As New Threading.Thread(AddressOf Op)
        OpT.Start()
        Transitions.Transition.run(Me, "Top", Me.Top - 15, New Transitions.TransitionType_Deceleration(250))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        'Update.7z
        Dim DownRyujinx = New Process
        DownRyujinx.StartInfo.FileName = Application.StartupPath & "\DownloadManage.exe"
        DownRyujinx.StartInfo.Arguments = "(URL)" & Label1.Tag & "(SavePath)" & Application.StartupPath & "(SaveName)Firm.7z(Job)Ryujinx 固件更新(x)" & Me.Left & "(y)" & Me.Top & "(End)"
        DownRyujinx.Start()
        DownRyujinx.WaitForExit()

        Me.Show()
        Button1.Enabled = False
        Button2.Enabled = False
        ProgressBar1.Visible = True
        Label7.Visible = True
        CheckForIllegalCrossThreadCalls = False
        Dim InstFile As New Threading.Thread(AddressOf C7zAndFile)
        InstFile.Start()

        Me.Top += 15
        Dim OpT As New Threading.Thread(AddressOf Op)
        OpT.Start()
        Transitions.Transition.run(Me, "Top", Me.Top - 15, New Transitions.TransitionType_Deceleration(250))
    End Sub
    Private Sub Op()

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

    Private Sub C7zAndFile()

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


        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        OpenRyu.SetValue("Firmver", Label8.Text)

        MsgBox("更新完成", 4096)

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