﻿using Biz.Extension.NullCheckerExtension;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.PersonManager
{
	public class PersonDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public PersonDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.People.Find(id);

				if (exist.IsNull()) throw new Exception("data not found");

				db.People.Remove(exist);
				db.SaveChanges();

				transac.Complete();
			}		
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
