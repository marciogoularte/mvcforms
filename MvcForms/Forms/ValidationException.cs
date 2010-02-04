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
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security.Permissions;


    /// <summary>
    /// Thrown when validation fails in field clean methods
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        #region Fields

        private ErrorCollection _messages;

        #endregion

        #region .ctor

        /// <summary>
        /// Constructor
        /// </summary>
        public ValidationException()
            : base()
        {
            this._messages = new ErrorCollection();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to display</param>
        public ValidationException(string message)
            : base(message)
        {
            this._messages = new ErrorCollection();
            this._messages.Add(message);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="ex">Previous exception</param>
        public ValidationException(string message, Exception ex)
            : base(message, ex)
        {
            this._messages = new ErrorCollection();
            this._messages.Add(message);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messages">List of messages to return</param>
        public ValidationException(ErrorCollection messages)
        {
            if (messages == null) throw new ArgumentNullException("messages");
            this._messages = messages;
        }

        /// <summary>
        /// Protected serialization constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion

        #region Methods

        /// <summary>
        /// Create a Validation exception with built in formatting
        /// </summary>
        /// <param name="message">Message format</param>
        /// <param name="args">Format parameters</param>
        /// <returns>New ValidationException object</returns>
        public static ValidationException Create(string message, params object[] args)
        {
            return new ValidationException(
                string.Format(CultureInfo.CurrentUICulture, message, args));
        }

        /// <summary>
        /// Helper method for use with serialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// List of messages
        /// </summary>
        public ErrorCollection Messages
        {
            get { return this._messages; }
        }

        #endregion
    }
}
