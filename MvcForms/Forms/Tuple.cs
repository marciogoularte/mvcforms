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

namespace MvcForms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;


    /// <summary>
    /// C# implimnetation of a Tuple
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "A tuple is a valid container type name.")]
    public sealed class Tuple : IEnumerable<object>
    {
        #region Fields

        private List<object> _items;

        #endregion

        #region .ctors

        /// <summary>
        /// Construct from Enumerable source.
        /// </summary>
        /// <param name="items"></param>
        public Tuple(IEnumerable items)
        {
            _items = new List<object>();
            foreach (var item in items) _items.Add(item);
       }

        /// <summary>
        /// Construct from Collection source.
        /// </summary>
        /// <param name="items"></param>
        public Tuple(ICollection items)
        {
            _items = new List<object>(items.Count);
            foreach (var item in items) _items.Add(item);
        }

        /// <summary>
        /// Construct from argument list.
        /// </summary>
        /// <param name="args">List of arguments.</param>
        public Tuple(params object[] args)
        {
            _items = new List<object>(args);
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="tuple">Existing tuple to copy from.</param>
        public Tuple(Tuple tuple)
        {
            _items = new List<object>(tuple._items);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get object enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<object> GetEnumerator()
        {
            foreach (var item in _items) yield return item;
        }

        /// <summary>
        /// Get generic enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get an item by index.
        /// </summary>
        /// <param name="index">Index of item.</param>
        /// <returns>Item at index.</returns>
        public object this[int index]
        {
            get 
            {
                return _items[index]; 
            }
        }

        /// <summary>
        /// Get an item from tuple.
        /// </summary>
        /// <param name="index">Index of item.</param>
        /// <returns>Item at index.</returns>
        public object Get(int index)
        {
            try
            {
                return _items[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Get an item from the tuple and cast to correct type.
        /// </summary>
        /// <typeparam name="T">Type to try and cast to.</typeparam>
        /// <param name="index">Index of item.</param>
        /// <returns>Item at index; default(T) if not found.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "This is a helper method.")]
        public T Get<T>(int index)
        {
            object obj = Get(index);
            if (obj is T) return (T)obj;
            return default(T);
        }

        /// <summary>
        /// Get the length of tuple
        /// </summary>
        public int Length
        {
            get { return _items.Count; }
        }

        #endregion
    }
}
