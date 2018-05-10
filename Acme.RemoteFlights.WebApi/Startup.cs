﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.RemoteFlights.Business.Contracts;
using Acme.RemoteFlights.Business.Repositories;
using Acme.RemoteFlights.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
 

namespace Acme.RemoteFlights.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<FlightDbContext>(_ => new FlightDbContext("FlightDbContext"));
            services.AddScoped<IFlightRepository,FlightRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
