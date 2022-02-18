Imports MaterialSkin

Public Class CRyu
    Private Sub Ryu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)

        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE)

        If OpenRyu.GetValue("MCL") = "EN" Then
            CheckEN.Checked = True
        ElseIf OpenRyu.GetValue("MCL") = "CN" Then
            CheckCN.Checked = True
        ElseIf OpenRyu.GetValue("MCL") = "LDN" Then
            CheckLDN.Checked = True
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button3.Click
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
        Me.Close()
    End Sub
    Private Sub 位置()
        Transitions.Transition.run(Me, "Top", Me.Top + 15, New Transitions.TransitionType_Deceleration(250))
    End Sub

#Region "复选框的关节炎"
    Private Sub CheckEN_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEN.CheckedChanged
        If CheckEN.Checked = True Then
            CheckCN.Checked = False
            CheckLDN.Checked = False
        End If
    End Sub
    Private Sub CheckCN_CheckedChanged(sender As Object, e As EventArgs) Handles CheckCN.CheckedChanged
        If CheckCN.Checked = True Then
            CheckEN.Checked = False
            CheckLDN.Checked = False
        End If
    End Sub
    Private Sub CheckLDN_CheckedChanged(sender As Object, e As EventArgs) Handles CheckLDN.CheckedChanged
        If CheckLDN.Checked = True Then
            CheckEN.Checked = False
            CheckCN.Checked = False
        End If
    End Sub
    Private Sub CheckEN_Click(sender As Object, e As EventArgs) Handles CheckEN.Click
        If CheckEN.Checked = False Then
            CheckEN.Checked = True
        End If
    End Sub
    Private Sub CheckCN_Click(sender As Object, e As EventArgs) Handles CheckCN.Click
        If CheckCN.Checked = False Then
            CheckCN.Checked = True
        End If
    End Sub
    Private Sub CheckLDN_Click(sender As Object, e As EventArgs) Handles CheckLDN.Click
        If CheckLDN.Checked = False Then
            CheckLDN.Checked = True
        End If
    End Sub
    Private Sub EN_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click, Label3.Click, Label4.Click, Panel7.Click
        CheckEN.Checked = True
        CheckCN.Checked = False
        CheckLDN.Checked = False
    End Sub
    Private Sub CN_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click, Label5.Click, Label6.Click, Panel8.Click
        CheckEN.Checked = False
        CheckCN.Checked = True
        CheckLDN.Checked = False
    End Sub
    Private Sub LDN_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click, Label7.Click, Label8.Click, Panel9.Click
        CheckEN.Checked = False
        CheckCN.Checked = False
        CheckLDN.Checked = True
    End Sub
#End Region

    '继续&取消键
    Private Sub CRyu_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.C Then
            Button3.PerformClick()
        ElseIf e.KeyCode = Keys.N Then
            Button2.PerformClick()
        End If
    End Sub
    '下一步
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim OpenRyu = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RyuzuTool", True).OpenSubKey("Ryujinx", True)
        If CheckEN.Checked = True Then
            OpenRyu.SetValue("MCL", "EN")
        ElseIf CheckCN.Checked = True Then
            OpenRyu.SetValue("MCL", "CN")
        ElseIf CheckLDN.Checked = True Then
            OpenRyu.SetValue("MCL", "LDN")
        End If

        ResetApp("manage")
    End Sub
    '官网
    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Shell("explorer https://ryujinx.org")
    End Sub
    Private Sub ResetApp(ByVal Arguments As String)
        Me.Hide()
        ReleaseManage.Hide()
        Shell(Application.StartupPath & "\Bin\ResetApp.exe " & Arguments, Style:=AppWinStyle.Hide)
    End Sub
End Class