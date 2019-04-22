using System.Web.Mvc;
using RentCar.Controllers;
using RentCar.Interfaces;
using RentCar.Repositories;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace RentCar
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IFuelRepository, FuelRepository>();
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<IDistrictRepository, DistrictRepository>();
            container.RegisterType<IGearRepository, GearRepository>();
            container.RegisterType<IDbFactory, DbFactory>();
            container.RegisterType<ICarRepository, CarRepository>();
            container.RegisterType<IAddressRepository, AddressRepository>();
            container.RegisterType<IRentsRepository, RentsRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IDiscountRepository, DiscountRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}