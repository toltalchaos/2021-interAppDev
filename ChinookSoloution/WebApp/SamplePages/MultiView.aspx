<%@ Page Title="multiview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiView.aspx.cs" Inherits="WebApp.SamplePages.MultiView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Multiview controll setup</h1>
    <div class="row">
        <div class="offset-1">
            <asp:Label ID="Label1" runat="server" Text="common data or controlls on webpage independant of multiview"></asp:Label>
        </div>
    </div>

     <div class="row">
        <div class="offset-1">
            <asp:Menu ID="Menu1" runat="server" Font-Bold="true" Orientation="Horizontal"
                CssClass="tabs" StaticSelectedStyle-CssClass="selectedTab" StaticSelectedStyle-BackColor="LightBlue"
                 StaticMenuItemStyle-HorizontalPadding="50px" OnMenuItemClick="Menu1_MenuItemClick">

                <Items>
                    <asp:MenuItem Selected="True" Text="page 1" ToolTip="page 1" Value="0"></asp:MenuItem>
                    <asp:MenuItem Text="page 2" Value="1"></asp:MenuItem>
                    <asp:MenuItem Text="page 3" Value="2"></asp:MenuItem>
                </Items>

            </asp:Menu>
        </div>
    </div>
    <div class="row">
        <div class="offset-1">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                <asp:View ID="View1" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="IODataV1" runat="server"></asp:TextBox>
                    <asp:Button ID="SendTo2From1" runat="server" Text="SendTo2From1" OnClick="SendTo2From1_Click" />
                    <asp:Button ID="SendTo3From1" runat="server" Text="SendTo3From1" OnClick="SendTo3From1_Click" />
                </asp:View>

                <asp:View ID="View2" runat="server">
                     <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="IODataV2" runat="server"></asp:TextBox>
                    <asp:Button ID="SendTo1From2" runat="server" Text="SendTo1From2" OnClick="SendTo1From2_Click" />
                    <asp:Button ID="SendTo3From2" runat="server" Text="SendTo3From2" OnClick="SendTo3From2_Click" />
                </asp:View>

                <asp:View ID="View3" runat="server">
                     <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="IODataV3" runat="server"></asp:TextBox>
                    <asp:Button ID="SendTo1From3" runat="server" Text="SendTo1From3" OnClick="SendTo1From3_Click" />
                    <asp:Button ID="SendTo2From3" runat="server" Text="SendTo2From3" Height="42px" OnClick="SendTo2From3_Click" />
                </asp:View>

            </asp:MultiView>
        </div>
    </div>


</asp:Content>
