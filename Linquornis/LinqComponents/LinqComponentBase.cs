using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linquornis.LinqComponents
{
    public abstract class LinqComponentBase : GH_Param<GH_ObjectWrapper>, IGH_PreviewObject
    {
        protected LinqComponentBase(IGH_InstanceDescription tag) : base(tag)
        {
        }

        protected LinqComponentBase(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        protected LinqComponentBase(string name, string nickname, string description) : base(name, nickname, description, "Linquornis", "Linq", GH_ParamAccess.tree)
        {
        }

        public string LinqExpression { get; set; }
        public bool Hidden { get; set; }

        public bool IsPreviewCapable => VolatileData.AllData(true).Any(x => x is IGH_PreviewObject prevObj && prevObj.IsPreviewCapable);

        public BoundingBox ClippingBox
        {
            get
            {
                BoundingBox bbox = new BoundingBox();
                foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewObject>())
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
            foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewObject>())
                item.DrawViewportMeshes(args);
        }

        public void DrawViewportWires(IGH_PreviewArgs args)
        {
            foreach (var item in VolatileData.AllData(true).OfType<IGH_PreviewObject>())
                item.DrawViewportWires(args);
        }
        protected override void OnVolatileDataCollected()
        {
            base.OnVolatileDataCollected();
            GH_Structure<GH_ObjectWrapper> evaluated = new GH_Structure<GH_ObjectWrapper>();
            string inType = default;
            string outType = default;

            foreach (var path in VolatileData.Paths)
            {
                var inputs = VolatileData.get_Branch(path) as List<GH_ObjectWrapper>;
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

                evaluated.AppendRange(result.Select(x => new GH_ObjectWrapper(x)), path);
            }

            VolatileData.Clear();
            AddVolatileDataTree(evaluated);
            AddRuntimeMessage(GH_RuntimeMessageLevel.Remark, $"{inType} => {outType}");
        }

        protected abstract IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression);
    }
}