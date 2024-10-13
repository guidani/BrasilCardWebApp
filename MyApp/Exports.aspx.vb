Public Class Exports
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DesabilitaProximosMeses()
        End If
    End Sub

    Private Sub DesabilitaProximosMeses()
        Try
            Dim currentMonth As Integer = New Date().Now.Month
            For Each item As ListItem In DropDownListMonth.Items
                If Convert.ToInt32(item.Value) > currentMonth Then
                    item.Enabled = False
                Else
                    item.Enabled = True
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ButtonExport_Click(sender As Object, e As EventArgs)
        Try
            Dim currentYear As Integer = New Date().Now.Year
            'Dim currentMonth As Integer = New Date().Now.Month

            Dim selectedMonth As Integer = Convert.ToInt32(DropDownListMonth.SelectedValue)

            ' Obter o primeiro dia do mês
            Dim primeiroDia As New DateTime(currentYear, selectedMonth, 1)

            ' Obter o último dia do mês - pega o primeiro dia do mês seguinte e subtrai 1 dia
            Dim ultimoDia As DateTime = primeiroDia.AddMonths(1).AddDays(-1)

            Dim primeiroDiaFormatado = primeiroDia.ToString("yyyy-MM-dd")
            Dim ultimoDiaFormatado = ultimoDia.ToString("yyyy-MM-dd")

            Dim query As String = $"SELECT 
    T.Numero_Cartao,
    T.Valor_Transacao,
    T.Data_Transacao,
	T.Descricao,
    dbo.fn_CategoriaTransacao(T.Valor_Transacao) AS Categoria
FROM Transacoes T
WHERE T.Data_Transacao >= CONVERT(date, '{primeiroDiaFormatado}')  and T.Data_Transacao <= CONVERT(date, '{ultimoDiaFormatado}')"

        Catch ex As Exception

        End Try
    End Sub
End Class