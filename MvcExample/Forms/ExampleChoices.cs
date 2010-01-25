namespace MvcExample.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using JL.Web.Forms;


    /// <summary>
    /// Some examples of choice methods
    /// </summary>
    public static class ExampleChoices
    {
        public static IEnumerable<Choice> Colours()
        {
            string[] colours = new string[] {"green", "pink", "yellow", "blue", "red", "black", "white"};
            foreach (var colour in colours) yield return new Choice(colour);
        }
    }
}
