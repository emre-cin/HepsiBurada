using HepsiBurada.Core.Models;
using HepsiBurada.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using HepsiBurada.Core.UnitOfWork;
using HepsiBurada.Service.CampaignService;
using AutoMapper.Configuration;
using HepsiBurada.Model.Mapper;
using AutoMapper;
using HepsiBurada.Service.ProductService;
using System.IO;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace HepsiBurada.App
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider;

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            #region Options

            services.AddOptions();

            var builder = new ConfigurationBuilder();
            var configuration = builder.Build();

            AppSettings appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            #endregion

            #region DbContext

            //services.AddDbContext<HepsiBuradaContext>(options => options.UseSqlServer(appSettings.DatabaseSettings.ConnectionString, b => b.MigrationsAssembly("Data")));

            services.AddDbContext<HepsiBuradaContext>(options =>
            {
                options.UseSqlServer(GetDbByName("HepsiBuradaCon"));
            });

            #endregion

            #region UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Auto Mapper

            MapperConfigurationExpression mapperConfig = new MapperConfigurationExpression();

            mapperConfig.AllowNullCollections = true;

            mapperConfig.AddProfile(new AutoMapperProfileConfiguration());

            Mapper.Initialize(mapperConfig);

            #endregion

            #region Injection

            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<IProductService, ProductService>();

            #endregion

            services.AddHostedService<BackgroundService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        private static string GetDbByName(string connectionName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            return config.GetConnectionString(connectionName);

            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional: false);
            //var configuration = builder.Build();
            //return configuration.GetConnectionString(connectionName);
        }
    }
}
