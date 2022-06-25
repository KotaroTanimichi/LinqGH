using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Min_TextInput : LinqComponentBase_TextInput
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Min_TextInput()
          : base("Min_TextInput", "Min_TextInput",
            "Returns the minimum value in a sequence of values. ex. \"x => x*x\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
                return new object[] { values.MinDynamic<object>(lambdaExpression) };
            else
            {
                var converted = values.SelectDynamic(lambdaExpression).Cast<IComparable>();
                return new object[] { converted.MinDynamic<object>() };
            }
        }

        protected override System.Drawing.Bitmap Icon => Resources.IMin;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("fed8d04a-a4e9-4180-8b3c-163a895df544");

    }
}