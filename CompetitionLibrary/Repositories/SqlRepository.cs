using CompetitionLibrary.Enums;
using CompetitionLibrary.Exeptions;
using CompetitionLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using static System.Console;


namespace CompetitionLibrary.Repositories
{
	public class SqlRepository<ModelT> : IRepository<ModelT> where ModelT : class, IObjStatus
	{
        private readonly ILogger _logger;
        private readonly DbCompetContext _context;
		private IQueryable<ModelT> _query;

		public SqlRepository(DbCompetContext context, IQueryable<ModelT>? query = null)
		{
			_context = context;
			_query = query ?? _context.Set<ModelT>();
		}
		public async Task<ModelT> Create(ModelT model)
		{
			try
			{
				await((DbSet<ModelT>)_query).AddAsync(model);
				await _context.SaveChangesAsync();
				return model;
			}
			catch (DbUpdateException exception) when (exception.InnerException is SqlException)
            {
                _logger.LogError(exception.Message);
                switch (exception.InnerException.HResult)
                {
                    case -2146232060:
                        throw new AlreadyExistingException("");
                }
                throw;
            }
            catch (Exception e)
			{
                _logger.LogError(e.Message);
                throw;
			}
		}

		public async Task Delete(int id, int userId)
		{
			try
			{
				var entry = await _context.Set<ModelT>().FindAsync(id);

				if (entry != null)
				{
					entry.ObjStatusId = (int)EnumStatus.Delete;
					entry.UpdateUserId = userId;
				}
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		public IRepository<ModelT> Get(params Expression<Func<ModelT, object>>[] includes)
		{
			foreach (var expression in includes)
			{
				_query = _query.Include(expression);
			}
			return this;
		}

		public IRepository<ModelT> Get(params string[]? includes)
		{
			if (includes != null)
			{
				_query = includes.Aggregate(_query,
					(current, include) => current.Include(include));
			}
			return this;
		}

		public async Task<List<ModelT>> GetAll()
		{
			try
			{
				return await _query.Where(e => e.ObjStatusId == (int)EnumStatus.Active).ToListAsync();
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		public async Task<ModelT> GetOne(int id, IEnumerable<string>? includes = null)
		{
			try
			{
				var entry = await _context.Set<ModelT>().FindAsync(id);
				if (includes != null)
					foreach (var path in includes)
						await _context.Entry(entry).Reference(path).LoadAsync();
				switch (entry)
				{
					case { ObjStatusId: (int)EnumStatus.Active }:
						return entry;
					case null:
						break;
				}
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}

            throw new NotFoundException();
        }

        public async Task<ModelT> GetOne()
		{
			try
			{
				var model = await _query.FirstOrDefaultAsync();
				if (model != null) return model;
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}

            throw new NotFoundException();
        }

        public async Task<ModelT> Update<T>(int id, T model)
		{
			try
			{
				var local = await((DbSet<ModelT>)_query).FindAsync(id);
				if (local == null)
				{
                    throw new NotFoundException("No such item");
                }

                if (model != null) _context.Entry(local).CurrentValues.SetValues(model);
				await _context.SaveChangesAsync();
				return local;
			}
			catch (Exception e)
			{
				WriteLine(e);
				throw;
			}
		}

		public IRepository<ModelT> Where(Expression<Func<ModelT, bool>> predicate)
		{
			_query = _query.Where(predicate);
			return this;
		}

		public IRepository<ModelT> Where(params Expression<Func<ModelT, bool>>[] predicates)
		{
			Array.ForEach(predicates, predicate => _query = _query.Where(predicate));
			return this;
		}
	}
}
