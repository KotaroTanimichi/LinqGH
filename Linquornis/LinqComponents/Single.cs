using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Single : LinqComponentBase
    {

        public Single(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Single(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Single() : base("Single", "Single", "Returns a single, specific element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("ee8d1634-1488-43af-8616-29ba09b3f4f6");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new[] { values.SingleDynamic(LinqExpression) };
        }
    }
}