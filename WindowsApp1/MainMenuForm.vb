Imports System.IO
Public Class MainMenuForm

    Public Property IsLoggedIn As Boolean = False
    Private Sub NewAccountButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newAccountForm As New NewAccountForm()
        newAccountForm.Show()
        Me.Hide()
    End Sub

    Private Sub LogInButton_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim loginForm As New LoginForm()

        If loginForm.ShowDialog() = DialogResult.OK Then
            IsLoggedIn = True
        End If
        Me.Hide()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Application.Exit()
    End Sub

    Private Sub MainMenuForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim gameForm As New GameForm()
        gameForm.Show()
        Me.Hide()
    End Sub
End Class