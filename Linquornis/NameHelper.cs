namespace LinqGH
{
    internal static class NameHelper
    {
        internal static string Category => "LinqGH";
        internal static string Subcategory(LinqGHSubcategory subcategory)
        {
            return $"{(int)subcategory}_{subcategory}";
        }
    }
}
