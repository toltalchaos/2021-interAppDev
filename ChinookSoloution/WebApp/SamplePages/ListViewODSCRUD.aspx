<%@ Page Title="ListView ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ListView ODS CRUD</h1>

    <div class="row">
        <div class="offset-2">
            <blockquote class="alert alert-info">
                this sample will use the ASP:ListView control <br />
                This Sample Will use Object Data Source for the List View Control<br />
                This sample will use MINIMAL code behind <br />
                This sample will use the course MessageUserControl for error handling<br />
                This sample will use validation on the ListView control
            </blockquote>
        </div>
    </div>
    <div class="row">
        <asp:ListView ID="ListView1" runat="server"></asp:ListView>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>

    </div>
</asp:Content>
