using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class TakeWhile : LinqComponentBase
    {

        public TakeWhile(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public TakeWhile(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public TakeWhile() : base("TakeWhile", "TakeWhile", "Returns elements from a sequence as long as a specified condition is true, and then skips the remaining elements. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("9a3556d5-b67a-425e-af70-a695b91bfb35");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.TakeWhileDynamic(LinqExpression).ToArray();
        }
    }
}