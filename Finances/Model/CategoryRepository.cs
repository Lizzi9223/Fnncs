using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
    public class CategoryRepository : IRepository<Category>
    {
        private FinancesEntities db;

        public CategoryRepository(FinancesEntities context)
        {
            db = context;

        }
        public void Create(Category item)
        {
            if (item != null)
                db.Categories.Add(item);
        }

        public void Delete(Category item)
        {
            if (item != null)
                db.Categories.Remove(item);
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

        public Category GetItem(object id)
        {
            return db.Categories.FirstOrDefault(c => c.CategoryName == id.ToString());
        }

        public ObservableCollection<Category> GetItems()
        {
            return new ObservableCollection<Category>(db.Categories.ToList());
        }

        public void Update(Category item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
