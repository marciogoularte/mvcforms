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

namespace JL.Web.Forms.Widgets
{
    using System.Globalization;

    using Extensions;


    /// <summary>
    /// Basic Input widget
    /// </summary>
    public abstract class Input : BaseWidget
    {
        #region Fields

        private readonly string inputType = null;

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="inputType">Type of input element</param>
        protected Input(string inputType)
        {
            this.inputType = inputType;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity</param>
        /// <returns>HTML</returns>
        public override string Render(string name, object value, ElementAttributesDictionary extraAttributes)
        {
            ElementAttributesDictionary finalAttributes = BuildAttributes(extraAttributes);
            finalAttributes["type"] = this.inputType; // Override any other value
            finalAttributes.SetDefault("name", name);
            if (DisableAutoComplete) finalAttributes.SetDefault("autocomplete", "off");
            if (value != null)
            {
                string stringValue = value.ToString();
                if (!string.IsNullOrEmpty(stringValue)) finalAttributes.SetDefault("value", stringValue);
            }
            return string.Format(CultureInfo.InstalledUICulture, "<input{0} />", finalAttributes.ToString());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Disable autocompletion on this form element
        /// </summary>
        /// <remarks>
        /// This is a "non-stardard" XHTML attribute so may cause validation failures.
        /// Is generally supported across major browsers, IE, Mozilla and Chrome.
        /// </remarks>
        public bool DisableAutoComplete { get; set; }

        #endregion
    }
}
