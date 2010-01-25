<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IDictionary<string, object>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Results
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Results</h2>

    <table>
        <tr>
            <th>Field</th>
            <th>Value</th>
        </tr>
        <% foreach (var pair in Model) {%>
        <tr>
            <td><strong><%= pair.Key %></strong></td>
            <td><%= pair.Value %></td>
        </tr>
        <%} %>
    </table>
    
    <a href=".">&lt; Back</a>
</asp:Content>
