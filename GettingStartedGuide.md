

# Working with forms #
While it is possible to process form submissions just using ASP.Net MVC's
form handling features class, using the MVC Forms library takes care of
a number of common form-related tasks. Using it, you can:
  1. Display an HTML form with automatically generated form widgets.
  1. Check submitted data against a set of validation rules.
  1. Redisplay a form in the case of validation errors.
  1. Convert submitted form data to the relevant .Net data types.

Source code for examples included in this overview is included in the
`ContactController` as part of the MvcExample project in source control.

## Overview ##
The library deals with these concepts:
| **Widget** | A class that corresponds to an HTML form widget, e.g. `<input type="text">` or `<textarea>`. This handles rendering of the widget as HTML. |
|:-----------|:-------------------------------------------------------------------------------------------------------------------------------------------|
| **Field**  | A class that is responsible for doing validation, e.g. an `EmailField` that makes sure its data is a valid e-mail address.                 |
| **Form**   | A collection of fields that knows how to validate itself and display itself as HTML.                                                       |
| **Form Media** | The CSS and JavaScript resources that are required to render a form.                                                                       |

## Form objects ##
A Form object encapsulates a sequence of form fields and a collection of
validation rules that must be fulfilled in order for the form to be accepted.
Form classes are created as subclasses of `MvcForms.Form` and makes
use of a declarative style.

For example, consider a form used to implement “contact me” functionality
on a personal Web site:
```
using MvcForms;
using Fields = MvcForms.Fields;

public class ContactForm : Form
{
    public static IField Subject = new Fields.StringField { MaxLength = 100 };
    public static IField Message = new Fields.StringField {};
    public static IField Sender = new Fields.EmailField {};
    public static IField CCMyself = new Fields.BooleanField { Required = false };

    #region .ctors

    public ContactForm () { }
    public ContactForm (NameValueCollection data) : base(data, null) { }

    #endregion
}
```

A form is composed of `IField` objects. In this case, our form has four fields: `Subject`, `Message`, `Sender` and `CCMyself`. `StringField`, `EmailField` and `BooleanField` are just three of the available field types; a full list can be found in [Form fields](FormFields.md).

## Using a form in an action method ##
The standard pattern for processing a form in an action method looks like this:
```
public ActionResult Contact(FormCollection data)
{
    IForm form;
    if (Request.RequestType == "POST")
    {
        form = new ContactForm(data);
        if (form.IsValid)
        {
            // Process the data in form.CleanedData
            return RedirectToAction("Thanks");
        }
    }
    else
    {
        form = new ContactForm();
    }

    return View(form);
}
```

There are three code paths here:
  1. If the form has not been submitted, an unbound instance of `ContactForm` is created and passed to the view.
  1. If the form has been submitted, a bound instance of the form is created from `FormCollection`. If the submitted data is valid, it is processed and the user is re-directed to a "thanks" page.
  1. If the form has been submitted but is invalid, the bound form instance is passed on to the view.

The distinction between bound and unbound forms is important. An unbound form does not have any data associated with it; when rendered to the user, it will be empty or will contain default values. A bound form does have submitted data, and hence can be used to tell if that data is valid. If an invalid bound form is rendered it can include inline error messages telling the user where they went wrong.

**New in 0.4** - MVC forms now includes a [model binder](FormModelBinder.md) for forms. The standard pattern for using forms has been greatly simplified:
```
public ActionResult Contact(ContactForm form)
{
    if (form.IsValid)
    {
        // Process the data in form.CleanedData
        return RedirectToAction("Thanks");
    }
    return View(form);
}
```

The new model binder will create an instance of the `ContactForm`; if data has been submitted, this data will be bound to the form. The process for determining the validity of the submitted data can be taken advantage of to keep Action methods simple. An unbound form is considered to be invalid but any cleaning will not be attempted and errors will not be generated against form fields.

There are two code paths here:
  1. If the submitted data is valid, it is processed and the user is re-directed to a "thanks" page.
  1. If the form not has been submitted or the submitted data is invalid, the form instance is passed on to the view.

## Processing the data from a form ##
Once `IsValid` returns True, you can process the form submission safe in the knowledge that it conforms to the validation rules defined by your form. While you could access Request.Post directly at this point, it is better to access `form.CleanedData`. This data has not only been validated but will also be converted in to the relevant .Net types for you. In the above example, `CCMyself` will be a `bool` value. Likewise, fields such as `IntegerField` and `FloatField` convert values to a .Net int and float respectively.

Extending the above example, here's how the form data could be processed:
```
if (form.IsValid)
{
	var subject = form.CleanedData["Subject"] as string;
	var message = form.CleanedData["Message"] as string;
	var sender = form.CleanedData["Sender"] as string;
	var ccMyself = form.CleanedData["CCMyself"] as bool?;

	var msg = new System.Net.Mail.MailMessage(sender, "info@example.com");
	msg.Subject = subject;
	msg.Body = message;
	msg.IsBodyHtml = false;
	if (ccMyself.Value) 
		msg.To.Add(sender);

	new System.Net.Mail.SmtpClient().Send(msg);

	// Process the data in form.CleanedData
	return RedirectToAction("Thanks");
}
```

## Displaying a form using a view ##
Forms are designed to work with the ASP.NET MVC template language. In the above example, we passed our `ContactForm` instance to the view as the `Model` object. Here's a simple example view:

```
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<form action="" method="post">
    <%=Model.AsP%>
    <input type="submit" value="Submit" />
</form>
```

The form only outputs its own fields; it is up to you to provide the surrounding `<form>` tags and the submit button.

