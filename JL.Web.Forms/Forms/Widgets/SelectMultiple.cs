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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Web;

    using Extensions;
    using Utils;


    /// <summary>
    /// Render a multiple select element
    /// </summary>
    public class SelectMultiple : Select
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
            StringBuilder sb = new StringBuilder();
            ElementAttributesDictionary finalAttributes = BuildAttributes(extraAttributes);
            finalAttributes["multiple"] = "multiple";
            finalAttributes.SetDefault("name", name);

            // Create selected choices list
            IList<object> selectedChoices = value as List<object>;
            if (selectedChoices == null)
            {
                if (value != null && !string.Empty.Equals(value)) selectedChoices = new object[] { value };
            }

            // Render output
            sb.AppendFormat(CultureInfo.CurrentUICulture, "<select{0}>\n", finalAttributes);
            RenderOptions(sb, selectedChoices);
            sb.Append("</select>");

            return sb.ToString();
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
            var value = data.Get(name);
            if (string.IsNullOrEmpty(value)) return null;
            return ConversionHelper.SplitString(value);
        }

        #endregion
    }
}
