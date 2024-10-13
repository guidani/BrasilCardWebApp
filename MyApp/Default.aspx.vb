Imports System.Data.SqlClient

Public Class _Default
    Inherits Page

    Dim cardNumber As String
    Dim transactionDate As String
    Dim minValue As String
    Dim maxValue As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        cardNumber = Request.QueryString("numero_cartao").Trim
        transactionDate = Request.QueryString("data_transacao").Trim
        minValue = Request.QueryString("valor_min").Trim
        maxValue = Request.QueryString("valor_max").Trim

        If Not IsPostBack Then
            If String.IsNullOrWhiteSpace(cardNumber) OrElse String.IsNullOrWhiteSpace(transactionDate) OrElse String.IsNullOrWhiteSpace(minValue) OrElse String.IsNullOrWhiteSpace(maxValue) OrElse Then
                LoadTransactions()
            Else
                LoadFilter()
            End If
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

    Protected Sub ButtonSearch_Click(sender As Object, e As EventArgs)
        Try
            Dim cardNumber = TextBoxCardNumberFilter.Text
            Dim transactionDate = TextBoxTransactionDateFilter.Text
            Dim minValue = TextBoxTransactionValueFilterMinValue.Text
            Dim maxValue = TextBoxTransactionValueFilterMaxValue.Text

            Dim query As String = $"?numero_cartao={cardNumber}&data_transacao={transactionDate}&valor_min={minValue}&valor_max={maxValue}"

            Response.Redirect("Default.aspx" & query)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadFilter()
        Try
            Dim sb As New StringBuilder
            sb.Append("SELECT Id_Transacao, Numero_Cartao, Valor_Transacao, Data_Transacao, Descricao FROM Transacoes WHERE ")

            If Not String.IsNullOrEmpty(cardNumber) Then
                sb.Append($" Numero_Cartao = '{cardNumber}' ")
            End If

            Dim query As String = sb.ToString


        Catch ex As Exception

        End Try
    End Sub
End Class