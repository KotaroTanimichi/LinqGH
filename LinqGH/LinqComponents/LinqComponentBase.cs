using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Special;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public abstract class LinqComponentBase : GH_Component, IGH_VariableParameterComponent
    {
        private string[] inputLabels;
        public LinqComponentBase(string name, string nickname, string description)
          : base(name, nickname,
            description,
            NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.Linq))
        {
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Lambda", "Lm", "Lambda expression string. ex. \"x => x > 0\"", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List or item as result", GH_ParamAccess.list);
        }
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (Params.Input.Count < 2)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No list supplied.");
                return;
            }

            if (DA.Iteration == 0)
            {
                UpdateInputLabels();
            }

            Dictionary<string, object[]> inputLists = new Dictionary<string, object[]>();
            for (int i = 0; i < inputLabels.Length; i++)
            {
                List<GH_ObjectWrapper> wrapperList = new List<GH_ObjectWrapper>();
                DA.GetDataList(i + 1, wrapperList);
                inputLists.Add(inputLabels[i], GooHelper.ExtractValues(wrapperList).ToArray());
            }

            int listLength = inputLists.Values.First().Length;
            if (inputLists.Values.Any(list => list.Length != listLength))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Lists with different length supplied.");
                return;
            }

            string expression = default;

            if (!DA.GetData(0, ref expression))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No expressioh supplied.");
                return;
            }

            if (expression.Contains("=>"))
            {
                expression = EvalHelper.GetCodeFromExpression(expression);
            }

            IEnumerable<object> result = default;
            try
            {
                result = Evaluate(expression, inputLists, listLength);
            }
            catch
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Failed to evaluate lambda expression.");
                return;
            }

            if (result.Any())
                Message = $"({string.Join(",", inputLists.Select(x => x.Value.First().GetType().Name))}) => {result.First().GetType().Name}";

            DA.SetDataList(0, result);
        }

        protected abstract IEnumerable<object> Evaluate(string expression, Dictionary<string, object[]> inputLists, int listLength);

        private void UpdateInputLabels()
        {
            List<string> labels = new List<string>();
            for (int i = 1; i < Params.Input.Count; i++)
            {
                labels.Add(Params.Input[i].NickName);
            }
            inputLabels = labels.ToArray();
        }

        protected abstract IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression);
        public override void AddedToDocument(GH_Document document)
        {
            base.AddedToDocument(document);
            if (Params.Input[0].SourceCount == 0)
            {
                GH_Panel panel = new GH_Panel();
                panel.AddVolatileData(new GH_Path(0), 0, new GH_String("P1"));
                panel.UserText = "P1";

                panel.CreateAttributes();
                var pAtt = panel.Attributes;
                var pBou = pAtt.Bounds;
                panel.Attributes.Bounds = new System.Drawing.RectangleF(pBou.X, pBou.Y, 150, 20);
                panel.Attributes.Pivot = new System.Drawing.PointF(Attributes.Pivot.X - 220, Attributes.Pivot.Y + 0);
                document.AddObject(panel, true);

                Params.Input[0].AddSource(panel);
                Params.Input[0].CollectData();
            }
        }

        public bool CanInsertParameter(GH_ParameterSide side, int index)
        {
            return side == GH_ParameterSide.Input && index > 0;
        }

        public bool CanRemoveParameter(GH_ParameterSide side, int index)
        {
            return side == GH_ParameterSide.Input && index > 0;
        }

        public IGH_Param CreateParameter(GH_ParameterSide side, int index)
        {
            return new Param_GenericObject() { Name = $"P{index}", NickName = $"P{index}", Access = GH_ParamAccess.list };
        }

        public bool DestroyParameter(GH_ParameterSide side, int index)
        {
            return true;
        }

        public void VariableParameterMaintenance()
        {
        }
        protected Dictionary<string, object> GetParameterDict(Dictionary<string, object[]> dict, int index)
        {
            return dict.Select(x => (x.Key, x.Value[index])).ToDictionary(x => x.Key, x => x.Item2);
        }
    }
    public abstract class _LinqComponentBase_TextInput : GH_Component
    {
        public _LinqComponentBase_TextInput(string name, string nickname, string description)
          : base(name, nickname,
            description,
            NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.Linq))
        {
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List to apply LINQ operation.", GH_ParamAccess.list);
            pManager.AddTextParameter("Lambda", "Lm", "Lambda expression string. ex. \"x => x > 0\"", GH_ParamAccess.item);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List or item as result", GH_ParamAccess.list);
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