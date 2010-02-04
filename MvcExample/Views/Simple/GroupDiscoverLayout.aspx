<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcForms.FormGroup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GroupAutoLayout
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <%= Model.Head %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Form Group - Auto layout</h1>
    <p>Layout of this form is auto discovered from the UsePartialView attribute applied to the form.</p>
    
    <% using(Html.BeginMvcForm(Model)) { %>
        <% Html.RenderMvcForm(Model); %>
        <input type="submit" value="Submit" />
    <%} %>
    
    <%= Model.Footer %>
</asp:Content>

