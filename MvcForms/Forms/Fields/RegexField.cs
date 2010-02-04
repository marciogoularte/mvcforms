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
    using System.Text.RegularExpressions;


    /// <summary>
    /// Regular expression field type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
         Justification = "Designed be inheritable")]
    public class RegexField : StringField
    {
        #region .ctors

        /// <summary>s
        /// Pattern constructor
        /// </summary>
        /// <param name="pattern">Regex pattern</param>
        public RegexField(string pattern) : this(new Regex(pattern)) { }
        /// <summary>
        /// Regex constructor
        /// </summary>
        /// <param name="regularExpression">Regular expression object</param>
        public RegexField(Regex regularExpression) : this(regularExpression, null) { }
        /// <summary>
        /// Pattern and Widget constructor
        /// </summary>
        /// <param name="pattern">Regex pattern</param>
        /// <param name="widget">Override default Widget</param>
        public RegexField(string pattern, IWidget widget) : this(new Regex(pattern), widget) { }
        /// <summary>
        /// Regex and Widget constructor
        /// </summary>
        /// <param name="regularExpression">Regular expression object</param>
        /// <param name="widget">Override default Widget</param>
        public RegexField(Regex regularExpression, IWidget widget) 
            : base(widget) 
        {
            this.RegularExpression = regularExpression;
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
            string result = base.Clean(value) as string;

            if (string.IsNullOrEmpty(result)) return null;

            if (!this.RegularExpression.Match(result).Success)
            {
                throw new ValidationException(this.ErrorMessages[MessageInvalid]);
            }
            return value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Regular expression used to match
        /// </summary>
        public Regex RegularExpression { get; private set; }

        /// <summary>
        /// Pattern used to match regular expression
        /// </summary>
        public string Pattern
        {
            get { return this.RegularExpression.ToString(); }
        }

        #endregion Properties
    }
}
