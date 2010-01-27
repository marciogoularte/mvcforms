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

namespace JL.Web.Forms
{
    using System.Web.Mvc;
    using System;


    /// <summary>
    /// Controller with extentions to easily enable AJAX validation.
    /// </summary>
    /// <remarks>
    /// Please note, AJAX validation is unsupported with <typeparamref name="FileField"/>s 
    /// (or any sub classes of <typeparamref name="FileField"/>.
    /// Validation of fields that have dependancies on other fields (ie comparing password 
    /// fields) is only supported by the full clean methods. Be aware that full clean will
    /// do full validation of all fields even if values are not supplied for them, this is
    /// important if any custom Clean methods validate against a database or other data
    /// source external to your application.
    /// </remarks>
    public abstract class AjaxController : Controller
    {
        #region Methods

        /// <summary>
        /// Validates form fields passed against a form and returns an Json result.
        /// </summary>
        /// <param name="formToValidate">Form instance to validate against.</param>
        /// <param name="data">Form values to validate.</param>
        /// <returns>Action result containing Json.</returns>
        protected ActionResult CleanForm(Form formToValidate, FormCollection data)
        {
            var fields = formToValidate.Fields;
            var errors = new ErrorDictionary();
            var cleanedData = new NameObjectDictionary();

            foreach (string fieldName in data.Keys)
            {
                IField field;
                // Check if supplied field is valid
                if (fields.TryGetValue(fieldName, out field))
                {
                    object value = field.Widget.GetValueFromDataCollection(data, null, fieldName);
                    try
                    {
                        cleanedData.Add(fieldName, field.Clean(value));

                        // Do custom validation
                        if (field.CustomClean != null)
                        {
                            cleanedData = field.CustomClean(cleanedData);
                        }
                    }
                    catch (ValidationException vex)
                    {
                        errors.Add(fieldName, vex.Messages);
                    }
                }
            }

            if (errors.Count == 0)
            {
                return Json(new { isValid = true });
            }
            else
            {
                return Json(new { isValid = false, errors = errors });
            }
        }

        /// <summary>
        /// Validates a number of form fields within a form and returns an Json result.
        /// </summary>
        /// <param name="formToValidate">IForm instance to validate against.</param>
        /// <param name="data">Form values to validate.</param>
        /// <returns>Action result containing Json.</returns>
        protected ActionResult FullCleanForm(IForm formToValidate, FormCollection data)
        {
            formToValidate.BindData(data, null);
            formToValidate.FullClean();

            if (formToValidate.IsValid)
            {
                return Json(new { isValid = true });
            }
            else
            {
                return Json(new { isValid = false, errors = formToValidate.Errors });
            }
        }

        #endregion
    }
}
