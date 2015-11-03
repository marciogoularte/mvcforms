## Form Groups ##
**Why does validation behave oddly with form groups, it only works sometimes?**

Make sure forms are not defined as static in your group class.
Fields should be static but Forms should not i.e.:
```
public class MyForm : Form
{
    // Constructor removed

    public static IField FirstName = new Fields.StringField();
    public static IField LastName = new Fields.StringField();
}

public class MyOtherForm : Form
{
    // Constructor removed

    public static IField Email = new Fields.EmailField();
    public static IField Age = new Fields.IntegerField();
}

public class MyFormCollection : FormGroup
{
    // Constructor removed

    public IForm UserDetails = new MyForm();
    public IForm MoreDetails = new MyOtherForm();
}
```