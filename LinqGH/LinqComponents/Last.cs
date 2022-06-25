using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Last : LinqComponentBase
    {

        public Last(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Last(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Last() : base("Last", "Last", "Returns the last element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("29891df0-2cad-4f96-9ad0-68216c8d490c");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.LastDynamic(LinqExpression) };
        }
        protected override Bitmap Icon => Resources.Last;
    }
}