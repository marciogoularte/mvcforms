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
    using System;
    using System.Web;
    using System.Web.Mvc;


    /// <summary>
    /// Custom model binder for forms
    /// </summary>
    public class FormModelBinder : IModelBinder
    {
        private ControllerContext _controllerContext;

        /// <summary>
        /// Determine if a specific type impliments the IForm interface.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>true if type impliments IForm; false if not.</returns>
        public static bool TypeIsIForm(Type type)
        {
            return type.GetInterface(typeof(IForm).FullName) != null;
        }

        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns>The bound object.</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            _controllerContext = controllerContext;

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            if (TypeIsIForm(bindingContext.ModelType))
            {
                // Create form instance
                var form = Activator.CreateInstance(bindingContext.ModelType) as IForm;

                // Bind data and clean if this is a postback
                if (IsPostBack)
                {
                    form.BindData(Request.Form, Request.Files);
                    form.FullClean();
                }

                return form;
            }
            return null;
        }

        /// <summary>
        /// This is a postback request
        /// </summary>
        private bool IsPostBack
        {
            get 
            {
                return Request.RequestType.Equals("POST", System.StringComparison.OrdinalIgnoreCase); 
            }
        }

        /// <summary>
        /// The request object
        /// </summary>
        private HttpRequestBase Request
        {
            get { return this._controllerContext.HttpContext.Request; }
        }
    }
}
