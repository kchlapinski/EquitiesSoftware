using Autofac;
using AutoMapper;
using Ubs.Equities.Core.Calculators;
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

            builder.RegisterType<EquityCalculator>().AsSelf();

            builder.RegisterType<BondCalculator>().AsSelf();

            builder.Register(context => new EquityModel(context.Resolve<EquityCalculator>())).Keyed<StockModel>(StockType.Equity);

            builder.Register(context => new BondModel(context.Resolve<BondCalculator>())).Keyed<StockModel>(StockType.Bond);

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