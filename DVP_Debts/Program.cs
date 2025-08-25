using Domain.Services.Interfaces;
using Domain.Services.Services;
using Infraestructure.Core.Context;
using Infraestructure.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DVP_Debts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy",
                    builder =>
                        builder.WithOrigins("http://localhost:4200").SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithHeaders("authorization", "accept", "content-type", "origin"));
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

            builder.Services.AddDbContext<CoreContext>(dbContextOptions =>
                dbContextOptions.UseNpgsql(connectionString));

            builder.Services.AddScoped<CoreContext, CoreContext>();

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IUser, UserService>();
            builder.Services.AddTransient<IDebtService, DebtService>();
            builder.Services.AddTransient<ICipherSerializerService, CipherSerializerService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();
            builder.Services.AddTransient<ILoginService, LoginService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("SiteCorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
