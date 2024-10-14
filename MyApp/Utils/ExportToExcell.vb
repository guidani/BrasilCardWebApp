Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports System.Data
Imports System.IO
Imports System.Web.UI.Page
Imports ClosedXML.Excel

Public Class ExportToExcell

    Public Shared Sub Export(gv As GridView)
        Try
            Using sw As New StringWriter

                Using htw = New HtmlTextWriter(sw)

                    Dim table As New Table()

                    table.Rows.Add(gv.HeaderRow)

                    For Each row As GridViewRow In gv.Rows
                        table.Rows.Add(row)
                    Next

                    table.RenderControl(htw)

                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "exemplo.xlsx"))
                    HttpContext.Current.Response.ContentType = "application/ms-excel"
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.End()
                End Using
            End Using
        Catch ex As Exception

        End Try


    End Sub

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
                                page.Response.ContentType = "application/vnd.ms-excel"
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
