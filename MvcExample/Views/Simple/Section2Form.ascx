<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JL.Web.Forms.Form>" %>

<div>Please insert the details of your preferences.</div>

<% 
    var favColours = Model["FavoriteColours"];
    var yourState = Model["YourState"];
    var price = Model["Price"];
    var includePartner = Model["IncludePartner"];
    var expiryDate = Model["ExpiryDate"];
%>

<style>
    .space { margin-bottom: 1em; }
    .colours ul li 
    {
    	list-style: none;
    	float: left;
    	margin-left: 2em;
    }
    .clear { clear: both; }
    .cols {}
    .cols .col { float: left; width: 20%; margin-right: 5%; }
</style>

<div class="colours space">
    <%= favColours.ApplyLabel(new { style = "color:red;" }) %>
    <%= favColours %>
    <div class="clear"></div>
    <%= favColours.Errors%>
</div>

<div class="space">
    <%= yourState.LabelTag%>
    <%= yourState %>
    <%= yourState.Errors %>
</div>

<div class="cols space">
    <div class="col">
        <%= price.LabelTag %>
        <%= price %>
        <%= price.Errors%>
    </div>
    
    <div class="col">
        <%= includePartner.LabelTag %>
        <%= includePartner %>
        <%= includePartner.Errors%>
    </div>
    
    <div class="clear"></div>
</div>

<div class="space">
    <%= expiryDate.LabelTag%>
    <%= expiryDate %>
    <%= expiryDate.Errors%>
</div>
