using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Special;
using Grasshopper.Kernel.Types;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents_TextInput
{
    public abstract class LinqComponentBase_TextInput : GH_Component
    {
        public LinqComponentBase_TextInput(string name, string nickname, string description)
          : base(name, nickname,
            description,
            NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.LinqWithTextInput))
        {
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "", GH_ParamAccess.list);
            pManager.AddTextParameter("Lambda", "Lm", "", GH_ParamAccess.item);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "", GH_ParamAccess.list);
        }
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<object> list = new List<object>();
            string lm = default;

            if (!DA.GetDataList(0, list)) return;
            if (!DA.GetData(1, ref lm))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No lambda supplied and data passed through.");
                DA.SetDataList(0, list);
                return;
            }

            IEnumerable<object> values = default;
            try
            {
                values = list.SelectDynamic(x => "x.Value");
            }
            catch
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Failed to get values from input.");
                return;
            }

            IEnumerable<object> result = default;
            try
            {
                result = Evaluate(values, lm);
            }
            catch
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Failed to evaluate lambda expression.");
                return;
            }

            if (values.Any() && result.Any())
                Message = $"{values.First().GetType().Name} => {result.First().GetType().Name}";

            DA.SetDataList(0, result);
        }

        protected abstract IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression);
        public override void AddedToDocument(GH_Document document)
        {
            base.AddedToDocument(document);
            if (Params.Input[1].SourceCount == 0)
            {
                GH_Panel panel = new GH_Panel();
                panel.AddVolatileData(new GH_Path(0), 0, new GH_String("x => x"));
                panel.UserText = "x => x";

                panel.CreateAttributes();
                var pAtt = panel.Attributes;
                var pBou = pAtt.Bounds;
                panel.Attributes.Bounds = new System.Drawing.RectangleF(pBou.X, pBou.Y, 150, 20);
                panel.Attributes.Pivot = new System.Drawing.PointF(Attributes.Pivot.X - 220, Attributes.Pivot.Y + 0);
                document.AddObject(panel, true);

                Params.Input[1].AddSource(panel);
                Params.Input[1].CollectData();
            }
        }
    }
}