using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class SkipWhile : LinqComponentBase
    {

        public SkipWhile(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public SkipWhile(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public SkipWhile() : base("SkipWhile", "SkipWhile", "Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("294d9665-cfe7-47f3-a852-e8fa2d89ba13");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.SkipWhileDynamic(LinqExpression).ToArray();
        }
        protected override Bitmap Icon => Resources.SkipWhile;
    }
}