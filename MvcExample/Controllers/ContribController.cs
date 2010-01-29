namespace MvcExample.Controllers
{
    using System.Web.Mvc;


    public class ContribController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// jQuery UI widgets example
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult jQueryUI(Forms.Contrib.JQueryUIForm form)
        {
            if (form.IsValid)
            {
                return View("ShowResults", form.CleanedData);
            }
            return View(form);        
        }
    }
}
