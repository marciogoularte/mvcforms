<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JL.Web.Forms.WizardModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Wizard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Form.Head %>
    
    <h2>Wizard</h2>
    
    <p>Step <%= Model.CurrentStep %> of <%= Model.StepCount %></p>
    
    <%= Model.Form.Errors %>
    
    <form action="" method="post"<%= Model.Form.IsMultipart ? " enctype=\"multipart/form-data\"" : "" %>>    
        <table>
            <%= Model.Form %>
            <tr>
                <td></td>
                <td><input type="submit" value="Next" /></td>
            </tr>
        </table>

        <%= Model.HiddenFields %>
    </form>
    
    <%= Model.Form.Footer%>
</asp:Content>
