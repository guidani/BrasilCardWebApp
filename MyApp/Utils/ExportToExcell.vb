Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports System.Data
Imports System.IO
Imports System.Web.UI.Page
Imports ClosedXML.Excel

Public Class ExportToExcell


    Public Shared Sub ExportWithClosedXML(query As String, page As Page)
        Try
            Dim connString As String = ConfigurationManager.ConnectionStrings("Brasil_CardConnectionString").ConnectionString
            Using con As New SqlConnection(connString)
                Using cmd As New SqlCommand(query)
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Using wb As New XLWorkbook()
                                wb.Worksheets.Add(dt, "Transacoes")

                                page.Response.Clear()
                                page.Response.Buffer = True
                                page.Response.Charset = ""
                                page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                page.Response.AddHeader("content-disposition", "attachment;filename=Transacoes.xlsx")
                                Using MyMemoryStream As New MemoryStream()
                                    wb.SaveAs(MyMemoryStream)
                                    MyMemoryStream.WriteTo(page.Response.OutputStream)
                                    page.Response.Flush()
                                    page.Response.End()
                                End Using
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class
