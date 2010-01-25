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
    using System.Globalization;
    using System.Web;

    using Extensions;


    /// <summary>
    /// An object used by to represents a single &lt;input type='radio'&gt;.
    /// </summary>
    public class RadioInput
    {
        #region .ctors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Form name of the widget being renderd.</param>
        /// <param name="value">Value of the field being rendered.</param>
        /// <param name="attributes">Attributes to assign to HTML entity.</param>
        /// <param name="choice">Choice for this element.</param>
        /// <param name="index">This elements position in the radio button collection.</param>
        public RadioInput(string name, string value, ElementAttributesDictionary attributes, IChoice choice, int index)
        {
            this.Name = name;
            this.Value = value;
            this.Attributes = new ElementAttributesDictionary(attributes);
            this.Choice = choice;
            this.Index = index;
        }

        #endregion 

        #region Methods

        /// <summary>
        /// Render this element and label.
        /// </summary>
        /// <returns>HTML element.</returns>
        public string Render()
        {
            const string forFormat = " for=\"{0}_{1}\"";
            const string labelFormat = "<label{0}>{1} {2}</label>";

            // Generate label id
            string labelFor = string.Empty;
            string id = Attributes.Get("id");
            if (!string.IsNullOrEmpty(id))
            {
                labelFor = string.Format(CultureInfo.CurrentUICulture, forFormat, id, this.Index);
            }

            // Render label
            return string.Format(CultureInfo.CurrentUICulture, labelFormat, 
                labelFor, HttpUtility.HtmlEncode(this.Choice.Label),
                this.RenderInput());
        }

        /// <summary>
        /// Render the input element.
        /// </summary>
        /// <returns></returns>
        protected string RenderInput()
        {
            const string idFormat = "{0}_{1}";
            const string inputFormat = "<input{0} />";

            // Setup id attribute
            string id = Attributes.Get("id");
            if (!string.IsNullOrEmpty(id))
            {
                Attributes["id"] = string.Format(CultureInfo.InvariantCulture,
                    idFormat, id, this.Index);
            }

            // Setup other attributes
            Attributes["type"] = "radio";
            Attributes["name"] = this.Name;
            Attributes["value"] = this.Choice.Value.ToString();
            if (this.IsChecked) Attributes["checked"] = "checked";

            return string.Format(CultureInfo.CurrentUICulture, inputFormat, Attributes.ToString());
        }

        /// <summary>
        /// Convert this object into it's string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Render();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Is this element checked
        /// </summary>
        public bool IsChecked
        {
            get { return Value == Choice.Value.ToString(); }
        }

        /// <summary>
        /// HTML name of this element.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value of this element. 
        /// </summary>
        public string Value { get; private set; }
        
        /// <summary>
        /// Attributes for this element.
        /// </summary>
        public ElementAttributesDictionary Attributes { get; private set; }

        /// <summary>
        /// This elements choice object.
        /// </summary>
        public IChoice Choice { get; private set; }

        /// <summary>
        /// This elements position in the radio button collection.
        /// </summary>
        public int Index { get; private set; }

        #endregion
    }
}
