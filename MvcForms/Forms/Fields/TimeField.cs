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
    using System.Globalization;

    using Utils;


    /// <summary>
    /// Time field
    /// </summary>
    public class TimeField : BaseField
    {
        #region Feilds

        /// <summary>
        /// Valid input formats
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly")]
        public static readonly string[] DefaultInputFormats = new string[] {
            "HH:mm:ss",  // "14:30:59"
            "HH:mm"      // "14:30"
        };

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeField() : this(new Widgets.DateInput()) { }
        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public TimeField(IWidget widget)
            : base(widget) 
        {
            this.InputFormats = DefaultInputFormats;

            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.InvalidTime;
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

            // Get date element
            DateTime? date = value as DateTime?;
            if (date != null && date.HasValue) return date.Value.TimeOfDay; 

            // Parse date
            string dateStr = value as string;
            if (dateStr != null)
            {
                try
                {
                    return DateTime.ParseExact(dateStr, InputFormats, CultureInfo.CurrentUICulture,
                        DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces).TimeOfDay;
                }
                catch (FormatException) { } // Ignore here will be delt with later
            }

            throw ValidationException.Create(ErrorMessages[MessageInvalid]);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Supported input Formats for time value (in standard .Net Date 
        /// format see http://msdn.microsoft.com/en-us/library/97x6twsz.aspx)
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public string[] InputFormats { get; set; }

        #endregion
    }
}
