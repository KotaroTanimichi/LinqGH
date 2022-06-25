using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class WhereExpression : LinqExpressionBase
    {

        public WhereExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public WhereExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public WhereExpression() : base("WhereExpression", "WhereExpression", "Filters a sequence of values based on a predicate. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("2ebe15f0-e0af-45d6-9cfe-c6dbd449e1b8");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
            if (labels.Count() != 1) throw new Exception();
            var label = labels.Single();
            var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
            return values.Where(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } }));
        }

        protected override Bitmap Icon => Resources.Where;
    }
}