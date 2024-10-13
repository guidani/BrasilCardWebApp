<%@ Page Title="Brasil Card" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="MyApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h1>Transações</h1>
    </div>
    
    <div class="row">
        <div class="col">
            <div class="form-group">
                <asp:Label ID="LabelCardNumberFilter" runat="server" Text="Numero do cartão" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextBoxCardNumberFilter" runat="server" CssClass="form-control" MaxLength="16"></asp:TextBox>
            </div>
        </div>
        <div class="col">

            <div class="form-group">
                <asp:Label ID="LabelTransactionDateFilter" runat="server" Text="Data da transação" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextBoxTransactionDateFilter" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col">
            <div class="form-group">
                <%-- Min value --%>
                <asp:Label ID="LabelMinValue" runat="server" Text="Valor Mínimo"></asp:Label>
                <div class="input-group">

                    <span class="input-group-text">$</span>
                    <asp:TextBox ID="TextBoxTransactionValueFilterMinValue" runat="server" CssClass="form-control" ToolTip="asdasd" placeholder="123,00"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="form-group">
                <%-- Max value --%>
                <asp:Label ID="LabelMaxValue" runat="server" Text="Valor Máximo"></asp:Label>
                <div class="input-group">

                    <span class="input-group-text">$</span>
                    <asp:TextBox ID="TextBoxTransactionValueFilterMaxValue" runat="server" CssClass="form-control" placeholder="123,00"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <div class="form-group my-2">
        <asp:Button ID="ButtonSearch" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="ButtonSearch_Click" />
    </div>


    <div class="row">
        <asp:GridView
            ID="GridViewTransactions"
            runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="Id_Transacao"
            AllowPaging="true"
            AllowSorting="false"
            EmptyDataText="Nenhuma transação encontrada"
            OnRowCommand="GridViewTransactions_RowCommand"
            OnPageIndexChanging="GridViewTransactions_PageIndexChanging"
            CssClass="table table-hover table-striped">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Id_Transacao" HeaderText="Id Transacão" SortExpression="Id_Transacao" />
                <asp:BoundField DataField="Numero_Cartao" HeaderText="Numero Cartão" SortExpression="Numero_Cartao" />
                <asp:BoundField DataField="Data_Transacao" HeaderText="Data Transacão" SortExpression="Data_Transacao" />
                <asp:BoundField DataField="Valor_Transacao" HeaderText="Valor Transacão" SortExpression="Valor_Transacao" />
                <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" />
                <asp:ButtonField Text="Deletar" ButtonType="Button" CommandName="Delete" ControlStyle-CssClass="btn btn-danger" />
                <asp:ButtonField Text="Editar" ButtonType="Button" CommandName="Edit" ControlStyle-CssClass="btn btn-primary" />
            </Columns>
            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right"/>
        </asp:GridView>

        <asp:SqlDataSource
            ID="SqlDataSource1"
            runat="server"
            ConnectionString="<%$ ConnectionStrings:Brasil_CardConnectionString %>"></asp:SqlDataSource>
    </div>

</asp:Content>
