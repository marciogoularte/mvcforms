namespace MvcExample.Forms
{
    using System.Collections.Specialized;

    using MvcForms;
    using Fields = MvcForms.Fields;
    using Widgets = MvcForms.Widgets;


    /// <summary>
    /// Example form - Section 3
    /// </summary>
    public class Section3Form : Form
    {
        #region .ctors

        public Section3Form() : base(null, null) { }
        public Section3Form(NameValueCollection data) : base(data, null) { }

        #endregion

        #region Fields

        public static IField Logo = new Fields.ImageField {
            MaxSize = 102400, // 100KB
            MaxWidth = 640,
            MaxHeight = 480,
        };

        public static IField Comments = new Fields.StringField(new Widgets.TextArea()) {
            Required = false,
            MinLength = 20,
        };

        public static IField SomeHtml = new Fields.HtmlStringField
        {
        };

        #endregion
    }
}
