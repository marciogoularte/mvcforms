﻿#region License
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
    using System.Collections.Specialized;
    using System.Web;


    /// <summary>
    /// Widget
    /// </summary>
    public interface IWidget
    {
        #region Methods

        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <returns>HTML</returns>
        string Render(string name, object value);
        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity</param>
        /// <returns>HTML</returns>
        string Render(string name, object value, ElementAttributesDictionary extraAttributes);

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
        string RenderJavaScript(string name, object value, string id);

        /// <summary>
        /// Get value of field from data collection as widgets could store data
        /// across multiple HTML fields
        /// </summary>
        /// <param name="data">Data dictionary</param>
        /// <param name="files">Files dictionary</param>
        /// <param name="name">Name of field</param>
        /// <returns>Data</returns>
        object GetValueFromDataCollection(NameValueCollection data, HttpFileCollectionBase files, string name);

        /// <summary>
        /// Element attributes, this can be an ElementAttributesDictionary or an anon object dictionary
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "The Attributes property is typed object in Widget classes to provide syntactic sugar for users therefore we must use an accessor function to provide access to this object.")]
        ElementAttributesDictionary GetAttributes();

        /// <summary>
        /// Returns the HTML ID attribute of this Widget for use by a &gt;label&lt;,
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
        string IdForLabel(string id);

        #endregion Methods

        #region Properties

        /// <summary>
        /// Is this widget visible
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Does this widget require a multipart-encoded form
        /// </summary>
        bool NeedsMultipartForm { get; }

        /// <summary>
        /// Media required by this widget
        /// </summary>
        Widgets.Media Media { get; }

        #endregion Properties
    }
}
