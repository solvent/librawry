using librawry.portable;
using librawry.portable.entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace librawry.Pages {

	public class SearchModel : PageModel {
		private readonly LibrawryContext db;

		public string SearchText {
			get; private set;
		}

		public IEnumerable<Title> SearchResult {
			get; private set;
		}

		public string SearchError {
			get; private set;
		}

		public SearchModel(LibrawryContext db) {
			this.db = db;
		}

		public async Task OnGetAsync(string query) {
			if (string.IsNullOrEmpty(query) || query.Length < 3) {
				SearchError = "Please use at least 3 characters length string to search.";
				return;
			}
			SearchText = query;
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
