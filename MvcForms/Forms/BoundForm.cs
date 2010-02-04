#region License
/******************************************************************************
 * Copyright (c) 2009, Tim Savage - Joocey Labs
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * * Redistributions of source code must retain the above copyright notice, this 
 *   list of conditions and the following disclaimer.
 *    
 * * Redistributions in binary form must reproduce the above copyright notice, this
 *   list of conditions and the following disclaimer in the documentation and/or 
 *   other materials provided with the distribution.
 *    
 * * Neither the name of Joocey Labs nor the names of its contributors may be used
 *   to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *   
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
 * OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/
#endregion

namespace MvcForms
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
            return string.Format(CultureInfo.InvariantCulture, format,
                string.Empty, this.Label, renderedForm);
        }

        #endregion

        #region Render Properties

        /// <summary>
        /// Start output of fieldset (used primarily by HtmlHelperExtensions.RenderMvcForm)
        /// </summary>
        public string StartFieldSet
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "<fieldset{0}>\n<legend>{1}</legend>\n", string.Empty, this.Label);
            }
        }

        /// <summary>
        /// End output of fieldset (used primarily by HtmlHelperExtensions.RenderMvcForm)
        /// </summary>
        public static string EndFieldSet
        {
            get
            {
                return "</fieldset>";
            }
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
