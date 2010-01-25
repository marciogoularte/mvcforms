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
    using System.Runtime.Serialization;
    

    /// <summary>
    /// Name object collection
    /// </summary>
    [Serializable]
    public class NameObjectDictionary : Dictionary<string, object>
    {
        #region .ctors

        /// <summary>
        /// Constructor
        /// </summary>
        public NameObjectDictionary() { }

        /// <summary>
        /// Serialization constructor
        /// </summary>
        /// <param name="info">
        /// A System.Runtime.Serialization.SerializationInfo object containing the 
        /// information required to serialize this object.
        /// </param>
        /// <param name="context">
        /// A System.Runtime.Serialization.StreamingContext object containing the
        /// source and distination of the serialized stream associated with this
        /// object.
        /// </param>
        protected NameObjectDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion

        #region Methods

        /// <summary>
        /// Get a value from the collection and perform type casting
        /// </summary>
        /// <typeparam name="T">Type you want value cast to.</typeparam>
        /// <param name="key">Key of value.</param>
        /// <param name="value">
        /// Value associated with the key if the key is found and is of the correct type;
        /// if type does not match or key is not found default(T) is returned.
        /// </param>
        /// <returns>True if the key exists and is type matches; else False.</returns>
        public bool TryGetValue<T>(string key, out T value)
        {
            object obj;
            if (base.TryGetValue(key, out obj))
            {
                if (obj is T)
                {
                    value = (T)obj;
                    return true;
                }
                else if (obj == null)
                {
                    value = default(T);
                    return true;
                }
            }
            value = default(T);
            return false;
        }

        #endregion
    }
}
