using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Select : LinqComponentBase
    {

        public Select(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Select(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Select() : base("Select", "Select", "Projects each element of a sequence into a new form. ex. \"x => x.ToString()\"")
        {
        }

        public override Guid ComponentGuid => new Guid("63f614bf-e09f-41a9-8418-430463dc0936");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.SelectDynamic(LinqExpression).ToArray();
        }
    }
}