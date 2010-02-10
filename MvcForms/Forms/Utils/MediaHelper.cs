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

namespace MvcForms.Utils
{
    using System;
    using System.Globalization;
    using System.Web;


    /// <summary>
    /// Types of media
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// JavaScript
        /// </summary>
        JS,
        /// <summary>
        /// Cascading style sheet
        /// </summary>
        Css
    }


    /// <summary>
    /// Helper methods for dealing with media.
    /// </summary>
    public static class MediaHelper
    {
        #region Fields

        /// <summary>
        /// Application/Request/Configuration key for JavaScript media path.
        /// </summary>
        public const string MediaJSPathKey = "Forms:mediaJavaScriptPath";

        /// <summary>
        /// Application/Request/Configuration key for CSS media path.
        /// </summary>
        public const string MediaCssPathKey = "Forms:mediaCssPath";

        private static string _jsMediaPath;
        private static string _cssMediaPath;

        #endregion

        #region Methods

        /// <summary>
        /// Resolve a path into a real path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="mediaType">Type of media this path refers to.</param>
        /// <returns>Full path for client.</returns>
        public static string ResolvePath(string path, MediaType mediaType)
        {
            // Just return an absolute or FQ URL
            if (path.StartsWith("http", StringComparison.OrdinalIgnoreCase) || VirtualPathUtility.IsAbsolute(path))
            {
                return path;
            }

            // Convert a relative path to absolute path and return
            if (path.StartsWith("~", false, CultureInfo.CurrentUICulture))
            {
                return VirtualPathUtility.ToAbsolute(path);
            }

            // Get media path
            string mediaPath;
            if (mediaType == MediaType.JS)
            {
                mediaPath = VirtualPathUtility.ToAbsolute(JSMediaPath);
            }
            else
            {
                mediaPath = VirtualPathUtility.ToAbsolute(CssMediaPath);
            }
            return string.Concat(mediaPath, path);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get path of JavaScript Media.
        /// </summary>
        public static string JSMediaPath
        {
            get
            {
                // Allow override per request
                string mediaPath = GetRequestItem(MediaJSPathKey);
                if (mediaPath != null) return mediaPath;
                
                if (_jsMediaPath == null)
                {
                    // Get path from config
                    _jsMediaPath = Configuration.MvcFormsSettings.Instance.Forms.Media.JavaScriptPath;
                }
                return _jsMediaPath;
            }
        }

        /// <summary>
        /// Get path of CSS Media.
        /// </summary>
        public static string CssMediaPath
        {
            get
            {
                // Allow override per request
                string mediaPath = GetRequestItem(MediaCssPathKey);
                if (mediaPath != null) return mediaPath;
                
                if (_cssMediaPath == null)
                {
                    // Get path from config
                    _cssMediaPath = Configuration.MvcFormsSettings.Instance.Forms.Media.CssPath;
                }
                return _cssMediaPath;
            }
        }

        /// <summary>
        /// Get an item from HttpContext.Current.Items
        /// </summary>
        /// <param name="key">Item key</param>
        /// <returns></returns>
        private static string GetRequestItem(string key)
        {
            if (HttpContext.Current == null) return null;
            return HttpContext.Current.Items[key] as string;
        }

        #endregion
    }
}
