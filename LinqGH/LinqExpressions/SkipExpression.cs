using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqExpressions
{
    public class SkipExpression : GH_Component
    {
        public SkipExpression() : base("SkipExpression", "SkipExpression", "Bypasses a specified number of elements in a sequence and then returns the remaining elements.", NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.Linq))
        {
        }

        public override Guid ComponentGuid => new Guid("01d3006d-4215-400e-bd60-270e980f945b");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List to apply LINQ operation.", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Count", "C", "Item count to skip.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List as result.", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<GH_ObjectWrapper> list = new List<GH_ObjectWrapper>();
            int count = default;

            if (!DA.GetDataList(0, list)) return;
            if (!DA.GetData(1, ref count)) return;

            DA.SetDataList(0, list.Skip(count));

            //string typeName = list.First().Value.GetType().Name;
            //Message = $"{typeName} => {typeName}";
        }
        protected override Bitmap Icon => Resources.Skip;
    }
}