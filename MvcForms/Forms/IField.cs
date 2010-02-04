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

namespace MvcForms
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;


    /// <summary>
    /// Allows for custom validation of a particular field. Any ValidationException 
    /// thrown by this method will be associated with that particular field.
    /// </summary>
    /// <param name="cleanedData">Data that has been cleaned</param>
    /// <returns>Data that has been cleaned</returns>
    public delegate NameObjectDictionary Clean(NameObjectDictionary cleanedData);


    /// <summary>
    /// Interface to field classes
    /// </summary>
    public interface IField
    {
        #region Members

        /// <summary>
        /// Validates the given value and returns its "cleaned" value as an
        /// appropriate .Net objects.
        /// </summary>
        /// <param name="value">Value to clean.</param>
        /// <returns>Cleaned value.</returns>
        /// <exception cref="ValidationException">On an invalid validation.</exception>
        object Clean(object value);

        /// <summary>
        /// Additional attributes to assign to widget on render.
        /// </summary>
        /// <param name="widget">Widget attributes are targeted at.</param>
        /// <param name="attributes">Attributes to append to.</param>
        void AppendWidgetAttributes(IWidget widget, ElementAttributesDictionary attributes);

        #endregion

        #region Properties

        /// <summary>
        /// Custom clean method delegate
        /// </summary>
        Clean CustomClean { get; set; }

        /// <summary>
        /// Specifies whether the field is required. TrueValue by default.
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// A Widget class, or instance of a Widget class, that should be used 
        /// for this Field when displaying it. Each Field has a default Widget 
        /// that it'll use if you don't specify this. In most cases, the default 
        /// widget is TextInput.
        /// </summary>
        IWidget Widget { get; }

        /// <summary>
        /// A verbose name for this field, for use in displaying this field in 
        /// a form. By default, a "pretty" version of the form field name, if 
        /// the Field is part of a Form.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// A value to use in this Field's initial display. This value is *not*
        /// used as a fallback if data isn't given.
        /// </summary>
        object Initial { get; }

        /// <summary>
        /// Optional "help text" for this Field.
        /// </summary>
        string HelpText { get; }

        /// <summary>
        /// Dictionary of errormessages that can be used
        /// </summary>
        Dictionary<string, string> ErrorMessages { get; }

        #endregion Properties
    }
}
