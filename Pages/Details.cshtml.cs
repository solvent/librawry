using System.Linq;
using System.Threading.Tasks;
using librawry.classes;
using librawry.classes.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace librawry.Pages {
	public class DetailsModel : PageModel {
		private readonly SqliteContext db;

		public DetailsModel(SqliteContext db) {
			this.db = db;
		}

		public Title DetailsResult {
			get; private set;
		}

		public async Task<IActionResult> OnGetAsync(int id) {
			DetailsResult = await db.Titles
				.Include("TagRefs.Tag")
				.Include("Episodes")
				.Where(x => x.Id == id)
				.FirstOrDefaultAsync();

			if (DetailsResult == null) {
				return NotFound();
			}

			return Page();
		}
	}
}
