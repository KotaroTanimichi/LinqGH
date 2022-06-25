using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Distinct : LinqComponentBase
    {

        public Distinct(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Distinct(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Distinct() : base("Distinct", "Distinct", "Returns distinct elements from a sequence.", false)
        {
        }

        public override Guid ComponentGuid => new Guid("c07e3f13-1b24-4521-a5e3-f44b8dd4f7fc");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.DistinctDynamic().ToArray();
        }
    }
}