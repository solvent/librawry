using System.Collections.Generic;

namespace librawry.classes.entities {

	public class Episode {

		public int Id {
			get; set;
		}

		public string Name {
			get; set;
		}

		public int TitleId {
			get; set;
		}

		public Title Title {
			get; set;
		}
	}

}
