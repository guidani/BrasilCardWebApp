Imports System.Data.SqlClient

Public Class NewTransaction
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ButtonSaveTransaction_Click(sender As Object, e As EventArgs)
        Try
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("Brasil_CardConnectionString").ConnectionString
            Dim query As String = "INSERT INTO Transacoes (Id_Transacao, Numero_Cartao, Valor_Transacao, Data_Transacao, Descricao) VALUES (@IdTransacao, @NumeroCartao, @ValorTransacao, @DataTransacao, @Descricao)"

            If String.IsNullOrEmpty(TextBoxIdTransaction.Text) OrElse String.IsNullOrEmpty(TextBoxCardNumber.Text) OrElse String.IsNullOrEmpty(TextBoxTransactionValue.Text) OrElse String.IsNullOrEmpty(TextBoxTransactionDate.Text) Then
                Return
            End If

            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdTransacao", TextBoxIdTransaction.Text)
                    cmd.Parameters.AddWithValue("@NumeroCartao", TextBoxCardNumber.Text)
                    cmd.Parameters.AddWithValue("@ValorTransacao", Convert.ToDecimal(TextBoxTransactionValue.Text))
                    cmd.Parameters.AddWithValue("@DataTransacao", Convert.ToDateTime(TextBoxTransactionDate.Text))
                    cmd.Parameters.AddWithValue("@Descricao", TextBoxTransactionDescription.Text)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                        'Limpandos os campos
                        TextBoxIdTransaction.Text = ""
                        TextBoxCardNumber.Text = ""
                        TextBoxTransactionValue.Text = ""
                        TextBoxTransactionDate.Text = ""
                        TextBoxTransactionDescription.Text = ""

                    Catch ex As Exception

                    End Try
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
End Class