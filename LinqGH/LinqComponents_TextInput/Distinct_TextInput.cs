using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using LinqGH.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents_TextInput
{
    public class Distinct_TextInput : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Distinct_TextInput()
          : base("Distinct_TextInput", "Distinct_TextInput",
            "Returns distinct elements from a sequence.", NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.LinqWithTextInput))
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<IGH_Goo> list = new List<IGH_Goo>();

            if (!DA.GetDataList(0, list)) return;

            var values = GooHelper.ExtractValues(list);
            DA.SetDataList(0, values.Distinct().Select(x => GooHelper.ToGoo(x)));
        }


        protected override System.Drawing.Bitmap Icon => Resources.IDistinct;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("9aa92563-83b6-42b1-8581-dfd2ee066df8");

    }
}