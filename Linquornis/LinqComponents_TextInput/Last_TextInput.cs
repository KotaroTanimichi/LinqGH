using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents_TextInput
{
    public class Last_TextInput : LinqComponentBase_TextInput
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Last_TextInput()
          : base("Last_TextInput", "Last_TextInput",
            "Description")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.LastDynamic(lambdaExpression) };
        }

        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("389f539d-cc2a-42bb-ad99-b1ff9f5caf65");

    }
}