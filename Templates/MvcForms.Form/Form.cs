namespace $rootnamespace$
{
    using System.Collections.Specialized;

    using JL.Web.Forms;
    using Fields = JL.Web.Forms.Fields;
    using Widgets = JL.Web.Forms.Widgets;
    
    
    public class $safeitemrootname$ : Form
    {
        // Example:
        //
        // public static IField MyField = new Fields.StringField 
        // { 
        //     MaxLength = 100,
        // };
        
        #region .ctors
        
        /// <summary>
        /// Construct form object.
        /// </summary>
        public $safeitemrootname$() { }
        
        /// <summary>
        /// Construct form object.
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        public $safeitemrootname$(NameValueCollection data) 
            : base(data, null) { }
        
        /// <summary>
        /// Construct form object.
        /// </summary>
        /// <param name="data">Data to bind form to.</param>
        /// <param name="files">File data to bind form to.</param>
        public $safeitemrootname$(NameValueCollection data, System.Web.HttpFileCollectionBase files) 
            : base(data, files) { }
        
        #endregion
    }
}
