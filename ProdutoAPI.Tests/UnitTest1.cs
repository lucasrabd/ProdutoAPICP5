using Xunit;
using Moq;
using ProdutoAPI.Controllers;
using ProdutoAPI.Models;
using ProdutoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

public class ProdutoControllerTests
{
    private readonly Mock<IProdutoRepository> _mockRepo;
    private readonly ProdutoController _controller;

    public ProdutoControllerTests()
    {
        _mockRepo = new Mock<IProdutoRepository>();
        _controller = new ProdutoController(_mockRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsAllProdutos()
    {
        var produtos = new List<Produto>
        {
            new Produto { Id = "1", Nome = "Produto1", Preco = 10.0m, Descricao = "Descricao1" },
            new Produto { Id = "2", Nome = "Produto2", Preco = 20.0m, Descricao = "Descricao2" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

        var result = await _controller.Get();

        var okResult = Assert.IsType<ActionResult<List<Produto>>>(result);
        var returnValue = Assert.IsType<List<Produto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task Create_AddsProduto()
    {
        var produto = new Produto { Id = "1", Nome = "Produto1", Preco = 10.0m, Descricao = "Descricao1" };
        await _controller.Create(produto);
        _mockRepo.Verify(repo => repo.CreateAsync(produto), Times.Once);
    }
}
