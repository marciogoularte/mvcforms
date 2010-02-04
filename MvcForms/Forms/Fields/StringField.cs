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

namespace MvcForms.Fields
{
    using System;
    using System.Collections.Specialized;

    using Extensions;
    using Utils;


    /// <summary>
    /// String field type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
         Justification = "Designed be inheritable")]
    public class StringField : BaseField
    {
        #region Feilds

        /// <summary>
        /// Maximum length msg key
        /// </summary>
        public const string MessageMaxLength = "max_length";
        /// <summary>
        /// Minimum length msg key
        /// </summary>
        public const string MessageMinLength = "min_length";

        #endregion

        #region .ctors

        /// <summary>
        /// StringField Constructor
        /// </summary>
        public StringField() : this(new Widgets.TextInput()) { }
        /// <summary>
        /// StringField Constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public StringField(IWidget widget) 
            : base(widget) 
        {
            this.ErrorMessages[MessageMaxLength] = DefaultErrorMessages.MaxLength;
            this.ErrorMessages[MessageMinLength] = DefaultErrorMessages.MinLength;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Validates the given value and returns its "cleaned" value as an
        /// appropriate .Net object.
        /// </summary>
        /// <param name="value">Value to clean</param>
        /// <returns>Cleaned value</returns>
        /// <exception cref="ValidationException">On an invalid validation</exception>
        public override object Clean(object value)
        {
            base.Clean(value);

            if (ConversionHelper.IsEmpty(value)) return null;

            string result = value.ToString();
            int valueLen = result.Length;
            if (this.MaxLength != null && valueLen > this.MaxLength)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMaxLength], this.MaxLength, valueLen);
            }
            if (this.MinLength != null && valueLen < this.MinLength)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMinLength], this.MinLength, valueLen);
            }
            return result;
        }

        /// <summary>
        /// Additional attributes to assign to widget on render.
        /// </summary>
        /// <param name="widget">Widget attributes are targeted at.</param>
        /// <param name="attributes">Attributes to append to.</param>
        public override void AppendWidgetAttributes(IWidget widget, ElementAttributesDictionary attributes)
        {
            base.AppendWidgetAttributes(widget, attributes);
            if (this.MaxLength != null && widget is Widgets.TextInput)
            {
                attributes.SetDefault("maxlength", this.MaxLength.ToString());
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Maximum length of string
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Minimum length of string
        /// </summary>
        public int? MinLength { get; set; }

        #endregion 
    }
}
