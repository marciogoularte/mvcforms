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

namespace JL.Web.Forms.Contrib.Widgets
{
    using System.Collections.Generic;
    using System.Configuration;

    using JL.Web.Forms.Utils;
    using System.Globalization;


    /// <summary>
    /// Libraries currently supported
    /// </summary>
    public enum JavaScriptLibrary
    {
        /// <summary>
        /// JQuery
        /// </summary>
        JQuery,
        /// <summary>
        /// JQuery
        /// </summary>
        JQueryUI,
    }

    /// <summary>
    /// Supported locations of libraries
    /// </summary>
    public enum LibraryLocation
    {
        /// <summary>
        /// Locally stored in ~/Scripts folder
        /// </summary>
        Local,
        /// <summary>
        /// Google CDN
        /// </summary>
        GoogleCdn,
        /// <summary>
        /// Custom userdefined location
        /// </summary>
        Custom,
    }

    /// <summary>
    /// Locations of script files for common JavaScript libraries
    /// </summary>
    public static class ScriptSource
    {
        #region Fields

        private static ReadOnlyDictionary<LibraryLocation, ReadOnlyDictionary<JavaScriptLibrary, string>> _defaultLocations;       
        private static ReadOnlyDictionary<JavaScriptLibrary, string> _defaultVersions;

        /// <summary>
        /// Settings key in AppSettings for location
        /// </summary>
        public const string LocationSettingsKey = "Forms.Contrib:JavaScriptLocation";

        #endregion

        #region .ctors

        /// <summary>
        /// Static constructor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification="This is not possible with a ReadonlyDictionary")]
        static ScriptSource()
        {
            var libraryLocations = new Dictionary<LibraryLocation, ReadOnlyDictionary<JavaScriptLibrary, string>>();
            
            // Add local paths
            libraryLocations.Add(LibraryLocation.Local, new ReadOnlyDictionary<JavaScriptLibrary, string>(new Dictionary<JavaScriptLibrary, string>
            {
                {JavaScriptLibrary.JQuery, "~/Scripts/jquery-{0}.min.js"},
                {JavaScriptLibrary.JQueryUI, "~/Scripts/jquery-ui-{0}.min.js"},
            }));

            // Add Google CDN paths
            libraryLocations.Add(LibraryLocation.GoogleCdn, new ReadOnlyDictionary<JavaScriptLibrary, string>(new Dictionary<JavaScriptLibrary, string>
            {
                {JavaScriptLibrary.JQuery, "http://ajax.googleapis.com/ajax/libs/jquery/{0}/jquery.min.js"},
                {JavaScriptLibrary.JQueryUI, "http://ajax.googleapis.com/ajax/libs/jqueryui/{0}/jquery-ui.min.js"},
            }));

            // Assign to field
            _defaultLocations = new ReadOnlyDictionary<LibraryLocation, ReadOnlyDictionary<JavaScriptLibrary, string>>(libraryLocations);

            // Setup default versions
            _defaultVersions = new ReadOnlyDictionary<JavaScriptLibrary, string>(new Dictionary<JavaScriptLibrary, string>
            {
                {JavaScriptLibrary.JQuery, "1.4.0"},
                {JavaScriptLibrary.JQueryUI, "1.7.2"},
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get URL of a JavaScript library. Location is determined from configuration,
        /// default is to fallback to Google CDN.
        /// </summary>
        /// <param name="lib">Library to get URL of.</param>
        /// <returns>URL of JavaScript library.</returns>
        public static string GetLibraryPath(JavaScriptLibrary lib)
        {
            return GetLibraryPath(lib, ConfigurationHelper.GetEnumSetting<LibraryLocation>(LocationSettingsKey,
                LibraryLocation.GoogleCdn));
        }

        /// <summary>
        /// Get URL of a JavaScript library.
        /// </summary>
        /// <param name="lib">Library to get URL of.</param>
        /// <param name="location">Location to obtain library.</param>
        /// <returns>URL of JavaScript library.</returns>
        public static string GetLibraryPath(JavaScriptLibrary lib, LibraryLocation location)
        {
            return GetLibraryPath(lib, location, null);
        }

        /// <summary>
        /// Get URL of a JavaScript library.
        /// </summary>
        /// <param name="lib">Library to get URL of.</param>
        /// <param name="location">Location to obtain library.</param>
        /// <param name="version">Version string, ie "1.4.0"</param>
        /// <returns>URL of JavaScript library.</returns>
        public static string GetLibraryPath(JavaScriptLibrary lib, LibraryLocation location, string version)
        {
            // Setup version
            if (string.IsNullOrEmpty(version)) version = _defaultVersions[lib];
            if (location == LibraryLocation.Custom)
            {
                // Get location from config
                string configKey = "Forms.Contrib:LocationUrl" + lib.ToString();
                string url = ConfigurationManager.AppSettings.Get(configKey);
                if (string.IsNullOrEmpty(url)){
                    throw new ConfigurationErrorsException(
                        string.Format(CultureInfo.CurrentUICulture,
                            "Custom location url was not defined in AppSettings[\"{0}\"].", configKey));
                }
                return url;
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, _defaultLocations[location][lib], version);
            }
        }

        #endregion
    }
}
