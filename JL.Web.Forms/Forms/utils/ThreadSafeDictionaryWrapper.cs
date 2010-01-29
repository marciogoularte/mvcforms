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

namespace JL.Web.Forms.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Threading;


    /// <summary>
    /// A thread safe wrapper around Generic Dictionary
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TValue">Type of value.</typeparam>
    public class ThreadSafeDictionaryWrapper<TKey, TValue>
    {
        #region Fields

        private ReaderWriterLock _rwLock = new ReaderWriterLock();
        private Dictionary<TKey, TValue> _store = new Dictionary<TKey, TValue>();

        #endregion

        #region Methods

        /// <summary>
        /// Try to get a value from the dictionary.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns contains the value associated with the supplied key; if
        /// the key is not found value will be set to the default value of the TValue type.
        /// </param>
        /// <returns>True if key is found; False if key is not found.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.TryGetValue(key, out value, Timeout.Infinite);
        }

        /// <summary>
        /// Try to get a value from the dictionary.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns contains the value associated with the supplied key; if
        /// the key is not found value will be set to the default value of the TValue type.
        /// </param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds to aquire read lock.</param>
        /// <returns>True if key is found; False if key is not found.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#",
            Justification = "Matches pattern used on Dictionary")]
        public bool TryGetValue(TKey key, out TValue value, int millisecondsTimeout)
        {
            _rwLock.AcquireReaderLock(millisecondsTimeout);
            try
            {
                return _store.TryGetValue(key, out value);
            }
            finally
            {
                _rwLock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Insert a value into dictionary but do not overwrite existing value.
        /// </summary>
        /// <param name="key">The key to associated value with.</param>
        /// <param name="value">Value to insert.</param>
        /// <returns>
        /// Value that was associated with key; if key already exists in the dictionary 
        /// return assciated value else return supplied value.
        /// </returns>
        public TValue SetDefault(TKey key, TValue value)
        {
            return this.SetDefault(key, value, Timeout.Infinite);
        }

        /// <summary>
        /// Insert a value into dictionary but do not overwrite existing value.
        /// </summary>
        /// <param name="key">The key to associated value with.</param>
        /// <param name="value">Value to insert.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds to aquire write lock.</param>
        /// <returns>
        /// Value that was associated with key; if key already exists in the dictionary 
        /// return assciated value else return supplied value.
        /// </returns>
        public TValue SetDefault(TKey key, TValue value, int millisecondsTimeout)
        {
            _rwLock.AcquireWriterLock(millisecondsTimeout);
            try
            {
                TValue existingEntry;
                if (_store.TryGetValue(key, out existingEntry))
                {
                    // Another thread already inserted an item, so use that one
                    return existingEntry;
                }

                _store[key] = value;
                return value;
            }
            finally
            {
                _rwLock.ReleaseWriterLock();
            }
        }

        #endregion 
    }
}
