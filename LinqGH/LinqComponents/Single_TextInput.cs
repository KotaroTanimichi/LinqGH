using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Single_TextInput : LinqComponentBase
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Single_TextInput()
          : base("Single_TextInput", "Single_TextInput",
            "Returns a single, specific element of a sequence. ex. \"x => x>0\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.SingleDynamic(lambdaExpression) };
        }

        protected override IEnumerable<object> Evaluate(string expression, Dictionary<string, object[]> inputLists, int listLength)
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Bitmap Icon => Resources.ISingle;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("c204a738-d093-4033-bd0c-acbd4bbcf47c");

    }
}