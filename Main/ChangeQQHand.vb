Imports DSAPI.神扩展
Imports DSAPI
Public Class ChangeQQHand
    Dim 窗体是否可以拖动 As Boolean
    Private Sub ChangeQQHand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, Me.Height - 1), .Size = New Size(Me.Width, 1), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(0, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})
        Me.Controls.Add(New Panel With {.Location = New Point(Me.Width - 1, 0), .Size = New Size(1, Me.Height), .BackColor = Color.FromArgb(162, 164, 168), .BorderStyle = BorderStyle.FixedSingle})

        窗体是否可以拖动 = True

        Me.Top += 15
        CheckForIllegalCrossThreadCalls = False
        Dim 透明度线程 As New Threading.Thread(AddressOf 启动透明度)
        透明度线程.Start()
        Transitions.Transition.run(Me, "Top", Me.Top - 15, New Transitions.TransitionType_Deceleration(250))

        If 窗体是否可以拖动 = True Then
            AddHandler MyBase.MouseDown, AddressOf 鼠标按下
            AddHandler MyBase.MouseMove, AddressOf 鼠标移动
            AddHandler MyBase.MouseUp, AddressOf 鼠标弹起
        End If

        Dim 加载QQ列表线程 As New Threading.Thread(AddressOf 加载QQ列表)
        加载QQ列表线程.Start()
    End Sub
    Private Sub 加载QQ列表()
        ListBox1.Items.Clear()
        Dim 所有文件夹 = IO.Directory.GetDirectories($"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Tencent Files")
        所有文件夹.遍历(Sub(a)
                     If a.短文件名.是纯数字 Then ListBox1.Items.Add(a.短文件名)
                 End Sub)
        Try
            ListBox1.SelectedIndex = 0
        Catch ex As Exception
        End Try

    End Sub
    Private Sub 启动透明度()
        For i = 1 To 25
            Threading.Thread.Sleep(10)
            Me.Opacity += (i / 100) * 4
            Application.DoEvents()
        Next
    End Sub
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
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim 处理位置 As New Threading.Thread(AddressOf 位置)
        Dim 处理透明度 As New Threading.Thread(AddressOf 透明)
        处理透明度.Start()
        处理位置.Start()
    End Sub
    Private Sub 透明()
        For i = 1 To 25
            Threading.Thread.Sleep(10)
            Me.Opacity -= (i / 100) * 4
            Application.DoEvents()
        Next
        Me.Close()
    End Sub
    Private Sub 位置()
        Transitions.Transition.run(Me, "Top", Me.Top + 15, New Transitions.TransitionType_Deceleration(250))
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim 下载的图像 As Bitmap = DSAPI.QQ用户相关.下载QQ头像(ListBox1.SelectedItem.ToString)
        PictureBox1.Image = 下载的图像.剪成圆形
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim 下载的图像 As Bitmap = DSAPI.QQ用户相关.下载QQ头像(ListBox1.SelectedItem.ToString)
            Form1.PictureBox1.Image = 下载的图像.剪成圆形

            Dim 处理位置 As New Threading.Thread(AddressOf 位置)
            Dim 处理透明度 As New Threading.Thread(AddressOf 透明)
            处理透明度.Start()
            处理位置.Start()
        Catch ex As Exception

            Dim 处理位置 As New Threading.Thread(AddressOf 位置)
            Dim 处理透明度 As New Threading.Thread(AddressOf 透明)
            处理透明度.Start()
            处理位置.Start()
        End Try
    End Sub

End Class