<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcForms.BaseForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Wizard landing page.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Wizard landing page.</h2>
    
    <%= Html.ActionLink("Start wizard", "Step") %>

</asp:Content>
