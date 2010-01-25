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
        public ActionResult jQueryUI(FormCollection data)
        {
            Forms.Contrib.JQueryUIForm form;
            if (Request.RequestType == "POST")
            {
                form = new Forms.Contrib.JQueryUIForm(data);
                if (form.IsValid)
                {
                    return View("ShowResults", form.CleanedData);
                }
            }
            else
            {
                form = new Forms.Contrib.JQueryUIForm();
            }
            
            return View(form);        
        }
    }
}
