using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Domain.OtherModels;
using CrossFit.Glack.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CrossFit.Glack.Repository.Repository
{
    public class ClassRepository : RepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(ApplicationContext context) : base(context)
        {

        }

        public IEnumerable<ClassViewModel> GetTodaysClasses(DateTime dateTime)
        {
            var connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            try
            {
                string sp = "spSelectTodaysClasses";
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("dateTime", dateTime);
                var data = connection.Query<ClassViewModel>(sp, parameters, commandType: CommandType.StoredProcedure);

                return data;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<ClassViewModel> GetClassesInFuture(DateTime dateTime)
        {
            var connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            try
            {
                string sp = "spSelectClassesInFuture";
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("dateTime", dateTime);
                var data = connection.Query<ClassViewModel>(sp, parameters, commandType: CommandType.StoredProcedure);

                return data;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Class> GetTodaysClassesCustomer(DateTime dateTime, string userId)
        {
            var connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            try
            {
                string sp = "spSelectTodaysClassesCustomer";
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("dateTime", dateTime);
                parameters.Add("userId", userId);
                var data = connection.Query<Class>(sp, parameters, commandType: CommandType.StoredProcedure);

                return data;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Class> GetClassesInFutureCustomer(DateTime dateTime, string userId)
        {
            var connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            try
            {
                string sp = "spSelectClassesInFutureCustomer";
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("dateTime", dateTime);
                parameters.Add("userId", userId);
                var data = connection.Query<Class>(sp, parameters, commandType: CommandType.StoredProcedure);

                return data;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}