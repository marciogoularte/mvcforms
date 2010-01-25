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
    using System.Globalization;

    using Extensions;
    using Utils;

    
    /// <summary>
    /// A field plus data
    /// </summary>
    public sealed class BoundField
    {
        #region Fields

        private Form _form;
        private IField _field;
        private ErrorCollection _errors;

        #endregion

        #region .ctors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="form">Form to bind field to.</param>
        /// <param name="field">Bound field.</param>
        /// <param name="name">Name of field.</param>
        public BoundField(Form form, IField field, string name)
        {
            this._form = form;
            this._field = field;
            this.Name = name;
            this.HtmlName = form.AddPrefix(name);
            this.Label = string.IsNullOrEmpty(field.Label) ? FormatHelper.BeautifyName(name) : field.Label;
            this.HelpText = string.IsNullOrEmpty(field.HelpText) ? string.Empty : field.HelpText;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Output a string version of this field
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.AsWidget(null, null);
        }

        /// <summary>
        /// Output a string version of this field
        /// </summary>
        /// <param name="attributes">Additional attributes for widget.</param>
        /// <returns></returns>
        public string ToString(object attributes)
        {
            return this.AsWidget(null, ElementAttributesDictionary.Create(attributes));
        }

        /// <summary>
        /// Renders the field by rendering the passed widget, adding any HTML
        /// attributes passed as attrs.
        /// </summary>
        /// <param name="widget">Widget to render.</param>
        /// <param name="attributes">Additional attributes for widget.</param>
        /// <returns>Widget rendered as string.</returns>
        public string AsWidget(IWidget widget, ElementAttributesDictionary attributes)
        {
            if (widget == null) widget = this._field.Widget;
            if (attributes == null) attributes = new ElementAttributesDictionary();

            // Assign additional attributes by field
            this._field.AppendWidgetAttributes(widget, attributes);

            // Add ID
            if (!(attributes.ContainsKey("id") || widget.GetAttributes().ContainsKey("id")))
            {
                attributes["id"] = this.AutoId;
            }

            return widget.Render(this.HtmlName, this.DataValue, attributes);
        }

        /// <summary>
        /// Returns a string of HTML for representing this as an <input type="text" />.
        /// </summary>
        /// <returns>Widget rendered as string.</returns>
        public string AsText()
        {
            return AsWidget(new Widgets.TextInput(), null);
        }

        /// <summary>
        /// Returns a string of HTML for representing this as an <input type="text" />.
        /// </summary>
        /// <param name="attributes">Additional attributes for widget.</param>
        /// <returns>Widget rendered as string.</returns>
        public string AsText(ElementAttributesDictionary attributes)
        {
            return AsWidget(new Widgets.TextInput(), attributes);
        }

        /// <summary>
        /// Returns a string of HTML for representing this as an <textarea></textarea>.
        /// </summary>
        /// <returns>Widget rendered as string.</returns>
        public string AsTextArea()
        {
            return AsWidget(new Widgets.TextArea(), null);
        }

        /// <summary>
        /// Returns a string of HTML for representing this as an <textarea></textarea>.
        /// </summary>
        /// <param name="attributes">Additional attributes for widget.</param>
        /// <returns>Widget rendered as string.</returns>
        public string AsTextArea(ElementAttributesDictionary attributes)
        {
            return AsWidget(new Widgets.TextArea(), attributes);
        }

        /// <summary>
        /// Returns a string of HTML for representing this as an <input type="hidden" />.
        /// </summary>
        /// <returns>Widget rendered as string.</returns>
        public string AsHidden()
        {
            return AsWidget(new Widgets.HiddenInput(), null);
        }

        /// <summary>
        /// Returns a string of HTML for representing this as an <input type="hidden" />.
        /// </summary>
        /// <param name="attributes">Additional attributes for widget.</param>
        /// <returns>Widget rendered as string.</returns>
        public string AsHidden(ElementAttributesDictionary attributes)
        {
            return AsWidget(new Widgets.HiddenInput(), attributes);
        }

        /// <summary>
        /// Apply a label element to string content.
        /// </summary>
        /// <param name="contents">Content to tag.</param>
        /// <returns></returns>
        public string ApplyLabel(string contents)
        {
            return ApplyLabel(contents, (ElementAttributesDictionary)null);
        }

        /// <summary>
        /// Apply a label element to string content.
        /// </summary>
        /// <param name="attributes">Attributes to apply.</param>
        /// <returns></returns>
        public string ApplyLabel(object attributes)
        {
            return ApplyLabel(this.Label, ElementAttributesDictionary.Create(attributes));
        }

        /// <summary>
        /// Apply a label element to string content.
        /// </summary>
        /// <param name="attributes">Attributes to apply.</param>
        /// <returns></returns>
        public string ApplyLabel(ElementAttributesDictionary attributes)
        {
            return ApplyLabel(this.Label, attributes);
        }

        /// <summary>
        /// Apply a label element to string content.
        /// </summary>
        /// <param name="contents">Content to tag.</param>
        /// <param name="attributes">Attributes to apply.</param>
        /// <returns></returns>
        public string ApplyLabel(string contents, object attributes)
        {
            return ApplyLabel(contents, ElementAttributesDictionary.Create(attributes));
        }

        /// <summary>
        /// Apply a label element to string content.
        /// </summary>
        /// <param name="contents">Content to tag.</param>
        /// <param name="attributes">Attributes to apply.</param>
        /// <returns></returns>
        public string ApplyLabel(string contents, ElementAttributesDictionary attributes)
        {
            const string labelFormat = "<label for=\"{0}\"{1}>{2}</label>";
            IWidget widget = _field.Widget;

            // Tag with label if we have an ID
            string id = widget.GetAttributes().Get("id");
            if (string.IsNullOrEmpty(id)) id = this.AutoId;
            if (!string.IsNullOrEmpty(id))
            {
                string attrs = attributes == null ? "" : attributes.ToString();
                contents = string.Format(CultureInfo.CurrentUICulture, labelFormat, 
                    widget.IdForLabel(id), attrs, contents);
            }
            return contents;
        }

        /// <summary>
        /// Render any JavaScript associated with a Widget.
        /// </summary>
        /// <returns>JavaScript string; null if widget doesn't use any Javascript.</returns>
        public string RenderWidgetJavaScript()
        {
            return this._field.Widget.RenderJavaScript(this.Name, this.DataValue, this.AutoId);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the field on form
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// HTML name for the field (from the HTML element)
        /// </summary>
        public string HtmlName { get; private set; }

        /// <summary>
        /// Label for the field
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// Tag Label text with label element.
        /// </summary>
        public string LabelTag
        {
            get { return this.ApplyLabel(this.Label); }
        }

        /// <summary>
        /// Help text to be displayed with the field
        /// </summary>
        public string HelpText { get; private set; }

        /// <summary>
        /// This is a hidden field
        /// </summary>
        public bool IsHidden
        {
            get { return this._field.Widget.IsHidden; }
        }

        /// <summary>
        /// Automatically generated ID for this field
        /// </summary>
        public string AutoId
        {
            get
            {
                string autoId = this._form.AutoId;
                if (!string.IsNullOrEmpty(autoId))
                {
                    return string.Format(CultureInfo.InvariantCulture, autoId, this.HtmlName);
                }
                else
                {
                    return this.HtmlName;
                }
            }
        }

        /// <summary>
        /// Data associated with this field
        /// </summary>
        public object Data
        {
            get
            {
                return this._field.Widget.GetValueFromDataCollection(this._form.Data, this._form.Files, this.HtmlName);
            }
        }

        /// <summary>
        /// Get collection of errorDictionary generated by this field.
        /// </summary>
        public ErrorCollection Errors
        {
            get
            {
                // Cache a local version
                if (this._errors == null)
                {
                    this._errors = this._form.Errors.Get(this.HtmlName, new ErrorCollection());
                }
                return this._errors;
            }
        }

        /// <summary>
        /// Field has errors
        /// </summary>
        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        /// <summary>
        /// Get value to use for field
        /// </summary>
        private object DataValue
        {
            get
            {
                // Obtain data value
                object data = null;
                if (this._form.IsBound)
                {
                    data = this.Data;
                }
                else
                {
                    data = this._form.Initial.Get(this.HtmlName);
                    if (data == null) data = this._field.Initial == null ? null : this._field.Initial;
                }
                return data;
            }
        }

        #endregion
    }
}

