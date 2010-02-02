namespace $rootnamespace$
{
	using System.Collections.Specialized;
	using System.Web;

    using JL.Web.Forms;
    
    
	public class $safeitemrootname$ : FormGroup
	{
		// Example:
		//
		// public IForm Example = new ExampleForm
		// {
		//     Label = "Example form label",
		// };
		
		#region .ctors
		
        /// <summary>
        /// Construct form group object.
        /// </summary>
        public $safeitemrootname$() { }
        
        /// <summary>
        /// Construct form group object.
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        public $safeitemrootname$(NameValueCollection data) : base(data, null) { }
        
        /// <summary>
        /// Construct form group object.
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        /// <param name="files">File data to bind form to.</param>
        public $safeitemrootname$(NameValueCollection data, HttpFileCollectionBase files) : base(data, files) { }
		
		#endregion
	}
}
