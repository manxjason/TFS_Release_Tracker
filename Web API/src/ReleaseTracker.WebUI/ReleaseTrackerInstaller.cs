using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ReleaseTracker.Core.Concrete;
using ReleaseTracker.Core.Interfaces;
using ReleaseTracker.DataAccess.Repositories;
using ReleaseTracker.DataAccess.Services;
using Component = Castle.MicroKernel.Registration.Component;

namespace ReleaseTrackers
{
    public class ReleaseTrackerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer Container, IConfigurationStore Store)
        {
            Container.Register(
                Classes.FromThisAssembly().BasedOn<IController>(),
                Component.For<IReleaseRepository>().ImplementedBy<AdoNetReleaseRepository>(),
                Component.For<IReleaseService>().ImplementedBy<ReleaseService>(),
                Component.For<IReleaseFactory>().ImplementedBy<ReleaseFactory>(),
                Classes.FromThisAssembly()
                                        .BasedOn<IHttpController>()
                                        .LifestylePerWebRequest()
                                        .Configure(x => x.Named(x.Implementation.FullName)));

        }
    }
}