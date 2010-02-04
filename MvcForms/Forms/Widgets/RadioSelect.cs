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
    using System.Collections.Generic;
    using System.Text;


    /// <summary>
    /// Custom render function.
    /// </summary>
    /// <param name="name">Form name of the widget being renderd.</param>
    /// <param name="value">Value of the field being rendered.</param>
    /// <param name="extraAttributes">Attributes to assign to HTML entity.</param>
    /// <param name="choices">Options to offer user.</param>
    /// <returns>String represented rendered items.</returns>
    public delegate string RadioFieldRenderer(string name, string value, ElementAttributesDictionary extraAttributes, IEnumerable<Choice> choices);

    /// <summary>
    /// Render a selection of Radio button controls
    /// </summary>
    public class RadioSelect : Select
    {
        #region Fields

        private RadioFieldRenderer _renderer = DefaultRadioFieldRenderer;

        #endregion

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
            string strValue = value == null ? string.Empty : value.ToString();
            if (extraAttributes == null) extraAttributes = new ElementAttributesDictionary();
            return this.Renderer(name, strValue, extraAttributes, Choices);
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

        /// <summary>
        /// Custom render function.
        /// </summary>
        /// <param name="name">Form name of the widget being renderd.</param>
        /// <param name="value">Value of the field being rendered.</param>
        /// <param name="attributes">Attributes to assign to HTML entity.</param>
        /// <param name="choices">Options to offer user.</param>
        /// <returns>String represented rendered items.</returns>
        private static string DefaultRadioFieldRenderer(string name, string value, ElementAttributesDictionary attributes, IEnumerable<Choice> choices)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>\n");

            int index = 0;
            foreach (var choice in choices)
            {
                sb.AppendFormat("<li>{0}</li>\n", new RadioInput(name, value, attributes, choice, index++).Render());
            }

            sb.Append("</ul>");
            return sb.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Delegate function to customise rendering
        /// </summary>
        public RadioFieldRenderer Renderer
        {
            get { return _renderer; }
            set { _renderer = value; }
        }

        #endregion 
    }
}
