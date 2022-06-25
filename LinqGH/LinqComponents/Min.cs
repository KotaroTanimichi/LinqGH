using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Min : LinqComponentBase
    {

        public Min(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Min(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Min() : base("Min", "Min", "Returns the minimum value in a sequence of values. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("afacd478-3eb6-4776-8059-e8f3b00517b1");

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
        protected override Bitmap Icon => Resources.Min;
    }
}