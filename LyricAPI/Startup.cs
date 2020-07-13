using Lyric.API.Logic;
using Lyric.API.Logic.Interfaces;
using Lyrics.Service;
using Lyrics.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicBrainz.Service;
using MusicBrainz.Service.Interfaces;
using VueCliMiddleware;

namespace LyricAPI
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
			services.AddControllers();
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp";
			});

			services.AddSingleton<ILyricService, LyricService>()
				.AddSingleton<IMusicBrainzService, MusicBrainzService>()
				.AddSingleton<ILyricApiLogic, LyricApiLogic>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseSpaStaticFiles();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSpa(spa =>
			{
				if (env.IsDevelopment())
					spa.Options.SourcePath = "ClientApp";
				else
					spa.Options.SourcePath = "dist";

				if (env.IsDevelopment())
				{
					spa.UseVueCli(npmScript: "serve");
				}

			});
		}
	}
}
