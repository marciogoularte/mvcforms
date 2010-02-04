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
    using System.Globalization;
    using System.Text;
    using System.Web;

    using Extensions;
    using Utils;


    /// <summary>
    /// Multiple checkbox select elements
    /// </summary>
    public class CheckBoxSelectMultiple : SelectMultiple
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
            const string forFormat = " for=\"{0}\"";
            const string itemFormat = "<li><label{0}>{1} {2}</label></li>\n";

            StringBuilder sb = new StringBuilder();

            // Setup attributes
            ElementAttributesDictionary finalAttributes = BuildAttributes(extraAttributes);
            finalAttributes.SetDefault("name", name);
            string id = finalAttributes.Get("id");

            // Create selected choices list
            IList<object> selectedChoices = ConversionHelper.ObjectList(value);

            // Render choices
            sb.Append("<ul>\n");
            int index = 0;
            foreach (IChoice choice in Choices)
            {
                // Generate label id
                string labelFor = string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    string newId = string.Concat(id, "_", index.ToString(CultureInfo.InvariantCulture));
                    finalAttributes["id"] = newId;
                    labelFor = string.Format(CultureInfo.CurrentUICulture, forFormat, newId, index++);
                }

                // Render checkbox
                var cb = new CheckBoxInput() { 
                    Attributes = finalAttributes,
                    CheckTest = v => selectedChoices == null ? false : selectedChoices.Contains(v) // Lambda expression
                };
                string renderedCb = cb.Render(name, choice.Value);
                sb.AppendFormat(CultureInfo.CurrentUICulture, itemFormat,
                    labelFor, renderedCb, HttpUtility.HtmlEncode(choice.Label));
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

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
        public override string IdForLabel(string id) { return string.Concat(id, "_0"); }

        #endregion
    }
}
