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
    using System.Collections.ObjectModel;

    using Utils;


    /// <summary>
    /// Field that will return a collection of objects.
    /// </summary>
    public class CollectionField : BaseField
    {
        #region Feilds

        /// <summary>
        /// Invalid list message key
        /// </summary>
        public const string MessageInvalidList = "invalid_list";
        /// <summary>
        /// Maximum date message key
        /// </summary>
        public const string MessageInvalidListValue = "invalid_list_value";

        #endregion

        #region .ctors

        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public CollectionField(ICollectionWidget widget) 
            : base(widget) 
        { 
            this.ErrorMessages[MessageInvalidList] = DefaultErrorMessages.InvalidList;
            this.ErrorMessages[MessageInvalidListValue] = DefaultErrorMessages.InvalidListValue;
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
            if (ConversionHelper.IsEmpty(value))
            {
                if (this.Required)
                {
                    throw new ValidationException(this.ErrorMessages[MessageRequired]);
                }
                else
                {
                    return new Collection<object>();
                }
            }

            // Ensure each value is valid.
            foreach (var val in ConversionHelper.ObjectList(value))
            {
                if (!this.ValidValue(val))
                {
                    throw ValidationException.Create(this.ErrorMessages[MessageInvalidListValue], val);
                }
            }

            return value;
        }

        /// <summary>
        /// Check to see if the provided value is valid.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <returns>True if value found in choices; otherwise false.</returns>
        public virtual bool ValidValue(object value)
        {
            return !ConversionHelper.IsEmpty(value);
        }

        #endregion
    }
}
