using Autofac;
using Project.Core.Repositories;
using Project.Core.Services;
using Project.Core.UnitOfWorks;
using Project.Data;
using Project.Data.Repositories;
using Project.Data.UnitOfWorks;
using Project.Service.Mapping;
using Project.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace Project.API.Modules
{
    public class RepoServiceModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<GenericRepository<Product>>().As<IGenericRepository<Product>>().InstancePerLifetimeScope();




            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(ProjectDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


           // builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

        }
    }
}
