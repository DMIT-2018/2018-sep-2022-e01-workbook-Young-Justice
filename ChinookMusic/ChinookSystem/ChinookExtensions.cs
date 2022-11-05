using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using ChinookSystem.BLL;
#endregion

namespace ChinookSystem
{
    // Your class needs to be public so it can be used outside of this project
    // This class also needs to be static
    public static class ChinookExtensions
    {
        // Method name can be anything, it must match
        // the builder.Services.xxxxx(options => ...)
        // statement in your Program.cs

        // The first parameter is the class that you are attempting
        // to extend.
        // The second parameter is the options value in your
        // call statement.
        // It is receiving the connection string for your application.
        public static void ChinookSystemBackendDependencies(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options
            )
        {
            // Register the DbContext class with the service collection
            services.AddDbContext<ChinookContext>(options);

            // Add any services that you create in the class library
            // using .AddTransient<serviceclassname>(...)

            services.AddTransient<TrackServices>((serviceProvider) =>
            {
                // Retrieve the registered DbContext done with
                // AddDbContext
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new TrackServices(context);
            });

            services.AddTransient<PlaylistTrackServices>((serviceProvider) =>
            {
                // Retrieve the registered DbContext done with
                // AddDbContext
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new PlaylistTrackServices(context);
            });
        }
    }
}
