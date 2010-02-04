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
    using System;


    /// <summary>
    /// A Widget that splits datetime input into two input text boxes
    /// </summary>
    public class SplitDateTime : BaseMultiWidget
    {
        #region Fields

        private DateInput _date;
        private TimeInput _time;

        #endregion

        #region .ctors

        /// <summary>
        /// Constructor
        /// </summary>
        public SplitDateTime()
            : base(new DateInput(), new TimeInput())
        {
            this._date = Widgets[0] as DateInput;
            this._time = Widgets[1] as TimeInput;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Decompress value into multipart tuple.
        /// </summary>
        /// <param name="value">Value to decompress.</param>
        /// <returns>Tuple containing values.</returns>
        protected override Tuple Decompress(object value)
        {
            DateTime? dateTime = value as DateTime?;
            if (dateTime != null && dateTime.HasValue)
            {
                return new Tuple(dateTime.Value.Date, dateTime.Value.TimeOfDay);
            }
            return new Tuple(null, null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Format of date value (in standard .Net Date format see http://msdn.microsoft.com/en-us/library/97x6twsz.aspx)
        /// </summary>
        public string DateFormat 
        {
            get { return this._date.Format; }
            set { this._date.Format = value; }
        }

        /// <summary>
        /// Format of time value (in standard .Net Date format see http://msdn.microsoft.com/en-us/library/97x6twsz.aspx)
        /// </summary>
        public string TimeFormat
        {
            get { return this._time.Format; }
            set { this._time.Format = value; }
        }

        #endregion
    }
}
