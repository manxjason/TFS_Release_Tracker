using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Action = Antlr.Runtime.Misc.Action;

namespace ReleaseTrackers
{
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer _Container;

        public WindsorCompositionRoot(IWindsorContainer Container)
        {
            _Container = Container;
        }

        public IHttpController Create(HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller =
                (IHttpController)_Container.Resolve(controllerType);

            request.RegisterForDispose(
                new Release(() => _Container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action _Release;

            public Release(Action Release) { _Release = Release; }

            public void Dispose()
            {
                _Release();
            }
        }
    }
}