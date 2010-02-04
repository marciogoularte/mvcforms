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

namespace MvcForms.Contrib.Widgets.JQueryUI
{
    using System.Globalization;

    using MvcForms.Widgets;
    using MvcForms.Extensions;


    /// <summary>
    /// Datepicker for Date input using jQuery UI Datepicker widget
    /// </summary>
    /// <see cref="http://jqueryui.com/demos/datepicker/"/>
    /// <remarks>Visit the jQuery UI documentation site for complete documentation of options.</remarks>
    public class DatePicker : DateInput
    {
        #region Fields

        private NameObjectDictionary _options = new NameObjectDictionary();

        #endregion

        #region .ctors

        /// <summary>
        /// Construct a date picker object
        /// </summary>
        public DatePicker() 
            : base()
        {
            DisableAutoComplete = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gives your widget an opertunity to populate media object.
        /// </summary>
        /// <remarks>
        /// This method is not called until the media property is accessed. This lets
        /// your widget be provided with additional information regarding which CSS/JS
        /// files your widget wishes to use. For example selecting weather to load 
        /// JavaScript libraries off your local server or the Google Javascript hosting
        /// service.
        /// </remarks>
        public override void PopulateMedia()
        {
            Media
                .AddJS(ScriptSource.GetLibraryPath(JavaScriptLibrary.JQuery))
                .AddJS(ScriptSource.GetLibraryPath(JavaScriptLibrary.JQueryUI));
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
            if (DisplayInline)
            {
                string id = extraAttributes.Get("id", name);
                return string.Format(CultureInfo.CurrentUICulture,
                    "{0}\n<div id=\"{1}_ui\"></div>",
                    new HiddenInput().Render(name, value, extraAttributes), id);
            }
            else
            {
                return base.Render(name, value, extraAttributes);
            }
        }

        /// <summary>
        /// Returns JavaScript for this element. Which can be rendered to the bottom of the BODY element.
        /// </summary>
        /// <remarks>
        /// The JavaScript script tag is automatically rendered, all items to be rendered are 
        /// combined into a single script tag. 
        /// 
        /// If your widget requires varables or any complex rendering it is recomended your 
        /// JavaScript code is wrapped in a closure.
        /// </remarks>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="id">HTML element ID of widget.</param>
        /// <returns>JavaScript string; null if widget does not use any JavaScript.</returns>
        public override string RenderJavaScript(string name, object value, string id)
        {
            if (DisplayInline)
            {
                this._options["defaultDate"] = value;
                this._options["altField"] = "#" + id;
                id = id + "_ui";
            }
            return string.Format(CultureInfo.InvariantCulture,
                "$(function(){{\n$('#{0}').datepicker({1});\n}});",
                id, this._options.ToJson());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Display the date picker inline
        /// </summary>
        public bool DisplayInline { get; set; }

        /// <summary>
        /// Whether to show the button panel.
        /// </summary>
        public bool ShowButtonPanel
        {
            get { return (bool)this._options.Get("showButtonPanel", (bool?)false); }
            set { this._options["showButtonPanel"] = value; }
        }

        /// <summary>
        /// Set the name of the animation used to show/hide the datepicker. Use 'show' (the 
        /// default), 'slideDown', 'fadeIn', or any of the show/hide jQuery UI effects.
        /// </summary>
        public string ShowAnimation
        {
            get { return this._options.Get("showAnim", "show") as string; }
            set { this._options["showAnim"] = value; }
        }

        #endregion
    }
}
