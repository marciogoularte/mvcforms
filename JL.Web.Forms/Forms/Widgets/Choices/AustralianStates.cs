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

namespace JL.Web.Forms.Widgets.Choices
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;


    /// <summary>
    /// Collections of Australian states for select,multiselect etc widgets
    /// </summary>
    public static class AustralianStates
    {
        /// <summary>
        /// Abbreviated names of Australian states
        /// </summary>
        public static ReadOnlyCollection<Choice> Abbreviated { get { return _abbeviated; } }
        private static ReadOnlyCollection<Choice> _abbeviated = new ReadOnlyCollection<Choice>(
            new List<Choice>() {
                new Choice("ACT"),
                new Choice("NSW"),
                new Choice("QLD"),
                new Choice("SA"),
                new Choice("TAS"),
                new Choice("VIC"),
                new Choice("WA")
            }
        );

        /// <summary>
        /// Full names of Australian states
        /// </summary>
        public static ReadOnlyCollection<Choice> Full { get { return _full; } }
        private static ReadOnlyCollection<Choice> _full = new ReadOnlyCollection<Choice>(
            new List<Choice>() {
                new Choice("ACT", "Australian Capitol Territory"),
                new Choice("NSW", "New South Wales"),
                new Choice("QLD", "Queensland"),
                new Choice("SA", "South Australia"),
                new Choice("TAS", "Tasmania"),
                new Choice("VIC", "Victoria"),
                new Choice("WA", "Western Australia")
            }
        );
    }
}
