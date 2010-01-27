namespace JL.Web.Forms.Html
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
using System.Globalization;


    /// <summary>
    /// Extentions for HtmlHelper class
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Begin an MVC Forms form.
        /// </summary>
        /// <param name="self">HTML Helper.</param>
        /// <param name="form">MVC Forms form instance</param>
        /// <returns></returns>
        public static MvcForm BeginMvcForm(this HtmlHelper self, IForm form)
        {
            object htmlAttributes = null;
            if (form.IsMultipart)
            {
                htmlAttributes = new { enctype = "multipart/form-data" };
            }
            return self.BeginForm(null, null, FormMethod.Post, htmlAttributes);
        }

        /// <summary>
        /// Render a form (and discover any defined partial views).
        /// </summary>
        /// <param name="self">HTML Helper.</param>
        /// <param name="formGroup">Form group to render.</param>
        public static void RenderMvcForm(this HtmlHelper self, FormGroup formGroup)
        {
            var response = self.ViewContext.HttpContext.Response;

            // Render form group
            foreach (var boundForm in formGroup)
            {
                response.Write(boundForm.StartFieldSet);
                RenderMvcForm(self, boundForm.Form);
                response.Write(boundForm.EndFieldSet);
            }
        }

        /// <summary>
        /// Render a form (and discover any defined partial views).
        /// </summary>
        /// <param name="self">HTML Helper.</param>
        /// <param name="form">Form to render.</param>
        public static void RenderMvcForm(this HtmlHelper self, Form form)
        {
            var response = self.ViewContext.HttpContext.Response;

            // Check for partitial view attribute
            var attr = Attribute.GetCustomAttribute(form.GetType(),
                typeof(UsePartialViewAttribute)) as UsePartialViewAttribute;
            if (attr == null)
            {
                // Render the form normally
                response.Write(form.ToString());
            }
            else
            {
                var formType = form.GetType();

                // Establish view name
                string partialViewName;
                if (attr.UseFormName)
                {
                    partialViewName = string.Concat("Forms/", formType.Name);
                }
                else
                {
                    partialViewName = attr.ViewName;
                }

                // Render the form
                self.RenderPartial(partialViewName, form);
            }
        }
    }
}
