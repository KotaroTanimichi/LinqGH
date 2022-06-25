using GH_IO.Serialization;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public abstract class LinqComponentBase : GH_Param<IGH_Goo>, IGH_PreviewObject
    {
        protected LinqComponentBase(IGH_InstanceDescription tag) : base(tag)
        {
        }

        protected LinqComponentBase(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        protected LinqComponentBase(string name, string nickname, string description, bool enableTextBox = true)
            : base(name, nickname, description, NameHelper.Category, NameHelper.Subcategory(LinqGHSubcategory.Linq), GH_ParamAccess.tree)
        {
            EnableTextBox = enableTextBox;
        }

        public string LinqExpression { get; set; } = "x => x";
        public string TypeInfo { get; set; }
        public bool Hidden { get; set; }
        public bool EnableTextBox { get; set; } = true;

        public bool IsPreviewCapable => VolatileData.AllData(true).Any(x => x is IGH_PreviewData || x is IGH_PreviewObject);

        public BoundingBox ClippingBox
        {
            get
            {
                BoundingBox bbox = new BoundingBox();
                foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewObject>())
                    bbox.Union(item.ClippingBox);
                foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewData>())
                    bbox.Union(item.ClippingBox);
                return bbox;
            }
        }

        public override void CreateAttributes()
        {
            m_attributes = new LinqComponentAttributes(this);
        }

        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            var mArgs = new GH_PreviewMeshArgs(args.Viewport, args.Display, Attributes.GetTopLevel.Selected ? args.ShadeMaterial_Selected : args.ShadeMaterial, args.MeshingParameters);
            foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewData>())
                item.DrawViewportMeshes(mArgs);
            foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewObject>())
                item.DrawViewportMeshes(args);
        }

        public void DrawViewportWires(IGH_PreviewArgs args)
        {
            var wArgs = new GH_PreviewWireArgs(args.Viewport, args.Display, Attributes.GetTopLevel.Selected ? args.WireColour_Selected : args.WireColour, args.DefaultCurveThickness);
            foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewData>())
                item.DrawViewportWires(wArgs);
            foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewObject>())
                item.DrawViewportWires(args);
        }
        protected override void OnVolatileDataCollected()
        {
            base.OnVolatileDataCollected();
            GH_Structure<IGH_Goo> evaluated = new GH_Structure<IGH_Goo>();
            string inType = default;
            string outType = default;

            foreach (var path in VolatileData.Paths)
            {
                var inputs = VolatileData.get_Branch(path) as List<IGH_Goo>;
                IEnumerable<object> values = inputs;
                IEnumerable<object> result = default;
                bool success = false;

                while (!success)
                {
                    try
                    {
                        values = values.SelectDynamic(x => "x.Value").ToArray();
                    }
                    catch
                    {
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Failed to get values from input.");
                        VolatileData.Clear();
                        TypeInfo = String.Empty;
                        return;
                    }
                    try
                    {
                        result = Evaluate(values, LinqExpression).ToArray();
                        success = true;
                        inType = values.First().GetType().Name;
                        outType = result.First().GetType().Name;
                    }
                    catch
                    {
                    }
                }
                //AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Failed to evaluate lambda expression.");
                //VolatileData.Clear();
                //return;

                evaluated.AppendRange(result.Select(x => GooHelper.ToGoo(x)), path);
            }

            SetEvaluatedValuesToVolatile(evaluated);

            if (!string.IsNullOrEmpty(inType) && !string.IsNullOrEmpty(outType))
                TypeInfo = $"{inType} => {outType}";
            else
                TypeInfo = string.Empty;
            //AddRuntimeMessage(GH_RuntimeMessageLevel.Remark, TypeInfo);
        }
        protected virtual void SetEvaluatedValuesToVolatile(GH_Structure<IGH_Goo> values)
        {
            VolatileData.Clear();
            AddVolatileDataTree(values);
        }
        protected abstract IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression);

        public override bool Write(GH_IWriter writer)
        {
            writer.SetString("Expression", LinqExpression);
            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            LinqExpression = reader.GetString("Expression");
            return base.Read(reader);
        }

        protected override IGH_Goo InstantiateT()
        {
            return new GH_ObjectWrapper();
        }
    }
}