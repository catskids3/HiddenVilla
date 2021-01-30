using HiddenVilla.Models;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service.IService
{
	public interface IStripePaymentService
	{
		public Task<SuccessModel> CheckOut(StripePaymentDTO model);
	}
}
