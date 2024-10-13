﻿Imports System.Data.SqlClient

Public Class _Default
    Inherits Page

    'Dim cardNumber As String
    'Dim transactionDate As String
    'Dim minValue As String
    'Dim maxValue As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'cardNumber = Request.QueryString("numero_cartao").Trim
        'transactionDate = Request.QueryString("data_transacao").Trim
        'minValue = Request.QueryString("valor_min").Trim
        'maxValue = Request.QueryString("valor_max").Trim

        If Not IsPostBack Then
            If Request.QueryString.Count > 0 Then
                LoadFilter()
            Else
                LoadTransactions()
            End If
            'If String.IsNullOrWhiteSpace(cardNumber) OrElse String.IsNullOrWhiteSpace(transactionDate) OrElse String.IsNullOrWhiteSpace(minValue) OrElse String.IsNullOrWhiteSpace(maxValue) OrElse Then
            '    LoadTransactions()
            'Else
            '    LoadFilter()
            'End If
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
            Dim numeroCartao As String = Request.QueryString("numero_cartao")
            Dim dataTransacao As String = Request.QueryString("data_transacao")
            Dim valorMinStr As String = Request.QueryString("valor_min")
            Dim valorMaxStr As String = Request.QueryString("valor_max")

            If String.IsNullOrEmpty(numeroCartao) AndAlso String.IsNullOrEmpty(dataTransacao) AndAlso String.IsNullOrEmpty(valorMinStr) AndAlso String.IsNullOrEmpty(valorMaxStr) Then
                LoadTransactions()
                Return
            End If

            'Dim sqlds = New SqlDataSource

            Dim query As String = "SELECT Id_Transacao, Numero_Cartao, Data_Transacao, Valor_Transacao, Descricao FROM Transacoes WHERE 1=1"
            Dim parametros As New List(Of SqlParameter)

            ' Filtro por Número do Cartão
            If Not String.IsNullOrEmpty(numeroCartao) Then
                query &= " AND Numero_Cartao = @Numero_Cartao"
                'parametros.Add(New SqlParameter("@Numero_Cartao", numeroCartao))
                SqlDataSource1.SelectParameters.Add("Numero_Cartao", dbType:=DbType.String, numeroCartao)
            End If

            ' Filtro por Data da Transação
            If Not String.IsNullOrEmpty(dataTransacao) Then
                query &= " AND CONVERT(DATE, Data_Transacao) = @Data_Transacao"
                'parametros.Add(New SqlParameter("@Data_Transacao", DateTime.Parse(dataTransacao)))
                SqlDataSource1.SelectParameters.Add("Data_Transacao", dbType:=DbType.Date, DateTime.Parse(dataTransacao).ToString("yyyy-MM-dd"))

            End If

            ' Filtro por Valores Mínimo e Máximo
            Dim valorMin As Decimal = 0
            Dim valorMax As Decimal = Decimal.MaxValue

            If Not String.IsNullOrEmpty(valorMinStr) Then
                valorMin = Decimal.Parse(valorMinStr)
            End If

            If Not String.IsNullOrEmpty(valorMaxStr) Then
                valorMax = Decimal.Parse(valorMaxStr)
            End If

            If valorMin > 0 Then
                query &= " AND Valor_Transacao >= @ValorMin"
                'parametros.Add(New SqlParameter("@ValorMin", valorMin))
                SqlDataSource1.SelectParameters.Add("ValorMin", dbType:=DbType.Decimal, valorMin)

            End If

            If valorMax <> Decimal.MaxValue Then
                query &= " AND Valor_Transacao <= @ValorMax"
                'parametros.Add(New SqlParameter("@ValorMax", valorMax))
                SqlDataSource1.SelectParameters.Add("ValorMax", dbType:=DbType.Decimal, valorMax)

            End If

            For Each param As Parameter In SqlDataSource1.SelectParameters
                Console.WriteLine(param.Name & " = " & param.DefaultValue)
            Next

            Console.WriteLine($"Query >>>>>>>>>>>>> {query}")
            SqlDataSource1.SelectCommand = query
            SqlDataSource1.DataBind()

            GridViewTransactions.DataSource = SqlDataSource1
            GridViewTransactions.DataBind()

        Catch ex As Exception

        End Try
    End Sub
End Class