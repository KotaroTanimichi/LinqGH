using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class CountExpression : LinqExpressionBase
    {

        public CountExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public CountExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public CountExpression() : base("CountExpression", "CountExpression", "Returns the number of elements in a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("5f954036-9631-49b2-b87a-14da08eb1584");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
            {
                return new object[] { values.Count() };
            }
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.Count(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.Count;
    }
}