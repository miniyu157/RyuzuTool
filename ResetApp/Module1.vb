Module Module1

    Sub Main()

        Dim appDir As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase


        Dim i As Integer
        Dim proc As Process()
        If System.Diagnostics.Process.GetProcessesByName("RyuzuTool").Length > 0 Then
            proc = Process.GetProcessesByName("RyuzuTool")
            For i = 0 To proc.Length - 1
                proc(i).Kill()
            Next
        End If

        Process.Start((My.Computer.FileSystem.GetParentPath(appDir) & "\RyuzuTool.exe"), Command)
    End Sub

End Module
