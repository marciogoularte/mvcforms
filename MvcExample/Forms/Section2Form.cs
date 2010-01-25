namespace MvcExample.Forms
{
    using System.Collections.Specialized;

    using JL.Web.Forms;
    using Fields = JL.Web.Forms.Fields;
    using Widgets = JL.Web.Forms.Widgets;
    using ContribWidgets = JL.Web.Forms.Contrib.Widgets;


    /// <summary>
    /// Example form - Section 2
    /// </summary>
    public class Section2Form : Form
    {
        #region .ctors

        public Section2Form() : base(null, null) { }
        public Section2Form(NameValueCollection data) : base(data, null) { }

        #endregion

        #region Fields

        public static IField FavoriteColours = new Fields.MultipleChoiceField(new Widgets.CheckBoxSelectMultiple())
        {
            Choices = ExampleChoices.Colours()
        };

        public static IField YourState = new Fields.StringField(
            new Widgets.Select()
            {
                Choices = Widgets.Choices.AustralianStates.Full,
                ShowDefault = true
            }
        );

        public static IField Price = new Fields.FloatField()
        {
            HelpText = "Enter the price you wish to get...",
            MinValue = 0,
            MaxValue = 100000,
            Required = false
        };

        public static IField IncludePartner = new Fields.NullBooleanField()
        {
            Label = "Include your partner?",
            HelpText = "Do you wish to include your partner in this"
        };

        public static IField ExpiryDate = new Fields.DateField(new ContribWidgets.JQueryUI.DatePicker());

        #endregion

        #region Clean Methods

        /// <summary>
        /// Clean method
        /// </summary>
        /// <param name="cleanedData"></param>
        /// <returns></returns>
        protected override NameObjectDictionary Clean(NameObjectDictionary cleanedData)
        {
            float price;
            bool includePartner;

            if (cleanedData.TryGetValue<float>("Price", out price) &&
                cleanedData.TryGetValue<bool>("IncludePartner", out includePartner) && 
                includePartner)
            {
                if (price > 50) throw new ValidationException("Price with partner can not be over 50.");
            }

            return base.Clean(cleanedData);
        }

        #endregion
    }
}
