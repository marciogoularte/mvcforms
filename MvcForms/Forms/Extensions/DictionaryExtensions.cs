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

namespace MvcForms.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;


    /// <summary>
    /// Extentions for dictionaries
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Append an identically typed dictionary to this one replacing any existing items.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in this dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in this dictionary.</typeparam>
        /// <param name="self"></param>
        /// <param name="source">Dictionary to append.</param>
        public static void Append<TKey, TValue>(this IDictionary<TKey, TValue> self, IDictionary<TKey, TValue> source) 
        {
            if (source == null) return;
            foreach (var pair in source)
            {
                self[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Append an anon object to a dictionary; Dictionary key must be a string to use this method.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in this dictionary.</typeparam>
        /// <param name="self"></param>
        /// <param name="source">Anon object to append.</param>
        public static void AppendObject<TValue>(this IDictionary<string, TValue> self, object source)
        {
            // Get properties
            Type type = source.GetType();
            PropertyInfo[] properties = type.GetProperties();

            // Append properties
            foreach (PropertyInfo propInfo in properties)
            {
                object value = propInfo.GetValue(source, null);
                if (value != null && value is TValue) self[propInfo.Name] = (TValue)value;
            }
        }

        /// <summary>
        /// Updates an identically typed dictionary to this one but will not replace existing items.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in this dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in this dictionary.</typeparam>
        /// <param name="self"></param>
        /// <param name="source">Dictionary to append.</param>
        public static void Update<TKey, TValue>(this IDictionary<TKey, TValue> self, IDictionary<TKey, TValue> source) 
        {
            if (source == null) return;
            foreach (var pair in source)
            {
                self.SetDefault(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Return the value for key if key is in the dictionary, else default.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in this dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in this dictionary.</typeparam>
        /// <param name="self"></param>
        /// <param name="key">Key to get value of.</param>
        /// <returns>Value associated with key; if key does not exist return default(TValue).</returns>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        {
            return self.Get(key, default(TValue));
        }

        /// <summary>
        /// Return the value for key if key is in the dictionary, else default.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in this dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in this dictionary.</typeparam>
        /// <param name="self"></param>
        /// <param name="key">Key to get value of.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value associated with key; if key does not exist return default(TValue).</returns>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue defaultValue)
        {
            TValue result;
            if (key != null && self.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// If key is in the dictionary, return its value. If not, insert key with a default value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in this dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in this dictionary.</typeparam>
        /// <param name="self"></param>
        /// <param name="key">Key of value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value associated with key; if key does not exist defaultValue.</returns>
        public static TValue SetDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue defaultValue)
        {
            TValue result;
            if (self.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                // Add value
                self.Add(key, defaultValue);
                return defaultValue;
            }
        }
    }
}
