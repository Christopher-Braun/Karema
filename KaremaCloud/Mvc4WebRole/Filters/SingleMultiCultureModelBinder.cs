using System;
using System.Web.Mvc;
using Mvc4WebRole.Models;

namespace Mvc4WebRole.Filters
{
    public class SingleMultiCultureModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToSingle(valueResult.AttemptedValue.Replace('.', ','), Defines.Culture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            var key = bindingContext.ModelName;

            if (bindingContext.ModelState.ContainsKey(key))
            {
                bindingContext.ModelState[key] = modelState;
            }
            else
            {
                bindingContext.ModelState.Add(key, modelState);
            }
            return actualValue;
        }
    }
}