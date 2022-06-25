using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class LastExpression : LinqExpressionBase
    {

        public LastExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public LastExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public LastExpression() : base("LastExpression", "LastExpression", "Returns the last element of a sequence. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("29891df0-2cad-4f96-9ad0-68216c8d490c");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            if (string.IsNullOrEmpty(lambdaExpression))
            {
                return new object[] { values.Last() };
            }
            else
            {
                var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
                if (labels.Count() != 1) throw new Exception();
                var label = labels.Single();
                var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
                return new object[] { values.Last(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
            }
        }
        protected override Bitmap Icon => Resources.Last;
    }
}