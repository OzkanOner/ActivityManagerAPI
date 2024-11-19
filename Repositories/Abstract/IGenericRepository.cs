using ActivityManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagerAPI.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Create(TEntity entity);
        Task<IActionResult> Update(TEntity entity);
        Task<IActionResult> Delete(int id);
    }
}
