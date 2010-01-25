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
    using System.Collections.Specialized;
    using System.Web;

    using Utils;


    /// <summary>
    /// Basic interface to a Form.
    /// </summary>
    public interface IForm
    {
        #region Methods

        /// <summary>
        /// Bind data to this "form" instance
        /// </summary>
        /// <param name="data">Data to bind form to</param>
        /// <param name="files">File data to bind form to</param>
        void BindData(NameValueCollection data, HttpFileCollectionBase files);

        /// <summary>
        /// Do a full clean (validation) on all fields
        /// </summary>
        void FullClean();

        #endregion

        #region Render Properties

        /// <summary>
        /// Render form using a Table layout
        /// </summary>
        /// <remarks>Does not include &gt;table&lt;&gt;/table&lt;</remarks>
        /// <returns></returns>
        string AsTable { get; }

        /// <summary>
        /// Render form using a UL layout
        /// </summary>
        /// <remarks>Does not include &gt;ul&lt;&gt;/ul&lt;</remarks>
        /// <returns></returns>
        string AsUL { get; }

        /// <summary>
        /// Render form using a paragraph layout
        /// </summary>
        /// <returns></returns>
        string AsP { get; }

        #endregion

        #region Properties

        /// <summary>
        /// Data stored by form.
        /// </summary>
        NameValueCollection Data { get; }

        /// <summary>
        /// Files stored by form.
        /// </summary>
        HttpFileCollectionBase Files { get; }

        /// <summary>
        /// Format for html ID attribute.
        /// </summary>
        /// <remarks>Use "{0}" for the name field; {1} for form name in form collections.</remarks>
        string AutoId { get; set; }

        /// <summary>
        /// Prefix to append to field names.
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// Label for Form
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Is this form bound to any data.
        /// </summary>
        bool IsBound { get; }

        /// <summary>
        /// Get errorDictionary returned from validation(clean) operation.
        /// </summary>
        ErrorDictionary Errors { get; }

        /// <summary>
        /// Returns TrueValue if the form has no errorDictionary. Otherwise, FalseValue. If errorDictionary are
        /// being ignored, returns FalseValue.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Returns True if the form needs to be multipart-encrypted, i.e. it has
        /// FileInput; otherwise, False.
        /// </summary>
        bool IsMultipart { get; }

        /// <summary>
        /// Data that has been cleaned.
        /// </summary>
        ReadOnlyDictionary<string, object> CleanedData { get; }

        /// <summary>
        /// Media required by Widgets.
        /// </summary>
        Widgets.Media Media { get; }

        /// <summary>
        /// Any inline JS needed by widgets.
        /// </summary>
        string InlineJS { get; }

        /// <summary>
        /// All content to be rendered in the head (Css)
        /// </summary>
        string Head { get; }

        /// <summary>
        /// All content to be rendered in the footer (Media + Inline JS)
        /// </summary>
        string Footer { get; }

        #endregion
    }
}
