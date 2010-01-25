<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Simple Examples
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Simple Examples</h2>
    
    <ul>
        <li><%=Html.ActionLink("Basic form", "Form") %></li>
        <li><%=Html.ActionLink("Form Group", "Group") %></li>
        <li><%=Html.ActionLink("Form Group with customised layout", "CustomGroup")%></li>
    </ul>
</asp:Content>
