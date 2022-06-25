using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Any : LinqComponentBase
    {

        public Any(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Any(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Any() : base("Any", "Any", "Determines whether any element of a sequence exists or satisfies a condition. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("f53decb1-429d-441f-99d8-8de9669aa1c5");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.AnyDynamic(LinqExpression) };
        }
        protected override Bitmap Icon => Resources.Any;
    }
}