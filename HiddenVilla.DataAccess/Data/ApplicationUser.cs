using Microsoft.AspNetCore.Identity;

namespace HiddenVilla.DataAccess.Data
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
