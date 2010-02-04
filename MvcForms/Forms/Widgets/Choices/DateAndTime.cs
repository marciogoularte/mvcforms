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

namespace MvcForms.Widgets.Choices
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;


    /// <summary>
    /// Collections of DateTime choices for select,multiselect etc widgets
    /// </summary>
    public static class DateAndTime
    {
        /// <summary>
        /// Long names of Months
        /// </summary>        
        public static ReadOnlyCollection<Choice> MonthsLong { get { return _monthsLong; } }
        private static ReadOnlyCollection<Choice> _monthsLong = new ReadOnlyCollection<Choice>(
            new List<Choice>() {
                new Choice("January"),
                new Choice("Febuary"),
                new Choice("March"),
                new Choice("April"),
                new Choice("May"),
                new Choice("June"),
                new Choice("July"),
                new Choice("August"),
                new Choice("September"),
                new Choice("October"),
                new Choice("November"),
                new Choice("December")
            }
        );

        /// <summary>
        /// Short names of Months
        /// </summary>
        public static ReadOnlyCollection<Choice> MonthsShort { get { return _monthsShort; } }
        private static ReadOnlyCollection<Choice> _monthsShort = new ReadOnlyCollection<Choice>(
            new List<Choice>() {
                new Choice("Jan"),
                new Choice("Feb"),
                new Choice("Mar"),
                new Choice("Apr"),
                new Choice("May"),
                new Choice("Jun"),
                new Choice("Jul"),
                new Choice("Aug"),
                new Choice("Sep"),
                new Choice("Oct"),
                new Choice("Nov"),
                new Choice("Dec")
            }
        );

        /// <summary>
        /// Long names of Days
        /// </summary>
        public static ReadOnlyCollection<Choice> DaysLong { get { return _daysLong; } }
        private static ReadOnlyCollection<Choice> _daysLong = new ReadOnlyCollection<Choice>(
            new List<Choice>() {
                new Choice("Monday"),
                new Choice("Tuesday"),
                new Choice("Wednesday"),
                new Choice("Thursday"),
                new Choice("Friday"),
                new Choice("Saturday"),
                new Choice("Sunday")
            }
        );

        /// <summary>
        /// Short names of Days
        /// </summary>
        public static ReadOnlyCollection<Choice> DaysShort { get { return _daysShort; } }
        private static ReadOnlyCollection<Choice> _daysShort = new ReadOnlyCollection<Choice>(
            new List<Choice>() {
                new Choice("Mon"),
                new Choice("Tue"),
                new Choice("Wed"),
                new Choice("Thu"),
                new Choice("Fri"),
                new Choice("Sat"),
                new Choice("Sun")
            }
        );
    }
}
