using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        T GetItem(object id);
        ObservableCollection<T> GetItems();

    }
}
