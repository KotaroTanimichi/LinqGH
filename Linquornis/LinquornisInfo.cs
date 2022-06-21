using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Linquornis
{
    public class LinquornisInfo : GH_AssemblyInfo
    {
        public override string Name => "Linquornis";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("8D8EEAE0-12D6-4EFC-8BA2-429C23651E02");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}