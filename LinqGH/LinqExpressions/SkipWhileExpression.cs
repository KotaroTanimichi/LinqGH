using Grasshopper.Kernel;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class SkipWhileExpression : LinqExpressionBase
    {

        public SkipWhileExpression(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public SkipWhileExpression(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public SkipWhileExpression() : base("SkipWhileExpression", "SkipWhileExpression", "Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. ex. \"x => x>0\"")
        {
        }

        public override Guid ComponentGuid => new Guid("294d9665-cfe7-47f3-a852-e8fa2d89ba13");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            var labels = EvalHelper.GetParameterLabelsFromExpression(lambdaExpression);
            if (labels.Count() != 1) throw new Exception();
            var label = labels.Single();
            var code = EvalHelper.GetCodeFromExpression(lambdaExpression);
            return values.SkipWhile(value => (bool)EvalHelper.Execute(code, new Dictionary<string, object>() { { label, value } }));
        }
        protected override Bitmap Icon => Resources.SkipWhile;
    }
}