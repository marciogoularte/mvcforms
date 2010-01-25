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

namespace JL.Web.Forms.Fields
{
    using System.Collections.Generic;

    using Extensions;
    using Utils;


    /// <summary>
    /// Base concrete implimentation of a Field
    /// </summary>
    public abstract class BaseField : IField
    {
        #region Feilds

        /// <summary>
        /// Required message key
        /// </summary>
        public const string MessageRequired = "required";
        /// <summary>
        /// Invalid message key
        /// </summary>
        public const string MessageInvalid = "invalid";

        #endregion Members

        #region .ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="widget">Widget instance</param>
        protected BaseField(IWidget widget)
        {
            if (widget == null)
            {
                this.Widget = new Widgets.TextInput();
            }
            else
            {
                this.Widget = widget;
            }

            // Set defaults
            Required = true;

            // Add error messages
            this.ErrorMessages = new Dictionary<string, string>();
            this.ErrorMessages[MessageRequired] = DefaultErrorMessages.Required;
            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.Invalid;
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
        public virtual object Clean(object value)
        {
            if (this.Required && ConversionHelper.IsEmpty(value))
            {
                throw new ValidationException(this.ErrorMessages[MessageRequired]);
            }
            return value;
        }

        /// <summary>
        /// Additional attributes to assign to widget on render.
        /// </summary>
        /// <param name="widget">Widget attributes are targeted at.</param>
        /// <param name="attributes">Attributes to append to.</param>
        public virtual void AppendWidgetAttributes(IWidget widget, ElementAttributesDictionary attributes)
        {
            if (this.Required) attributes.AddCssClass("required");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Custom clean method delegate
        /// </summary>
        public Clean CustomClean { get; set; }

        /// <summary>
        /// The field is required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Widget used to render this field
        /// </summary>
        public IWidget Widget { get; private set; }

        /// <summary>
        /// This fields label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This fields initial value
        /// </summary>
        public object Initial { get; set; }

        /// <summary>
        /// Help text for this field
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// Error messages dictionary for this field
        /// </summary>
        public Dictionary<string, string> ErrorMessages { get; private set; }

        /// <summary>
        /// Helper to allow error messages to be set using object init syntax on definition
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly",
            Justification = "This is to give a nice user experience for defining error messages")]
        public object Messages 
        {
            set { ErrorMessages.AppendObject(value); }
        }

        #endregion
    }
}
