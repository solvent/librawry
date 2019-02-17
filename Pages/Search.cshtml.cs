using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using librawry.classes;
using librawry.classes.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace librawry.Pages {

	public class SearchModel : PageModel {
		private readonly SqliteContext db;

		public IEnumerable<Title> SearchResult {
			get; private set;
		}

		public string SearchError {
			get; private set;
		}

		public SearchModel(SqliteContext db) {
			this.db = db;
		}

		public async Task OnGetAsync(string query) {
			if (string.IsNullOrEmpty(query) || query.Length < 3) {
				SearchError = "Please use at least 3 characters length string to search.";
				return;
			}
			var search = string.Join("%", query.Split(' '));
			SearchResult = await db.Titles
				.Include("TagRefs.Tag")
				.Where(x => EF.Functions.Like(x.Name, $"%{search}%") ||
					x.Episodes.Any(y => EF.Functions.Like(y.Name, $"%{search}%")))
				.OrderBy(x => x.Name)
				.ToListAsync();
		}
	}
}
