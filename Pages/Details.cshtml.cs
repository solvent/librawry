using librawry.portable;
using librawry.portable.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace librawry.Pages {
	public class DetailsModel : PageModel {
		private readonly LibrawryContext db;

		public DetailsModel(LibrawryContext db) {
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
