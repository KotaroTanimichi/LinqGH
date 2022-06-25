using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class MinExpression : LinqExpressionBase
    {

        public MinExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public MinExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public MinExpression() : base("MinExpression", "MinExpression", "Returns the minimum value in a sequence of values. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("afacd478-3eb6-4776-8059-e8f3b00517b1");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
                return new object[] { values.Min() };
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.Min(value => EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.Min;
    }
}