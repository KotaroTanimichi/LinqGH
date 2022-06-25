using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class SelectExpression : LinqExpressionBase
    {

        public SelectExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public SelectExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public SelectExpression() : base("SelectExpression", "SelectExpression", "Projects each element of a sequence into a new form. ex. \"x => x.ToString()\"")
        {
        }

        public override Guid ComponentGuid => new Guid("63f614bf-e09f-41a9-8418-430463dc0936");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
            if (labels.Count() != 1) throw new Exception();
            var label = labels.Single();
            var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
            return values.Select(value => EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } }));
        }
        protected override Bitmap Icon => Resources.Select;
    }
}