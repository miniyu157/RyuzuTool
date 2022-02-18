Public Class SetRyuFolder

    Private Sub SetRyuFolder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        TextBox1.Text = OpenRyu.GetValue("InstallFolder")
        TextBox2.Text = OpenRyu.GetValue("Version")
        TextBox3.Text = OpenRyu.GetValue("Firmver")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SetNewYuzuFolder As New FolderBrowserDialog With {
            .ShowNewFolderButton = False
        }

        If SetNewYuzuFolder.ShowDialog = 1 Then
            If IO.File.Exists(SetNewYuzuFolder.SelectedPath & "\ryujinx.exe") Then
                If My.Computer.FileSystem.GetName(SetNewYuzuFolder.SelectedPath) <> "publish" Then
                    If IO.Directory.Exists(SetNewYuzuFolder.SelectedPath & "\protable") Then
                        MsgBox("你可能需要把protable文件夹转移到c盘，因为更新会删除Ryujinx.exe所在的文件夹")
                    End If
                    TextBox1.Text = SetNewYuzuFolder.SelectedPath
                    TextBox2.Text = "正在获取..."
                    Dim GetYuzuVer = New Threading.Thread(AddressOf YuzuVerGet)
                    GetYuzuVer.Start()

                Else
                    ShowMessage("文件夹不能叫 publish")

                End If
            Else
                ShowMessage("选择的文件夹必须包含ryujinx.exe")
            End If
        End If
    End Sub
    Private Sub YuzuVerGet()
        Dim Tit = GetEXETitle(TextBox1.Text & "\ryujinx.exe", 1000).Replace(" ", "").Replace("RyujinxConsole", "")
        If Tit = "" Then
            TextBox2.Text = "失败：第一次重试"
            Tit = GetEXETitle(TextBox1.Text & "\ryujinx.exe", 1500).Replace(" ", "").Replace("RyujinxConsole", "")
            If Tit = "" Then
                TextBox2.Text = "失败：第二次重试"
                Tit = GetEXETitle(TextBox1.Text & "\ryujinx.exe", 2000).Replace(" ", "").Replace("RyujinxConsole", "")
                If Tit = "" Then
                    TextBox2.Text = "失败：第三次重试"
                    Tit = GetEXETitle(TextBox1.Text & "\ryujinx.exe", 3000).Replace(" ", "").Replace("RyujinxConsole", "")
                    If Tit = "" Then
                        TextBox2.Text = "未知"
                    Else
                        TextBox2.Text = Tit
                    End If
                Else
                    TextBox2.Text = Tit
                End If
            Else
                TextBox2.Text = Tit
            End If
        Else
            TextBox2.Text = Tit
        End If
    End Sub
    Private Sub ShowMessage(ByVal Text As String)
        ShowMessageBox.Label1.Text = Text
        ShowMessageBox.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        OpenYuzu.SetValue("InstallFolder", TextBox1.Text)
        OpenYuzu.SetValue("Version", TextBox2.Text)
        OpenYuzu.SetValue("Firmver", TextBox3.Text)

        ShowMessage("修改成功，重启程序生效")
        Dim ResetTool = New Process
        ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
        ResetTool.StartInfo.Arguments = "manage"
        ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ResetTool.Start()
    End Sub

    Function GetEXETitle(ByVal FileName As String, ByVal Waiting As Integer) As String
        Dim NewProcess = New Process
        NewProcess.StartInfo.FileName = FileName
        NewProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        NewProcess.Start()
        Threading.Thread.Sleep(Waiting)
        GetEXETitle = NewProcess.MainWindowTitle
        Threading.Thread.Sleep(Waiting)
        NewProcess.Kill()
    End Function
End Class