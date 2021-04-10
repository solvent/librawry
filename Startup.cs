using librawry.classes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace librawry {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			var cons = Configuration.GetConnectionString("SqliteDatabase");
			services.AddDbContext<SqliteContext>(options => options.UseSqlite(cons));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			app.UseDeveloperExceptionPage();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseMvc();
		}
	}
}
