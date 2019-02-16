using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using librawry.classes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
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
