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

namespace JL.Web.Forms.Contrib.Widgets.JQueryUI
{
    using System.Globalization;

    using JL.Web.Forms.Widgets;
    using JL.Web.Forms.Extensions;


    /// <summary>
    /// Orientation of slider
    /// </summary>
    public enum SliderOrientation
    {
        /// <summary>
        /// Let widget automatically determine orientation
        /// </summary>
        Auto,
        Horizontal,
        Vertical,
    }

    /// <summary>
    /// Slider control using jQuery UI Slider widget
    /// </summary>
    /// <see cref="http://jqueryui.com/demos/slider/"/>
    /// <remarks>Visit the jQuery UI documentation site for complete documentation of options.</remarks>
    public class Slider : BaseWidget
    {
        #region Fields

        private NameObjectDictionary _options = new NameObjectDictionary();

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
            string id = extraAttributes.Get("id", name);
            return string.Format(CultureInfo.CurrentUICulture,
                "{0}\n<div id=\"{1}_ui\"></div>",
                new HiddenInput().Render(name, value == null ? this.Minimum : value, extraAttributes), 
                id);
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
            this._options["value"] = value;
            this._options["orientation"] = Orientation.ToString()
                .ToLower(CultureInfo.CurrentUICulture);

            return string.Format(CultureInfo.InvariantCulture,
                "$(function(){{\n$('#{0}_ui').slider({1}, \"change\":function(event, ui){{ $(\"#{0}\").val(ui.value); }}}});\n}});",
                id, this._options.ToJson().Trim(new char[] {'}'}));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Display the date picker inline
        /// </summary>
        public SliderOrientation Orientation { get; set; }

        /// <summary>
        /// Whether to slide handle smoothly when user click outside 
        /// handle on the bar.
        /// </summary>
        public bool Animate
        {
            get { return (bool)this._options.Get("animate", false); }
            set { this._options["animate"] = value; }
        }

        /// <summary>
        /// The minimum value of the slider.
        /// </summary>
        public int Minimum 
        {
            get { return (int)this._options.Get("min", 0); }
            set { this._options["min"] = value;}
        }

        /// <summary>
        /// The maximum value of the slider.
        /// </summary>
        public int Maximum
        {
            get { return (int)this._options.Get("max", 100); }
            set { this._options["max"] = value; }
        }

        /// <summary>
        /// Determines the size or amount of each interval or step the 
        /// slider takes between min and max. The full specified value 
        /// range of the slider (max - min) needs to be evenly divisible 
        /// by the step.
        /// </summary>
        public int Step
        {
            get { return (int)this._options.Get("step", 1); }
            set { this._options["step"] = value; }
        }

        #endregion
    }
}
