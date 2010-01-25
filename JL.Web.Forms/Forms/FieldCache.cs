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

namespace JL.Web.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;


    /// <summary>
    /// Threadsafe Cache of fields for Form types
    /// </summary>
    public sealed class FieldCache
    {
        #region Global cache instance

        private static FieldCache _instance;

        /// <summary>
        /// The default global field cache instance
        /// </summary>
        public static FieldCache Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FieldCache();
                }
                return _instance;
            }
        }

        #endregion

        #region Fields

        private ThreadSafeDictionaryWrapper<string, Dictionary<string, IField>> _cache = new ThreadSafeDictionaryWrapper<string, Dictionary<string, IField>>();

        #endregion

        #region Methods

        /// <summary>
        /// Obtains a field dictionary from the cache.
        /// </summary>
        /// <param name="instance">Form instance to obtain dictionary for.</param>
        /// <returns>Dictionary of fields</returns>
        public IDictionary<string, IField> FetchOrCreateFieldDictionary(Form instance)
        {
            Type type = instance.GetType();
            string key = type.ToString();

            // Check cache
            Dictionary<string, IField> existingEntry;
            if (_cache.TryGetValue(key, out existingEntry))
            {
                return existingEntry;
            }

            // Create and Insert into cache
            Dictionary<string, IField> newEntry = BuildFieldDictionary(instance);
            return _cache.SetDefault(key, newEntry);
        }

        /// <summary>
        /// Build dictionary from supplied type.
        /// </summary>
        /// <param name="instance">Form to obtain fields.</param>
        /// <returns>Dictionary of fields.</returns>
        private static Dictionary<string, IField> BuildFieldDictionary(Form instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            Type formType = instance.GetType();

            // Build field dictionary
            Dictionary<string, IField> fields = new Dictionary<string, IField>();

            // Loop through this form fields
            foreach (FieldInfo fieldInfo in formType.GetFields())
            {
                object obj = fieldInfo.GetValue(instance);
                IField field = obj as IField;
                if (field != null) ProcessField(field, fieldInfo.Name, formType, fields);
            }

            return fields;
        }

        /// <summary>
        /// Process a single field.
        /// </summary>
        /// <param name="field">Field to process.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="formType">Forms type.</param>
        /// <param name="fields">Collection of resulting fields.</param>
        private static void ProcessField(IField field, string fieldName, Type formType, IDictionary<string, IField> fields)
        {
            // Check for a clean method
            MethodInfo cleanMethod = formType.GetMethod(string.Concat("Clean", fieldName));
            if (cleanMethod != null)
            {
                try
                {
                    field.CustomClean = Delegate.CreateDelegate(typeof(Clean), cleanMethod) as Clean;
                }
                catch (ArgumentException aex)
                {
                    throw new BindException(fieldName, formType.AssemblyQualifiedName, aex);
                }
            }
            fields.Add(fieldName, field);
        }

        #endregion
    }
}
