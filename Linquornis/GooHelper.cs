using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH
{

    internal static class GooHelper
    {
        internal static IEnumerable<object> ExtractValues(IEnumerable<IGH_Goo> goos)
        {
            List<object> values = goos.Cast<object>().ToList();
            while (values.Any(x => x is IGH_Goo))
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] is IGH_Goo)
                    {
                        try
                        {
                            var valueProp = values[i].GetType().GetProperty("Value");
                            values[i] = valueProp.GetValue(values[i]);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            return values;
        }
        internal static IGH_Goo ToGoo(object obj)
        {
            switch (obj)
            {
                // point
                case Point3d p3d:
                    return new GH_Point(p3d);
                case Point3f p3f:
                    return new GH_Point(p3f);
                case Point2d p2d:
                    return new GH_Point(new Point3d(p2d.X, p2d.Y, 0));
                case Point2f p2f:
                    return new GH_Point(new Point3d(p2f.X, p2f.Y, 0));
                case Point rhPt:
                    return new GH_Point(rhPt.Location);

                // curve
                case Curve rhCrv:
                    return new GH_Curve(rhCrv);
                case Polyline polyline:
                    return new GH_Curve(polyline.ToPolylineCurve());
                case Circle rhCircle:
                    return new GH_Circle(rhCircle);
                case Rectangle3d rhRect:
                    return new GH_Rectangle(rhRect);

                // surface
                case Surface rhSrf:
                    return new GH_Surface(rhSrf);
                case Brep rhBrep:
                    return new GH_Brep(rhBrep);

                // mesh
                case Mesh rhMesh:
                    return new GH_Mesh(rhMesh);
                case SubD rhSubD:
                    return new GH_SubD(rhSubD);

                // Misc
                case Box rhBox:
                    return new GH_Box(rhBox);
                case BoundingBox bBox:
                    return new GH_Box(bBox);
                case Vector3d v3d:
                    return new GH_Vector(v3d);
                case Vector3f v3f:
                    return new GH_Vector(v3f);
                case Vector2d v2d:
                    return new GH_Vector(new Vector3d(v2d.X, v2d.Y, 0));
                case Vector2f v2f:
                    return new GH_Vector(new Vector3d(v2f.X, v2f.Y, 0));
                case Plane rhPlane:
                    return new GH_Plane(rhPlane);

                default:
                    return new GH_ObjectWrapper(obj);
            }

        }
    }
}
