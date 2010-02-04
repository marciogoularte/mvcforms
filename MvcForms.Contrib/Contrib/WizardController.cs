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

namespace MvcForms.Contrib
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;


    /// <summary>
    /// MVC Wizard Controller - implements a multi-page form, validating between each
    /// step and storing the form's state as HTML hidden fields so that no state is
    /// stored on the server side.
    /// </summary>
    public abstract class WizardController : Controller
    {
        #region Fields

        /// <summary>
        /// The default HTML (and POST data) field name for the "step" variable.
        /// </summary>
        public const string DefaultStepFieldName = "wizard_step";

        private List<IForm> _forms;

        #endregion

        #region .ctors

        /// <summary>
        /// Construct a wizard controller
        /// </summary>
        protected WizardController()
        {
            StepFieldName = DefaultStepFieldName;
        }

        #endregion

        #region Reflection methods

        /// <summary>
        /// Obtain a list of forms
        /// </summary>
        /// <returns></returns>
        private List<IForm> BuildFormList(NameValueCollection data, HttpFileCollectionBase files)
        {
            // Build field dictionary
            var forms = new List<IForm>();

            // Loop through this form fields
            foreach (FieldInfo fieldInfo in this.GetType().GetFields())
            {
                object obj = fieldInfo.GetValue(this);
                var form = obj as IForm;
                if (form != null)
                {
                    // Setup form to work inside container
                    form.Prefix = this.AddPrefix(fieldInfo.Name);
                    form.BindData(data, files);

                    // Add to collection
                    forms.Add(form);
                }
            }

            return forms;
        }

        #endregion

        #region View methods

        /// <summary>
        /// Method that actuall handles each step
        /// </summary>
        /// <param name="currentStepIndex">The step we are currently on.</param>
        /// <param name="data">Form data.</param>
        /// <param name="files">File data.</param>
        /// <returns></returns>
        protected virtual ActionResult StepHandler(int currentStepIndex, NameValueCollection data, HttpFileCollectionBase files)
        {
            bool firstDisplay = !IsPostBack;

            // Build form collection and setup
            this._forms = BuildFormList(data, files);

            // Sanity check.
            if (currentStepIndex < 0 || currentStepIndex >= StepCount)
            {
                throw new HttpException(404, string.Format(CultureInfo.CurrentUICulture,
                    "Step {0} does not exist.", currentStepIndex));
            }

            // TODO: Process other steps


            // Process the current step. If it's valid, go to the next step or call
            // complete(), depending on whether any steps remain.
            IForm currentForm = _forms[currentStepIndex];
            if (currentForm.IsValid)
            {
                this.ProcessStep(currentForm, currentStepIndex);
                int nextStep = currentStepIndex + 1;

                // If this was the last step, validate all of the forms one more
                // time, as a sanity check, and call complete().
                if (nextStep == StepCount)
                {
                    bool areValid = true;
                    // Revalidate all forms to ensure they are valid
                    for (var idx = 0; idx < _forms.Count; idx++)
                    {
                        var form = _forms[idx];
                        if (!form.IsValid)
                        {
                            areValid = false;
                            firstDisplay = false;
                            currentForm = form;
                            CurrentStep = currentStepIndex = idx;
                            break;
                        }
                    }

                    // Only finish if we are all valid
                    if (areValid) return OnComplete();
                }
                else
                {
                    // Otherwise, move along to the next step.
                    firstDisplay = true;
                    currentForm = _forms[nextStep];
                    CurrentStep = currentStepIndex = nextStep;
                }
            }

            // Since all forms are technically bound be default in the wizard
            // clear all errors the first time the form is displayed so user
            // isn't confused.
            if (firstDisplay)
            {
                currentForm.Errors.Clear();
            }
            return Render(currentForm, data, currentStepIndex);
        }

        /// <summary>
        /// Method that actually handles POST request for a step.
        /// </summary>
        /// <param name="data">Data returned by form</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Step(FormCollection data)
        {
            return StepHandler(DetermineStep(data), data, Request.Files);
        }

        #endregion

        #region Virtual members

        /// <summary>
        /// Return action to perform when wizard is complete
        /// </summary>
        /// <returns></returns>
        protected virtual ActionResult OnComplete()
        {
            return RedirectToAction("Complete");
        }

        /// <summary>
        /// Generate form prefix for this step.
        /// </summary>
        /// <param name="stepIndex">Step number.</param>
        /// <returns>Form prefix.</returns>
        protected virtual string PrefixForStep(int stepIndex)
        {
            return stepIndex.ToString(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Returns the name of the template for a particular step.
        /// </summary>
        /// <param name="stepIndex">Step number.</param>
        /// <returns>Name of template.</returns>
        protected virtual string GetTemplate(int stepIndex)
        {
            return "wizard";
        }

        /// <summary>
        /// Renders the template for the given step, returning an ViewResult object.
        /// Override this method if you want to add a custom context, return a
        /// different MIME type, etc. If you only need to override the template
        /// name, use GetTemplate() instead.
        /// </summary>
        /// <remarks>
        /// The model is assigned a WizardModel object that encapsulates all form elements.
        /// </remarks>
        /// <param name="stepForm">Current steps form.</param>
        /// <param name="hiddenFields">Previous forms rendered as a hidden fields.</param>
        /// <returns><paramref name="System.Web.Mvc.ViewResult"/></returns>
        protected virtual ViewResult RenderTemplate(IForm stepForm, string hiddenFields)
        {
            return View(GetTemplate(CurrentStep), MasterName, new WizardModel(stepForm, this, hiddenFields));
        }

        /// <summary>
        /// Hook for modifying the FormWizard's internal state, given a fully
        /// validated Form object. The Form is guaranteed to have clean, valid
        /// data.
        /// </summary>
        /// <remarks>
        /// This method should *not* modify any of that data. Rather, it might want
        /// to set self.extra_context or dynamically alter self.form_list, based on
        /// previously submitted forms.
        /// 
        /// Note that this method is called every time a page is rendered for *all*
        /// submitted steps.
        /// </remarks>
        /// <param name="stepForm">Fully validated form object.</param>
        /// <param name="stepIndex">Current step.</param>
        protected virtual void ProcessStep(IForm stepForm, int stepIndex) { }

        #endregion

        #region Methods

        /// <summary>
        /// Add form prefix to a fields name
        /// </summary>
        /// <param name="name">Field name</param>
        /// <returns></returns>
        public string AddPrefix(string name)
        {
            return string.IsNullOrEmpty(this.Prefix) ? name : string.Format(CultureInfo.CurrentUICulture, "{0}-{1}", this.Prefix, name);
        }

        /// <summary>
        /// Determine what step we should be on.
        /// </summary>
        /// <param name="data">Data returned from postback.</param>
        /// <returns>The current step</returns>
        protected int DetermineStep(FormCollection data)
        {
            if (!IsPostBack) return 0;
            int stepIndex;
            if (!int.TryParse(data.Get(this.StepFieldName), out stepIndex)) return 0;
            return stepIndex;
        }

        /// <summary>
        /// Renders out the form object and previous field information.
        /// </summary>
        /// <param name="stepForm">Form to render.</param>
        /// <param name="data">Form data posted back to page.</param>
        /// <param name="stepIndex">Current step</param>
        /// <returns>View result object.</returns>
        protected ViewResult Render(IForm stepForm, NameValueCollection data, int stepIndex)
        {
            var hiddenFields = new List<string>();

            // Add current step
            hiddenFields.Add(new MvcForms.Widgets.HiddenInput().Render(StepFieldName, stepIndex, 
                ElementAttributesDictionary.Create(new {id = "id_" + StepFieldName})));

            for (int idx = 0; idx < _forms.Count; idx++)
            {
                if (idx != stepIndex)
                {
                    // Check if we are dealing with a form
                    var form = _forms[idx] as Form;
                    if (form != null)
                    {
                        // TODO: Add hash support
                        foreach (var boundField in form)
                        {
                            hiddenFields.Add(boundField.AsHidden());
                        }
                        continue;
                    }

                    // Check if we are dealing with a form group
                    var formGroup = _forms[idx] as FormGroup;
                    if (formGroup != null)
                    {
                        foreach (var boundForm in formGroup)
                        {
                            // TODO: Add hash support
                            foreach (var boundField in boundForm.Form)
                            {
                                hiddenFields.Add(boundField.AsHidden());
                            }
                        }
                    }
                }
            }

            return RenderTemplate(stepForm, string.Join("\n", hiddenFields.ToArray()));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Prefix to append to field names.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Override for master page
        /// </summary>
        protected string MasterName { get; set; }

        /// <summary>
        /// The HTML (and POST data) field name for the "step" variable.
        /// </summary>
        public string StepFieldName { get; set; }

        /// <summary>
        /// The current step
        /// </summary>
        public int CurrentStep { get; private set; }

        /// <summary>
        /// Number of forms/pages in wizard
        /// </summary>
        public int StepCount
        {
            get { return _forms.Count; }
        }

        /// <summary>
        /// Returns true if data has been posted
        /// </summary>
        protected bool IsPostBack
        {
            get { return Request.RequestType == "POST"; }
        }

        #endregion
    }
}
