Imports System.IO
Imports DSAPI.神扩展

Public Class SetYuzuFolder
    Private Sub SetYuzuFolder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
        TextBox1.Text = OpenYuzu.GetValue("InstallFolder")
        TextBox2.Text = OpenYuzu.GetValue("Version")
        TextBox3.Text = OpenYuzu.GetValue("Firmver")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SetNewYuzuFolder As New FolderBrowserDialog With {
            .ShowNewFolderButton = False
        }

        If SetNewYuzuFolder.ShowDialog = 1 Then
            If File.Exists(SetNewYuzuFolder.SelectedPath & "\yuzu.exe") Then
                If My.Computer.FileSystem.GetName(SetNewYuzuFolder.SelectedPath) <> "yuzu-windows-msvc" Then
                    If My.Computer.FileSystem.GetName(SetNewYuzuFolder.SelectedPath) <> "yuzu-windows-msvc-early-access" Then
                        TextBox1.Text = SetNewYuzuFolder.SelectedPath
                        TextBox2.Text = "正在获取..."
                        Dim GetYuzuVer = New Threading.Thread(AddressOf YuzuVerGet)
                        GetYuzuVer.Start()

                    Else
                        ShowMessage("文件夹不能叫 yuzu-windows-msvc-early-access")
                    End If
                Else
                    ShowMessage("文件夹不能叫 yuzu-windows-msvc")
                End If
            Else
                ShowMessage("选择的文件夹必须包含yuzu.exe")
            End If
        End If
    End Sub
    Private Sub YuzuVerGet()

        Dim NewProcess = New Process
        With NewProcess.StartInfo
            .FileName = TextBox1.Text & "\yuzu.exe"
            .WindowStyle = ProcessWindowStyle.Minimized
        End With

        NewProcess.Start()

        Dim YuzuTitle = ""
        Do Until YuzuTitle <> ""
            Threading.Thread.Sleep(50)
            YuzuTitle = Process.GetProcessById(NewProcess.Id).MainWindowTitle.ToString
        Loop

        TextBox2.Text = YuzuTitle.Replace("yuzu Early Access", "").Replace("yuzu", "").Replace(" ", "")

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
        Dim OpenYuzu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Yuzu", True)
        OpenYuzu.SetValue("InstallFolder", TextBox1.Text)
        OpenYuzu.SetValue("Version", TextBox2.Text)
        OpenYuzu.SetValue("Firmver", TextBox3.Text)
        OpenYuzu.SetValue("Insted", "True")

        ShowMessage("修改成功，重启程序生效")
        Dim ResetTool = New Process
        ResetTool.StartInfo.FileName = Application.StartupPath & "\Bin\ResetApp.exe"
        ResetTool.StartInfo.Arguments = "manage"
        ResetTool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ResetTool.Start()
    End Sub

End Class