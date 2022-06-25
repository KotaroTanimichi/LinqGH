using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class TakeWhile_TextInput : LinqComponentBase_TextInput
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public TakeWhile_TextInput()
          : base("TakeWhile_TextInput", "TakeWhile_TextInput",
            "Returns elements from a sequence as long as a specified condition is true, and then skips the remaining elements. ex. \"x => x>0\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.TakeWhileDynamic(lambdaExpression);
        }

        protected override System.Drawing.Bitmap Icon => Resources.ITakeWhile;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("b5046ed0-120e-4e63-ac2b-7c4024aa703c");

    }
}