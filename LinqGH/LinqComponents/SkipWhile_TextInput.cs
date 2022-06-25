using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class SkipWhile_TextInput : LinqComponentBase
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SkipWhile_TextInput()
          : base("SkipWhile_TextInput", "SkipWhile_TextInput",
            "Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. ex. \"x => x>0\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.SkipWhileDynamic(lambdaExpression);
        }

        protected override IEnumerable<object> Evaluate(string expression, Dictionary<string, object[]> inputLists, int listLength)
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Bitmap Icon => Resources.ISkipWhile;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("f43e9211-7ffe-4a53-a395-60d7489c8463");

    }
}