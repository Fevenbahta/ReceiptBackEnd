
using LIB.API.Application.Contracts.Persistence;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Persistence.Repositories;

using LIBPROPERTY.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PERSISTANCE.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;



namespace LIB.API.Persistence
{
    public static partial class PersistenceServiceRegistrtion
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LIBAPIDbContext>(options => options.UseOracle(configuration.GetConnectionString("LIBAPIConnectionString")));

            services.AddDbContext<LIBAPIDbSQLContext>(options => options.UseSqlServer(configuration.GetConnectionString("LIBAPISQLConnectionString")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            services.AddScoped<IRecieptRepository, RecieptRepository>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();
            // Inside ConfigureServices method of Startup.cs
            services.AddScoped<UpdateLogService>();

       
            services.AddHttpClient<SoapClient2>();
         

       

            services.AddHttpClient();

            string connectionString = configuration.GetConnectionString("LIBAPIConnectionString");
            string connectionSqlString = configuration.GetConnectionString("LIBAPISQLConnectionString");

            // Register IDbConnection with a SqlConnection instance using the retrieved connection string
               services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));

            services.AddScoped<IDbConnection>(_ => new OracleConnection(connectionString));
            // services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionSqlString));

    

            return services;
        }
    }
}
