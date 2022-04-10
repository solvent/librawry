using librawry.portable;
using librawry.portable.repo.titles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace librawry.Pages {
	public class DetailsModel : PageModel {
		private readonly IUnitOfWork unitOfWork;

		public DetailsModel(IUnitOfWork unitOfWork) {
			this.unitOfWork = unitOfWork;
		}

		public DetailsResponse DetailsResult {
			get; private set;
		}

		public async Task<IActionResult> OnGetAsync(int id) {
			DetailsResult = await unitOfWork.TitleRepository.GetDetails(id);

			if (DetailsResult == null) {
				return NotFound();
			}

			return Page();
		}
	}
}
