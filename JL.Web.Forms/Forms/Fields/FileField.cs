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

namespace JL.Web.Forms.Fields
{
    using System.Web;


    /// <summary>
    /// Field that handles file uploads
    /// </summary>
    public class FileField : BaseField, IFileField
    {
        #region Fields

        /// <summary>
        /// Missing value message key
        /// </summary>
        public const string MessageMissing = "missing";
        /// <summary>
        /// Empty value message key
        /// </summary>
        public const string MessageEmpty = "empty";
        /// <summary>
        /// Max length value message key
        /// </summary>
        public const string MessageMaxLength = "max_length";
        /// <summary>
        /// Max size value message key
        /// </summary>
        public const string MessageMaxSize = "max_size";

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileField() : this(new Widgets.FileInput()) { }
        /// <summary>
        /// Widget override constructor
        /// </summary>
        /// <param name="widget">Widget to override default</param>
        public FileField(IFileWidget widget)
            : base(widget) 
        {
            this.ErrorMessages[MessageInvalid] = DefaultErrorMessages.InvalidFile;
            this.ErrorMessages[MessageMissing] = DefaultErrorMessages.MissingFile;
            this.ErrorMessages[MessageEmpty] = DefaultErrorMessages.EmptyFile;
            this.ErrorMessages[MessageMaxLength] = DefaultErrorMessages.MaxLengthFile;
            this.ErrorMessages[MessageMaxSize] = DefaultErrorMessages.MaxSize;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Validates the given value and returns its "cleaned" value as an
        /// appropriate .Net object.
        /// </summary>
        /// <param name="value">Value to clean</param>
        /// <returns>Cleaned value</returns>
        /// <exception cref="ValidationException">On an invalid validation</exception>
        public override object Clean(object value)
        {
            var file = value as HttpPostedFileBase;
            base.Clean(file);
            return CleanFile(file);
        }

        /// <summary>
        /// Typed Clean method
        /// </summary>
        /// <param name="file">File objec to clean.</param>
        /// <returns>Cleaned file.</returns>
        protected virtual HttpPostedFileBase CleanFile(HttpPostedFileBase file)
        {
            // Ensure a valid file name is set
            if (string.IsNullOrEmpty(file.FileName) && Required)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageRequired]);
            }

            // Ensure filename isn't too long
            if (MaxLength != null && file.FileName.Length > MaxLength)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMaxLength],
                    MaxLength, file.FileName.Length
                );
            }

            // Check file isn't empty
            if (file.ContentLength == 0)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageEmpty]);
            }

            // Check file isn't to big
            if (MaxSize != null && file.ContentLength > MaxSize)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMaxSize],
                    Utils.HumaniseHelper.FileSize(MaxSize),
                    Utils.HumaniseHelper.FileSize(file.ContentLength)
                );
            }

            return file;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Maximum length of file name
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Maximum size of file
        /// </summary>
        public int? MaxSize { get; set; }

        #endregion
    }
}
