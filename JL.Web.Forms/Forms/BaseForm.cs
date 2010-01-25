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

namespace JL.Web.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Text;

    using Extensions;
    using Utils;


    /// <summary>
    /// Base class for Form and FormCollection
    /// </summary>
    public abstract class BaseForm : IForm
    {
        #region Fields

        private ErrorDictionary _errors;
        private Dictionary<string, object> _initial;

        #endregion

        #region .ctors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initial">Initial data to be displayed by form.</param>
        protected BaseForm(Dictionary<string, object> initial)
        {
            _initial = initial;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Data to bind form to</param>
        /// <param name="files">File data to bind form to</param>
        protected BaseForm(NameValueCollection data, HttpFileCollectionBase files)
        {
            this.Data = data;
            this.Files = files;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        /// <param name="files">File data to bind form to.</param>
        /// <param name="initial">Initial data to be displayed by form.</param>
        protected BaseForm(NameValueCollection data, HttpFileCollectionBase files, Dictionary<string, object> initial)
        {
            this.Data = data;
            this.Files = files; 
            _initial = initial;
        }

        #endregion

        #region Render methods

        /// <summary>
        /// Convert form into a HTML string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.AsTable;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add form prefix to a fields name
        /// </summary>
        /// <param name="name">Field name</param>
        /// <returns></returns>
        public string AddPrefix(string name)
        {
            return string.IsNullOrEmpty(this.Prefix) ? name : string.Format(CultureInfo.CurrentUICulture, "{0}-{1}", this.Prefix, name);
        }

        /// <summary>
        /// Do a full clean (validation) on all fields
        /// </summary>
        public virtual void FullClean()
        {
            this._errors = new ErrorDictionary();
        }

        /// <summary>
        /// Virtual method for any form-wide cleaning after Field.clean() has 
        /// been called on every field. Any ValidationException thrown by this
        /// method will not be associated with a particular field.
        /// </summary>
        /// <param name="cleanedData">Data that has been cleaned</param>
        /// <returns></returns>
        protected virtual NameObjectDictionary Clean(NameObjectDictionary cleanedData)
        {
            return cleanedData;
        }

        /// <summary>
        /// Bind data to this "form" instance
        /// </summary>
        /// <param name="data">Data to bind form to</param>
        /// <param name="files">File data to bind form to</param>
        public virtual void BindData(NameValueCollection data, HttpFileCollectionBase files)
        {
            this.Data = data;
            this.Files = files;
        }

        #endregion Methods

        #region Render Properties

        /// <summary>
        /// Render form using a Table layout
        /// </summary>
        /// <remarks>Does not include &gt;table&lt;&gt;/table&lt;</remarks>
        /// <returns></returns>
        public abstract string AsTable { get; }

        /// <summary>
        /// Render form using a UL layout
        /// </summary>
        /// <remarks>Does not include &gt;ul&lt;&gt;/ul&lt;</remarks>
        /// <returns></returns>
        public abstract string AsUL { get; }

        /// <summary>
        /// Render form using a paragraph layout
        /// </summary>
        /// <returns></returns>
        public abstract string AsP { get; }

        /// <summary>
        /// Build media collection from child objects (fields/forms).
        /// </summary>
        /// <returns>Media collection.</returns>
        protected abstract Widgets.Media BuildMediaCollection();

        /// <summary>
        /// Build inline JS collection from child objects (fields/forms).
        /// </summary>
        protected abstract string BuildInlineJS();

        #endregion

        #region Properties

        /// <summary>
        /// Data stored by form.
        /// </summary>
        public NameValueCollection Data { get; private set; }

        /// <summary>
        /// Files stored by form.
        /// </summary>
        public HttpFileCollectionBase Files { get; private set; }

        /// <summary>
        /// Format for html ID attribute.
        /// </summary>
        /// <remarks>Use "{0}" for the name field.</remarks>
        public string AutoId { get; set; }

        /// <summary>
        /// Prefix to append to field names.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Is this form bound to any data.
        /// </summary>
        public bool IsBound { get { return this.Data != null || this.Files != null; } }

        /// <summary>
        /// Label for form.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Get errorDictionary returned from validation(clean) operation.
        /// </summary>
        public ErrorDictionary Errors
        {
            get
            {
                if (this._errors == null)
                {
                    this.FullClean();
                }
                return this._errors;
            }
        }

        /// <summary>
        /// Returns TrueValue if the form has no errorDictionary. Otherwise, FalseValue. If errorDictionary are
        /// being ignored, returns FalseValue.
        /// </summary>
        public bool IsValid
        {
            get { return this.IsBound && this.Errors.Count == 0; }
        }

        /// <summary>
        /// Returns True if the form needs to be multipart-encrypted, i.e. it has
        /// FileInput; otherwise, False.
        /// </summary>
        public abstract bool IsMultipart { get; }

        /// <summary>
        /// Data that has been cleaned.
        /// </summary>
        public abstract ReadOnlyDictionary<string, object> CleanedData { get; }

        /// <summary>
        /// Initial data.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ReadOnlyDictionary<string, object> Initial 
        {
            get
            {
                // Ensure initial is always defined
                if (_initial == null)
                {
                    _initial = new Dictionary<string, object>();
                }
                return new ReadOnlyDictionary<string, object>(_initial);
            }
        }

        /// <summary>
        /// Media required by Widgets.
        /// </summary>
        public Widgets.Media Media 
        {
            get
            {
                // TODO: Cache this result
                return BuildMediaCollection();
            }
        }


        /// <summary>
        /// Any inline JS needed by widgets.
        /// </summary>
        public string InlineJS 
        {
            get
            {
                // TODO: Cache this result
                return BuildInlineJS();
            }
        }

        /// <summary>
        /// All content to be rendered in the head (Css)
        /// </summary>
        public string Head 
        {
            get
            {
                return Media.Css;
            }
        }

        /// <summary>
        /// All content to be rendered in the footer (Media + Inline JS)
        /// </summary>
        public string Footer 
        {
            get
            {
                const string scriptFormat = "<script type=\"text/javascript\">\n//<![CDATA[\n{0}\n//]]>\n</script>";
                var inlineJS = this.InlineJS;
                if (!string.IsNullOrEmpty(inlineJS))
                {
                    inlineJS = string.Format(CultureInfo.CurrentUICulture, scriptFormat, inlineJS);
                }
                return string.Concat(Media.JS, inlineJS);
            }
        }

        #endregion
    }
}
