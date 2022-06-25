using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Where_TextInput : LinqComponentBase_TextInput
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Where_TextInput()
          : base("Where_TextInput", "Where_TextInput",
            "Filters a sequence of values based on a predicate. ex. \"x => x>0\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.WhereDynamic(lambdaExpression);
        }

        protected override System.Drawing.Bitmap Icon => Resources.IWhere;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("f4d28c6c-d85b-4cb5-a433-76eac5ba9b1f");

    }
}