using librawry.portable;
using librawry.portable.repo.titles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace librawry.Pages {

	public class SearchModel : PageModel {
		private readonly IUnitOfWork unitOfWork;

		public SearchModel(IUnitOfWork unitOfWork) {
			this.unitOfWork = unitOfWork;
		}

		[BindProperty(SupportsGet = true)]
		public ListRequest ListRequest {
			get; set;
		} = null!;

		public IEnumerable<ListResponse> ListResponse {
			get; private set;
		} = Enumerable.Empty<ListResponse>();

		public async Task OnGetAsync() {
			if (ModelState.IsValid) {
				ListResponse = await unitOfWork.TitleRepository.GetList(ListRequest);
			}
		}
	}
}
