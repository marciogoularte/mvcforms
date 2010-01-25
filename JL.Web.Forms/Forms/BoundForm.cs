/******************************************************************************
 * Copyright (c) 2009, Tim Savage - Joocey Labs
 * All rights reserved.
 * 
 * See LICENSE.txt or http://mvcforms.codeplex.com/license for lincense 
 * terms and conditions.
 *****************************************************************************/
namespace JL.Web.Forms
{
    using System.Globalization;

    using Extensions;
    using Utils;

    
    /// <summary>
    /// Method of rendering form.
    /// </summary>
    /// <param name="boundForm">Bound form to render.</param>
    /// <returns>HTML string of rendered form.</returns>
    public delegate string RenderMethod(BoundForm boundForm);

    /// <summary>
    /// A form plus data
    /// </summary>
    public sealed class BoundForm
    {
        #region .ctors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="form">Bound form.</param>
        /// <param name="name">Name of form.</param>
        public BoundForm(Form form, string name)
        {
            this.Form = form;
            this.Name = name;
            this.Label = string.IsNullOrEmpty(form.Label) ? FormatHelper.BeautifyName(name) : form.Label;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Convert form into string.
        /// </summary>
        /// <returns>Form rendered to HTML string.</returns>
        public override string ToString()
        {
            return AsTable;
        }

        /// <summary>
        /// Render output of form.
        /// </summary>
        /// <param name="format">Format of output.</param>
        /// <param name="renderedForm">Form rendered into HTML.</param>
        /// <returns>Complete output.</returns>
        private string RenderHtml(string format, string renderedForm)
        {
            return string.Format(CultureInfo.CurrentUICulture, format,
                string.Empty, this.Label, renderedForm);
        }

        /// <summary>
        /// Render form using a Table layout
        /// </summary>
        /// <remarks>Does not include &gt;table&lt;&gt;/table&lt;</remarks>
        /// <returns></returns>
        public string AsTable
        {
            get
            {
                return this.RenderHtml(
                    "<fieldset{0}>\n<legend>{1}</legend>\n<table>{2}</table>\n</fieldset>",
                    Form.AsTable);
            }
        }

        /// <summary>
        /// Render form using a UL layout
        /// </summary>
        /// <remarks>Does not include &gt;ul&lt;&gt;/ul&lt;</remarks>
        /// <returns></returns>
        public string AsUL
        {
            get
            {
                return this.RenderHtml(
                    "<fieldset{0}>\n<legend>{1}</legend>\n<ul>{2}</ul>\n</fieldset>",
                    Form.AsUL);
            }
        }

        /// <summary>
        /// Render form using a paragraph layout
        /// </summary>
        /// <returns></returns>
        public string AsP
        {
            get
            {
                return this.RenderHtml(
                    "<fieldset{0}>\n<legend>{1}</legend>\n<div>{2}</div>\n</fieldset>",
                    Form.AsP);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The form object
        /// </summary>
        public Form Form { get; private set; }

        /// <summary>
        /// Name of the form in collection.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Label for the form fieldset.
        /// </summary>
        public string Label { get; private set; }

        #endregion
    }
}
