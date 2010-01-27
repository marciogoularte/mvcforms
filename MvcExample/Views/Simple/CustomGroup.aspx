<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JL.Web.Forms.FormGroup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Custom Layout
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Head %>
    
    <h1>Form Group - With custom layout</h1>
        
    <% using(Html.BeginMvcForm(Model)) { %>
        <%=Model.Errors%>
        
        <fieldset>
            <legend><%= Model["Section2"].Label %></legend>
            
            <% Html.RenderPartial("Forms/Section2Form", Model["Section2"].Form); %>
        </fieldset>
        
        <fieldset>
            <legend><%= Model["Section3"].Label%></legend>
            
            <table>
                <%= Model["Section3"].Form %>
            </table>
        </fieldset>
        
        <input type="submit" value="Submit" />
    <%} %>
    
    <%= Model.Footer %>  
</asp:Content>
