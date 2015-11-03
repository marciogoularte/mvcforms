Extensions provided for the `HtmlHelper` class.



# Setup #
To use the extensions within your views the `JL.Web.Forms.Html` namespace must be added into the _pages.namespaces_ section of your Web.config i.e.:
```
<namespaces>
	<add namespace="System.Web.Mvc"/>
	<add namespace="System.Web.Mvc.Ajax"/>
	<add namespace="System.Web.Mvc.Html"/>
	<add namespace="System.Web.Routing"/>
	<add namespace="System.Linq"/>
	<add namespace="System.Collections.Generic"/>
	<add namespace="JL.Web.Forms.Html"/>
</namespaces>
```

# Methods #
## BeginMvcForm Method ##
This method works very similar to the default `BeginForm` method except the first parameter will always take a `JL.Web.Forms.IForm` object. The encoding type will automatically be set to the correct type for the supplied form.

Just like the default method wrapping the call in a using statement will output a closing tag for your form.

Example:
```
<% using (Html.BeginMvcForm(Model)) { %>
	<table>
		<%=Model %>
		<tr>
			<td></td>
			<td><input type="submit" value="Submit" /></td>
		</tr>
	</table>
<% } %>
```

## RenderMvcForm Method ##
On the surface this method appears to add little, but once you get under the surface it provides some very useful functionality.

This method makes use of the `UsePartialView` attribute to discover what template should be used to render a form or if no template is specified the default render method is used.

This is particularly useful when working with `FormGroups`.

Example:
```
<% using (Html.BeginMvcForm(Model)) { %>
	<table>
		<% Html.RenderMvcForm(Model); %>
		<tr>
			<td></td>
			<td><input type="submit" value="Submit" /></td>
		</tr>
	</table>
<% } %>
```

Example of `UsePartialView` attribute:
```
    [UsePartialView]
    public class ExampleForm : Form
    {
        public static IField MyField = new Fields.StringField();
    }
```

**Note:** for this method to work the view "Forms/ExampleForm.ascx" must be in your view search path.