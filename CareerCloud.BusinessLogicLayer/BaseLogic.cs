using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
	public abstract class BaseLogic<TPoco> where TPoco : IPoco
	{
		protected IDataRepository<TPoco> _repository;
		public BaseLogic(IDataRepository<TPoco> repository)
		{
			_repository = repository;
		}

		protected virtual void Verify(TPoco[] pocos)
		{
			return;
		}

		public virtual TPoco Get(Guid id)
		{
			return _repository.GetSingle(c => c.Id == id);
		}

		public virtual List<TPoco> GetList(Expression<Func<TPoco, bool>> where, params Expression<Func<TPoco, object>>[] navigationProperties)
		{
			return _repository.GetList(where, navigationProperties).ToList();
		}

		public virtual List<TPoco> GetAll(params Expression<Func<TPoco, object>>[] navigationProperties)
		{
			return _repository.GetAll(navigationProperties).ToList();
		}

		public virtual void Add(TPoco[] pocos)
		{
			foreach (TPoco poco in pocos)
			{
				if (poco.Id == Guid.Empty)
				{
					poco.Id = Guid.NewGuid();
				}
			}

			_repository.Add(pocos);
		}

		public virtual void Update(TPoco[] pocos)
		{
			_repository.Update(pocos);
		}

		public virtual void Delete(TPoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
