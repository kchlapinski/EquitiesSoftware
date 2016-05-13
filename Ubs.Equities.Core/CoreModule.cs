using Autofac;
using AutoMapper;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services.Implementors;
using Ubs.Equities.Core.ViewModels;
using Ubs.Equities.EntityFramework;
using Ubs.Equities.EntityFramework.DbContexts;
using Ubs.Equities.EntityFramework.Entities;

namespace Ubs.Equities.Core
{
    public class CoreModule : Module
    {
        #region Overrides

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FoundDbContext>().AsImplementedInterfaces();

            builder.RegisterType<FoundService>().AsImplementedInterfaces();

            builder.RegisterType<ActionViewModel>().AsImplementedInterfaces();

            builder.RegisterType<ListViewModel>().AsImplementedInterfaces();

            builder.RegisterType<SidebarViewModel>().AsImplementedInterfaces();

            builder.RegisterType<EquityModel>().Keyed<StockModel>(StockType.Equity);

            builder.RegisterType<BondModel>().Keyed<StockModel>(StockType.Bond);

            builder.RegisterInstance(new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Stock, EquityModel>();
                configuration.CreateMap<Stock, BondModel>();
            })).AsSelf();

            builder.RegisterAdapter<MapperConfiguration, IMapper>(configuration => configuration.CreateMapper());
        }

        #endregion
    }
}