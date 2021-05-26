using Finances.Model;
using Finances.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Finances.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        public Action CloseAction { get; set; }
        private User currentuser;
        private BaseVM currentVM;
        private MessageBoxService service;
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
        public User CurrentUser
        {
            get
            {
                return currentuser;
            }
            set
            {
                currentuser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        public RelayCommand SelectCommand { get; set; }
        public MainWindowVM()
        {
            service = new MessageBoxService();
            CurrentUser = User.current;
            CurrentVM = new MainControlVM();
            SelectCommand = new RelayCommand((selectedItem) =>
            {
                ListBoxItem item = selectedItem as ListBoxItem;
                if (item != null)
                {
                    switch (item.Name)
                    {
                        case "main":
                            CurrentVM = new MainControlVM();
                            break;
                        case "income":
                            CurrentVM = new IncomeControlVM();
                            break;
                        case "cost":
                            CurrentVM = new CostControlVM();
                            break;
                        default:
                            break;
                    }
                }
            });
        }

        private RelayCommand logoutCommand;
        public RelayCommand LogOutCommand
        {
            get
            {
                return logoutCommand ??
                    (logoutCommand = new RelayCommand((obj) =>
                    {
                        bool result = service.ShowMessage("Хотите выйти?", "Выход", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                        if(result)
                        {
                            User.current = null;
                            Start start = new Start();
                            CloseAction();
                            start.Show();
                        }
                    }));
            }
        }
        private RelayCommand profileCommand;
        public RelayCommand ProfileCommand
        {
            get
            {
                return profileCommand ??
                    (profileCommand = new RelayCommand((obj) =>
                    {
                        ProfileWindow profile = new ProfileWindow();
                        profile.ShowDialog();
                    }));
            }
        }


    }
}
