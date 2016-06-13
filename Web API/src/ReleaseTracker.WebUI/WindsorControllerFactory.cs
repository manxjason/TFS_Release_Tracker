using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace ReleaseTrackers
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _Kernel;

        public WindsorControllerFactory(IKernel Kernel)
        {
            _Kernel = Kernel;
        }

        public override void ReleaseController(IController Controller)
        {
            _Kernel.ReleaseComponent(Controller);
        }

        protected override IController GetControllerInstance(RequestContext RequestContext, Type ControllerType)
        {
            if (ControllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", RequestContext.HttpContext.Request.Path));
            }
            return (IController)_Kernel.Resolve(ControllerType);
        }
    }
}