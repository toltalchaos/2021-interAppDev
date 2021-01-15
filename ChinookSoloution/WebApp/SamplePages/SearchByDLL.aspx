<%@ Page Title="Search by DLL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchByDLL.aspx.cs" Inherits="WebApp.SamplePages.SearchByDLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        Search Albums By Artist
    </h1>
    <%-- Search area --%>

    <div class="row">
        <div class="offset-3 ">

            <asp:Label ID="Label1" runat="server" Text="Select an Artist"></asp:Label> &nbsp;&nbsp;

            <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>&nbsp;&nbsp;

            <asp:LinkButton ID="SearchAlbumsBTN" runat="server" OnClick="SearchAlbumsBTN_Click">Search<i class="fa fa-search"></i></asp:LinkButton>

        </div>
    </div>

</asp:Content>
