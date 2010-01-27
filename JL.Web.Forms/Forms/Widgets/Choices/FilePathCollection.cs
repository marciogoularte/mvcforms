﻿#region License
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

namespace JL.Web.Forms.Widgets.Choices
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;


    /// <summary>
    /// Choices class that enumerates a file path
    /// </summary>
    public class FilePathCollection : IEnumerable<Choice>
    {
        #region Methods

        /// <summary>
        /// Get Choice enumerator.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Choice> GetEnumerator()
        {
            if (string.IsNullOrEmpty(Path)) throw new ArgumentException("Path not defined", "Path");
            foreach (var file in Directory.GetFiles(Path, 
                string.IsNullOrEmpty(SearchPattern) ? "*.*" : SearchPattern,
                Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                yield return new Choice(file);
        }

        /// <summary>
        /// Get generic enumerator.
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Path to directory that supplies files
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Should the search be recursive
        /// </summary>
        public bool Recursive { get; set; }

        /// <summary>
        /// Pattern to match files
        /// </summary>
        public string SearchPattern { get; set; }

        #endregion Properties
    }
}