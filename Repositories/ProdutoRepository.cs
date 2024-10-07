using MongoDB.Driver;

public class ProdutoRepository : IProdutoRepository
{
    private readonly IMongoCollection<Produto> _produtos;

    public ProdutoRepository(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDB"));
        var database = client.GetDatabase("ProdutosDB");
        _produtos = database.GetCollection<Produto>("Produtos");
    }

    public async Task<List<Produto>> GetAllAsync()
    {
        return await _produtos.Find(p => true).ToListAsync();
    }

    public async Task<Produto> GetByIdAsync(string id)
    {
        return await _produtos.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Produto produto)
    {
        await _produtos.InsertOneAsync(produto);
    }

    public async Task UpdateAsync(string id, Produto produto)
    {
        await _produtos.ReplaceOneAsync(p => p.Id == id, produto);
    }

    public async Task DeleteAsync(string id)
    {
        await _produtos.DeleteOneAsync(p => p.Id == id);
    }
}
