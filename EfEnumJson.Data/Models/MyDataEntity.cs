namespace EfEnumJson.Data.Models;

public record MyDataEntity
{
    public int Id { get; set; }
    public MyRecord Record { get; set; } = new();
}