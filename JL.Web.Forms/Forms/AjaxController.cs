namespace JL.Web.Forms
{
    using System.Web.Mvc;
    using System;


    /// <summary>
    /// Controller with extentions to easily enable AJAX validation.
    /// </summary>
    /// <remarks>
    /// Please note, AJAX validation is unsupported with <typeparamref name="FileField"/>s 
    /// (or any sub classes of <typeparamref name="FileField"/>.
    /// Validation of fields that have dependancies on other fields (ie comparing password 
    /// fields) is only supported by the full clean methods. Be aware that full clean will
    /// do full validation of all fields even if values are not supplied for them, this is
    /// important if any custom Clean methods validate against a database or other data
    /// source external to your application.
    /// </remarks>
    public abstract class AjaxController : Controller
    {
        #region Methods

        /// <summary>
        /// Validates form fields passed against a form and returns an Json result.
        /// </summary>
        /// <param name="formToValidate">Form instance to validate against.</param>
        /// <param name="data">Form values to validate.</param>
        /// <returns>Action result containing Json.</returns>
        protected ActionResult CleanForm(Form formToValidate, FormCollection data)
        {
            var fields = formToValidate.Fields;
            var errors = new ErrorDictionary();
            var cleanedData = new NameObjectDictionary();

            foreach (string fieldName in data.Keys)
            {
                IField field;
                // Check if supplied field is valid
                if (fields.TryGetValue(fieldName, out field))
                {
                    object value = field.Widget.GetValueFromDataCollection(data, null, fieldName);
                    try
                    {
                        cleanedData.Add(fieldName, field.Clean(value));

                        // Do custom validation
                        if (field.CustomClean != null)
                        {
                            cleanedData = field.CustomClean(cleanedData);
                        }
                    }
                    catch (ValidationException vex)
                    {
                        errors.Add(fieldName, vex.Messages);
                    }
                }
            }

            if (errors.Count == 0)
            {
                return Json(new { isValid = true });
            }
            else
            {
                return Json(new { isValid = false, errors = errors });
            }
        }

        /// <summary>
        /// Validates a number of form fields within a form and returns an Json result.
        /// </summary>
        /// <param name="formToValidate">IForm instance to validate against.</param>
        /// <param name="data">Form values to validate.</param>
        /// <returns>Action result containing Json.</returns>
        protected ActionResult FullCleanForm(IForm formToValidate, FormCollection data)
        {
            formToValidate.BindData(data, null);
            formToValidate.FullClean();

            if (formToValidate.IsValid)
            {
                return Json(new { isValid = true });
            }
            else
            {
                return Json(new { isValid = false, errors = formToValidate.Errors });
            }
        }

        #endregion
    }
}
