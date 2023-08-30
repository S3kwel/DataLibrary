using DATA.Repository.Abstraction.ConcurrencyHandling;
using DATA.Tests.Integration;
using DATA.Tests;
using Microsoft.EntityFrameworkCore;

public class EmployeeDatabaseTests : IClassFixture<DatabaseFixture>
{
    private readonly TestContext _context;
    private readonly IUnitOfWork<TestContext> _unitOfWork;

    public EmployeeDatabaseTests(DatabaseFixture fixture)
    {
        var options = new DbContextOptionsBuilder<TestContext>()
                        .UseSqlServer("Your SQL Server Connection String Here")
                        .Options;

        _context = new TestContext(options);
        _unitOfWork = new TestUnitOfWork(_context);
    }

    [Fact]
    public void InsertDocument_ShouldIncreaseCountByOne()
    {
        // Arrange
        var initialCount = _unitOfWork.Repository<Document, int>().Count;
        var newDocument = new Document { Title = "Test Document", Body = "TEST BODY" };

        // Act
        _unitOfWork.Repository<Document, int>().Insert(newDocument);
        _unitOfWork.Save();
        var finalCount = _unitOfWork.Repository<Document, int>().Count;

        // Assert
        Assert.Equal(initialCount + 1, finalCount);
    }

    // You can add more tests here as needed.
}
