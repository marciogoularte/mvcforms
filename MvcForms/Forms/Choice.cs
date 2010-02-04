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

namespace MvcForms
{
    /// <summary>
    /// Choice object.
    /// </summary>
    public class Choice : IChoice
    {
        #region .ctors

        /// <summary>
        /// Construct a choice object.
        /// </summary>
        public Choice() { }

        /// <summary>
        /// Construct a choice object.
        /// </summary>
        /// <param name="value">Value for choice</param>
        public Choice(object value)
        {
            this.Value = value;
            this.Label = value.ToString();
        }
        
        /// <summary>
        /// Construct a choice object.
        /// </summary>
        /// <param name="value">Value for choice</param>
        /// <param name="label">Label for choice</param>
        public Choice(object value, string label)
        {
            this.Label = label;
            this.Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Label for choice
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Value for choice
        /// </summary>
        public object Value { get; set; }

        #endregion
    }
}