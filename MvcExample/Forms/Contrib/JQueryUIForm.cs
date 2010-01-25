namespace MvcExample.Forms.Contrib
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using JL.Web.Forms;
    using Fields = JL.Web.Forms.Fields;
    using Widgets = JL.Web.Forms.Widgets;
    using ContribWidgets = JL.Web.Forms.Contrib.Widgets;


    public class JQueryUIForm : Form
    {
        public static IField DatePicker = new Fields.DateField(
            new ContribWidgets.JQueryUI.DatePicker
            {
                ShowButtonPanel = true,
                ShowAnimation = "fadeIn",
            });

        public static IField Slider = new Fields.IntegerField(
            new ContribWidgets.JQueryUI.Slider
            {
                Animate = true,
                Step = 10,
            });

        public static IField SortableCollection = new Fields.CollectionField(
            new ContribWidgets.JQueryUI.SortableCollection())
            {
                Initial = new Collection<object> { 1, 2, 3, 4, 5 },
            };

        #region .ctors

        public JQueryUIForm() { }
        public JQueryUIForm(NameValueCollection data) : base(data, null) { }

        #endregion
    }
}
