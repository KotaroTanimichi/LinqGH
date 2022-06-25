using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Select_TextInput : LinqComponentBase
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Select_TextInput()
          : base("Select_TextInput", "Select_TextInput",
            "Projects each element of a sequence into a new form. ex. \"x => x.ToString()\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.SelectDynamic(lambdaExpression);
        }

        protected override IEnumerable<object> Evaluate(string expression, Dictionary<string, object[]> inputLists, int listLength)
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Bitmap Icon => Resources.ISelect;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("89068685-2954-4FB9-84C2-548BE1D0EFB0");

    }
}