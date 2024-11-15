Public Class NewMainMenuForm
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub NewMainMenuForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim gameForm As New GameForm()
        gameForm.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Controls.Clear()

        Me.Hide()
        Dim mainMenuForm As New MainMenuForm()
        mainMenuForm.Show()
    End Sub

End Class