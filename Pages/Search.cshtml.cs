using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using librawry.classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace librawry.Pages {

	public class SearchModel : PageModel {
		private readonly SqliteContext db;

		public IEnumerable<string> SearchResult {
			get; private set;
		}

		public SearchModel(SqliteContext db) {
			this.db = db;
		}

		public async Task OnGetAsync(string query) {
			SearchResult = await db.Titles
				.Where(x => EF.Functions.Like(x.Name, $"%{query}%") ||
					x.Episodes.Any(y => EF.Functions.Like(y.Name, $"%{query}%")))
				.Select(x => x.Name)
				.ToListAsync();
		}
	}
}
