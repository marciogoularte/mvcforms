namespace MvcExample.Forms
{
    using System.Collections.Specialized;
    using System.Web;

    using MvcForms;


    /// <summary>
    /// Example form collection
    /// </summary>
    public class ExampleFormGroup : FormGroup
    {
        #region .ctors

        public ExampleFormGroup() : base(null, null) { }
        public ExampleFormGroup(NameValueCollection data, HttpFileCollectionBase files) : base(data, files) { }

        #endregion

        public IForm Section2 = new Section2Form() {
            Label = "User Preferences"
        };

        public IForm Section3 = new Section3Form() {
            Label = "Additional Information"
        };
    }
}
