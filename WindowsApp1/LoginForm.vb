Imports System.Data.SqlClient

Public Class LoginForm
    Private connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db.mdf;Integrated Security=True"

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        If Not String.IsNullOrEmpty(username) AndAlso Not String.IsNullOrEmpty(password) Then
            Try
                Using connection As New SqlConnection(connectionString)
                    connection.Open()
                    Dim query As String = "SELECT COUNT(*) FROM [dbo].[Table] WHERE Username = @Username AND Password = @Password"
                    Using command As New SqlCommand(query, connection)
                        command.Parameters.AddWithValue("@Username", username)
                        command.Parameters.AddWithValue("@Password", password)
                        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                        If count > 0 Then
                            MessageBox.Show("Login successful!")
                            Dim newmainmenuForm As New NewMainMenuForm()
                            newmainmenuForm.Show()
                            Me.Hide()
                        Else
                            MessageBox.Show("Incorrect username or password.")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error logging in: " & ex.Message)
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
End Class
