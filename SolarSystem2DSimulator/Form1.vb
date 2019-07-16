Imports System.ComponentModel

Public Class Form1
    Public sunCentre As Point
    Public neptuneCentre As Point
    Public uranusCentre As Point
    Public saturnCentre As Point
    Public jupiterCentre As Point
    Public marsCentre As Point
    Public earthCentre As Point
    Public moonCentre As Point
    Public venusCentre As Point
    Public mercuryCentre As Point
    Public degrees As Double = 9000

    Public guideEnabled = False
    Public guidePoint As Point
    Public FullScreen = False

    Dim lostfocusbyForm2 = False

    Dim delta = 0.5
    Dim length = 100
    Dim focusdPlanet As String = ""
    Dim allowrange = 30
    Dim plSize As Double = 1
    Dim plDist As Double = 1
    Dim loc As Point

#Region "Aero 그림자 효과 (Vista이상)"

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        CreateDropShadow(Me)
        MyBase.OnHandleCreated(e)
    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetSunCentre()
        Me.Refresh()
        Opacity = 0
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Refresh()
        FadeIn(Me, 1)
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        FadeOut(Me)
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        With e.Graphics
            .DrawImage(My.Resources.sun,
                       Me.sunCentre.X - Convert.ToInt32(30 * plSize),
                       Me.sunCentre.Y - Convert.ToInt32(30 * plSize),
                       Convert.ToInt32(60 * plSize),
                       Convert.ToInt32(60 * plSize))

            .DrawImage(My.Resources.uranus,
                       Me.uranusCentre.X - Convert.ToInt32(11 * plSize),
                       Me.uranusCentre.Y - Convert.ToInt32(20 * plSize),
                       Convert.ToInt32(22 * plSize),
                       Convert.ToInt32(40 * plSize))

            .DrawImage(My.Resources.jup,
                       Me.jupiterCentre.X - Convert.ToInt32(20 * plSize),
                       Me.jupiterCentre.Y - Convert.ToInt32(20 * plSize),
                       Convert.ToInt32(40 * plSize),
                       Convert.ToInt32(40 * plSize))

            .DrawImage(My.Resources.saturn,
                       Me.saturnCentre.X - Convert.ToInt32(25 * plSize),
                       Me.saturnCentre.Y - Convert.ToInt32(25 * plSize),
                       Convert.ToInt32(50 * plSize),
                       Convert.ToInt32(50 * plSize))

            .DrawImage(My.Resources.neptune,
                       Me.neptuneCentre.X - Convert.ToInt32(10 * plSize),
                       Me.neptuneCentre.Y - Convert.ToInt32(10 * plSize),
                       Convert.ToInt32(20 * plSize),
                       Convert.ToInt32(20 * plSize))

            .DrawImage(My.Resources.mars,
                       Me.marsCentre.X - Convert.ToInt32(8 * plSize),
                       Me.marsCentre.Y - Convert.ToInt32(8 * plSize),
                       Convert.ToInt32(16 * plSize),
                       Convert.ToInt32(16 * plSize))

            .DrawImage(My.Resources.earth,
                       Me.earthCentre.X - Convert.ToInt32(10 * plSize),
                       Me.earthCentre.Y - Convert.ToInt32(10 * plSize),
                       Convert.ToInt32(20 * plSize),
                       Convert.ToInt32(20 * plSize))

            .DrawImage(My.Resources.venus,
                       Me.venusCentre.X - Convert.ToInt32(10 * plSize),
                       Me.venusCentre.Y - Convert.ToInt32(10 * plSize),
                       Convert.ToInt32(20 * plSize),
                       Convert.ToInt32(20 * plSize))

            .DrawImage(My.Resources.mercury,
                       Me.mercuryCentre.X - Convert.ToInt32(5 * plSize),
                       Me.mercuryCentre.Y - Convert.ToInt32(5 * plSize),
                       Convert.ToInt32(10 * plSize),
                       Convert.ToInt32(10 * plSize))

            .DrawImage(My.Resources.moon,
                       Me.moonCentre.X - Convert.ToInt32(3 * plSize),
                       Me.moonCentre.Y - Convert.ToInt32(3 * plSize),
                       Convert.ToInt32(6 * plSize),
                       Convert.ToInt32(6 * plSize))

        End With

        Dim from As Point = earthCentre
        Dim targ As Point

        Select Case focusdPlanet
            Case "sun"
                targ = sunCentre

            Case "mercury"
                targ = mercuryCentre

            Case "venus"
                targ = venusCentre

            Case "mars"
                targ = marsCentre

            Case "neptune"
                targ = neptuneCentre

            Case "saturn"
                targ = saturnCentre

            Case "jupiter"
                targ = jupiterCentre

            Case "uranus"
                targ = uranusCentre

            Case "neptune"
                targ = neptuneCentre

            Case Else
                targ = Nothing
        End Select


        If Not targ = Nothing Then
            Dim angle As Double = Math.Atan2((targ.Y - from.Y), (targ.X - from.X))

            e.Graphics.DrawLine(Pens.Azure, from.X, from.Y, targ.X + Convert.ToSingle(Math.Cos(angle) * length), targ.Y + Convert.ToSingle(Math.Sin(angle) * length))
            guidePoint = New Point(targ.X + Convert.ToSingle(Math.Cos(angle) * length), targ.Y + Convert.ToSingle(Math.Sin(angle) * length))
        End If


    End Sub

    Private Sub Form1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Me.SetSunCentre()
        Me.Refresh()
        Form2.Size = Size
    End Sub

    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        Form2.Location = Location
    End Sub

    Private Sub SetSunCentre()
        Dim clientSize As Size = Me.ClientSize

        Me.sunCentre = New Point((clientSize.Width - Panel1.Width) \ 2,
                                 clientSize.Height \ 2)
    End Sub

    '해왕성
    Private Sub SetNeptuneCentre()
        Dim clientSize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientSize.Width, clientSize.Height) \ 2) - Convert.ToInt32(10 * plDist)
        Dim radians As Double = 0.014 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.neptuneCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '천왕성
    Private Sub SetUranusCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(55 * plDist)
        Dim radians As Double = 0.03 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.uranusCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '토성
    Private Sub SetSaturnCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(100 * plDist)
        Dim radians As Double = 0.08 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.saturnCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '목성
    Private Sub SetJupiterCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(145 * plDist)
        Dim radians As Double = 0.2 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.jupiterCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '화성
    Private Sub SetMarsCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(183 * plDist)
        Dim radians As Double = 1.3 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.marsCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '지구
    Private Sub SetEarthCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(211 * plDist)
        Dim radians As Double = 2.4 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.earthCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '금성
    Private Sub SetVenusCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(241 * plDist)
        Dim radians As Double = 3.6 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.venusCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    '수성
    Private Sub SetMercuryCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = (Math.Min(clientsize.Width, clientsize.Height) \ 2) - Convert.ToInt32(290 * plDist)
        Dim radians As Double = 10 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.mercuryCentre = New Point(Me.sunCentre.X + x, Me.sunCentre.Y + y)
    End Sub

    Private Sub SetMoonCentre()
        Dim clientsize As Size = Me.ClientSize
        Dim distance As Integer = Convert.ToInt32(16 * plDist)
        Dim radians As Double = 35 * Me.degrees * Math.PI / 180
        Dim x As Integer = Math.Floor(distance * Math.Sin(radians))
        Dim y As Integer = Math.Floor(distance * Math.Cos(radians))
        Me.moonCentre = New Point(Me.earthCentre.X + x, Me.earthCentre.Y + y)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        degrees += delta

        'SetPlutoCentre()
        SetNeptuneCentre()
        SetUranusCentre()
        SetSaturnCentre()
        SetJupiterCentre()
        SetMarsCentre()
        SetEarthCentre()
        SetMoonCentre()
        SetVenusCentre()
        SetMercuryCentre()

        Invalidate()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If guideEnabled Then
            lostfocusbyForm2 = False
            guideEnabled = False
            Form2.Close()
        Else
            lostfocusbyForm2 = True
            Form2.Size = Size
            Form2.SetDesktopLocation(Location.X, Location.Y)
            guideEnabled = True
            Form2.Show()
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        Dim curPlanet As String = ""

        If Math.Abs(e.X - sunCentre.X) < allowrange And Math.Abs(e.Y - sunCentre.Y) < allowrange Then
            curPlanet = "태양"
        ElseIf Math.Abs(e.X - mercuryCentre.X) < allowrange And Math.Abs(e.Y - mercuryCentre.Y) < allowrange Then
            curPlanet = "수성"
        ElseIf Math.Abs(e.X - venusCentre.X) < allowrange And Math.Abs(e.Y - venusCentre.Y) < allowrange Then
            curPlanet = "금성"
        ElseIf Math.Abs(e.X - earthCentre.X) < allowrange And Math.Abs(e.Y - earthCentre.Y) < allowrange Then
            curPlanet = "지구"
        ElseIf Math.Abs(e.X - marsCentre.X) < allowrange And Math.Abs(e.Y - marsCentre.Y) < allowrange Then
            curPlanet = "화성"
        ElseIf Math.Abs(e.X - jupiterCentre.X) < allowrange And Math.Abs(e.Y - jupiterCentre.Y) < allowrange Then
            curPlanet = "목성"
        ElseIf Math.Abs(e.X - saturnCentre.X) < allowrange And Math.Abs(e.Y - saturnCentre.Y) < allowrange Then
            curPlanet = "토성"
        ElseIf Math.Abs(e.X - uranusCentre.X) < allowrange And Math.Abs(e.Y - uranusCentre.Y) < allowrange Then
            curPlanet = "천왕성"
        ElseIf Math.Abs(e.X - neptuneCentre.X) < allowrange And Math.Abs(e.Y - neptuneCentre.Y) < allowrange Then
            curPlanet = "해왕성"
        End If

        If Not curPlanet = "" Then Label1.Text = curPlanet
    End Sub

    Private Sub SpinSpeed_Scroll(sender As Object, e As EventArgs) Handles SpinSpeed.Scroll
        delta = SpinSpeed.Value * 0.1
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick

        focusdPlanet = ""

        If Math.Abs(e.X - sunCentre.X) < allowrange And Math.Abs(e.Y - sunCentre.Y) < allowrange Then
            focusdPlanet = "sun"
        ElseIf Math.Abs(e.X - mercuryCentre.X) < allowrange And Math.Abs(e.Y - mercuryCentre.Y) < allowrange Then
            focusdPlanet = "mercury"
        ElseIf Math.Abs(e.X - venusCentre.X) < allowrange And Math.Abs(e.Y - venusCentre.Y) < allowrange Then
            focusdPlanet = "venus"
        ElseIf Math.Abs(e.X - earthCentre.X) < allowrange And Math.Abs(e.Y - earthCentre.Y) < allowrange Then
            focusdPlanet = "earth"
        ElseIf Math.Abs(e.X - marsCentre.X) < allowrange And Math.Abs(e.Y - marsCentre.Y) < allowrange Then
            focusdPlanet = "mars"
        ElseIf Math.Abs(e.X - jupiterCentre.X) < allowrange And Math.Abs(e.Y - jupiterCentre.Y) < allowrange Then
            focusdPlanet = "jupiter"
        ElseIf Math.Abs(e.X - saturnCentre.X) < allowrange And Math.Abs(e.Y - saturnCentre.Y) < allowrange Then
            focusdPlanet = "saturn"
        ElseIf Math.Abs(e.X - uranusCentre.X) < allowrange And Math.Abs(e.Y - uranusCentre.Y) < allowrange Then
            focusdPlanet = "uranus"
        ElseIf Math.Abs(e.X - neptuneCentre.X) < allowrange And Math.Abs(e.Y - neptuneCentre.Y) < allowrange Then
            focusdPlanet = "neptune"
        End If

    End Sub

    Private Sub PlanetSize_Scroll(sender As Object, e As EventArgs) Handles PlanetSize.Scroll
        plSize = PlanetSize.Value / 5
    End Sub

    Private Sub PlanetDist_Scroll(sender As Object, e As EventArgs) Handles PlanetDist.Scroll
        plDist = (100 - PlanetDist.Value) / 50
    End Sub

    Private Sub FormDrag_MouseDown(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseDown, TopPanel2.MouseDown, TopTitle.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            loc = e.Location
        End If
    End Sub

    Private Sub FormDrag_MouseMove(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseMove, TopPanel2.MouseMove, TopTitle.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub

    Private Sub Panel2_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseMove

        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Size = New Size(Me.PointToClient(MousePosition).X, Me.PointToClient(MousePosition).Y)
        End If

    End Sub

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        SuspendLayout()
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseUp
        ResumeLayout()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles CloseBT.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not FullScreen Then
            FullScreen = True
            WindowState = FormWindowState.Maximized
        Else
            FullScreen = False
            WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub BarLength_Scroll(sender As Object, e As EventArgs) Handles BarLength.Scroll
        length = Convert.ToInt32(100 * (BarLength.Value / 5))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim msg As String = "제작: 경북고등학교 3학년 8반 13번 박동준" + vbCr _
            + "이 프로그램을 지구과학II 과목에 바칩니다" + vbCr + vbCr _
            + "참조: Microsoft .Net API Browser, VBForums MaximilianMayrhofer" _
            + vbCr + "개발: Visual Basic .Net, Visual Studio Community 2017" _
            + vbCr + "최종 빌드 일자: 2019-7-17" + vbCr + "Developed by PBJSoftware (github.com/pdjdev)"

        MsgBox(msg, vbInformation)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles MinBT.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub BT_MouseEnter(sender As Object, e As EventArgs) Handles CloseBT.MouseEnter, MinBT.MouseEnter

        Dim ctrl As Object = sender
        ctrl.BackColor = ControlPaint.Light(TopPanel2.BackColor, 0.2)

    End Sub

    Private Sub BT_MouseLeave(sender As Object, e As EventArgs) Handles CloseBT.MouseLeave, MinBT.MouseLeave

        Dim ctrl As Object = sender
        ctrl.BackColor = Color.Transparent

    End Sub




    'Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
    '    If Not lostfocusbyForm2 Then
    '        guideEnabled = False
    '        Form2.Close()
    '    End If

    '    lostfocusbyForm2 = False
    'End Sub
End Class
