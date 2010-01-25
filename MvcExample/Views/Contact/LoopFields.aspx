<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<JL.Web.Forms.Form>" %>
<html>
<body>

<form action="" method="post">
    <% foreach (var field in Model) { %>
        <div class="fieldWrapper">
            <%=field.Errors %>
            <%=field.LabelTag %>: <%=field %>
        </div>
    <% } %>
    <p><input type="submit" value="Send message" /></p>
</form>

</body>
</html>