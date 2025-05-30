namespace ExcelPDF.Data;

public interface IRepository<T>
{
    List<T> GetAll();
}
