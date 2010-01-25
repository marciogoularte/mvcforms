namespace MvcExample.Controllers
{
    using System.Web.Mvc;


    /// <summary>
    /// Example source code from documentation
    /// </summary>
    /// <see cref="http://mvcforms.codeplex.com/wikipage?title=Working%20with%20forms"/>
    public class ContactController : JL.Web.Forms.AjaxController
    {

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Simple view action method
        /// </summary>
        /// <param name="data">Form data.</param>
        /// <returns></returns>
        public ActionResult Simple(FormCollection data)
        {
            Forms.ContactForm form;
            if (Request.RequestType == "POST")
            {
                form = new Forms.ContactForm(data);
                if (form.IsValid)
                {
                    var subject = form.CleanedData["Subject"] as string;
                    var message = form.CleanedData["Message"] as string;
                    var sender = form.CleanedData["Sender"] as string;
                    var ccMyself = form.CleanedData["CCMyself"] as bool?;

                    var msg = new System.Net.Mail.MailMessage(sender, "info@example.com");
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = false;
                    if (ccMyself.Value)
                        msg.To.Add(sender);

                    // Commented out as many people will not have a local SMTP server running
                    //new System.Net.Mail.SmtpClient("localhost").Send(msg);

                    // Process the data in form.CleanedData
                    return RedirectToAction("Thanks");
                }
            }
            else
            {
                form = new Forms.ContactForm();
            }

            return View(form);
        }

        /// <summary>
        /// Custom layout action method with custom layout
        /// </summary>
        /// <param name="data">Form data.</param>
        /// <returns></returns>
        public ActionResult CustomLayout(FormCollection data)
        {
            return Simple(data);
        }

        /// <summary>
        /// Custom error layout action method with custom layout
        /// </summary>
        /// <param name="data">Form data.</param>
        /// <returns></returns>
        public ActionResult CustomErrorLayout(FormCollection data)
        {
            return Simple(data);
        }

        /// <summary>
        /// Looping fields action method with custom layout
        /// </summary>
        /// <param name="data">Form data.</param>
        /// <returns></returns>
        public ActionResult LoopFields(FormCollection data)
        {
            return Simple(data);
        }

        /// <summary>
        /// Reusable action method with custom layout
        /// </summary>
        /// <param name="data">Form data.</param>
        /// <returns></returns>
        public ActionResult Reusable(FormCollection data)
        {
            return Simple(data);
        }
    }
}
