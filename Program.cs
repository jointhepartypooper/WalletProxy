﻿using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.AspNetCore.Hosting;

namespace WalletProxy
{
    public interface IRpcClientFactory
    {
        Uri GetUri();
        HttpClient GetClient();
    }

    public class StaticRestClientFactory : IRpcClientFactory
    {
        //todo configsettings:
        public static Uri EndPoint = new("http://127.0.0.1:8332");
        public static string UserName = "helloiamaproxy";
        public static string PassWord = "YcF7!&93YTs2Nhc@CJf";

        private static readonly Lazy<HttpClient> ClientInstance = new Lazy<HttpClient>(
            () =>
            {
                var client = new HttpClient
                {
                    BaseAddress = EndPoint
                };
                var auth = UserName + ":" + PassWord;
                auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(auth), Base64FormattingOptions.None);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                return client;
            });


        public Uri GetUri()
        {
            return EndPoint;
        }

        public HttpClient GetClient()
        {
            return ClientInstance.Value;
        }
    }

    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var container = CreateContainer();

            var host = CreateHost(args, container);

            await host.RunAsync();
            return 0;
        }

        private static Container CreateContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IRpcClient, RPCClient>();
            container.Register<IRpcClientFactory, StaticRestClientFactory>();
            return container;
        }

        public static IHost CreateHost(string[] args, Container container)
        {
            return Host.CreateDefaultBuilder(args)
               
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseUrls($"http://127.0.0.1:9009")
                        .UseStartup(_ => new WebServerStartup());

                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSimpleInjector(container, options =>
                        {
                            options.DisposeContainerWithServiceProvider = false;

                            options.AddHostedService<WalletAsWebserviceService>();
                            options.AddAspNetCore().AddControllerActivation();
                        });
                    });
                })
                .Build();
        }
    }
}