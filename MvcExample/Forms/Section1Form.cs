namespace MvcExample.Forms
{
    using System.Collections.Specialized;

    using MvcForms;
    using Fields = MvcForms.Fields;
    using Widgets = MvcForms.Widgets;


    /// <summary>
    /// Example form - Section 1
    /// </summary>
    public class Section1Form : Form
    {
        #region .ctors

        public Section1Form() { }
        public Section1Form(NameValueCollection data) : base(data, null) { }

        #endregion

        #region Fields

        public static IField FirstName = new Fields.StringField
        {
            MaxLength = 50,
            Messages = new
            {
                required = "First name is required."
            },
        };

        public static IField LastName = new Fields.StringField
        {
            MaxLength = 50,
            Messages = new
            {
                required = "Last name is required."
            },
        };

        public static IField Email = new Fields.EmailField
        {
            MaxLength = 100,
            Messages = new
            {
                required = "Email is required.",
                invalid = "Email address entered is not valid.",
            },
        };

        public static IField LogoUrl = new Fields.UrlField
        {
            VerifyExists = true,
        };

        public static IField Password = new Fields.StringField(new Widgets.PasswordInput())
        {
            Messages = new
            {
                required = "Password is required.",
            },
        };

        public static IField PasswordAgain = new Fields.StringField(new Widgets.PasswordInput())
        {
            Required = false
        };

        #endregion

        #region Clean Methods

        /// <summary>
        /// Clean method
        /// </summary>
        /// <param name="cleanedData"></param>
        /// <returns></returns>
        public static NameObjectDictionary CleanPasswordAgain(NameObjectDictionary cleanedData)
        {
            object password, passwordAgain;

            if (cleanedData.TryGetValue("Password", out password) &&
                cleanedData.TryGetValue("PasswordAgain", out passwordAgain))
            {
                if (!password.Equals(passwordAgain))
                {
                    throw ValidationException.Create("Password fields do not match");
                }
            }
            return cleanedData;
        }

        #endregion
    }
}
