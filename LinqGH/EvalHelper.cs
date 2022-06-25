using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Z.Expressions;

namespace LinqGH
{
    internal static class EvalHelper
    {
        private static readonly EvalContext context;
        internal static EvalContext Context => context;

        static EvalHelper()
        {
            context = new EvalContext();
            context.RegisterAssembly(Assembly.GetAssembly(typeof(Point3d)));
            context.RegisterAssembly(Assembly.GetAssembly(typeof(GH_Point)));
        }

        internal static object Execute(string code, object parameter)
        {
            return context.Execute(code, parameter);
        }

        internal static IEnumerable<string> GetParameterLabelsFromExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression)) throw new ArgumentNullException(nameof(expression));
            if (!expression.Contains("=>")) throw new ArgumentException(nameof(expression));

            string parameterPart = expression.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)[0];
            return parameterPart.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim());
        }
        internal static string GetCodeFromExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression)) throw new ArgumentNullException(nameof(expression));
            if (!expression.Contains("=>")) throw new ArgumentException(nameof(expression));

            return expression.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}
