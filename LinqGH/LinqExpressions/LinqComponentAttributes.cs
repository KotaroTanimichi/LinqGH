using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace LinqGH.LinqExpressions
{

    public class LinqComponentAttributes : GH_Attributes<LinqExpressionBase>
    {


        public LinqComponentAttributes(LinqExpressionBase component) : base(component)
        {
        }

        protected override void Layout()
        {
            base.Layout();

            if (Owner.EnableTextBox)
            {
                var rec = Bounds;
                rec.Height = 35;
                rec.Width = Math.Max(200, string.IsNullOrEmpty(Owner.LinqExpression) ? 0 : GH_FontServer.StringWidth(Owner.LinqExpression, GH_FontServer.Standard) + 100);
                Bounds = rec;
                Owner.OnAttributesChanged();
            }
        }

        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (Owner.EnableTextBox)
            {
                string initial = Owner.LinqExpression;

                var matrix = sender.Viewport.XFormMatrix(GH_Viewport.GH_DisplayMatrix.CanvasToControl);
                var field = new LinqTextField(Owner)
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
            switch (channel)
            {
                case GH_CanvasChannel.Wires:
                    RenderIncomingWires(canvas.Painter, Owner.Sources, Owner.WireDisplay);
                    break;
                case GH_CanvasChannel.Objects:

                    GH_CapsuleRenderEngine.RenderInputGrip(graphics, canvas.Viewport.Zoom, InputGrip, true);
                    GH_CapsuleRenderEngine.RenderOutputGrip(graphics, canvas.Viewport.Zoom, OutputGrip, true);

                    var cmpBound = Bounds;
                    int offset = 3;
                    int typeInfoOffset = 20;
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;

                    if (Owner.EnableTextBox)
                    {
                        GH_Palette gH_Palette = GH_CapsuleRenderEngine.GetImpliedPalette(Owner);
                        if (gH_Palette == GH_Palette.Normal && !Owner.IsPreviewCapable)
                        {
                            gH_Palette = GH_Palette.Hidden;
                        }
                        GH_Capsule gH_Capsule = GH_Capsule.CreateCapsule(Bounds, gH_Palette);
                        GH_PaletteStyle impliedStyle = GH_CapsuleRenderEngine.GetImpliedStyle(gH_Palette, Selected, Owner.Locked, Owner.Hidden);
                        gH_Capsule.Render(graphics, impliedStyle);

                        // label
                        var labelBound = new Rectangle((int)cmpBound.X + offset, (int)cmpBound.Y + offset, (int)cmpBound.Width - 2 * offset, (int)cmpBound.Height - 2 * offset - 20);
                        graphics.DrawString(this.Owner.Name, GH_FontServer.Small, Brushes.Black, labelBound, format);

                        // expression
                        var expressionBound = new Rectangle((int)cmpBound.X + offset, (int)cmpBound.Y + offset + 12, (int)cmpBound.Width - 2 * offset, (int)cmpBound.Height - 2 * offset - 12);
                        graphics.FillRectangle(Brushes.White, expressionBound);
                        graphics.DrawRectangle(new Pen(Brushes.Black), expressionBound);
                        if (!string.IsNullOrEmpty(Owner.LinqExpression))
                            graphics.DrawString(Owner.LinqExpression, GH_FontServer.Standard, Brushes.Black, expressionBound, format);

                        // type
                        var typeInfoBound = new Rectangle((int)cmpBound.X + typeInfoOffset, (int)(cmpBound.Y - 8), (int)cmpBound.Width - 2 * typeInfoOffset, 8);
                        graphics.FillRectangle(Brushes.Black, typeInfoBound);
                        graphics.DrawString(this.Owner.TypeInfo, GH_FontServer.Small, Brushes.White, typeInfoBound, format);

                    }
                    else
                    {
                        base.Render(canvas, graphics, channel);
                        // type
                        var typeInfoBound = new Rectangle((int)cmpBound.X + typeInfoOffset, (int)(cmpBound.Y - 8), (int)cmpBound.Width - 2 * typeInfoOffset, 8);
                        graphics.FillRectangle(Brushes.Black, typeInfoBound);
                        graphics.DrawString(this.Owner.TypeInfo, GH_FontServer.Small, Brushes.White, typeInfoBound, format);
                    }
                    break;

            }
        }
        public override bool HasInputGrip => true;
        public override bool HasOutputGrip => true;

        public override bool TooltipEnabled => true;

    }

}
