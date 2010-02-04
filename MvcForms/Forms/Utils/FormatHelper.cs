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

namespace MvcForms.Utils
{
    using System.Globalization;
    using System.Text;


    /// <summary>
    /// Static class that provides helpers for formatting form elements
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// Helper method to convert camelcase method names into readable labels
        /// </summary>
        /// <remarks>This method assums that a name is a valid C# Property/Field name.</remarks>
        /// <param name="name">Name of field</param>
        /// <returns>Readable label</returns>
        public static string BeautifyName(string name)
        {
            return BeautifyName(name, false);
        }

        /// <summary>
        /// Helper method to convert camelcase method names into readable labels
        /// </summary>
        /// <remarks>This method assums that a name is a valid C# Property/Field name.</remarks>
        /// <param name="name">Name of field</param>
        /// <param name="titleCase">Return result in title case.</param>
        /// <returns>Readable label</returns>
        public static string BeautifyName(string name, bool titleCase)
        {
            StringBuilder result = new StringBuilder(name.Length);

            // Loop through all characters
            foreach (char letter in name)
            {
                // New word boundary
                if (char.IsUpper(letter))
                {
                    if (result.Length > 0) result.Append(" ");

                    if (result.Length == 0 || titleCase)
                    {
                        result.Append(letter);
                    }
                    else
                    {
                        result.Append(char.ToLower(letter, CultureInfo.CurrentUICulture));
                    }
                }
                // Insert spaces for underscores
                else if (letter == '_')
                {
                    if (result.Length != 0) result.Append(" ");
                }
                else
                {
                    if (result.Length == 0)
                    {
                        result.Append(char.ToUpper(letter, CultureInfo.CurrentUICulture));
                    }
                    else
                    {
                        result.Append(letter);
                    }
                }
            }

            return result.ToString().Trim();
        }
    }
}
