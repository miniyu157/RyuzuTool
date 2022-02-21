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
                    If IO.Directory.Exists(SetNewYuzuFolder.SelectedPath & "\portable") Then
                        If MsgBox("需要把portable文件夹转移到 """ & System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx"" ，我们建议你立即转移，因为以后更新会删除Ryujinx.exe所在的文件夹" & vbCrLf & vbCrLf & "选择""是""，将为你自动转移，选择""否""，则丢弃以前的存档以及数据"， 52， "数据丢失警告") = 6 Then
                            CheckForIllegalCrossThreadCalls = False
                            Label4.Visible = True
                            ProgressBar1.Visible = True
                            Dim 转移数据线程 As New Threading.Thread(AddressOf 转移数据)
                            转移数据线程.Start()
                        End If
                    End If
                    TextBox1.Text = SetNewYuzuFolder.SelectedPath
                    TextBox2.Text = "正在获取..."
                    Dim GetYuzuVer = New Threading.Thread(AddressOf RyuVerGet)
                    GetYuzuVer.Start()

                Else
                    ShowMessage("文件夹不能叫 publish")

                End If
            Else
                ShowMessage("选择的文件夹必须包含ryujinx.exe")
            End If
        End If
    End Sub
    Private Sub 转移数据()
        Button2.Enabled = False
        Button3.Enabled = False

        If IO.Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx") Then
            IO.Directory.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx", True)
        End If

        My.Computer.FileSystem.CopyDirectory(TextBox1.Text & "\portable", System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx")

        Label4.Text = "转移完成"
        Button2.Enabled = True
        Button3.Enabled = True
    End Sub
    Private Sub RyuVerGet()

        Dim NewProcess = New Process
        With NewProcess.StartInfo
            .FileName = TextBox1.Text & "\ryujinx.exe"
            .WindowStyle = ProcessWindowStyle.Minimized
        End With

        NewProcess.Start()

        Dim RyuTitle = ""
        Do Until RyuTitle <> "" And RyuTitle <> "E:\Ryu\Ryujinx\ryujinx.exe"
            Threading.Thread.Sleep(50)
            RyuTitle = Process.GetProcessById(NewProcess.Id).MainWindowTitle.ToString
        Loop

        TextBox2.Text = RyuTitle.Replace("yuzu Early Access", "").Replace(" ", "").Replace("RyujinxConsole", "")

        NewProcess.Kill()

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
        OpenYuzu.SetValue("MCL", "EN")

        ShowMessage("修改成功，重启程序生效")
        Dim ResetTool = New Process
        ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
        ResetTool.StartInfo.Arguments = "manage"
        ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ResetTool.Start()
    End Sub

End Class