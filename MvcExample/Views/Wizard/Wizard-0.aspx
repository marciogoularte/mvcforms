<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JL.Web.Forms.WizardModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Wizard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Form.Head %>
    
    <h2>Wizard</h2>
    
    <p>Step <%= Model.CurrentStep %> of <%= Model.StepCount %></p>
    
    <%= Model.Form.Errors %>
    
    <% using (Html.BeginMvcForm(Model.Form))
       { %>
        <table>
            <%= Model.Form%>
            <tr>
                <td></td>
                <td><input type="submit" value="Next" /></td>
            </tr>
        </table>

        <%= Model.HiddenFields%>
    <%} %>
    
    <%= Model.Form.Footer%>
</asp:Content>
