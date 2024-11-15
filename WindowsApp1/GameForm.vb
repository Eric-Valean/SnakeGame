Imports System.Drawing
Imports System.Windows.Forms

Public Class GameForm
    Inherits Form


    Const blockSize As Integer = 20
    Dim gameWidth As Integer
    Dim gameHeight As Integer
    Const initialSnakeLength As Integer = 3
    Const initialGameSpeed As Integer = 100

    Dim snake As New List(Of Point)()
    Dim food As Point
    Dim direction As Integer
    Dim score As Integer = 0
    Dim gameTimer As New Timer()
    Dim random As New Random()

    Public Sub New()

        InitializeComponent()

        Me.Text = "Snake Game"
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        Me.ClientSize = New Size(40 * blockSize, 30 * blockSize)

        InitializeGame()

        gameTimer.Interval = initialGameSpeed
        AddHandler gameTimer.Tick, AddressOf GameTimer_Tick

        AddHandler Me.KeyDown, AddressOf GameForm_KeyDown
    End Sub

    Private Sub InitializeGame()
        gameWidth = Me.ClientSize.Width \ blockSize
        gameHeight = Me.ClientSize.Height \ blockSize

        snake.Clear()
        direction = 1
        For i As Integer = 0 To initialSnakeLength - 1
            snake.Add(New Point(gameWidth \ 2 - i, gameHeight \ 2))
        Next

        GenerateFood()

        score = 0

        gameTimer.Interval = initialGameSpeed

        gameTimer.Start()
    End Sub

    Private Sub GenerateFood()
        Dim marginDistance As Integer = 1
        Dim foodX As Integer = random.Next(marginDistance, gameWidth - marginDistance)
        Dim foodY As Integer = random.Next(marginDistance, gameHeight - marginDistance)
        food = New Point(foodX, foodY)
    End Sub

    Private Sub ShowGameOver()

        Dim gameOverLabel As New Label()
        gameOverLabel.ForeColor = Color.White
        gameOverLabel.Text = "Game Over! Score: " & score
        gameOverLabel.AutoSize = True
        gameOverLabel.Font = New Font("Showcard Gothic", 20, FontStyle.Bold)
        Dim offset As Integer = 100
        gameOverLabel.Location = New Point((Me.ClientSize.Width - gameOverLabel.Width) \ 2 - offset, (Me.ClientSize.Height - gameOverLabel.Height) \ 2 - 60)
        Me.Controls.Add(gameOverLabel)
        gameOverLabel.BringToFront()


        Dim btnPlayAgain As New Button()
        btnPlayAgain.Text = "Play Again"
        btnPlayAgain.Size = New Size(120, 50)
        btnPlayAgain.ForeColor = Color.White
        btnPlayAgain.BackColor = Color.Green
        btnPlayAgain.Location = New Point((Me.ClientSize.Width - btnPlayAgain.Width) \ 2, (Me.ClientSize.Height - btnPlayAgain.Height) \ 2)
        AddHandler btnPlayAgain.Click, AddressOf btnPlayAgain_Click
        Me.Controls.Add(btnPlayAgain)
        btnPlayAgain.BringToFront()


        Dim btnBackToMenu As New Button()
        btnBackToMenu.Text = "Back to Menu"
        btnBackToMenu.Size = New Size(120, 50)
        btnBackToMenu.Location = New Point((Me.ClientSize.Width - btnBackToMenu.Width) \ 2, (Me.ClientSize.Height - btnBackToMenu.Height) \ 2 + 60)
        btnBackToMenu.ForeColor = Color.White
        btnBackToMenu.BackColor = Color.Red
        AddHandler btnBackToMenu.Click, AddressOf btnBackToMenu_Click
        Me.Controls.Add(btnBackToMenu)
        btnBackToMenu.BringToFront()
    End Sub

    Private Sub GameForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.Up
                If direction <> 2 Then direction = 0
            Case Keys.Right
                If direction <> 3 Then direction = 1
            Case Keys.Down
                If direction <> 0 Then direction = 2
            Case Keys.Left
                If direction <> 1 Then direction = 3
        End Select
    End Sub

    Private Sub btnPlayAgain_Click(sender As Object, e As EventArgs)
        Me.Controls.Clear()
        InitializeGame()
    End Sub

    Private Sub btnBackToMenu_Click(sender As Object, e As EventArgs)
        Me.Controls.Clear()
        Me.Hide()
        Dim newmainMenuForm As New NewMainMenuForm()
        newmainMenuForm.Show()
    End Sub

    Private Sub GameTimer_Tick(sender As Object, e As EventArgs)
        MoveSnake()

        If CheckCollision() Then
            gameTimer.Stop()
            ShowGameOver()
        End If

        If snake(0) = food Then
            score += 10
            GenerateFood()

            Dim tail As Point = snake(snake.Count - 1)
            snake.Add(New Point(tail.X, tail.Y))
        End If

        Me.Invalidate()
    End Sub

    Private Sub GameForm_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint

        Dim g As Graphics = e.Graphics


        For Each segment As Point In snake
            g.FillRectangle(Brushes.Green, segment.X * blockSize, segment.Y * blockSize, blockSize, blockSize)
        Next


        g.FillEllipse(Brushes.Red, food.X * blockSize, food.Y * blockSize, blockSize, blockSize)
    End Sub

    Private Sub MoveSnake()
        Dim newHead As Point = snake(0)
        Select Case direction
            Case 0 ' Sus
                newHead.Y -= 1
            Case 1 ' Dreapta
                newHead.X += 1
            Case 2 ' Jos
                newHead.Y += 1
            Case 3 ' Stânga
                newHead.X -= 1
        End Select

        snake.Insert(0, newHead)

        snake.RemoveAt(snake.Count - 1)
    End Sub

    Private Function CheckCollision() As Boolean
        Dim head As Point = snake(0)

        If head.X < 0 OrElse head.Y < 0 OrElse head.X >= gameWidth OrElse head.Y >= gameHeight Then
            Return True
        End If

        For i As Integer = 1 To snake.Count - 1
            If snake(i) = head Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub GameForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub
End Class