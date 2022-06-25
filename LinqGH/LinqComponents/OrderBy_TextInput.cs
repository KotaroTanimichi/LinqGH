using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class OrderBy_TextInput : LinqComponentBase
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public OrderBy_TextInput()
          : base("OrderBy_TextInput", "OrderBy_TextInput",
            "Sorts the elements of a sequence in ascending order. ex. \"x => x*x\"")
        {
        }

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.OrderByDynamic(lambdaExpression);
        }

        protected override IEnumerable<object> Evaluate(string expression, Dictionary<string, object[]> inputLists, int listLength)
        {
            throw new NotImplementedException();
        }

        protected override System.Drawing.Bitmap Icon => Resources.IOrderBy;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("80b117ef-8e3f-4cec-906c-af1f6e248040");

    }
}