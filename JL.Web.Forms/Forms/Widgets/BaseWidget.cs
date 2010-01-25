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

namespace JL.Web.Forms.Widgets
{
    using System;
    using System.Collections.Specialized;
    using System.Web;

    using Extensions;


    /// <summary>
    /// Base concrete widget class
    /// </summary>
    public abstract class BaseWidget : IWidget
    {
        #region Fields

        private ElementAttributesDictionary _attributes;
        private Media _media;

        #endregion

        #region Methods

        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <returns>HTML</returns>
        public string Render(string name, object value) { return this.Render(name, value, null); }

        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity</param>
        /// <returns>HTML.</returns>
        public virtual string Render(string name, object value, ElementAttributesDictionary extraAttributes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns JavaScript for this element. Which can be rendered to the bottom of the BODY element.
        /// </summary>
        /// <remarks>
        /// The JavaScript script tag is automatically rendered, all items to be rendered are 
        /// combined into a single script tag. 
        /// 
        /// If your widget requires varables or any complex rendering it is recomended your 
        /// JavaScript code is wrapped in a closure.
        /// </remarks>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="id">HTML element ID of widget.</param>
        /// <returns>JavaScript string; null if widget does not use any JavaScript.</returns>
        public virtual string RenderJavaScript(string name, object value, string id) { return null; }

        /// <summary>
        /// Get value of field from data dictionary as widgets could store data
        /// across multiple HTML fields
        /// </summary>
        /// <param name="data">Data dictionary</param>
        /// <param name="files">Files dictionary</param>
        /// <param name="name">Name of field</param>
        /// <returns>Data</returns>
        public virtual object GetValueFromDataCollection(NameValueCollection data, HttpFileCollectionBase files, string name)
        {
            return data.Get(name);
        }

        /// <summary>
        /// Build Element attributes for rendering widget
        /// </summary>
        /// <param name="extraAttributes">Additional attributes to apply</param>
        /// <returns>Attributes for rendering</returns>
        protected ElementAttributesDictionary BuildAttributes(ElementAttributesDictionary extraAttributes)
        {
            var finalAttributes = new ElementAttributesDictionary(GetAttributes());
            finalAttributes.Append(extraAttributes);
            return finalAttributes;
        }

        /// <summary>
        /// Element attributes, this can be an ElementAttributesDictionary or an anon object dictionary
        /// </summary>
        public ElementAttributesDictionary GetAttributes()
        {
            if (this._attributes == null)
            {
                this._attributes = new ElementAttributesDictionary();
            }
            return this._attributes;
        }

        /// <summary>
        /// Returns the HTML ID attribute of this Widget for use by a &lt;label&gt;,
        /// given the ID of the field. Returns None if no ID is available.
        /// </summary>
        /// <remarks>
        /// This hook is necessary because some widgets have multiple HTML
        /// elements and, thus, multiple IDs. In that case, this method should
        /// return an ID value that corresponds to the first ID in the widget's
        /// tags.
        /// </remarks>
        /// <param name="id">Suggested ID of control.</param>
        /// <returns>Actual ID of control.</returns>
        public virtual string IdForLabel(string id) { return id; }

        /// <summary>
        /// Gives your widget an opertunity to populate media object.
        /// </summary>
        /// <remarks>
        /// This method is not called until the media property is accessed. This lets
        /// your widget be provided with additional information regarding which CSS/JS
        /// files your widget wishes to use. For example selecting weather to load 
        /// JavaScript libraries off your local server or the Google Javascript hosting
        /// service.
        /// </remarks>
        public virtual void PopulateMedia() {}

        #endregion

        #region Properties

        /// <summary>
        /// Element attributes, this can be an ElementAttributesDictionary or an anon object dictionary
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly",
            Justification = "Attributes property is to provide syntactic sugar. GetAttributes() provides a typed method."), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Attributes property is to provide syntactic sugar. GetAttributes() provides a typed method.")]
        public object Attributes
        {
            set 
            {
                this._attributes = GetAttributes();
                var elements = value as ElementAttributesDictionary;
                if (elements != null)
                {
                    this._attributes.Append(elements);
                }
                else
                {
                    this._attributes.AppendObject(value);
                }
            }
        }

        /// <summary>
        /// Is this widget visible
        /// </summary>
        public virtual bool IsHidden
        {
            get { return false; }
        }

        /// <summary>
        /// Does this widget require a multipart-encoded form
        /// </summary>
        public virtual bool NeedsMultipartForm
        {
            get { return false; }
        }

        /// <summary>
        /// Media required by this widget
        /// </summary>
        public Media Media 
        {
            get
            {
                if (this._media == null)
                {
                    this._media = new Media();
                    this.PopulateMedia();
                }
                return this._media;
            }
        }

        #endregion Properties
    }
}
