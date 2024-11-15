Imports System.Data.SqlClient
Imports System.IO

Public Class NewAccountForm
    Private connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db.mdf;Integrated Security=True"

    Private Sub CreateAccountButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        If Not String.IsNullOrEmpty(username) AndAlso Not String.IsNullOrEmpty(password) Then
            Try
                Using connection As New SqlConnection(connectionString)
                    connection.Open()
                    Dim query As String = "INSERT INTO [dbo].[Table] (Username, Password) VALUES (@Username, @Password)"
                    Using command As New SqlCommand(query, connection)
                        command.Parameters.AddWithValue("@Username", username)
                        command.Parameters.AddWithValue("@Password", password)
                        command.ExecuteNonQuery()
                    End Using
                End Using
                MessageBox.Show("Account created successfully!")
                Me.Controls.Clear()
                Me.Hide()
                Dim newmainMenuForm As New NewMainMenuForm()
                newmainMenuForm.Show()
            Catch ex As Exception
                MessageBox.Show("Error creating account: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please enter username and password.")
        End If
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Dim mainMenuForm As New MainMenuForm()
        mainMenuForm.Show()
    End Sub

    Private Sub NewAccountForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
