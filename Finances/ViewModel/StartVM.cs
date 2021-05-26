using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.ViewModel
{
    public class StartVM : BaseVM
    {
        public Action CloseAction { get; set; }

        private BaseVM currentVM;
        public BaseVM CurrentVM
        {
            get
            {
                return currentVM;
            }
            set
            {
                currentVM = value;
                OnPropertyChanged("CurrentVM");
            }
        }

        public StartVM()
        {
            CurrentVM = new EnterControlVM(this);
        }
    }
}
