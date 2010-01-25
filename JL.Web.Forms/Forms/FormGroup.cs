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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Reflection;
    using System.Web;

    using Extensions;
    using Utils;

   
    /// <summary>
    /// Grouping of Form objects
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "This is the most correct name for this element")]
    public class FormGroup : BaseForm, IEnumerable<BoundForm>
    {
        #region Fields

        private IDictionary<string, Form> _forms;
        private ReadOnlyDictionary<string, object> _cleanedData;

        #endregion

        #region .ctors

        /// <summary>
        /// Construct form object
        /// </summary>
        public FormGroup() : this(null, null) { }
        /// <summary>
        /// Construct form object
        /// </summary>
        /// <param name="data">Data to bind form to</param>
        public FormGroup(NameValueCollection data) : this(data, null) { }
        /// <summary>
        /// Construct form object
        /// </summary>
        /// <param name="data">Data to bind form to</param>
        /// <param name="files">File data to bind form to</param>
        public FormGroup(NameValueCollection data, HttpFileCollectionBase files)
            : base(data, files)
        {
            this._forms = BuildFormCollection();
        }

        #endregion

        #region Reflection methods

        /// <summary>
        /// Obtain a collection of forms
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, Form> BuildFormCollection()
        {
            // Build field dictionary
            var forms = new Dictionary<string, Form>();

            // Loop through this form fields
            foreach (FieldInfo fieldInfo in this.GetType().GetFields())
            {
                object obj = fieldInfo.GetValue(this);
                var form = obj as Form;
                if (form != null)
                {
                    // Setup for to work inside container
                    form.Prefix = this.AddPrefix(fieldInfo.Name);
                    form.BindData(this.Data, this.Files);

                    // Add to collection
                    forms.Add(fieldInfo.Name, form);
                }
            }

            return forms;
        }

        #endregion

        #region Rendering Methods

        /// <summary>
        /// Render out collection as HTML
        /// </summary>
        /// <returns></returns>
        protected string RenderHtml(RenderMethod renderMethod)
        {
            List<string> output = new List<string>(this._forms.Count); // Help the list be guessing size.

            foreach (BoundForm bf in this)
            {
                output.Add(renderMethod(bf));
            }

            return string.Join("\n", output.ToArray());
        }

        /// <summary>
        /// Build media collection from forms.
        /// </summary>
        /// <returns>Media collection.</returns>
        protected override Widgets.Media BuildMediaCollection()
        {
            var media = new Widgets.Media();
            foreach (var form in this._forms.Values)
            {
                media.Append(form.Media);
            }
            return media;
        }

        /// <summary>
        /// Build inline JS collection from child objects (fields/forms).
        /// </summary>
        protected override string BuildInlineJS()
        {
            var output = new List<string>(_forms.Count);
            foreach (var form in this._forms.Values)
            {
                output.Add(form.InlineJS);
            }
            return string.Join("\n", output.ToArray());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get BoundField enumerator
        /// </summary>
        /// <returns>Bound field</returns>
        public IEnumerator<BoundForm> GetEnumerator()
        {
            foreach (var pair in this._forms)
            {
                yield return new BoundForm(pair.Value, pair.Key);
            }
        }

        /// <summary>
        /// Return generic IEnumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Do a full clean (validation) on all fields
        /// </summary>
        public override void FullClean()
        {
            base.FullClean();
            if (!this.IsBound) return; // No point continuing 

            foreach (var bf in this)
            {
                // Migrate errors to our local version
                if (!bf.Form.IsValid)
                {
                    this.Errors.Append(bf.Form.Errors);
                }
            }
        }

        /// <summary>
        /// Bind data to this "form" instance
        /// </summary>
        /// <param name="data">Data to bind form to</param>
        /// <param name="files">File data to bind form to</param>
        public override void BindData(NameValueCollection data, HttpFileCollectionBase files)
        {
            base.BindData(data, files);

            // Bind data to sub forms
            foreach (var bf in this)
            {
                bf.Form.BindData(data, files);
            }
        }

        #endregion

        #region Render Properties

        /// <summary>
        /// Render form using a Table layout
        /// </summary>
        /// <remarks>Does not include &gt;table&lt;&gt;/table&lt;</remarks>
        public override string AsTable
        {
            get { return this.RenderHtml(bf => bf.AsTable); }
        }

        /// <summary>
        /// Render form using a UL layout
        /// </summary>
        /// <remarks>Does not include &gt;ul&lt;&gt;/ul&lt;</remarks>
        public override string AsUL
        {
            get { return this.RenderHtml(bf => bf.AsUL); }
        }

        /// <summary>
        /// Render form using a paragraph layout
        /// </summary>
        public override string AsP
        {
            get { return this.RenderHtml(bf => bf.AsP); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indexor
        /// </summary>
        /// <param name="name">Field name</param>
        /// <returns>Bound field</returns>
        public BoundForm this[string name]
        {
            get
            {
                try
                {
                    return new BoundForm(this._forms[name], name);
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException(
                        string.Format(CultureInfo.InvariantCulture, "Form {0} not found in FormCollection", name));
                }
            }
        }

        /// <summary>
        /// Data that has been cleaned.
        /// </summary>
        public override ReadOnlyDictionary<string, object> CleanedData
        {
            get 
            {
                if (this._cleanedData == null)
                {
                    // Calculate total size (and check all data is valid)
                    int capacity = 0;
                    foreach (var form in _forms.Values)
                    {
                        if (!form.IsValid) return null;
                        capacity += form.CleanedData.Count;
                    }

                    // Create new dictionary
                    var cleanedData = new Dictionary<string, object>(capacity);

                    // Copy data into dictionary
                    foreach (var form in _forms.Values) cleanedData.Append(form.CleanedData);

                    // Cache the result
                    this._cleanedData = new ReadOnlyDictionary<string, object>(cleanedData);
                }
                return this._cleanedData;
            }
        }

        /// <summary>
        /// Returns True if the form needs to be multipart-encrypted, i.e. it has
        /// FileInput; otherwise, False.
        /// </summary>
        public override bool IsMultipart
        {
            get
            {
                foreach (var form in _forms.Values)
                {
                    if (form.IsMultipart) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Forms that make up this Group.
        /// </summary>
        public ReadOnlyDictionary<string, Form> Forms
        {
            get { return new ReadOnlyDictionary<string, Form>(_forms); }
        }

        #endregion
    }
}
