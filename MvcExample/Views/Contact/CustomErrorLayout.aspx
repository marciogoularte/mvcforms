<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>
<html>
<body>

<form action="" method="post">
    <div class="fieldWrapper">
        <% if (Model["Subject"].HasErrors) { %>
           <ol>
           <% foreach (var error in Model["Subject"].Errors) { %>
              <li><strong><%= error %></strong></li>
           <%} %>
           </ol>
        <%} %>
        <label for="id_Subject">E-mail subject:</label>
        <%=Model["Subject"] %>
    </div>
    
    <div class="fieldWrapper">
        <% if (Model["Message"].HasErrors) { %>
           <ol>
           <% foreach (var error in Model["Message"].Errors) { %>
              <li><strong><%= error %></strong></li>
           <%} %>
           </ol>
        <%} %>
        <label for="id_Message">Your message:</label>
        <%=Model["Message"]%>
    </div>
    
    <div class="fieldWrapper">
        <% if (Model["Sender"].HasErrors) { %>
           <ol>
           <% foreach (var error in Model["Sender"].Errors) { %>
              <li><strong><%= error %></strong></li>
           <%} %>
           </ol>
        <%} %>
        <label for="id_Sender">Your email address:</label>
        <%=Model["Sender"]%>
    </div>
    
    <div class="fieldWrapper">
        <% if (Model["CCMyself"].HasErrors) { %>
           <ol>
           <% foreach (var error in Model["CCMyself"].Errors) { %>
              <li><strong><%= error %></strong></li>
           <%} %>
           </ol>
        <%} %>
        <label for="id_CCMyself">CC yourself?</label>
        <%=Model["CCMyself"]%>
    </div>
    <input type="submit" value="Send message" />
</form>

</body>
</html>