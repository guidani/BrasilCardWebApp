<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="MyApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h1>Transações</h1>
    </div>

    <div class="row">


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" RenderMode="Block" ViewStateMode="Enabled">
           <ContentTemplate>
             
           </ContentTemplate> 
           <Triggers>
               <asp:AsyncPostBackTrigger ControlID="GridViewTransactions" EventName="RowCommand" />
           </Triggers> 
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <p>Carregando...</p>
            </ProgressTemplate>
        </asp:UpdateProgress>


          <asp:GridView 
            ID="GridViewTransactions" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="Id_Transacao" 
            AllowPaging="true"
            AllowSorting="true"
            EmptyDataText="Nenhuma transação encontrada"
            OnRowCommand="GridViewTransactions_RowCommand"
            OnPageIndexChanging="GridViewTransactions_PageIndexChanging"
            >
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Id_Transacao" HeaderText="Id Transacão" SortExpression="Id_Transacao" />
                <asp:BoundField DataField="Numero_Cartao" HeaderText="Numero Cartão" SortExpression="Numero_Cartao" />
                <asp:BoundField DataField="Data_Transacao" HeaderText="Data Transacão" SortExpression="Data_Transacao" />
                <asp:BoundField DataField="Valor_Transacao" HeaderText="Valor Transacão" SortExpression="Valor_Transacao"/>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" />
                <asp:ButtonField  Text="Deletar" ButtonType="Button" CommandName="Delete" ControlStyle-CssClass="btn btn-danger"/>
                <asp:ButtonField  Text="Editar" ButtonType="Button" CommandName="Edit" ControlStyle-CssClass="btn btn-primary"/>
            </Columns>
        </asp:GridView>
        
        <asp:SqlDataSource 
            ID="SqlDataSource1" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:Brasil_CardConnectionString %>"
            
            >

        </asp:SqlDataSource>
    </div>

</asp:Content>
