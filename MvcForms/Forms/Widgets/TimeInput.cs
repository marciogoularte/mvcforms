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

namespace MvcForms.Widgets
{
    using System;
    using System.Globalization;

    using Utils;


    /// <summary>
    /// Date time input widget
    /// </summary>
    public class TimeInput : Input
    {
        #region Fields

        /// <summary>
        /// Default format for date value
        /// </summary>
        public const string DefaultFormat = "HH:mm:ss";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TimeInput()
            : base("text")
        {
            Format = DefaultFormat;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Render date/time value using specified format
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string RenderValue(object value)
        {
            if (ConversionHelper.IsEmpty(value)) return null;
            DateTime? dateTime = value as DateTime?;
            if (dateTime != null)
            {
                return dateTime.Value.ToString(Format, CultureInfo.CurrentUICulture);
            }
            return value as string;
        }

        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity</param>
        /// <returns>HTML</returns>
        public override string Render(string name, object value, ElementAttributesDictionary extraAttributes)
        {
            return base.Render(name, RenderValue(value), extraAttributes);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Format of time value (in standard .Net Date format see http://msdn.microsoft.com/en-us/library/97x6twsz.aspx)
        /// </summary>
        public string Format { get; set; }

        #endregion
    }
}
