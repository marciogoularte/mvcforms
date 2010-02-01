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

namespace JL.Web.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Text;


    /// <summary>
    /// Collection of errorDictionary that can render itself
    /// </summary>
    [Serializable]
    public class ErrorDictionary : Dictionary<string, ErrorCollection>
    {
        #region .ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorDictionary() : base() { }

        /// <summary>
        /// Protected serialization constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ErrorDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion

        #region Methods

        /// <summary>
        /// Convert list into a UL.
        /// </summary>
        /// <returns></returns>
        public string AsUL() { return this.AsUL("errorlist"); }

        /// <summary>
        /// Convert list into a UL.
        /// </summary>
        /// <param name="errorClass">Specify the class to use for displaying errorDictionary</param>
        /// <returns>The string.</returns>
        public string AsUL(string errorClass)
        {
            StringBuilder results = new StringBuilder(this.Count);
            if (this.Count == 0) return string.Empty;
            foreach (var msg in this)
            {
                results.AppendFormat(CultureInfo.CurrentUICulture,
                    "<li>{0}</li>", msg.Value.AsUL(errorClass, msg.Key));
            }
            return string.Format(CultureInfo.CurrentUICulture, "<ul class=\"{0}\">{1}</ul>", errorClass,
                results.ToString());
        }

        /// <summary>
        /// Output string representation of the object.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return this.AsUL();
        }

        /// <summary>
        /// Output a plain text representation of the object.
        /// </summary>
        /// <returns>String.</returns>
        public string AsText()
        {
            StringBuilder results = new StringBuilder(this.Count);
            if (this.Count == 0) return string.Empty;
            foreach (var msg in this)
            {
                if (msg.Value.Count > 0) results.Append(msg.Value.AsText());
            }
            return results.ToString().Trim();
        }

        #endregion
    }
}