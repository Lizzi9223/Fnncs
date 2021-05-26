using Finances.Model;
using Finances.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finances.ViewModel
{
    public class EnterControlVM : BaseVM
    {
        private UnitOfWork unit;
        private StartVM vm;
        private string login = "";
        private MessageBoxService service;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        private string password = "";
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public EnterControlVM(StartVM vm)
        {
            this.vm = vm;
            service = new MessageBoxService();
        }






        private RelayCommand enterCommand;
        public RelayCommand EnterCommand
        {
            get
            {
                return enterCommand ??
                  (enterCommand = new RelayCommand(obj =>
                  {
                      unit = new UnitOfWork();
                      if (Login == "" || Password == "")
                      {
                          service.ShowMessage("Не все поля заполнены!", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      }
                      else
                      {
                          User.current = unit.Users.GetItem(Login);
                          if (User.current != null)
                          {
                              if (Password == User.current.Password)
                              {
                                  MainWindow main = new MainWindow();
                                  main.Show();
                                  vm.CloseAction();
                              }
                              else
                              {
                                  service.ShowMessage("Пароль неправильный!", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              }
                          }
                          else
                          {
                              service.ShowMessage("Такого пользователя нет!", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          }
                      }
                      unit.Dispose();
                  }));

            }
        }
        private RelayCommand regCommand;
        public RelayCommand RegCommand
        {
            get
            {
                return regCommand ??
                  (regCommand = new RelayCommand(obj =>
                  {
                      vm.CurrentVM = new RegisterControlVM(vm);
                  }));

            }
        }
    }
}
