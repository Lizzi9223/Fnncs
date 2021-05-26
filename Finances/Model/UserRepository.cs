using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
    public class UserRepository : IRepository<User>
    {
        private FinancesEntities db;

        private SqlConnection connection;

        string connectionString = @"data source=LAPTOP-JC77JP4P;initial catalog=Finances;integrated security=True";

        public UserRepository(FinancesEntities context)
        {
            db = context;
            //connection = new SqlConnection(connectionString);

        }
        public void Create(User item)
        {
            if (item != null)
                db.Users.Add(item);
        }

        public void Delete(User item)
        {
            if (item != null)
                db.Users.Remove(item);
        }
        public void DeleteIncome(Income item)
        {
            db.Incomes.Remove(item);
        }
        public async void DeleteCost(Cost item)
        {
            db.Costs.Remove(item);

            //string sqlExpression = "DELETE from Cost where ID_COST=" + item.ID_Cost.ToString();
            //await connection.OpenAsync();
            //SqlTransaction transaction = connection.BeginTransaction();
            //SqlCommand command = connection.CreateCommand();
            //command.Transaction = transaction;
            //try
            //{
            //    command.CommandText = sqlExpression;
            //    int number = await command.ExecuteNonQueryAsync();
            //    transaction.Commit();
            //}
            //catch
            //{
            //    transaction.Rollback();
            //}
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public User GetItem(object login)
        {
            return db.Users.FirstOrDefault(c => c.Login == login.ToString());
        }
        public Cost GetCost(object id)
        {
            return db.Costs.FirstOrDefault(c => c.ID_Cost == (int)id);
        }
        public Income GetIncome(object id)
        {
            return db.Incomes.FirstOrDefault(c => c.ID_Income == (int)id);
        }
        public void CreateIncome(Income item)
        {
            if (item != null)
                db.Incomes.Add(item);
        }
        public void CreateCost(Cost item)
        {
            if (item != null)
                db.Costs.Add(item);

            //string sqlExpression = "INSERT INTO Costs VALUES ('" + item.UserLogin + "', '" + item.CategoryName + "', '" + item.Descrip + "', '" + item.date_when.ToString() + "', '" + item.sum + "')";
            //connection.Open();
            //SqlCommand command = new SqlCommand(sqlExpression, connection);
            //int number = command.ExecuteNonQuery();
        }
        public ObservableCollection<User> GetItems()
        {
            return new ObservableCollection<User>(db.Users.ToList());
        }
        public ObservableCollection<Income> GetIncomes()
        {
            return new ObservableCollection<Income>(db.Incomes.Where(c => c.UserLogin == User.current.Login).ToList());
        }
        public ObservableCollection<Cost> GetCosts()
        {
            return new ObservableCollection<Cost>(db.Costs.Where(c => c.UserLogin == User.current.Login).ToList());
        }
        public List<Cost> GetMonthlyCosts()
        {
            return new List<Cost>(db.Costs.Where(c => c.UserLogin == User.current.Login && c.date_when.Month == DateTime.Today.Month).ToList());
        }
        public List<Income> GetMonthlyIncomes()
        {
            return new List<Income>(db.Incomes.Where(c => c.UserLogin == User.current.Login && c.date_when.Month == DateTime.Today.Month).ToList());
        }

        public void Update(User item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
        public void UpdateIncome(Income item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
        public async void UpdateCost(Cost item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;   

            //string sqlExpression = "UPDATE Cost Set UserLogin=" + item.UserLogin + "', CategoryName=" + item.CategoryName + ", Descrip=" + item.Descrip + ", date_when=" + item.date_when.ToString() + ", sum" + item.sum.ToString() + " where ID_COST=" + item.ID_Cost.ToString();
            //await connection.OpenAsync();
            //SqlCommand command = new SqlCommand(sqlExpression, connection);
            //int number = await command.ExecuteNonQueryAsync();
        }
    }
}
