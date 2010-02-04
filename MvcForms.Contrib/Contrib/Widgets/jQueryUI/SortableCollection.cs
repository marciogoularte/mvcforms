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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using MvcForms.Extensions;
    using MvcForms.Widgets;


    /// <summary>
    /// Sortable collection control using jQuery UI Sortable widget
    /// </summary>
    /// <see cref="http://jqueryui.com/demos/sortable/"/>
    /// <remarks>Visit the jQuery UI documentation site for complete documentation of options.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix",
        Justification="This class does wrap collections, hence this name is correct." )]
    public class SortableCollection : BaseCollectionWidget
    {
        #region Fields

        private NameObjectDictionary _options = new NameObjectDictionary();

        /// <summary>
        /// Default ui state class
        /// </summary>
        public const string DefaultItemCssClass = "ui-state-default";

        /// <summary>
        /// Default ui placeholder state class
        /// </summary>
        public const string DefaultPlaceholderCssClass = "ui-state-highlight";

        #endregion

        #region .ctors

        public SortableCollection()
            : base()
        {
            this.ItemCssClass = DefaultItemCssClass;
            this.PlaceholderCssClass = DefaultPlaceholderCssClass;
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
                .AddJS(ScriptSource.GetLibraryPath(JavaScriptLibrary.JQueryUI))
                .AddJS("~/Scripts/jquery-ui.mvcforms-contrib.js");
        }

        /// <summary>
        /// Render a single choice item into HTML.
        /// </summary>
        /// <param name="output">Append output to this string builder.</param>
        /// <param name="name">Form name of the item being rendered.</param>
        /// <param name="choice">Choice item being rendered.</param>
        public override void RenderItemChoice(StringBuilder output, string name, object value)
        {
            var attributes = new ElementAttributesDictionary();
            attributes.SetDefault("class", ItemCssClass);
            output.AppendFormat(CultureInfo.InvariantCulture, "<li{0}>{1}{2}</li>",
                attributes, value, new HiddenInput().Render(name, value));
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
            this._options.SetDefault("items", "li");
            this._options.SetDefault("name", string.Concat(name, "[]"));
            return string.Format(CultureInfo.InvariantCulture,
                "$(function(){{$('#{0}').sortablecollection({1}).disableSelection()}});",
                id, this._options.ToJson());
        }

        #endregion

        #region Properties

        /// <summary>
        /// CSS class to apply to items.
        /// </summary>
        public string ItemCssClass { get; set; }

        /// <summary>
        /// Class for placeholder
        /// </summary>
        public string PlaceholderCssClass
        {
            get { return (string)this._options.Get("placeholder", DefaultPlaceholderCssClass); }
            set { this._options["placeholder"] = value; }
        }

        /// <summary>
        /// If set to true, items will be reverted to their correct DOM position with a 
        /// smooth animation.
        /// </summary>
        public bool Revert
        {
            get { return (bool)this._options.Get("revert", false); }
            set { this._options["revert"] = value; }
        }

        #endregion
    }
}
