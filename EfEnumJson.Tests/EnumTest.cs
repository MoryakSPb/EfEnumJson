using EfEnumJson.Data;
using EfEnumJson.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfEnumJson.Tests
{
    [TestClass]
    public class EnumTests : IDisposable, IAsyncDisposable
    {
        private readonly MyDataContext _dataContext;
        private readonly SqliteConnection _connection = new("Filename=:memory:");

        public EnumTests()
        {
            _connection.Open();
            _dataContext = new(new DbContextOptionsBuilder<MyDataContext>().UseSqlite(_connection).Options);
            _dataContext.Database.EnsureCreated();
        }

        [TestMethod]
        [DataRow(MyEnum.PlusOne)]
        [DataRow(MyEnum.MinusOne)]
        public async Task EnumToJson(MyEnum value)
        {
            MyDataEntity original = new()
            {
                Record =
                {
                    Value = value
                }
            };
            EntityEntry<MyDataEntity> entry = await _dataContext.Entities.AddAsync(original);

            await _dataContext.SaveChangesAsync();

            Assert.AreEqual(value, entry.Entity.Record.Value);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            _connection.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _dataContext.DisposeAsync();
            await _connection.DisposeAsync();
        }
    }
}