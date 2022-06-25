using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class SingleExpression : LinqExpressionBase
    {

        public SingleExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public SingleExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public SingleExpression() : base("SingleExpression", "SingleExpression", "Returns a single, specific element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("ee8d1634-1488-43af-8616-29ba09b3f4f6");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
            {
                return new object[] { values.Single() };
            }
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.Single(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.Single;
    }
}