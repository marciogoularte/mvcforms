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
    using System.Configuration;
    using System.Globalization;


    /// <summary>
    /// Helper methods for reading configuration files
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Get a setting from AppSettings and convert to an enum value.
        /// </summary>
        /// <typeparam name="TEnum">Enum to convert value to.</typeparam>
        /// <param name="appSettingsKey">Settings key to use.</param>
        /// <returns>Value from configuration converted to enum.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification="This would be pointless.")]
        public static TEnum GetEnumSetting<TEnum>(string appSettingsKey)
        {
            return GetEnumSetting<TEnum>(appSettingsKey, default(TEnum));
        }

        /// <summary>
        /// Get a setting from AppSettings and convert to an enum value.
        /// </summary>
        /// <typeparam name="TEnum">Enum to convert value to.</typeparam>
        /// <param name="appSettingsKey">Settings key to use.</param>
        /// <param name="defaultValue">Default value if value is not found.</param>
        /// <returns>Value from configuration converted to enum.</returns>
        public static TEnum GetEnumSetting<TEnum>(string appSettingsKey, TEnum defaultValue)
        {
            string value = ConfigurationManager.AppSettings.Get(appSettingsKey);
            if (string.IsNullOrEmpty(value)) return defaultValue;
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value, true);
            }
            catch (ArgumentException)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.CurrentUICulture,
                        "The value in AppSettings[\"{0}\"] does not exist in {1}.",
                        appSettingsKey, typeof(TEnum).Name));
            }
        }

        /// <summary>
        /// Get a string value from AppSettings
        /// </summary>
        /// <param name="appSettingsKey">Settings key to use.</param>
        /// <param name="defaultValue">Default value if value is not found.</param>
        /// <returns>Value from settings.</returns>
        public static string GetSetting(string appSettingsKey, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings.Get(appSettingsKey);
            if (string.IsNullOrEmpty(value)) return defaultValue;
            return value;
        }
    }
}
