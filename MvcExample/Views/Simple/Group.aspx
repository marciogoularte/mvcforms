<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcForms.FormGroup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Collection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Head %>
    
    <h1>Form Group</h1>
    
    <% using(Html.BeginMvcForm(Model)) { %>
        <%= Model %>
        <input type="submit" value="Submit" />
    <%} %>
    
    <%= Model.Footer %>  
</asp:Content>
