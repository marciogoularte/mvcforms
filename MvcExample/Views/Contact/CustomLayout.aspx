<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<JL.Web.Forms.Form>" %>
<html>
<body>

<form action="" method="post">
    <div class="fieldWrapper">
        <%=Model["Subject"].Errors%>
        <label for="id_Subject">E-mail subject:</label>
        <%=Model["Subject"] %>
    </div>
    <div class="fieldWrapper">
        <%=Model["Message"].Errors %>
        <label for="id_Message">Your message:</label>
        <%=Model["Message"]%>
    </div>
    <div class="fieldWrapper">
        <%=Model["Sender"].Errors %>
        <label for="id_Sender">Your email address:</label>
        <%=Model["Sender"]%>
    </div>
    <div class="fieldWrapper">
        <%=Model["CCMyself"].Errors%>
        <label for="id_CCMyself">CC yourself?</label>
        <%=Model["CCMyself"]%>
    </div>
    <input type="submit" value="Send message" />
</form>

</body>
</html>