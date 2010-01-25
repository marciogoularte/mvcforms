<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JL.Web.Forms.Form>" %>

<% foreach (var field in Model) { %>
    <div class="fieldWrapper">
        <%=field.Errors %>
        <%=field.LabelTag %>: <%= field %>
    </div>
<% } %>