using HiddenVilla.Models;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service.IService
{
	public interface IAuthenticationService
	{
		Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration);

		Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

		Task Logout();
	}
}
