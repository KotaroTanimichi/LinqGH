using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class TakeWhileExpression : LinqExpressionBase
    {

        public TakeWhileExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public TakeWhileExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public TakeWhileExpression() : base("TakeWhileExpression", "TakeWhileExpression", "Returns elements from a sequence as long as a specified condition is true, and then skips the remaining elements. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("9a3556d5-b67a-425e-af70-a695b91bfb35");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
            if (labels.Count() != 1) throw new Exception();
            var label = labels.Single();
            var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
            return values.TakeWhile(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } }));
        }

        protected override Bitmap Icon => Resources.TakeWhile;
    }
}