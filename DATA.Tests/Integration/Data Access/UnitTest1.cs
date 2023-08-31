using DATA.Repository.Abstraction.ConcurrencyHandling;
using DATA.Tests.Integration;
using DATA.Tests;
using Microsoft.EntityFrameworkCore;

public class DocumentDatabaseTests<TKey> : IClassFixture<DatabaseFixture>
{
    private readonly TestContext   _context;
    private readonly IUnitOfWork<TestContext> _unitOfWork;

    public DocumentDatabaseTests(DatabaseFixture fixture)
    {
        var options = new DbContextOptionsBuilder<TestContext>()
                        .UseSqlServer("Server=SEKWEL;Database=dataTests;User Id=sa;Password=test;TrustServerCertificate=True;")
                        .Options;

        _context = new TestContext(options);
        _unitOfWork = new TestUnitOfWork<TKey>(_context);
    }

    [Fact]
    public void InsertDocument_ShouldIncreaseCountByOne()
    {
        // Arrange
        var initialCount = _unitOfWork.Repository<Document<TKey>, TKey>().Count;
        var newDocument = new Document<TKey> { Title = "Test Document", Body = "TEST BODY" };

        // Act
        _unitOfWork.Repository<Document<TKey>, TKey>().Insert(newDocument);
        _unitOfWork.Save();
        var finalCount = _unitOfWork.Repository<Document<TKey>, TKey>().Count;

        // Assert
        Assert.Equal(initialCount + 1, finalCount);
    }

    // You can add more tests here as needed.
}
