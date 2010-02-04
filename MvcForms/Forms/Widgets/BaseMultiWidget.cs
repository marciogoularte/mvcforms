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

namespace MvcForms.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Web;

    using Extensions;


    /// <summary>
    /// A widget that is composed of multiple widgets.
    /// </summary>
    /// <remarks>
    /// Its render() method is different than other widgets', because it has to
    /// figure out how to split a single value for display in multiple widgets.
    /// The ``value`` argument can be one of two things:
    ///     * A list.
    ///     * A normal value (e.g., a string) that has been "compressed" from
    ///       a list of values.
    ///       
    /// In the second case -- i.e., if the value is NOT a list -- render() will
    /// first "decompress" the value into a list before rendering it. It does so by
    /// calling the decompress() method, which MultiWidget subclasses must
    /// implement. This method takes a single "compressed" value and returns a
    /// list.
    /// 
    /// When render() does its HTML rendering, each value in the list is rendered
    /// with the corresponding widget -- the first value is rendered in the first
    /// widget, the second value is rendered in the second widget, etc.
    /// 
    /// Subclasses may implement FormatOutput(), which takes the list of rendered
    /// widgets and returns a string of HTML that formats them any way you'd like.
    /// 
    /// You'll probably want to use this class with MultiValueField.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi",
        Justification="This is a valid shortening of the word Multiple")]
    public abstract class BaseMultiWidget : BaseWidget
    {
        #region Fields

        private Collection<IWidget> _widgets;

        #endregion

        #region .ctors

        /// <summary>
        /// Collection of subwidgets to render
        /// </summary>
        /// <param name="widgets"></param>
        protected BaseMultiWidget(params IWidget[] widgets)
        {
            this._widgets = new Collection<IWidget>(widgets);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns this Widget rendered as HTML
        /// </summary>
        /// <param name="name">Form name of the widget being renderd</param>
        /// <param name="value">Value of the field being rendered</param>
        /// <param name="extraAttributes">Attributes to assign to HTML entity</param>
        /// <returns>HTML</returns>
        public override string Render(string name, object value, ElementAttributesDictionary extraAttributes)
        {
            // Correct value
            Tuple values = value as Tuple;
            if (values == null) values = Decompress(value);

            // Allow derived classes to make final changes
            PreRenderWidgets(values);

            // Build attributes
            ElementAttributesDictionary finalAttributes = BuildAttributes(extraAttributes);
            string id = finalAttributes.Get("id", null);

            // Build output
            var output = new List<string>();
            for (int index = 0; index < Widgets.Count; index++)
            {
                // Get value
                object widgetValue = values.Get(index);

                // Update ID
                if (id != null) finalAttributes["id"] = string.Concat(id, "_", index);

                // Render item
                output.Add(Widgets[index].Render(string.Concat(name, index), widgetValue, finalAttributes));
            }

            return FormatOutput(output.ToArray());
        }

        /// <summary>
        /// Hook for derived classes to configure widgets with values prior to rendering
        /// </summary>
        /// <param name="values">Tuple containing values of widgets.</param>
        protected virtual void PreRenderWidgets(Tuple values) { }

        /// <summary>
        /// Decompress value into multipart tuple.
        /// </summary>
        /// <param name="value">Value to decompress.</param>
        /// <returns>Tuple containing values.</returns>
        protected abstract Tuple Decompress(object value);

        /// <summary>
        /// Hook for custom widgets to customise the output from Render
        /// </summary>
        /// <param name="renderedWidgets">Pre-rendered widgets that make up this control.</param>
        /// <returns>HTML for display.</returns>
        protected virtual string FormatOutput(string[] renderedWidgets)
        {
            return string.Join("\n", renderedWidgets);
        }

        /// <summary>
        /// Get value of field from data dictionary as widgets could store data
        /// across multiple HTML fields
        /// </summary>
        /// <param name="data">Data dictionary</param>
        /// <param name="files">Files dictionary</param>
        /// <param name="name">Name of field</param>
        /// <returns>Data</returns>
        public override object GetValueFromDataCollection(NameValueCollection data, HttpFileCollectionBase files, string name)
        {
            var output = new List<object>();
            for(int index = 0; index < Widgets.Count; index++)
            {
                output.Add(data.Get(string.Concat(name, index)));
            }
            return new Tuple(output);
        }

        /// <summary>
        /// Returns the HTML ID attribute of this Widget for use by a &lt;label&gt;,
        /// given the ID of the field. Returns None if no ID is available.
        /// </summary>
        /// <remarks>
        /// This hook is necessary because some widgets have multiple HTML
        /// elements and, thus, multiple IDs. In that case, this method should
        /// return an ID value that corresponds to the first ID in the widget's
        /// tags.
        /// </remarks>
        /// <param name="id">Suggested ID of control.</param>
        /// <returns>Actual ID of control.</returns>
        public override string IdForLabel(string id) { return string.Concat(id, "_0"); }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of subwidgets used to render.
        /// </summary>
        public Collection<IWidget> Widgets
        {
            get { return this._widgets; }
        }

        #endregion
    }
}
