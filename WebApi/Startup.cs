using Microsoft.EntityFrameworkCore;

using DataModel.Repository;
using Application.Services;

namespace Domain
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			
			services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
			

			services
				.AddEndpointsApiExplorer()
				.AddSwaggerGen()
				.AddDbContext<AbsanteeContext>(options =>
				{
					options.UseSqlServer(Configuration["ConnectionString"]);
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ColaboratorConsumer colaboratorConsumer)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
			}

	 		
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors("AllowAllOrigins"); // Use the CORS policy
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
	
			
		}
	}
}