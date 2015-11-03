

# Bound and unbound forms #
A `Form` instance is either bound to a set of data, or unbound.

If it’s bound to a set of data, it’s capable of validating that data and rendering the form as HTML with the data displayed in the HTML.
If it’s unbound, it cannot do validation (because there’s no data to validate!), but it can still render the blank form as HTML.
```
class Form
```
To create an unbound Form instance, simply instantiate the class:
```
var f = new ContactForm();
```

To bind data to a form, pass the data as a `NameValueCollection` as the first parameter to your `Form` class constructor:
```
var data = new NameValueCollection {
    {"Subject", "hello"},
    {"Message", "Hi there"},
    {"Sender", "foo@example.com"},
    {"CCMyself", "True"},
};

var f = new ContactForm(data);
```
In this collection, the keys are the field names, which correspond to the attributes in your `Form` class. The values are the data you're trying to validate. These will be strings, but there's no requirement that they be strings; the type of data you pass depends on the `Field`, as we'll see in a moment.

## `Form.IsBound` ##
If you need to distinguish between bound and unbound form instances at runtime, check the value of the form's `IsBound` property:
```
var f = new ContactForm(data);
print f.IsBound;
> false

var f = new ContactForm(data);
print f.IsBound;
> true
```
Note that passing an empty dictionary creates a bound form with empty data:
```
var f = new ContactForm(new NameValueCollection());
print f.IsBound;
> true
```
If you have a bound `Form` instance and want to change the data somehow, or if you want to bind an unbound `Form` instance to some data, create another `Form` instance. There is no way to change data in a `Form` instance. Once a `Form` instance has been created, you should consider its data immutable, whether it has data or not.

# Using forms to validate data #

## `Form.IsValid` ##

The primary task of a `Form` object is to validate data. With a bound `Form` instance, check the IsValid property which will run validation and return a `bool` designating whether the data was valid:
```
var data = new NameValueCollection {
    {"Subject", "hello"},
    {"Message", "Hi there"},
    {"Sender", "foo@example.com"},
    {"CCMyself", "True"},
};

var f = new ContactForm(data);
print f.IsValid();
> true
```
Let's try with some invalid data. In this case, subject is blank (an error, because all fields are required by default) and sender is not a valid e-mail address:
```
var data = new NameValueCollection {
    {"Subject", ""},
    {"Message", "Hi there"},
    {"Sender", "invalid e-mail address"},
    {"CCMyself", "True"},
};

var f = new ContactForm(data);
print f.IsValid();
> false
```

## `Form.errors` ##
Access the errors attribute to get a dictionary of error messages.

In this dictionary, the keys are the field names, and the values are collections of  strings representing the error messages. The error messages are stored in collections  because a field can have multiple error messages.

You can access errors without having to check `IsValid` first. The form's data will be validated the first time either you check `IsValid` or access `Errors`.

The validation routines will only get called once, regardless of how many times you access `Errors` or check `IsValid`. This means that if validation has side effects, those side effects will only be triggered once.

## Behavior of unbound forms ##
It's meaningless to validate a form with no data, but, for the record, here's what happens with unbound forms:
```
var f = new ContactForm();
print f.IsValid;
> false
print f.Errors.Count;
> 0
```

# Dynamic initial values #
## `Form.Initial` ##
Use `Initial` to declare the initial value of form fields at runtime. For example, you might want to fill in a username field with the username of the current session.

To accomplish this, use the `Initial` argument to a `Form`. This argument, if given, should be a `NameValueCollection` mapping field names to initial values. Only include the fields for which you're specifying an initial value; it's not necessary to include every field in your form. For example:
```
var f = new ContactForm 
{
    Initial = new NameValueCollection {
        {"Subject", "Hi there!"},
    },
};
```

These values are only displayed for unbound forms, and they're not used as fallback values if a particular value isn't provided.

Note that if a Field defines `Initial` and you include `Initial` when instantiating the `Form`, then the latter `Initial` will have precedence. In this example, `Initial` is provided both at the field level and at the form instance level, and the latter gets precedence:
```
public class CommentForm : Form
{
    public static IField Name = new Fields.StringField { Initial = "Class" };
    public static IField Url = new Fields.URLField{};
    public static IField Comment = new Fields.StringField {};

    #region .ctors

    public CommentForm() { }
    public CommentForm(NameValueCollection data) : base(data, null) { }

    #endregion
}

var f = new CommentForm 
{
    Initial = new NameValueCollection {
        {"Name", "Instance"},
    },
};

print f.ToString()
> <tr><th>Name:</th><td><input type="text" name="Name" value="Instance" /></td></tr>
> <tr><th>Url:</th><td><input type="text" name="Url" /></td></tr>
> <tr><th>Comment:</th><td><input type="text" name="Comment" /></td></tr>
```


---

This document is based on Django Project documentation, &copy; 2005-2010 Django Software Foundation, &copy; 2010 Tim Savage