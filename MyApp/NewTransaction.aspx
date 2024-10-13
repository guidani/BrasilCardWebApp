<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="NewTransaction.aspx.vb" Inherits="MyApp.NewTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
    <h1 class="">Nova Transação</h1>
    </div>

    <div class="row">
        <div class="mb-3 form-group">
            <asp:Label ID="LabelIdTransaction" runat="server" Text="ID Transação: " CssClass="form-label"></asp:Label>
            <asp:TextBox ID="TextBoxIdTransaction" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorIdTransaction" runat="server" ErrorMessage="Este campo é obrigatório" Display="Dynamic" ControlToValidate="TextBoxIdTransaction"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3 form-group">
            <asp:Label ID="LabelCardNumber" runat="server" Text="Número do cartão: " CssClass="form-label"></asp:Label>
            <asp:TextBox ID="TextBoxCardNumber" runat="server" CssClass="form-control" MaxLength="16"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCardNumber" runat="server" ErrorMessage="Este campo é obrigatório" ControlToValidate="TextBoxCardNumber" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3 form-group">
            <asp:Label ID="LabelTransactionValue" runat="server" Text="Valor: " CssClass="form-label"></asp:Label>
            <asp:TextBox ID="TextBoxTransactionValue" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransactionValue" runat="server" ErrorMessage="Este campo é obrigatório" ControlToValidate="TextBoxTransactionValue" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3 form-group">
            <asp:Label ID="LabelTransactionDate" runat="server" Text="Data da transação: " CssClass="form-label"></asp:Label>
            <asp:TextBox ID="TextBoxTransactionDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransactionDate" runat="server" ErrorMessage="Este campo é obrigatório" ControlToValidate="TextBoxTransactionDate" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3 form-group">
            <asp:Label ID="LabelTransactionDescription" runat="server" Text="Descrição" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="TextBoxTransactionDescription" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mt-3 form-group">
            <asp:Button ID="ButtonSaveTransaction" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="ButtonSaveTransaction_Click"/>
        </div>
    </div>


</asp:Content>
