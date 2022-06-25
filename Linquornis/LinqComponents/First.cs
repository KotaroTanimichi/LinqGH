using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
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

        public Last() : base("Last", "Last", "Returns the first element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("29891df0-2cad-4f96-9ad0-68216c8d490c");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.LastDynamic(LinqExpression) };
        }
    }
    public class First : LinqComponentBase
    {

        public First(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public First(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public First() : base("First", "First", "")
        {
        }

        public override Guid ComponentGuid => new Guid("60409529-490b-41b0-a7f9-fd30db0aa5a8");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.FirstDynamic(LinqExpression) };
        }
    }
}