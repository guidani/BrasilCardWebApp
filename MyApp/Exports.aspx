<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Exports.aspx.vb" Inherits="MyApp.Exports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Exportações</h1>
    </div>
    <div class="row">
        <div class="form-group">
            <asp:Label ID="LabelMonth" runat="server" Text="Mês" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="DropDownListMonth" runat="server" CssClass="form-control">
                <asp:ListItem Text="Janeiro" Value="1"></asp:ListItem>
                <asp:ListItem Text="Fevereiro" Value="2"></asp:ListItem>
                <asp:ListItem Text="Março" Value="3"></asp:ListItem>
                <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                <asp:ListItem Text="Maio" Value="5"></asp:ListItem>
                <asp:ListItem Text="Junho" Value="6"></asp:ListItem>
                <asp:ListItem Text="Julho" Value="7"></asp:ListItem>
                <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                <asp:ListItem Text="Setembro" Value="9"></asp:ListItem>
                <asp:ListItem Text="Outubro" Value="10"></asp:ListItem>
                <asp:ListItem Text="Novembro" Value="11"></asp:ListItem>
                <asp:ListItem Text="Dezembro" Value="12"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Esse campo é obrigatório." ControlToValidate="DropDownListMonth" CssClass="text-bg-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="ButtonExport" runat="server" Text="Exportar" CssClass="btn btn-success mt-3" OnClick="ButtonExport_Click" />
        </div>
    </div>
    <div class="row">
                <asp:GridView 
                    ID="GridView1" 
                    runat="server" 
                    AutoGenerateColumns="true" 
                    Visible="false" 
                    AllowPaging="false" 
                    AllowSorting="false" 
                    DataSourceID="SqlDataSource1">

                </asp:GridView>
    </div>

    <%--  --%>
    <asp:SqlDataSource
            ID="SqlDataSource1"
            runat="server"
            ConnectionString="<%$ ConnectionStrings:Brasil_CardConnectionString %>"></asp:SqlDataSource>
</asp:Content>
