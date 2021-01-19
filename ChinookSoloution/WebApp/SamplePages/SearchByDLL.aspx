<%@ Page Title="Search by DLL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchByDLL.aspx.cs" Inherits="WebApp.SamplePages.SearchByDLL" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

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
      <div class="row">
        <div class="offset-3 ">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
          <div class="row">
        <div class="offset-3 ">
            <asp:GridView ID="ArtistAlbumList" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-striped" GridLines="Horizontal" BorderStyle="None">

                <Columns>
                    <asp:TemplateField HeaderText="Album">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Released">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Artist">
                        <ItemTemplate>
                            <asp:DropDownList ID="ArtistNameListDDL" runat="server" 
                                DataSourceID="ArtistAlbumListODS" 
                                DataTextField="DisplayField" 
                                DataValueField="ValueField"
                                selectedValue='<%# Eval("ArtistId") %>'>

                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No albums found
                </EmptyDataTemplate>

            </asp:GridView>

            <asp:ObjectDataSource ID="ArtistAlbumListODS" runat="server"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="Artist_DDLList"
                TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
