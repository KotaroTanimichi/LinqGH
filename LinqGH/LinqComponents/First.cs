using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class First : LinqComponentBase
    {

        public First(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public First(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public First() : base("First", "First", "Returns the first element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("60409529-490b-41b0-a7f9-fd30db0aa5a8");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.FirstDynamic(LinqExpression) };
        }
        protected override Bitmap Icon => Resources.First;
    }
}