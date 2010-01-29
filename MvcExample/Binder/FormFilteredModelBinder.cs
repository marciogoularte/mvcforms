namespace MvcExample.Binder
{
    using System;

    using JL.Web.Forms;


    /// <summary>
    /// Wrapper over FormModelBinder to impliment filtered model binder
    /// </summary>
    public class FormFilteredModelBinder : FormModelBinder, IFilteredModelBinder
    {
        public bool IsMatch(Type modelType)
        {
            return FormModelBinder.TypeIsIForm(modelType);
        }
    }
}
