using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Count : LinqComponentBase
    {

        public Count(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Count(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Count() : base("Count", "Count", "Returns the number of elements in a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("5f954036-9631-49b2-b87a-14da08eb1584");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.CountDynamic(LinqExpression) };
        }
        protected override Bitmap Icon => Resources.Count;
    }
}