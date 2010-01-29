namespace MvcExample.Binder
{
    using System;
    using System.Web.Mvc;


    /// <summary>
    /// Slightly smarter model binder
    /// </summary>
    public interface IFilteredModelBinder : IModelBinder
    {
        bool IsMatch(Type modelType);
    }
}
