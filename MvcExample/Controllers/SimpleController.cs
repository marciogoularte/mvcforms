namespace MvcExample.Controllers
{
    using System.Web.Mvc;
    using System.Net.Mail;

    using JL.Web.Forms;


    [HandleError]
    public class SimpleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// A basic form
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Form(Forms.Section1Form form)
        {
            if (form.IsValid)
            {
                return View("ShowResults", form.CleanedData);
            }
            return View(form);
        }

        /// <summary>
        /// A form group
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Group(Forms.ExampleFormGroup formGroup)
        {
            if (formGroup.IsValid)
            {
                return View("ShowResults", formGroup.CleanedData);
            }
            return View(formGroup);
        }

        /// <summary>
        /// A form group with customised layout
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult CustomGroup(Forms.ExampleFormGroup formGroup)
        {
            return Group(formGroup);
        }

        /// <summary>
        /// A form group with discovered layout
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult GroupDiscoverLayout(Forms.ExampleFormGroup formGroup)
        {
            return Group(formGroup);
        }
    }
}
