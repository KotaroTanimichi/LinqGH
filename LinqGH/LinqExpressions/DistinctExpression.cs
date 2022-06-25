using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class DistinctExpression : LinqExpressionBase
    {

        public DistinctExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public DistinctExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public DistinctExpression() : base("DistinctExpression", "DistinctExpression", "Returns distinct elements from a sequence.", false)
        {
        }

        public override Guid ComponentGuid => new Guid("c07e3f13-1b24-4521-a5e3-f44b8dd4f7fc");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.Distinct().ToArray();
        }
        protected override Bitmap Icon => Resources.Distinct;
    }
}