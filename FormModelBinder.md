Model Binder for `Form` instances.

# Setup #
To use the form model binder you need to add the binder to `ModelBinderDictionary` in your global.asax.cs file.

Unfortunately the model binders dictionary does not work with interfaces. Specifying IForm as the type for your binder will not work for **all** form objects. To get around this limitation I would recommend using the `SmartBinder` pattern as described by Jimmy Bogard in this [post](http://www.lostechies.com/blogs/jimmy_bogard/archive/2009/03/17/a-better-model-binder.aspx).

An implementation of this pattern has been included in the [MvcExample project](http://code.google.com/p/mvcforms/source/browse/#hg/MvcExample/Binder).

# Use #
Once the binder has been setup, any time a `Form` (or `FormGroup`) object is passed into a action method it will automatically be created by the binder, on post back it will also be bound and cleaned.

Example action method:
```
public ActionResult ContactForm(Forms.ContactForm form)
{
    if (form.IsValid)
    {
        // Do actions
    }
    return View(form);
}
```