<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Head %>
    
    <h1>Simple form</h1>
    
    <% using(Html.BeginMvcForm(Model)) { %>
        <table>
            <%= Model %>
            <tr>
                <td></td>
                <td><input type="submit" value="Submit" /></td>
            </tr>
        </table>
    <%} %>
    
    <%= Model.Footer %>
</asp:Content>
