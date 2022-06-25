using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class All : LinqComponentBase
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public All()
          : base("All", "All",
            "Determines whether all elements of a sequence satisfy a condition. ex. \"x>0\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.AllDynamic(lambdaExpression) };
        }

        protected override IEnumerable<object> Evaluate(string expression, Dictionary<string, object[]> inputLists, int listLength)
        {
            return new object[] { Enumerable.Range(0, listLength).All(i => (bool)EvalHelper.Execute(expression, GetParameterDict(inputLists, i))) };
        }

        protected override System.Drawing.Bitmap Icon => Resources.IAll;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("283aa0e1-597f-4933-a7fc-257f75f5ccdf");

    }
}