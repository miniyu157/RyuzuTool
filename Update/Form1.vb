Imports System.IO
Public Class Form1

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer
        Dim proc As Process()
        If System.Diagnostics.Process.GetProcessesByName("RyuzuTool").Length > 0 Then
            proc = Process.GetProcessesByName("RyuzuTool")
            For i = 0 To proc.Length - 1
                proc(i).Kill()
            Next
        End If

        Button1.Enabled = False
        Label5.Visible = True
        ProgressBar1.Visible = True

        Dim 下载线程 As New Threading.Thread(AddressOf 下载)
        下载线程.Start()
    End Sub
    Private Sub 下载()
        Dim SaveFileName = Application.StartupPath & "\Tool.zip"
        Dim URL = Label1.Tag
        Dim Client As New System.Net.WebClient
        Client.DownloadFileAsync(New Uri(URL), SaveFileName, Nothing)
        Dim UrlFileSize = System.Net.WebRequest.Create(URL).GetResponse.Headers.Get("Content-Length")
        Dim FileSize = New System.IO.FileInfo(SaveFileName).Length
        Do Until UrlFileSize = FileSize
            Application.DoEvents()
            Threading.Thread.Sleep(10)
            FileSize = New System.IO.FileInfo(SaveFileName).Length
            ProgressBar1.Value = Math.Round((FileSize / UrlFileSize) * 100)
        Loop

        Label5.Text = "正在安装..."

        File.Copy(Application.StartupPath & "\Bin\7z.exe", Application.StartupPath & "\7z.exe")
        File.Copy(Application.StartupPath & "\Bin\7z.dll", Application.StartupPath & "\7z.dll")

        Dim 解压zip As New Process
        解压zip.StartInfo.FileName = Application.StartupPath & "\7z.exe"
        解压zip.StartInfo.Arguments = " x Tool.zip -oTool"
        解压zip.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        解压zip.Start()
        解压zip.WaitForExit()

        Directory.Delete(Application.StartupPath & "\Bin", True)

        For Each 删除旧的 In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\")
            If My.Computer.FileSystem.GetName(删除旧的) <> "Update.exe" And My.Computer.FileSystem.GetName(删除旧的) <> "HtmlAgilityPack.dll" Then
                File.Delete(删除旧的)
            End If
        Next

        My.Computer.FileSystem.CopyDirectory(Application.StartupPath & "\Tool\Bin", Application.StartupPath & "\Bin")

        For Each 复制新的 In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Tool\")
            If My.Computer.FileSystem.GetName(复制新的) <> "Update.exe" And My.Computer.FileSystem.GetName(复制新的) <> "HtmlAgilityPack.dll" Then
                File.Copy(复制新的, Application.StartupPath & "\" & My.Computer.FileSystem.GetName(复制新的))
            End If
        Next

        File.Copy(Application.StartupPath & "\Tool\Update.exe", Application.StartupPath & "\Update.exe.New")

        Directory.Delete(Application.StartupPath & "\Tool", True)

        Dim 创建bat = File.CreateText(Application.StartupPath & "\Update.bat")
        创建bat.WriteLine("Timeout /t 2")
        创建bat.WriteLine("Del /F /Q Update.exe")
        创建bat.WriteLine("Ren Update.exe.New Update.exe")
        创建bat.WriteLine("Del /F /Q Update.bat")
        创建bat.Close()

        Shell(Application.StartupPath & "\Update.bat")

        End
    End Sub
End Class
