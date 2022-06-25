using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class All : LinqComponentBase
    {

        public All(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public All(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public All() : base("All", "All", "Determines whether all elements of a sequence satisfy a condition. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("2212bc5d-6c9a-4225-9b83-445c6781f588");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return new object[] { values.AllDynamic(LinqExpression) };
        }
    }
}