namespace MvcExample.Forms
{
    using System.Collections.Specialized;

    using JL.Web.Forms;
    using Fields = JL.Web.Forms.Fields;


    /// <summary>
    /// Contact form from documentation
    /// </summary>
    /// <see cref="http://mvcforms.codeplex.com/wikipage?title=Working%20with%20forms"/>
    public class ContactForm : Form
    {
        public static IField Subject = new Fields.StringField { MaxLength = 100 };
        public static IField Message = new Fields.StringField { };
        public static IField Sender = new Fields.EmailField { };
        public static IField CCMyself = new Fields.BooleanField { Required = false };

        #region .ctors

        public ContactForm() { }
        public ContactForm(NameValueCollection data) : base(data, null) { }

        #endregion
    }
}
