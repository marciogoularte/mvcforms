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

namespace MvcForms.Widgets
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Web;


    /// <summary>
    /// A widget that displays a list of items that can be reordered or added to.
    /// </summary>
    public abstract class BaseCollectionWidget : BaseWidget, ICollectionWidget
    {
        #region Methods

        /// <summary>
        /// Returns this Widget rendered as HTML.
        /// </summary>
        /// <param name="name">Form name of the widget being renderd.</param>
        /// <param name="value">Value of the field being rendered.</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity.</param>
        /// <returns>HTML</returns>
        public override string Render(string name, object value, ElementAttributesDictionary extraAttributes)
        {
            var choices = value as IEnumerable<object>;
            if (choices != null)
            {
                return RenderCollection(name, choices, extraAttributes);
            }
            else
            {
                return RenderCollection(name, new Collection<object>(), extraAttributes);
            }            
        }

        /// <summary>
        /// Returns this Widget collection rendered as HTML.
        /// </summary>
        /// <param name="name">Form name of the widget being renderd.</param>
        /// <param name="values">Values to be rendered.</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity.</param>
        /// <returns>HTML</returns>
        public virtual string RenderCollection(string name, IEnumerable<object> values, ElementAttributesDictionary extraAttributes)
        {
            var output = new StringBuilder();
            var valueName = string.Concat(name, "[]");
            output.AppendFormat(CultureInfo.InvariantCulture, "<ul{0}>\n", extraAttributes);
            foreach (var value in values)
            {
                RenderItemChoice(output, valueName, value);
            }
            output.AppendFormat(CultureInfo.InvariantCulture, "</ul>");
            return output.ToString();
        }

        /// <summary>
        /// Render a single choice item into HTML.
        /// </summary>
        /// <param name="output">Append output to this string builder.</param>
        /// <param name="name">Form name of the item being rendered.</param>
        /// <param name="value">Value being rendered.</param>
        public virtual void RenderItemChoice(StringBuilder output, string name, object value)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "<li>{0}</li>", new Widgets.TextInput().Render(name, value));
        }

        /// <summary>
        /// Get value of field from data dictionary as widgets could store data
        /// across multiple HTML fields
        /// </summary>
        /// <param name="data">Data dictionary</param>
        /// <param name="files">Files dictionary</param>
        /// <param name="name">Name of field</param>
        /// <returns>Data</returns>
        public override object GetValueFromDataCollection(NameValueCollection data, HttpFileCollectionBase files, string name)
        {
            return ConvertValues(data.GetValues(string.Concat(name, "[]")));
        }

        /// <summary>
        /// Convert each value in string array into object collection.
        /// </summary>
        /// <param name="values">Values returned from browser.</param>
        /// <returns></returns>
        protected virtual ICollection ConvertValues(string[] values)
        {
            return new ReadOnlyCollection<object>(values);
        }

        #endregion
    }
}
