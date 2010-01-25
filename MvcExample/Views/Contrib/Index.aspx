<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Contrib Examples
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Contrib Examples</h2>
    
    <ul>
        <li><%=Html.ActionLink("jQuery UI", "jQueryUI") %></li>
    </ul>

</asp:Content>
