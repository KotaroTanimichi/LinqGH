using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class Max : LinqExpressionBase
    {

        public Max(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public Max(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public Max() : base("Max", "Max", "Returns the maximum value in a sequence of values. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("a04baf32-472c-4b57-86a0-35319dcd7631");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
                return new object[] { values.Max() };
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.Max(value => EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.Max;
    }
}