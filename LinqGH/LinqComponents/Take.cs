using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class Take : GH_Component
    {
        public Take() : base("Take", "Take", "Returns a specified number of contiguous elements from the start of a sequence.", NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.Linq))
        {
        }

        public override Guid ComponentGuid => new Guid("941b279e-1160-4751-944e-731f2c22ee54");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List to apply LINQ operation.", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Count", "C", "Item count to take.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List as result", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<GH_ObjectWrapper> list = new List<GH_ObjectWrapper>();
            int count = default;

            if (!DA.GetDataList(0, list)) return;
            if (!DA.GetData(1, ref count)) return;

            DA.SetDataList(0, list.Take(count));

            //string typeName = list.First().Value.GetType().Name;
            //Message = $"{typeName} => {typeName}";
        }
        protected override Bitmap Icon => Resources.Take;
    }
}