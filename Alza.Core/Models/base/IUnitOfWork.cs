namespace Alza.Core.Models;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
}
