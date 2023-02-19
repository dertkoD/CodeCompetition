using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace CompetitionLibrary.Repositories
{
	public interface IRepository<ModelT> where ModelT: class, IObjStatus
	{
		public IRepository<ModelT> Get(params Expression<Func<ModelT, object>>[] includes);

		public Task<ModelT> Create(ModelT model);

		public Task<ModelT> Update<T>(int id, T model);

		public Task<List<ModelT>> GetAll();

		public IRepository<ModelT> Where(Expression<Func<ModelT, bool>> predicate);

		public IRepository<ModelT> Where(params Expression<Func<ModelT, bool>>[] predicate);

		public Task<ModelT> GetOne(int id, IEnumerable<string>? includes = null);

		public Task Delete(int id, int userId);

		public Task<ModelT> GetOne();

		public IRepository<ModelT> Get(params string[]? includes);
	}
}
