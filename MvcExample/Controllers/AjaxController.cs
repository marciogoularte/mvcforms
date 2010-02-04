namespace MvcExample.Controllers
{
    using System.Web.Mvc;
    using MvcForms;

    public class AjaxController : MvcForms.AjaxController
    {
        [OutputCache(Duration = 30, VaryByParam = "")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Ajax validation method.
        /// </summary>
        /// <param name="data">Data to validate.</param>
        /// <returns>Json data with results of validation.</returns>
        public ActionResult SimpleAjax(FormCollection data)
        {
            return CleanForm(new Forms.ContactForm(), data);
        }

        /// <summary>
        /// Ajax full form validation method.
        /// </summary>
        /// <param name="data">Data to validate.</param>
        /// <returns>Json data with results of validation.</returns>
        public ActionResult SimpleAjaxFull(FormCollection data)
        {
            return FullCleanForm(new Forms.ContactForm(), data);
        }
    }
}
