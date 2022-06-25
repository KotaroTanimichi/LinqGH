﻿using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGH.LinqComponents
{
    public class OrderBy : LinqComponentBase
    {

        public OrderBy(IGH_InstanceDescription tag) : base(tag)
        {
        }

        public OrderBy(IGH_InstanceDescription tag, GH_ParamAccess access) : base(tag, access)
        {
        }

        public OrderBy() : base("OrderBy", "OrderBy", "Sorts the elements of a sequence in ascending order. ex. \"x => x*x\"")
        {
        }

        public override Guid ComponentGuid => new Guid("5c3bdd6a-7c3a-4e53-ade9-5f069425e55f");

        protected override IEnumerable<object> Evaluate(IEnumerable<object> values, string lambdaExpression)
        {
            return values.OrderByDynamic(LinqExpression).ToArray();
        }
    }
}