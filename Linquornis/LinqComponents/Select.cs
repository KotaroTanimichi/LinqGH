using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper;

namespace Linquornis.LinqComponents
{
    public class Select : LinqComponentBase
    {

        public Select(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Select(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Select() : base("Select", "Select", "")
        {
        }

        public override Guid ComponentGuid => new Guid("63f614bf-e09f-41a9-8418-430463dc0936");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.SelectDynamic(LinqExpression).ToArray();
        }
    }
}