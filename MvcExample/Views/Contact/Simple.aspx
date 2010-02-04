<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>
<html>
<body>

<form action="" method="post">
    <%=Model.AsP %>
    <input type="submit" value="Submit" />
</form>

</body>
</html>