using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Parameters;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linquornis.LinqComponents
{

    public class LinqComponentAttributes : GH_Attributes<LinqComponentBase>
    {


        public LinqComponentAttributes(LinqComponentBase component) : base(component)
        {
        }

        protected override void Layout()
        {
            base.Layout();

            var rec = Bounds;
            rec.Height = 30;
            rec.Width = 200;

            Bounds = rec;

            Owner.OnAttributesChanged();
        }

        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (Owner is LinqComponentBase component)
            {
                string initial = component.LinqExpression;

                var matrix = sender.Viewport.XFormMatrix(GH_Viewport.GH_DisplayMatrix.CanvasToControl);
                var field = new LinqTextField(component)
                {
                    Bounds = GH_Convert.ToRectangle(Bounds)
                };

                field.ShowTextInputBox(sender, initial, true, true, matrix);
                return GH_ObjectResponse.Handled;
            }

            return base.RespondToMouseDoubleClick(sender, e);
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            var cmp = Owner as LinqComponentBase;
            switch (channel)
            {
                case GH_CanvasChannel.Wires:
                    RenderIncomingWires(canvas.Painter, Owner.Sources, Owner.WireDisplay);
                    break;
                case GH_CanvasChannel.Objects:

                    GH_CapsuleRenderEngine.RenderInputGrip(graphics, canvas.Viewport.Zoom, InputGrip, true);
                    GH_CapsuleRenderEngine.RenderOutputGrip(graphics, canvas.Viewport.Zoom, OutputGrip, true);


                    GH_Palette gH_Palette = GH_CapsuleRenderEngine.GetImpliedPalette(Owner);
                    if (gH_Palette == GH_Palette.Normal && !Owner.IsPreviewCapable)
                    {
                        gH_Palette = GH_Palette.Hidden;
                    }
                    GH_Capsule gH_Capsule = GH_Capsule.CreateCapsule(Bounds, gH_Palette);
                    GH_PaletteStyle impliedStyle = GH_CapsuleRenderEngine.GetImpliedStyle(gH_Palette, Selected, Owner.Locked, Owner.Hidden);
                    gH_Capsule.Render(graphics, impliedStyle);


                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;

                    var b = Bounds;
                    int offset = 3;
                    var bb = new Rectangle((int)b.X + offset, (int)b.Y + offset, (int)b.Width - 2 * offset, (int)b.Height - 2 * offset);
                    graphics.FillRectangle(Brushes.White, bb);
                    graphics.DrawRectangle(new Pen(Brushes.Black), bb);


                    var lc = Owner as LinqComponentBase;
                    if (!string.IsNullOrEmpty(lc.LinqExpression))
                        graphics.DrawString(lc.LinqExpression, GH_FontServer.Standard, Brushes.Black, b, format);
                    break;

            }
        }
        public override bool HasInputGrip => true;
        public override bool HasOutputGrip => true;

        public override bool TooltipEnabled => true;
    }

}
