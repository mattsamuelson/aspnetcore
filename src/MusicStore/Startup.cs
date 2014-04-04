﻿using Microsoft.AspNet;
using Microsoft.AspNet.Abstractions;
using Microsoft.AspNet.ConfigurationModel;
using Microsoft.AspNet.Configuration.Json;
using Microsoft.AspNet.DependencyInjection;
using Microsoft.AspNet.DependencyInjection.Fallback;
using Microsoft.AspNet.DependencyInjection.NestedProviders;
using Microsoft.AspNet.RequestContainer;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.InMemory;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using MusicStore.Models;
using MusicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class Startup
{
    public void Configuration(IBuilder app)
    {
        CreateAdminUser();

        //ErrorPageOptions.ShowAll to be used only at development time. Not recommended for production. 
        app.UseErrorPage(ErrorPageOptions.ShowAll);

        app.UseFileServer();

        var serviceProvider = MvcServices.GetDefaultServices().BuildServiceProvider(app.ServiceProvider);
        app.UseContainer(serviceProvider);

        var routes = new RouteCollection()
        {
            DefaultHandler = new MvcApplication(serviceProvider),
        };

        routes.MapRoute(
            "{controller}/{action}",
            new { controller = "Home", action = "Index" });

        routes.MapRoute(
            "{controller}",
            new { controller = "Home" });

        app.UseRouter(routes);

        SampleData.InitializeMusicStoreDatabaseAsync().Wait();
    }

    private async void CreateAdminUser()
    {
        var configuration = new Configuration();
        configuration.AddEnvironmentVariables(); //If configuration flows through environment we should pick that first
        configuration.AddJsonFile(Path.Combine(GetApplicationBasePath(), "Config.json"));

        string _username = configuration.Get("DefaultAdminUsername");
        string _password = configuration.Get("DefaultAdminPassword");
        string _role = "Administrator";

        var userManager = new UserManager<ApplicationUser>(new InMemoryUserStore<ApplicationUser>());
        var roleManager = new RoleManager<InMemoryRole>(new InMemoryRoleStore<InMemoryRole>());

        var role = new InMemoryRole(_role);
        var result = await roleManager.RoleExistsAsync(_role);
        if (result == false)
        {
            await roleManager.CreateAsync(role);
        }

        var user = await userManager.FindByNameAsync(_username);
        if (user == null)
        {
            user = new ApplicationUser { UserName = _username };
            await userManager.CreateAsync(user, _password);
            await userManager.AddToRoleAsync(user, _role);
        }
    }

    //To find the application base path at runtime. 
    private static string GetApplicationBasePath()
    {
#if NET45
            var applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
#else // CORECLR_TODO: ApplicationBase
        var appDomainType = typeof(object)
                                .GetTypeInfo()
                                .Assembly
                                .GetType("System.AppDomain");

        var currentAppDomainProperty = appDomainType.GetRuntimeProperty("CurrentDomain");

        var currentAppDomain = currentAppDomainProperty.GetValue(null);

        var getDataMethod = appDomainType
            .GetRuntimeMethod("GetData", new[] { typeof(string) });

        string applicationBase = (string)getDataMethod.Invoke(currentAppDomain, new object[] { "APPBASE" });
#endif

        return applicationBase;
    }
}