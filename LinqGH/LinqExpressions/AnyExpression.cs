using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class AnyExpression : LinqExpressionBase
    {

        public AnyExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public AnyExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public AnyExpression() : base("AnyExpression", "AnyExpression", "Determines whether any element of a sequence exists or satisfies a condition. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("f53decb1-429d-441f-99d8-8de9669aa1c5");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
            {
                return new object[] { values.Any() };
            }
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.Any(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.Any;
    }
}