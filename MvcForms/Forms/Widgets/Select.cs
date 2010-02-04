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
    using System.Globalization;
    using System.Text;

    using Extensions;
    

    /// <summary>
    /// Renders a HTML Password input element
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select",
        Justification = "This is in reference to the HTML element.")]
    public class Select : BaseWidget, IChoiceWidget
    {
        #region Fields

        /// <summary>
        /// Label of default option
        /// </summary>
        public const string DefaultDefaultLabel = "Select...";

        #endregion

        #region .ctors

        /// <summary>
        /// Constructor default
        /// </summary>
        public Select() : base() 
        {
            this.DefaultLabel = DefaultDefaultLabel;
        }

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
            StringBuilder sb = new StringBuilder();
            ElementAttributesDictionary finalAttributes = BuildAttributes(extraAttributes);
            finalAttributes.SetDefault("name", name);

            // Create selected choices list
            IList<object> selectedChoices = new List<object>();
            if (!string.Empty.Equals(value)) selectedChoices.Add(value);

            // Render output
            sb.AppendFormat(CultureInfo.CurrentUICulture, "<select{0}>\n", finalAttributes);
            RenderOptions(sb, selectedChoices);
            sb.Append("</select>");

            return sb.ToString();
        }

        /// <summary>
        /// Render select options.
        /// </summary>
        /// <param name="sb">String builder used to build select element.</param>
        /// <param name="selectedChoices">List of choices that have been selected.</param>
        protected void RenderOptions(StringBuilder sb, IList<object> selectedChoices)
        {
            const string OptionFormat = "<option value=\"{0}\"{1}>{2}</option>\n";

            if (this.ShowDefault)
            {
                sb.AppendFormat(CultureInfo.CurrentUICulture, OptionFormat, string.Empty, string.Empty, this.DefaultLabel);
            }

            if (this.Choices == null) return;
            foreach (IChoice choice in this.Choices)
            {
                string selectedAttribute = string.Empty;
                // Perform a string comparison
                if (selectedChoices != null && selectedChoices.Contains(choice.Value.ToString())) selectedAttribute = " selected=\"selected\"";
                sb.AppendFormat(CultureInfo.CurrentUICulture, OptionFormat, choice.Value, selectedAttribute, choice.Label);
            }
        }

        #endregion 

        #region Properties

        /// <summary>
        /// Options to offer user
        /// </summary>
        public IEnumerable<Choice> Choices { get; set; }

        /// <summary>
        /// Display a default option
        /// </summary>
        public bool ShowDefault { get; set; }

        /// <summary>
        /// Text to display for the default value
        /// </summary>
        public string DefaultLabel { get; set; }

        #endregion
    }
}