namespace JL.Web.Forms.Html
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;


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
    }
}
