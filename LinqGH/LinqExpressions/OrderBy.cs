using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class OrderBy : LinqExpressionBase
    {

        public OrderBy(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public OrderBy(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public OrderBy() : base("OrderBy", "OrderBy", "Sorts the elements of a sequence in ascending order. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("5c3bdd6a-7c3a-4e53-ade9-5f069425e55f");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
            if (labels.Count() != 1) throw new Exception();
            var label = labels.Single();
            var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
            return values.OrderBy(value => EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } }));
        }
        protected override Bitmap Icon => Resources.OrderBy;
    }
}