`Model.AsP` will output the form with each form field and accompanying label wrapped in a paragraph. Here's the output for our example view:
```
<form action="" method="post">  
<p><label for="id_Subject">Subject</label> <input class="required" maxlength="100" id="id_Subject" type="text" name="Subject" /></p>
<p><label for="id_Message">Message</label> <input class="required" id="id_Message" type="text" name="Message" /></p>
<p><label for="id_Sender">Sender</label> <input class="required" id="id_Sender" type="text" name="Sender" /></p>
<p><label for="id_CCMyself">C c myself</label> <input id="id_CCMyself" type="checkbox" name="CCMyself" /></p>
    <input type="submit" value="Submit" />
</form>
```
Note that each form field has an ID attribute set to `id_<field-name>`, which is referenced by the accompanying label tag. This is important for ensuring forms are accessible to assistive technology such as screen reader software. You can also customize the way in which labels and ids are generated.

You can also use `Model.AsTable` to output table rows (you'll need to provide your own `<table>` tags) and `Form.AsUl` to output list items.

## Customizing the form view ##
If the default generated HTML is not to your taste, you can completely customize the way a form is presented using the ASP.NET MVC view language. Extending the above example:

```
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<form action="" method="post">
    <div class="fieldWrapper">
        <%=Model["Subject"].Errors%>
        <label for="id_Subject">E-mail subject:</label>
        <%=Model["Subject"]%>
    </div>
    <div class="fieldWrapper">
        <%=Model["Message"].Errors%>
        <label for="id_Message">Your message:</label>
        <%=Model["Message"]%>
    </div>
    <div class="fieldWrapper">
        <%=Model["Sender"].Errors%>
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
```

Each named form-field can be output to the view using `Model["NameOfField"]` , which will produce the HTML needed to display the form widget. Using `Model["NameOfField"].Errors` displays a list of form errors, rendered as an unordered list. This might look like:
```
<ul class="errorlist">
    <li>Sender is required.</li>
</ul>
```

The list has a CSS class of errorlist to allow you to style its appearance. If you wish to further customize the display of errors you can do so by looping over them:

```
<% if (Model["Subject"].HasErrors) { %>
    <ol>
        <% foreach (var error in Model["Subject"].Errors) { %>
            <li><strong><%= error%></strong></li>
        <%} %>
    </ol>
<%} %>
```

## Looping over the form's fields ##
If you're using the same HTML for each of your form fields, you can reduce duplicate code by looping through each field in turn using a <% foreach %> loop:

```
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<form action="" method="post">
    <% foreach (var field in Model) { %>
        <div class="fieldWrapper">
            <%=field.Errors%>
            <%=field.LabelTag%>: <%=field%>
        </div>
    <% } %>
    <p><input type="submit" value="Send message" /></p>
</form>
```
**Changed in MVC Forms 0.4:** prior to version 0.4 the `field.LabelTag` property is a method, to use Label tag in versions prior to 0.4 use `field.LabelTag(field.Label)`.

Within this loop, `field` is an instance of `BoundField`. `BoundField` also has the following attributes, which can be useful in your templates:
| `field.Label` | The label of the field, e.g. E-mail address. |
|:--------------|:---------------------------------------------|
| `field.LabelTag` | The field's label wrapped in the appropriate HTML `<label>` tag, e.g. `<label for="id_email">E-mail address</label>` |
| `field.HtmlName` | The name of the field that will be used in the input element's name field. This takes the form prefix into account, if it has been set. |
| `field.HelpText` | Any help text that has been associated with the field. |
| `field.Errors` | Outputs a `<ul class="errorlist">` containing any validation errors corresponding to this field. You can customize the presentation of the errors with a `<% foreach(var error in field.Errors) %>` loop. In this case, each object in the loop is a simple string containing the error message. |
| `field.IsHidden` | This attribute is true if the form field is a hidden field and False otherwise. |

`IsHidden` is not particularly useful in a view, but could be useful in conditional tests such as:
```
<% if (field.IsHidden) { %>
   <%-- Do something special --%>
<% } %>
```

### Looping over hidden and visible fields ###
If you're manually laying out a form in a view, as opposed to relying on MVC Forms default form layout, you might want to treat `<input type="hidden">` fields differently than non-hidden fields. For example, because hidden fields don't display anything, putting error messages "next to" the field could cause confusion for your users -- so errors for those fields should be handled differently.

MVC Forms provides two properties on a form that allow you to loop over the hidden and visible fields independently: `HiddenFields` and `VisibleFields`. Here's a modification of an earlier example that uses these two methods:
```
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcForms.Form>" %>

<form action="" method="post">
    <% foreach (var hidden in Model.HiddenFields) { %>
        <div class="fieldWrapper">
            <%=hidden%>
        </div>
    <% } %>
    <% foreach (var visible in Model.VisibleFields) { %>
        <div class="fieldWrapper">
            <%=visible.Errors%>
            <%=visible.LabelTag%>: <%= visible%>
        </div>
    <% } %>
    <p><input type="submit" value="Send message" /></p>
</form>
```

This example does not handle any errors in the hidden fields. Usually, an error in a hidden field is a sign of form tampering, since normal form interaction won't alter them. However, you could easily insert some error displays for those form errors, as well.

## Reusable form views ##
If your site uses the same rendering logic for forms in multiple places, you can reduce duplication by saving the form's loop in a partial view and using the `RenderPartial` method to reuse it in other views:

```
<form action="" method="post">
    <% Html.RenderPartial("FormSnippet", Model); %>
    <p><input type="submit" value="Send message" /></p>
</form>

# In FormSnippet.ascx:

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcForms.Form>" %>

<% foreach (var field in Model) { %>
    <div class="fieldWrapper">
        <%=field.Errors%>
        <%=field.LabelTag%>: <%=field%>
    </div>
<% } %>
```


---

This document is based on Django Project documentation, © 2005-2010 Django Software Foundation, © 2010 Tim Savage