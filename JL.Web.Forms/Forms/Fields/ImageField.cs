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
    using System;
    using System.Drawing;
    using System.Web;


    /// <summary>
    /// Additional helpers for uploading images
    /// </summary>
    public class ImageField : FileField
    {
        #region Fields

        /// <summary>
        /// Invalid image message key
        /// </summary>
        public const string MessageInvalidImage = "invalid_image";

        /// <summary>
        /// Max width message key
        /// </summary>
        public const string MessageMaxWidth = "max_width";

        /// <summary>
        /// Max height message key
        /// </summary>
        public const string MessageMaxHeight = "max_height";

        #endregion

        #region .ctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ImageField() : this(new Widgets.FileInput()) { }

        /// <summary>
        /// Construct an image field
        /// </summary>
        public ImageField(IFileWidget widget)
            : base(widget)
        {
            this.ErrorMessages[MessageInvalidImage] = DefaultErrorMessages.InvalidImage;
            this.ErrorMessages[MessageMaxWidth] = DefaultErrorMessages.MaxWidthImage;
            this.ErrorMessages[MessageMaxHeight] = DefaultErrorMessages.MaxHeightImage;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Typed Clean method
        /// </summary>
        /// <param name="file">File objec to clean.</param>
        /// <returns>Cleaned file.</returns>
        protected override HttpPostedFileBase CleanFile(HttpPostedFileBase file)
        {
            file = base.CleanFile(file);

            Image img;
            
            // Try load the image
            try { img = Image.FromStream(file.InputStream); }
            catch (ArgumentException)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageInvalidImage]);
            }

            // Check image width
            if (MaxWidth != null && img.Width > MaxWidth)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMaxWidth],
                    MaxWidth, img.Width);
            }

            // Check image height
            if (MaxHeight != null && img.Height > MaxHeight)
            {
                throw ValidationException.Create(this.ErrorMessages[MessageMaxHeight],
                    MaxHeight, img.Height);
            }

            return file;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Maximum Width of image.
        /// </summary>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Maximum Height of image.
        /// </summary>
        public int? MaxHeight { get; set; }

        #endregion
    }
}
