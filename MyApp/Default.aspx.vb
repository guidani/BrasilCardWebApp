Imports System.Data.SqlClient

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadTransactions()
        End If
    End Sub

    Private Sub LoadTransactions()
        Try
            Dim query As String = "SELECT Id_Transacao, Numero_Cartao, Valor_Transacao, Data_Transacao, Descricao FROM Transacoes"

            SqlDataSource1.SelectCommand = query
            SqlDataSource1.DataBind()

            GridViewTransactions.DataSource = SqlDataSource1
            GridViewTransactions.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridViewTransactions_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "Delete" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim idTransacao As Integer = Convert.ToInt32(GridViewTransactions.DataKeys(index).Value)
                DeletarTransacao(idTransacao)
                'LoadTransactions()
                Page.Response.Redirect(Page.Request.Url.ToString(), True)
            ElseIf e.CommandName = "Edit" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim idTransacao = GridViewTransactions.DataKeys(index).Value
                Response.Redirect("/EditTransaction?id=" + idTransacao, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeletarTransacao(idTransacao As Integer)
        Try
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("Brasil_CardConnectionString").ConnectionString
            Dim query As String = "DELETE FROM Transacoes WHERE Id_Transacao = @IdTransacao"

            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdTransacao", idTransacao)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception

                    End Try
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridViewTransactions_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridViewTransactions.PageIndex = e.NewPageIndex
        LoadTransactions()
    End Sub
End Class