Imports DSAPI.神扩展
Imports System.IO
Public Class Form1
    Dim cDown As Boolean
    Dim URL As String
    Dim SavePath As String
    Dim SaveName As String
    Dim Job As String
    Dim x As Integer
    Dim y As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '命令
            '(URL)文件链接(SavePath)保存路径不带反斜杠(SaveName)文件名(Job)任务标题(x)x(y)y(End)

            URL = 提取中间文本(Command, "(URL)", "(SavePath)")
            SavePath = 提取中间文本(Command, "(SavePath)", "(SaveName)")
            SaveName = 提取中间文本(Command, "(SaveName)", "(Job)")
            Job = 提取中间文本(Command, "(Job)", "(x)")
            x = 提取中间文本(Command, "(x)", "(y)")
            y = 提取中间文本(Command, "(y)", "(End)")
            Me.Left = x
            Me.Top = y
            Label3.Text = Job

            If URL = "" Then
                MsgBox("参数无效" & vbCrLf & vbCrLf & "(URL)文件链接(SavePath)保存路径不带反斜杠(SaveName)文件名(Job)任务标题(x)x(y)y(End)")
                End
            End If

            CheckForIllegalCrossThreadCalls = False

            Dim D1Down As New Threading.Thread(AddressOf Down1)
            D1Down.Start()

            Dim D1Prog As New Threading.Thread(AddressOf Prog1)
            D1Prog.Start()
        Catch ex As Exception
            MsgBox("参数无效" & vbCrLf & vbCrLf & "(URL)文件链接(SavePath)保存路径不带反斜杠(SaveName)文件名(Job)任务标题(x)x(y)y(End)")
            End
        End Try
    End Sub

    Private Sub Down1()
        cDown = False

        Dim Down As New Process
        Down.StartInfo.FileName = "cmd"
        Down.StartInfo.Arguments = "/c aria2c.exe """ & URL & """ -d """ & SavePath & """ -o """ & SaveName & """ -s 8 -x 8 > Log.txt"
        Down.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Down.Start()
        Down.WaitForExit()

        cDown = True
    End Sub
    Private Sub Prog1()

        Do Until cDown = True
            Application.DoEvents()
            Threading.Thread.Sleep(100)

            If File.Exists(Application.StartupPath & "\Log.txt") Then
                File.Copy(Application.StartupPath & "\Log.txt", Application.StartupPath & "\Log2.txt")

                Dim AllText = File.ReadLines(Application.StartupPath & "\Log2.txt")
                Dim EndLine = AllText.Count - 1

                If InStr(AllText(EndLine), "ETA") <> 0 Then
                    '[#abcf93 55MiB/91MiB(61%) CN:3 DL:5.1MiB ETA:6s]
                    Dim 速度 = 提取中间文本(AllText(EndLine), "DL:", "Mib")
                    Dim 当前大小 = 提取中间文本(AllText(EndLine), " ", "Mib")
                    Dim 总共大小 = 提取中间文本(AllText(EndLine), "/", "Mib")
                    Dim 进度 = 提取中间文本(AllText(EndLine), "(", "%")
                    Dim 剩余时间 = 提取中间文本(AllText(EndLine), "ETA:", "s]")

                    Label6.Text = 速度 & " MB/s"
                    Label7.Text = 剩余时间 & " s"
                    ProgressBar4.Value = 进度
                    Label4.Text = 当前大小 & "MB / " & 总共大小 & "MB"
                    Label5.Text = 进度 & " %"
                End If

                File.Delete(Application.StartupPath & "\Log2.txt")
            End If
        Loop
        File.Delete(Application.StartupPath & "\Log.txt")
        End
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        KillProc("RyuzuTool")
        KillProc("aria2c")
        If File.Exists(Application.StartupPath & "\Log.txt") Then
            File.Delete(Application.StartupPath & "\Log.txt")
        End If
        If File.Exists(Application.StartupPath & "\Log2.txt") Then
            File.Delete(Application.StartupPath & "\Log2.txt")
        End If
        End
    End Sub

    Private Sub KillProc(ByVal Name As String)
        Dim i As Integer
        Dim proc As Process()
        If System.Diagnostics.Process.GetProcessesByName(Name).Length > 0 Then
            proc = Process.GetProcessesByName(Name)
            For i = 0 To proc.Length - 1
                proc(i).Kill()
            Next
        End If
    End Sub
End Class
