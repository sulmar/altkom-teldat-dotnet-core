using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teldat.Vehicles.Api.Constraints
{
    public class EmptyFromQueryActionConstraint : IActionConstraint
    {
        public int Order => 999;

        public bool Accept(ActionConstraintContext context)
        {
            return !context.RouteContext.HttpContext.Request.Query.Any();
        }
    }

    public class EmptyFromQueryAttribute : EmptyFromQueryActionConstraint
    {
    }

     public class RequiredFromQueryActionConstraint : IActionConstraint
    {
        private readonly string parameter;

        public RequiredFromQueryActionConstraint(string parameter)
        {
            this.parameter = parameter;
        }

        public int Order => 999;

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Query.ContainsKey(parameter);
        }
    }
    
    public class RequiredFromQueryAttribute : FromQueryAttribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.Action.Selectors != null && parameter.Action.Selectors.Any())
            {
                parameter.Action.Selectors.Last().ActionConstraints.Add(new RequiredFromQueryActionConstraint(parameter.BindingInfo?.BinderModelName ?? parameter.ParameterName));
            }
        }
    }


}
