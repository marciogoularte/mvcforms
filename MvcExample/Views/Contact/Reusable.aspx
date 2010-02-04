<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<html>
<body>

<form action="" method="post">
    <% Html.RenderPartial("FormSnippet"); %>
    <p><input type="submit" value="Send message" /></p>
</form>

</body>
</html>