using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Produto>>> Get()
    {
        return await _produtoRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> Get(string id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
            return NotFound();
        return produto;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto produto)
    {
        await _produtoRepository.CreateAsync(produto);
        return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Produto produto)
    {
        var existingProduto = await _produtoRepository.GetByIdAsync(id);
        if (existingProduto == null)
            return NotFound();

        await _produtoRepository.UpdateAsync(id, produto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingProduto = await _produtoRepository.GetByIdAsync(id);
        if (existingProduto == null)
            return NotFound();

        await _produtoRepository.DeleteAsync(id);
        return NoContent();
    }
}
