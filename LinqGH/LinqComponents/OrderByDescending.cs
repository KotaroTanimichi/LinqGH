using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class OrderByDescending : LinqComponentBase
    {

        public OrderByDescending(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public OrderByDescending(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public OrderByDescending() : base("OrderByDescending", "OrderByDescending", "Sorts the elements of a sequence in descending order. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("39dab4f9-fd89-41de-9aec-78fd9ec3f9db");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.OrderByDescendingDynamic(LinqExpression).ToArray();
        }
        protected override Bitmap Icon => Resources.OrderByDescending;
    }
}