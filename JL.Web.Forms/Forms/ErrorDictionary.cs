/******************************************************************************
 * Copyright (c) 2009, Tim Savage - Joocey Labs
 * All rights reserved.
 * 
 * See LICENSE.txt or http://mvcforms.codeplex.com/license for lincense 
 * terms and conditions.
 *****************************************************************************/
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