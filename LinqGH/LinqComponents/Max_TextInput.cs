using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Max_TextInput : LinqComponentBase_TextInput
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Max_TextInput()
          : base("Max_TextInput", "Max_TextInput",
            "Returns the maximum value in a sequence of values. ex. \"x => x*x\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
                return new object[] { values.MaxDynamic<object>(lambdaExpression) };
            else
            {
                var converted = values.SelectDynamic(lambdaExpression).Cast<IComparable>();
                return new object[] { converted.MaxDynamic<object>() };
            }
        }

        protected override System.Drawing.Bitmap Icon => Resources.IMax;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("14883103-f272-4a7a-a541-7a1db24513c5");

    }
}