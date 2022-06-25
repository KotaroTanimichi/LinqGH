using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class FirstExpression : LinqExpressionBase
    {

        public FirstExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public FirstExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public FirstExpression() : base("FirstExpression", "FirstExpression", "Returns the first element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("60409529-490b-41b0-a7f9-fd30db0aa5a8");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
            {
                return new object[] { values.First() };
            }
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.First(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.First;
    }
}