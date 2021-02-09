<%@ Page Title="ods repeater" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSrepreater.aspx.cs" Inherits="WebApp.SamplePages.ODS_repreater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1> Repeater With Nested Query</h1>


    <div class="row">

        <div class="offset-2">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>

    </div>


    <div class="row">

        <div class="offset-2">
            <%-- item type to aquire viewmodel  --%>
            <asp:Repeater ID="goofyGrape" runat="server" DataSourceID="goofyGrapeODS"
                ItemType="ChinookSystem.ViewModels.EmployeeCustomerList">
                <HeaderTemplate>
                    <h3> sales support employees</h3>
                </HeaderTemplate>
                <ItemTemplate>
                    <br />
                    <%# Item.Employee %> ( <%# Item.Title %> ) has 
                    <%# Item.CustomerSupportCount %> customers <br /> <br />


                    <%-- grid view inserted in repeater - use repeater for data property --%>
                    <asp:GridView ID="SupportedCustomers" runat="server"
                         DataSource="<%# Item.CustomerSupportItems %>">


                    </asp:GridView>

                </ItemTemplate>
            </asp:Repeater>


            <asp:ObjectDataSource ID="goofyGrapeODS" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="Employee_EmployeeCustomerList"   OnSelected="SelectCheckForException"
                TypeName="ChinookSystem.BLL.EmployeeController"></asp:ObjectDataSource>
        </div>

    </div>


</asp:Content>
