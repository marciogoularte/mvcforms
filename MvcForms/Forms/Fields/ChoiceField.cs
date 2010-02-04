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
    using System.Collections.Generic;

    using Utils;


    /// <summary>
    /// A field that is limited to a number of choices
    /// </summary>
    public class ChoiceField : BaseField, IChoiceField
    {
        #region Feilds

        /// <summary>
        /// Maximum date message key
        /// </summary>
        public const string MessageInvalidChoice = "invalid_choice";

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChoiceField() : this(new Widgets.Select()) { }
        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public ChoiceField(IChoiceWidget widget)
            : base(widget) 
        {
            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.Invalid;
            this.ErrorMessages[MessageInvalidChoice] = DefaultErrorMessages.InvalidChoice;
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

            if (!ValidValue(value))
            {
                throw ValidationException.Create(this.ErrorMessages[MessageInvalidChoice], value);
            }

            return value.ToString();
        }

        /// <summary>
        /// Check to see if the provided value is a valid choice.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <returns>True if value found in choices; otherwise false.</returns>
        public bool ValidValue(object value)
        {
            foreach (var choice in Choices)
            {
                if (choice.Value.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Widget used to render this field
        /// </summary>
        public new IChoiceWidget Widget 
        {
            get { return base.Widget as IChoiceWidget; }
        }

        /// <summary>
        /// Collection of choices
        /// </summary>
        public IEnumerable<Choice> Choices 
        {
            get { return this.Widget.Choices; }
            set { this.Widget.Choices = value; } 
        }

        /// <summary>
        /// Display a default option
        /// </summary>
        public bool ShowDefault
        {
            get { return this.Widget.ShowDefault; }
            set { this.Widget.ShowDefault = value; }
        }

        /// <summary>
        /// Text to display for the default value
        /// </summary>
        public string DefaultLabel
        {
            get { return this.Widget.DefaultLabel; }
            set { this.Widget.DefaultLabel = value; }
        }

        #endregion
    }
}
