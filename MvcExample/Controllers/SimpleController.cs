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
        public ActionResult Form(FormCollection data)
        {
            Forms.Section1Form form;
            if (Request.RequestType == "POST")
            {
                form = new Forms.Section1Form(data);
                if (form.IsValid)
                {
                    return View("ShowResults", form.CleanedData);
                }
            }
            else
            {
                form = new Forms.Section1Form();
            }
            
            return View(form);
        }

        /// <summary>
        /// A form group
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Group(FormCollection data)
        {
            Forms.ExampleFormGroup formGroup;
            if (Request.RequestType == "POST")
            {
                formGroup = new Forms.ExampleFormGroup(Request.Form, Request.Files);
                if (formGroup.IsValid)
                {
                    return View("ShowResults", formGroup.CleanedData);
                }
            }
            else
            {
                formGroup = new Forms.ExampleFormGroup();
            }

            return View(formGroup);
        }

        /// <summary>
        /// A form group with customised layout
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult CustomGroup(FormCollection data)
        {
            return Group(data);
        }
    }
}
