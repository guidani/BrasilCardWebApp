<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="MyApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h1>Transações</h1>
    </div>

    <div class="row">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_Transacao" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Id_Transacao" HeaderText="Id_Transacao" InsertVisible="False" ReadOnly="True" SortExpression="Id_Transacao" />
                <asp:BoundField DataField="Numero_Cartao" HeaderText="Numero_Cartao" SortExpression="Numero_Cartao" />
                <asp:BoundField DataField="Data_Transacao" HeaderText="Data_Transacao" SortExpression="Data_Transacao" />
                <asp:BoundField DataField="Valor_Transacao" HeaderText="Valor_Transacao" SortExpression="Valor_Transacao" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Brasil_CardConnectionString %>" SelectCommand="SELECT [Id_Transacao], [Numero_Cartao], [Data_Transacao], [Valor_Transacao] FROM [Transacoes]"></asp:SqlDataSource>
    </div>

</asp:Content>
