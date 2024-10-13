Imports System.Data.SqlClient

Public Class EditTransaction
    Inherits System.Web.UI.Page

    Dim Id_Transaction As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Id_Transaction = Request.QueryString("id")
        If Not IsPostBack Then

            LoadTransactions()
        End If
    End Sub

    Protected Sub ButtonSaveTransaction_Click(sender As Object, e As EventArgs)
        Try
            UpdateTransaction()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadTransactions()
        Try
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("Brasil_CardConnectionString").ConnectionString
            Dim query As String = $"SELECT Id_Transacao, Numero_Cartao, Valor_Transacao, Data_Transacao, Descricao FROM Transacoes WHERE Id_Transacao = @IdTransacao"

            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdTransacao", Id_Transaction)

                    Try
                        conn.Open()
                        Dim reader As SqlDataReader = cmd.ExecuteReader()

                        ' Se os dados forem encontrados, preencha os TextBox
                        If reader.HasRows Then
                            While reader.Read()
                                TextBoxIdTransaction.Text = reader("Id_Transacao").ToString()
                                TextBoxCardNumber.Text = reader("Numero_Cartao").ToString()
                                TextBoxTransactionValue.Text = reader("Valor_Transacao").ToString()
                                TextBoxTransactionDate.Text = DateTime.Parse(reader("Data_Transacao").ToString()).ToString("yyyy-MM-dd")
                                TextBoxTransactionDescription.Text = reader("Descricao").ToString()
                            End While
                        Else
                            ' Mensagem caso não encontre nenhum dado

                            ' Exibe uma mensagem de erro se não encontrar o registro
                            Response.Write("<script>alert('Transação não encontrada!');</script>")
                        End If

                    Catch ex As Exception
                        ' Exibe mensagem de erro
                        Response.Write("<script>alert('Erro ao buscar a transação: " & ex.Message & "');</script>")
                    End Try
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Sub UpdateTransaction()
        Try
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("Brasil_CardConnectionString").ConnectionString
            Dim query As String = "UPDATE Transacoes SET Numero_Cartao = @Numero_Cartao, Valor_Transacao = @Valor_Transacao, Data_Transacao = @Data_Transacao, Descricao = @Descricao WHERE Id_Transacao = @IdTransacao"

            Dim idTransacao As Integer = TextBoxIdTransaction.Text
            Dim numeroCartao As String = TextBoxCardNumber.Text
            Dim valorTransacao As Decimal = Decimal.Parse(TextBoxTransactionValue.Text)
            Dim dataTransacao As DateTime = DateTime.Parse(TextBoxTransactionDate.Text)
            Dim descricao As String = TextBoxTransactionDescription.Text

            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdTransacao", idTransacao)
                    cmd.Parameters.AddWithValue("@Numero_Cartao", numeroCartao)
                    cmd.Parameters.AddWithValue("@Valor_Transacao", valorTransacao)
                    cmd.Parameters.AddWithValue("@Data_Transacao", dataTransacao)
                    cmd.Parameters.AddWithValue("@Descricao", descricao)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                        ' Exibe uma mensagem de sucesso

                    Catch ex As Exception
                        ' Exibe uma mensagem de erro

                    End Try
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class