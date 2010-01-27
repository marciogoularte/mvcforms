﻿namespace MvcExample.Forms
{
    using System.Collections.Specialized;

    using JL.Web.Forms;
    using Fields = JL.Web.Forms.Fields;
    using Widgets = JL.Web.Forms.Widgets;


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

        #endregion
    }
}