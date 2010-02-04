<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcForms.Form>" %>

<% foreach (var field in Model) { %>
    <div class="fieldWrapper">
        <%=field.Errors %>
        <%=field.LabelTag %>: <%= field %>
    </div>
<% } %>