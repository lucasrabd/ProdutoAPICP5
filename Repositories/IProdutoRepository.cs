public interface IProdutoRepository
{
    Task<List<Produto>> GetAllAsync();
    Task<Produto> GetByIdAsync(string id);
    Task CreateAsync(Produto produto);
    Task UpdateAsync(string id, Produto produto);
    Task DeleteAsync(string id);
}
