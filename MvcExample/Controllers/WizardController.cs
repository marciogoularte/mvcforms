using System;
using System.Web.Mvc;

using JL.Web.Forms;


namespace MvcExample.Controllers
{
    [HandleError]
    public class WizardController : JL.Web.Forms.Contrib.WizardController
    {
        public IForm section1 = new Forms.Section1Form {
            Label = "Section 1",
        };

        public IForm exampleGroup = new Forms.ExampleFormGroup {
            Label = "Example Group",
        };

        #region Views

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Complete()
        {
            return View();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the name of the template for a particular step.
        /// </summary>
        /// <param name="step">Step number.</param>
        /// <returns>Name of template.</returns>
        protected override string GetTemplate(int step)
        {
            return string.Concat("wizard-", step);
        }

        #endregion
    }
}
