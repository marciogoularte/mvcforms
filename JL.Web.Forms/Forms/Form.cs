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

    using Extensions;
    using Utils;


    /// <summary>
    /// Form class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "Not technically a collection, form is a more appropriate name.")]
    public class Form : BaseForm, IEnumerable<BoundField>
    {
        #region Fields

        /// <summary>
        /// Error name of non-field errorDictionary raised in Clean()
        /// </summary>
        public const string NonFieldErrors = "__all__";

        /// <summary>
        /// Default value used for label suffix
        /// </summary>
        public const string DefaultLabelSuffix = ":";

        /// <summary>
        /// Default value used for AutoId
        /// </summary>
        public const string DefaultAutoId = "id_{0}";

        /// <summary>
        /// List of chars counted as punctuation
        /// </summary>
        private static List<char> Punctuation = new List<char>() { ':', '?', '.', '!' };

        private IDictionary<string, IField> _fields;
        private Dictionary<string, object> _cleanedData;

        #endregion

        #region .ctors

        /// <summary>
        /// Construct form object.
        /// </summary>
        public Form() : this(null, null) { }

        /// <summary>
        /// Construct form object.
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        public Form(NameValueCollection data) : this(data, null) { }

        /// <summary>
        /// Construct form object.
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        /// <param name="files">File data to bind form to.</param>
        public Form(NameValueCollection data, HttpFileCollectionBase files)
            : base(data, files)
        {
            this._fields = FieldCache.Instance.FetchOrCreateFieldDictionary(this);
            this.AutoId = DefaultAutoId;
            this.LabelSuffix = DefaultLabelSuffix;
        }

        #endregion

        #region Rendering Methods

        /// <summary>
        /// Helper function for outputting HTML. Used by asTable(), asUl(), asP().
        /// </summary>
        /// <remarks>
        /// Normal Row formatting placeholders:
        ///     {0} - Label
        ///     {1} - Field
        ///     {2} - Help Text
        ///     {3} - Errors
        /// </remarks>
        /// <param name="normalRow">Format of a normal row.</param>
        /// <param name="errorRow">Format of a error row.</param>
        /// <param name="rowEnder">HTML at the end of a normal row.</param>
        /// <param name="helpTextHtml">Format of text for a help row.</param>
        /// <param name="errorsOnSeparateRow">Place errorDictionary on a seperate row.</param>
        /// <returns></returns>
        protected string RenderHtml(string normalRow, string errorRow, string rowEnder, string helpTextHtml, bool errorsOnSeparateRow)
        {
            const string HiddenFieldErrorFormat = "(Hidden field {0}) {1}";

            List<string> output = new List<string>(this._fields.Count); // Help the list be guessing size.
            ErrorCollection topErrors = Errors.Get(NonFieldErrors, new ErrorCollection());
            List<string> hiddenFields = new List<string>();
            foreach (BoundField bf in this)
            {
                // Get the errorDictionary
                ErrorCollection bfErrors = new ErrorCollection(bf.Errors);

                if (bf.IsHidden)
                {
                    // Append errorDictionary to top errorDictionary
                    foreach (var error in bfErrors)
                    {
                        topErrors.Add(string.Format(CultureInfo.CurrentUICulture,
                            HiddenFieldErrorFormat, bf.Name, error));
                    }
                    hiddenFields.Add(bf.ToString());
                }
                else
                {
                    if (errorsOnSeparateRow)
                    {
                        output.Add(string.Format(CultureInfo.CurrentUICulture, errorRow, bfErrors));
                    }

                    // Format label
                    string label = bf.Label;
                    if (!string.IsNullOrEmpty(label))
                    {
                        label = HttpUtility.HtmlEncode(label);

                        // Only add the suffix if the label does not end in punctuation.
                        if (!string.IsNullOrEmpty(this.LabelSuffix))
                        {
                            if (Punctuation.Contains(label[label.Length - 1]))
                            {
                                label = string.Concat(label, this.LabelSuffix);
                            }
                        }

                        label = bf.ApplyLabel(label, (ElementAttributesDictionary)null);
                    }
                    else
                    {
                        label = string.Empty;
                    }

                    // Format help text
                    string helpText = string.Empty;
                    if (!string.IsNullOrEmpty(bf.HelpText))
                    {
                        helpText = string.Format(CultureInfo.CurrentUICulture,
                            helpTextHtml, HttpUtility.HtmlEncode(bf.HelpText));
                    }

                    // Append result
                    output.Add(string.Format(CultureInfo.CurrentUICulture,
                        normalRow, label, bf, helpText, bfErrors));
                }
            }

            // Render errorDictionary
            if (topErrors.Count > 0)
            {
                output.Insert(0, topErrors.ToString());
            }

            // Render hidden fields
            if (hiddenFields.Count > 0)
            {
                if (output.Count > 0)
                {
                    int lastRowIndex = output.Count - 1;
                    string lastRow = output[lastRowIndex];

                    // Chop off the trailing rowEnder (e.g. "</td></tr>") and
                    // insert the hidden fields.
                    if (!lastRow.EndsWith(rowEnder, StringComparison.OrdinalIgnoreCase))
                    {
                        // This can happen in the AsP() case (and possibly other
                        // user defined cases): if there are only top errorDictionary, we may
                        // not be able to conscript the last row for our purposes,
                        // so insert a new, empty row.
                        lastRow = string.Format(CultureInfo.CurrentUICulture,
                            normalRow, string.Empty, string.Empty, string.Empty, string.Empty);
                        output.Add(lastRow);
                        lastRowIndex++;
                    }
                    // Update last row with new row including hidden fields
                    output[lastRowIndex] = string.Concat(
                        lastRow.Remove(lastRow.Length - rowEnder.Length),
                        string.Join("\n", hiddenFields.ToArray()),
                        rowEnder
                    );
                }
                else
                {
                    // If there are not any rows in the output, just append the
                    // hidden fields.
                    output.Add(string.Join("\n", hiddenFields.ToArray()));
                }
            }

            return string.Join("\n", output.ToArray());
        }


        /// <summary>
        /// Build media collection from fields.
        /// </summary>
        /// <returns>Media collection.</returns>
        protected override Widgets.Media BuildMediaCollection()
        {
            var media = new Widgets.Media();
            foreach (var field in this._fields.Values)
            {
                media.Append(field.Widget.Media);
            }
            return media;
        }

        /// <summary>
        /// Build inline JS collection from child objects (fields/forms).
        /// </summary>
        protected override string BuildInlineJS()
        {
            var output = new List<string>(_fields.Count);
            foreach (var bf in this)
            {
                string inlineJS = bf.RenderWidgetJavaScript();
                if (!string.IsNullOrEmpty(inlineJS)) output.Add(inlineJS);
            }
            return string.Join("\n", output.ToArray());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get BoundField enumerator
        /// </summary>
        /// <returns>Bound field</returns>
        public IEnumerator<BoundField> GetEnumerator()
        {
            foreach (KeyValuePair<string, IField> pair in this._fields)
            {
                yield return new BoundField(this, pair.Value, pair.Key);
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
            base.FullClean(); // Creates an error instance
            if (!this.IsBound) return; // No point continuing 

            // Do field by field validation
            var cleanedData = new NameObjectDictionary();
            foreach (KeyValuePair<string, IField> pair in this._fields)
            {
                IField field = pair.Value;
                string fieldName = this.AddPrefix(pair.Key);
                object value = field.Widget.GetValueFromDataCollection(this.Data, this.Files, fieldName);
                try
                {
                    cleanedData.Add(pair.Key, field.Clean(value));

                    // Do custom validation
                    if (field.CustomClean != null)
                    {
                        cleanedData = field.CustomClean(cleanedData);
                    }
                }
                catch (ValidationException vex)
                {
                    this.Errors.Add(fieldName, vex.Messages);
                }
            }

            // Do form-wide validation
            try
            {
                this._cleanedData = this.Clean(cleanedData);
            }
            catch (ValidationException vex)
            {
                this.Errors.Add(NonFieldErrors, vex.Messages);
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
            get
            {
                return RenderHtml(
                    "<tr><th>{0}</th><td>{3}{1}{2}</td></tr>",
                    "<tr><td colspan=\"2\">{0}</td></tr>",
                    "</td></tr>",
                    "<br />{0}",
                    false);
            }
        }

        /// <summary>
        /// Render form using a UL layout
        /// </summary>
        /// <remarks>Does not include &gt;ul&lt;&gt;/ul&lt;</remarks>
        public override string AsUL
        {
            get
            {
                return RenderHtml(
                    "<li>{0}{1} {2}{3}</li>",
                    "<li>{0}</li>",
                    "</li>",
                    " {0}",
                    false);
            }
        }

        /// <summary>
        /// Render form using a paragraph layout
        /// </summary>
        public override string AsP
        {
            get
            {
                return RenderHtml(
                    "<p>{0} {1}{2}</p>",
                    "{0}",
                    string.Empty,
                    " {0}",
                    true);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Text to append to label for display.
        /// </summary>
        public string LabelSuffix { get; private set; }

        /// <summary>
        /// Indexor
        /// </summary>
        /// <param name="name">Field name</param>
        /// <returns>Bound field</returns>
        public BoundField this[string name]
        {
            get
            {
                try
                {
                    return new BoundField(this, this._fields[name], name);
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException(
                        string.Format(CultureInfo.InvariantCulture, "Field {0} not found in Form", name));
                }
            }
        }

        /// <summary>
        /// Get all hidden fields.
        /// </summary>
        public IEnumerator<BoundField> HiddenFields
        {
            get
            {
                foreach (KeyValuePair<string, IField> pair in this._fields)
                {
                    if (pair.Value.Widget.IsHidden)
                    {
                        yield return new BoundField(this, pair.Value, pair.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Get all visible fields.
        /// </summary>
        public IEnumerator<BoundField> VisibleFields
        {
            get
            {
                foreach (KeyValuePair<string, IField> pair in this._fields)
                {
                    if (!pair.Value.Widget.IsHidden)
                    {
                        yield return new BoundField(this, pair.Value, pair.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Data that has been cleaned.
        /// </summary>
        public override ReadOnlyDictionary<string, object> CleanedData
        {
            get { return new ReadOnlyDictionary<string, object>(this._cleanedData); }
        }

        /// <summary>
        /// Returns True if the form needs to be multipart-encrypted, i.e. it has
        /// FileInput; otherwise, False.
        /// </summary>
        public override bool IsMultipart
        {
            get
            {
                foreach (var field in _fields.Values)
                {
                    if (field.Widget.NeedsMultipartForm) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Fields that make up this form.
        /// </summary>
        public ReadOnlyDictionary<string, IField> Fields
        {
            get { return new ReadOnlyDictionary<string, IField>(_fields); }
        }

        #endregion
    }
}
