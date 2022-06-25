using System;

namespace LinqGH.LinqExpressions
{
    internal class LinqTextField : Grasshopper.GUI.Base.GH_TextBoxInputBase
    {
        private readonly LinqExpressionBase _component;

        public LinqTextField(LinqExpressionBase input)
        {
            _component = input ?? throw new ArgumentNullException(nameof(input));
        }

        protected override void HandleTextInputAccepted(string text)
        {
            _component.LinqExpression = text;
            _component.ExpireSolution(true);
            //if (ValidateText(text))
            //{
            //    _component.LinqExpression = text;
            //    _component.ExpireSolution(true);
            //}
            //else
            //{
            //    _component.LinqExpression = "x => x";
            //    _component.ExpireSolution(true);
            //}
        }

        private bool ValidateText(string text)
        {
            return !string.IsNullOrEmpty(text) && text.Contains("=>");
        }
    }

}
