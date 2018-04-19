using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Operations.Debugging;

namespace Operations.Web.Mvc
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class OperationsActionFilter : ActionFilterAttribute
    {
        private const string ActionToOperationMapKey = "OperationsActionFilter.map";
        private static readonly HttpRequestLocal<Dictionary<string, IOperationScope>> ActionToOperationMapKeeper = new HttpRequestLocal<Dictionary<string, IOperationScope>>(ActionToOperationMapKey);

        protected virtual Dictionary<string, IOperationScope> ActionToOperationMap
        {
            get
            {
                if (ActionToOperationMapKeeper.Value == null)
                {
                    ActionToOperationMapKeeper.Value = new Dictionary<string, IOperationScope>();
                }
                return ActionToOperationMapKeeper.Value;
            }
        }
        protected IOperationScope StartMvcOperation(MvcOperationContext context)
        {
            var scope = Op.Start(context.GetOperationName(), Op.Context(context.ToDictionary()));
            if (ActionToOperationMapKeeper.HasContext)
            {
                ActionToOperationMap[context.GetOperationKey()] = scope;
            }
            return scope;
        }

        protected void TryFinishMvcOperation(MvcOperationContext context)
        {
            if (!ActionToOperationMapKeeper.HasContext)
                return;

            IOperationScope scope;
            if (!ActionToOperationMap.TryGetValue(context.GetOperationKey(), out scope))
            {
                OperationsLog.WriteLine(() => "DBG: no active mvc operation to finish");
            }
            else
            {
                scope.Dispose();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = new MvcOperationContext(filterContext, "action");
            StartMvcOperation(context);
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var context = new MvcOperationContext(filterContext, "result");
            StartMvcOperation(context);
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            var context = new MvcOperationContext(filterContext, "result");
            TryFinishMvcOperation(context);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var context = new MvcOperationContext(filterContext, "action");
            TryFinishMvcOperation(context);
        }
    }
}