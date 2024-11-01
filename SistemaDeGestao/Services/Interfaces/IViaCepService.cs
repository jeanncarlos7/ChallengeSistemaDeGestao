using SistemaDeGestao.HttpObjects;

namespace SistemaDeGestao.Services.Interfaces
{
    public interface IViaCepService
    {
        Task<ViaCepResponse> GetAddressAsync(string cep);
    }
}
