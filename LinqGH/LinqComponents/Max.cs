using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Max : LinqComponentBase
    {

        public Max(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Max(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Max() : base("Max", "Max", "Returns the maximum value in a sequence of values. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("a04baf32-472c-4b57-86a0-35319dcd7631");

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
        protected override Bitmap Icon => Resources.Max;
    }
}