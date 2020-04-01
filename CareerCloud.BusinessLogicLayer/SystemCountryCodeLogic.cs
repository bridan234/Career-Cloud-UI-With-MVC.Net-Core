﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
	{
		protected IDataRepository<SystemCountryCodePoco> _repository;
		public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
		{
			_repository = repository;
			
		}

		protected  void Verify(SystemCountryCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();
			foreach (SystemCountryCodePoco poco in pocos)
			{
				if (string.IsNullOrEmpty(poco.Code))
					exceptions.Add(new ValidationException(900, $"Critical Error occured! \n *Couuntry Code* field cannot be blank"));
				if (string.IsNullOrEmpty(poco.Name))
					exceptions.Add(new ValidationException(901, $"Critical Error occured! \n *Country Name* field cannot be blank"));
			}

			if (exceptions.Count > 0)
				throw new AggregateException(exceptions);
		}

		public SystemCountryCodePoco Get(string code)
		{
			return _repository.GetSingle(c => c.Code == code);
		}

		public List<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
		{
			return _repository.GetAll(navigationProperties).ToList();
		}

		public virtual List<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
		{
			return _repository.GetList(where, navigationProperties).ToList();
		}
		public void Add(SystemCountryCodePoco[] pocos)
		{
			/*foreach (SystemCountryCodePoco poco in pocos)
			{
				if (poco.Id == Guid.Empty)
				{
					poco.Id = Guid.NewGuid();
					
				}
			}*/
			Verify(pocos);
			_repository.Add(pocos);
		}

		public void Update(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}

		public void Delete(SystemCountryCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
