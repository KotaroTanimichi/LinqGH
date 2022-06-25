using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System.Reflection;
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
    }
}
