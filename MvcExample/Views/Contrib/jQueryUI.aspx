<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	jQueryUI
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <%= Model.Head %>
    <style type="text/css">
        .ui-datepicker{z-index:50;}
        ul.ui-sortable{list-style-type:none;margin:0;padding:0;}
        ul.ui-sortable li{margin:5px 5px 0 0;padding:10px;float:left;width:64px;height:64px;font-size:4em;text-align:center;}
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>jQueryUI</h2>

    <% using(Html.BeginMvcForm(Model)) { %>
        <table>
            <%= Model%>
            <tr>
                <td></td>
                <td><a href="javascript:AddItem();">Add Item</a></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="submit" value="Submit" /></td>
            </tr>
        </table>
    <%} %>
    
    <script type="text/javascript">
        function AddItem() {
            var item = window.prompt("Message");
            $('#<%= Model["SortableCollection"].AutoId %>').sortablecollection('add', item);
        }
    </script>
    
    <%= Model.Footer %>
</asp:Content>
