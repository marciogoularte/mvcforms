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
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Web;

    using Extensions;


    /// <summary>
    /// Dictionary of attributes
    /// </summary>
    [Serializable]
    public class ElementAttributesDictionary : Dictionary<string, string>
    {
        #region Fields

        /// <summary>
        /// Name of the CSS class attribute.
        /// </summary>
        public const string CssClassAttributeName = "class";

        #endregion

        #region .ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ElementAttributesDictionary() : base() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="source"></param>
        public ElementAttributesDictionary(ElementAttributesDictionary source)
            : base(source.Count) { this.Append(source); }

        /// <summary>
        /// Protected serialization constructor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ElementAttributesDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Sized constructor.
        /// </summary>
        /// <param name="capacity">The initial number of elements.</param>
        public ElementAttributesDictionary(int capacity) : base(capacity) { }

        /// <summary>
        /// Create new ElementAttributesDictionary populated by object.
        /// </summary>
        /// <param name="attributes">Anon object.</param>
        /// <returns>New ElementAttributesDictionary.</returns>
        public static ElementAttributesDictionary Create(object attributes)
        {
            var attrs = new ElementAttributesDictionary();
            attrs.AppendObject(attributes);
            return attrs;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Convert attributes to string for rendering
        /// </summary>
        /// <returns>Attribute pair string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(this.Count);
            foreach (KeyValuePair<string, string> pair in this)
            {
                result.AppendFormat(" {0}=\"{1}\"", pair.Key, HttpUtility.HtmlEncode(pair.Value));
            }
            return result.ToString();
        }

        /// <summary>
        /// Add a CSS class to the class attribute.
        /// </summary>
        /// <param name="cssClass">Name of the CSS class to add.</param>
        public void AddCssClass(string cssClass)
        {
            if (string.IsNullOrEmpty(cssClass)) return;
            string classNames = this.Get(CssClassAttributeName, string.Empty);
            if (classNames.Contains(cssClass)) return;
            this[CssClassAttributeName] = string.Concat(classNames, " ", cssClass).Trim();
        }

        #endregion
    }
}
