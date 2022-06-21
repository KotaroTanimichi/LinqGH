using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linquornis.LinqComponents
{
    public class Where : LinqComponentBase
    {

        public Where(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Where(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Where() : base("Where", "Where", "")
        {
        }

        public override Guid ComponentGuid => new Guid("2ebe15f0-e0af-45d6-9cfe-c6dbd449e1b8");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.WhereDynamic(LinqExpression).ToArray();
        }
    }
}