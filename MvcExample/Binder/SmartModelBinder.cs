namespace MvcExample.Binder
{
    using System.Web.Mvc;


    /// <summary>
    /// Smart model binder as explained by Jimmy Bogard, see:
    /// http://www.lostechies.com/blogs/jimmy_bogard/archive/2009/03/17/a-better-model-binder.aspx
    /// </summary>
    public class SmartModelBinder : DefaultModelBinder
    {
        private readonly IFilteredModelBinder[] _filteredModelBinderers;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filteredModelBinderers">Filtered model binder instances.</param>
        public SmartModelBinder(IFilteredModelBinder[] filteredModelBinderers)
        {
            this._filteredModelBinderers = filteredModelBinderers;
        }

        /// <summary>
        /// Bind a model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binder context.</param>
        /// <returns>The bound model.</returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            foreach (var filteredModelBinder in this._filteredModelBinderers)
            {
                if (filteredModelBinder.IsMatch(bindingContext.ModelType))
                {
                    return filteredModelBinder.BindModel(controllerContext, bindingContext);
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
