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
    using System.Net;
    using System.Text.RegularExpressions;
    
    using MvcForms.Utils;


    /// <summary>
    /// URL field
    /// </summary>
    public class UrlField : RegexField
    {
        #region Feilds

        /// <summary>
        /// URL regular expression
        /// </summary>
        public static readonly Regex UrlRE = new Regex(
            @"^https?://" + // http:// or https://
            @"(?:(?:[A-Z0-9](?:[A-Z0-9-]{0,61}[A-Z0-9])?\.)+[A-Z]{2,6}\.?|" + // #domain...
            @"localhost|" + // localhost...
            @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})" + // ...or ip
            @"(?::\d+)?" + // optional port
            @"(?:/?|/\S+)$",
            RegexOptions.IgnoreCase);

        /// <summary>
        /// Invalid link msg key
        /// </summary>
        public const string MessageInvalidLink = "invalid_link";

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UrlField() : this(null) 
        {
            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.InvalidUrl;
            this.ErrorMessages[MessageInvalidLink] = DefaultErrorMessages.InvalidLink;
        }

        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public UrlField(IWidget widget) : this(widget, Configuration.MvcFormsSettings.Instance) { }

        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        /// <param name="config">Configuration object to use (for testability).</param>
        public UrlField(IWidget widget, Configuration.MvcFormsSettings config)
            : base(UrlRE, widget)
        {
            this.UserAgent = config.Fields.UrlField.UserAgent;

            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.InvalidUrl;
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
            var result = value as string;

            if (!string.IsNullOrEmpty(result))
            {
                // If no URL scheme given, assume http://
                if (!result.Contains("://")) result = "http://" + result;
            }

            base.Clean(result);
            if (string.IsNullOrEmpty(result)) return result;

            if (this.VerifyExists)
            {
                // Setup request
                var sourceUri = new Uri(result, UriKind.Absolute);
                var request = (HttpWebRequest)HttpWebRequest.Create(sourceUri);
                request.Method = "HEAD";
                request.UserAgent = this.UserAgent;

                try
                {
                    // Try to fetch the response
                    request.GetResponse().Close();
                }
                catch (WebException wex)
                {
                    var statusCode = (int)((HttpWebResponse)wex.Response).StatusCode;
                    // A 4xx response is just an invalid link
                    if (statusCode >= 400 && statusCode < 500)
                    {
                        throw ValidationException.Create(ErrorMessages[MessageInvalidLink]);
                    }
                    else
                    {
                        throw ValidationException.Create(ErrorMessages[MessageInvalid]);
                    }                    
                }
            }
            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Verify that the URL exists
        /// </summary>
        public bool VerifyExists { get; set; }

        /// <summary>
        /// The user agent to specify
        /// </summary>
        public string UserAgent { get; set; }

        #endregion
    }
}
