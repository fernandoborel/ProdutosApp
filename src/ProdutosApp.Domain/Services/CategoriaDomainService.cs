using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;

namespace ProdutosApp.Domain.Services;

public class CategoriaDomainService(IUnitOfWork unitOfWork) : ICategoriaDomainService
{
    public async Task<List<Categoria>> ObterTodos()
    {
        return await unitOfWork.CategoriaRepository.GetAllAsync();
    }
}