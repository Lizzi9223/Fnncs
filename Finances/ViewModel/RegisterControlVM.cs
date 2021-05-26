using Finances.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finances.ViewModel
{
    public class RegisterControlVM : BaseVM
    {
        private StartVM vm;
        private UnitOfWork unit;
        private MessageBoxService service;
        public UserReg User { get; set; }
        public RegisterControlVM(StartVM vm)
        {
            this.vm = vm;
            User = new UserReg();
            service = new MessageBoxService();
        }




        private RelayCommand imageCommand;
        public RelayCommand ImageCommand
        {
            get
            {
                return imageCommand ??
                  (imageCommand = new RelayCommand(obj =>
                  {
                      AddImage.Open();
                      if (AddImage.file_image != "")
                          User.Image = AddImage.Convert(AddImage.file_image);
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
                      unit = new UnitOfWork();
                      if (unit.Users.GetItem(User.Login) == null)
                      {
                          User newUser = new User();
                          newUser.Name = User.Name;
                          newUser.LastName = User.LastName;
                          newUser.Image = User.Image;
                          newUser.Login = User.Login;
                          newUser.Password = User.Password;
                          unit.Users.Create(newUser);
                          unit.Save();
                          unit.Dispose();
                          service.ShowMessage("Успешно зарегистрирован!", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.None);
                          vm.CurrentVM = new EnterControlVM(vm);
                      }
                      else
                      {
                          service.ShowMessage("Такой пользователь уже существует!", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      }

                  }, (obj) => !User.HasErrors));

            }
        }
    }
}
