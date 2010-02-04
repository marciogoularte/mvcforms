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
    /// <summary>
    /// Internal only class to pass to view during wizard process
    /// </summary>
    public class WizardModel
    {
        private WizardController _wizard;

        /// <summary>
        /// Construct a WizardModel objec that wraps a wizard controller.
        /// </summary>
        /// <param name="stepForm">The current form.</param>
        /// <param name="wizard">Wizard controller object that is wrapped by this class.</param>
        /// <param name="hiddenFields">Hidden fields (ie other content) rendered to a string.</param>
        public WizardModel(IForm stepForm, WizardController wizard, string hiddenFields)
        {
            this.Form = stepForm;
            this._wizard = wizard;
            this.HiddenFields = hiddenFields;
        }

        /// <summary>
        /// The current form for this wizard step.
        /// </summary>
        public IForm Form { get; private set; }

        /// <summary>
        /// Name of the form field used to store current step.
        /// </summary>
        public string StepFieldName
        {
            get { return this._wizard.StepFieldName; }
        }

        /// <summary>
        /// The current step number (1 based).
        /// </summary>
        public int CurrentStep 
        { 
            get { return this._wizard.CurrentStep + 1; } 
        }

        /// <summary>
        /// The current step (0 based).
        /// </summary>
        public int CurrentStep0
        {
            get { return this._wizard.CurrentStep; }
        }

        /// <summary>
        /// The current step index (analogous to CurrentStep0).
        /// </summary>
        public int CurrentIndex
        {
            get { return this._wizard.CurrentStep; }
        }

        /// <summary>
        /// The total number of steps in the wizard.
        /// </summary>
        public int StepCount
        {
            get { return this._wizard.StepCount; }
        }

        /// <summary>
        /// Hidden fields rendered as a string.
        /// </summary>
        public string HiddenFields { get; private set; }
    }
}
