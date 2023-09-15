using CrossFit.Glack.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossFit.Glack.Repository.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationContext applicationContext;

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public void Save()
        {
            applicationContext.SaveChanges();
        }
    }
}