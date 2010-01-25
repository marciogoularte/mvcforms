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

namespace JL.Web.Forms.Widgets
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;

    using Extensions;
    using Utils;


    /// <summary>
    /// Wrapper for specifying Media required by a widget.
    /// </summary>
    /// <remarks>Provides a fluent interface for Add and Append methods.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces",
        Justification = "This is the most appropriate name for this class")]
    public sealed class Media
    {
        #region Fields

        private IDictionary<string, CssMediaTypes> _css = new Dictionary<string, CssMediaTypes>();
        private ICollection<string> _js = new Collection<string>();

        private bool _cssDirty = true;
        private string _cssOutput;

        private bool _jsDirty = true;
        private string _jsOutput;

        #endregion

        #region Css Methods

        /// <summary>
        /// Add a reference to a css file
        /// </summary>
        /// <param name="path">
        /// Path to css file, this can be relative a complete URI, a relative path, 
        /// or a ASP.NET Virtual path (ie start with ~).
        /// </param>
        /// <returns>Fluent interface; returns this</returns>
        public Media AddCss(string path)
        {
            return AddCss(path, CssMediaTypes.None);
        }

        /// <summary>
        /// Add a reference to a CSS file
        /// </summary>
        /// <remarks>
        /// Path can be null, in which case the entry will be ignored. This is useful
        /// as controls can be passed a location for a CSS file that is null indicating
        /// that this control should not be included. This may be that the particular
        /// CSS file has been included in the page in a different way.
        /// </remarks>
        /// <param name="path">
        /// Path to CSS file, this can be relative a complete URI, a relative path, 
        /// or a ASP.NET Virtual path (ie start with ~).
        /// </param>
        /// <param name="mediaType">Type of media.</param>
        /// <returns>Fluent interface; returns this</returns>
        public Media AddCss(string path, CssMediaTypes mediaType)
        {
            if (string.IsNullOrEmpty(path)) return this;
            this._cssDirty = true;

            // Check if path is already specified
            if (this._css.ContainsKey(path))
            {
                var existingMediaType = this._css[path];
                if (existingMediaType == CssMediaTypes.None)
                {
                    // If existing type is None, need to make sure that
                    // Screen is defined as this is what None means in HTML.
                    this._css[path] = CssMediaTypes.Screen | mediaType;
                }
                else
                {
                    this._css[path] = existingMediaType | mediaType;
                }
            }
            else
            {
                this._css.Add(path, mediaType);
            }
            return this;
        }
        /// <summary>
        /// Renders collection of CSS links.
        /// </summary>
        /// <returns></returns>
        private string RenderCss()
        {
            const string linkFormat = "<link href=\"{0}\" type=\"text/css\"{1} rel=\"stylesheet\" />";

            var output = new List<string>(this._css.Count);
            foreach (var item in this._css)
            {
                output.Add(string.Format(CultureInfo.CurrentUICulture, linkFormat, 
                    MediaHelper.ResolvePath(item.Key, MediaType.Css),
                    BuildMedia(item.Value)));
            }
            return string.Join("\n", output.ToArray());
        }

        /// <summary>
        /// Build media type string.
        /// </summary>
        /// <param name="mediaType">Type of media.</param>
        /// <returns>Media type string.</returns>
        private static string BuildMedia(CssMediaTypes mediaType)
        {
            if (mediaType == CssMediaTypes.None) return string.Empty;
            if (mediaType == CssMediaTypes.All) return " media=\"all\"";

            var output = new List<string>();
            if ((mediaType & CssMediaTypes.Screen) != CssMediaTypes.None) output.Add("screen");
            if ((mediaType & CssMediaTypes.Tty) != CssMediaTypes.None) output.Add("tty");
            if ((mediaType & CssMediaTypes.TV) != CssMediaTypes.None) output.Add("tv");
            if ((mediaType & CssMediaTypes.Projection) != CssMediaTypes.None) output.Add("projection");
            if ((mediaType & CssMediaTypes.Handheld) != CssMediaTypes.None) output.Add("handheld");
            if ((mediaType & CssMediaTypes.Print) != CssMediaTypes.None) output.Add("print");
            if ((mediaType & CssMediaTypes.Braille) != CssMediaTypes.None) output.Add("braille");
            if ((mediaType & CssMediaTypes.Aural) != CssMediaTypes.None) output.Add("aural");
            return string.Concat(" media=\"", string.Join(",", output.ToArray()), "\"");
        }

        #endregion

        #region JS Methods

        /// <summary>
        /// Add a reference to a JavaScript file
        /// </summary>
        /// <remarks>
        /// Path can be null, in which case the entry will be ignored. This is useful
        /// as controls can be passed a location for a JavaScript file that is null indicating
        /// that this control should not be included. This may be that the particular
        /// JavaScript file has been included in the page in a different way.
        /// </remarks>
        /// <param name="path">
        /// Path to JavaScript file, this can be relative a complete URI, a relative path, 
        /// or a ASP.NET Virtual path (ie start with ~).
        /// </param>
        /// <returns>Fluent interface; returns this</returns>
        public Media AddJS(string path)
        {
            if (string.IsNullOrEmpty(path)) return this;
            if (!_js.Contains(path))
            {
                this._jsDirty = true;
                _js.Add(path);
            }
            return this;
        }

        /// <summary>
        /// Renders collection JavaScript references
        /// </summary>
        /// <returns></returns>
        private string RenderJS()
        {
            const string linkFormat = "<script src=\"{0}\" type=\"text/javascript\"></script>";

            var output = new List<string>(this._js.Count);
            foreach (var item in this._js)
            {
                output.Add(string.Format(CultureInfo.CurrentUICulture, linkFormat,
                    MediaHelper.ResolvePath(item, MediaType.JS)));
            }
            return string.Join("\n", output.ToArray());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Append an additional media collection to this one.
        /// </summary>
        /// <param name="source">Media collection to append.</param>
        /// <returns>Fluent interface; returns this</returns>
        public Media Append(Media source)
        {
            // Combine CSS
            foreach (var pair in source._css)
            {
                this.AddCss(pair.Key, pair.Value);
            }

            // Combine JS
            foreach (var js in source._js)
            {
                this.AddJS(js);
            }

            return this;
        }

        /// <summary>
        /// Render contents of class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(Css, "\n", JS);
        }

        #endregion

        #region Properties

        /// <summary>
        /// CSS links rendered as HTML.
        /// </summary>
        public string Css
        {
            get 
            {
                // Cache rendered result (unless it's been changed)
                if (this._cssDirty || this._cssOutput == null)
                {
                    this._cssOutput = RenderCss();
                    this._cssDirty = false;
                }
                return _cssOutput;
            }
        }

        /// <summary>
        /// JavaScript references rendered as HTML.
        /// </summary>
        public string JS
        {
            get 
            {
                // Cache rendered result (unless it's been changed)
                if (this._jsDirty || this._jsOutput == null)
                {
                    this._jsOutput = RenderJS();
                    this._jsDirty = false;
                }
                return this._jsOutput; 
            }
        }

        #endregion
    }
}
