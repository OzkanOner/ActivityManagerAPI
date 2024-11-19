using ActivityManagerAPI.Models;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return new NotFoundResult();
                }

                _context.Set<TEntity>().Remove((TEntity)entity);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
                if (entities == null || !entities.Any())
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(entities);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> Update(TEntity entity)
        {
            try
            {
                var existingEntity = await _context.Set<TEntity>().FindAsync(entity.Id);

                if (existingEntity == null)
                {
                    return new NotFoundObjectResult($"Entity with Id {entity.Id} not found.");
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();

                return new OkObjectResult(existingEntity);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
