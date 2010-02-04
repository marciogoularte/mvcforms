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

namespace MvcForms.Fields
{
    /// <summary>
    /// Allows choosing from files inside a certain directory.
    /// </summary>
    public class FilePathField : ChoiceField
    {
        #region Fields

        private Widgets.Choices.FilePathCollection _filePath = new Widgets.Choices.FilePathCollection();

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FilePathField() : this(new Widgets.Select()) { }

        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public FilePathField(IChoiceWidget widget)
            : base(widget) 
        {
            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.Invalid;
            this.ErrorMessages[MessageInvalidChoice] = DefaultErrorMessages.InvalidChoice;

            this.Choices = _filePath;
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Path to directory that supplies files
        /// </summary>
        public string Path
        { 
            get { return _filePath.Path; }
            set { _filePath.Path = value; }
        }

        /// <summary>
        /// Should the search be recursive
        /// </summary>
        public bool Recursive
        {
            get { return _filePath.Recursive; }
            set { _filePath.Recursive = value; }
        }

        /// <summary>
        /// Pattern to match files
        /// </summary>
        public string SearchPattern
        {
            get { return _filePath.SearchPattern; }
            set { _filePath.SearchPattern = value; }
        }

        #endregion Properties
    }
}
