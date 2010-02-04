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

    using Utils;


    /// <summary>
    /// Integer field type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
         Justification = "Designed be inheritable")]
    public class IntegerField : BaseField
    {
        #region Feilds

        /// <summary>
        /// Maximum value message key
        /// </summary>
        public const string MessageMaxValue = "max_value";
        /// <summary>
        /// Minimum value message key
        /// </summary>
        public const string MessageMinValue = "min_value";

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public IntegerField() : this(null) { }
        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public IntegerField(IWidget widget)
            : base(widget) 
        {
            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.InvalidInteger;
            this.ErrorMessages[MessageMaxValue] = DefaultErrorMessages.MaxValue;
            this.ErrorMessages[MessageMinValue] = DefaultErrorMessages.MinValue;
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

            // String checks
            if (ConversionHelper.IsEmpty(value)) return null;

            // Integer checks
            int result;
            if (!int.TryParse(value.ToString(), out result))
            {
                throw ValidationException.Create(this.ErrorMessages[MessageInvalid]);
            }
            if (this.MaxValue != null && result > this.MaxValue)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMaxValue], this.MaxValue, result);
            }
            if (this.MinValue != null && result < this.MinValue)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMinValue], this.MinValue, result);
            }
            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Maximum value allowed
        /// </summary>
        public int? MaxValue { get; set; }

        /// <summary>
        /// Minimum value allowed
        /// </summary>
        public int? MinValue { get; set; }

        #endregion 
    }
}
