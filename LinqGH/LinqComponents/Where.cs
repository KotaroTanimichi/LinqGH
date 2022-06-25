using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Where : LinqComponentBase
    {

        public Where(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Where(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Where() : base("Where", "Where", "Filters a sequence of values based on a predicate. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("2ebe15f0-e0af-45d6-9cfe-c6dbd449e1b8");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.WhereDynamic(LinqExpression).ToArray();
        }

        protected override Bitmap Icon => Resources.Where;
    }
}