using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class AllExpression : LinqExpressionBase
    {

        public AllExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public AllExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public AllExpression() : base("AllExpressioin", "AllExpression", "Determines whether all elements of a sequence satisfy a condition. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("2212bc5d-6c9a-4225-9b83-445c6781f588");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
            if (labels.Count() != 1) throw new Exception();
            var label = labels.Single();
            var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
            return new object[] { values.All(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } })) };
        }
        protected override Bitmap Icon => Resources.All;
    }
}