using Finances.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.ViewModel
{
    public class ProfileWindowVM :BaseVM
    {
        public Action EndAction { get; set; }
        private UnitOfWork unit;

        public UserReg User { get; set; }

        public ProfileWindowVM()
        {
            User = new UserReg();
            User.Name = Model.User.current.Name;
            User.LastName = Model.User.current.LastName;
            User.Image = Model.User.current.Image;
            User.Login = Model.User.current.Login;
            User.Password = Model.User.current.Password;
            unit = new UnitOfWork();
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
        private RelayCommand endCommand;
        public RelayCommand EndCommand
        {
            get
            {
                return endCommand ??
                  (endCommand = new RelayCommand(obj =>
                  {
                      Model.User.current.Name = User.Name;
                      Model.User.current.LastName = User.LastName;
                      Model.User.current.Image = User.Image;
                      unit.Users.Update(Model.User.current);
                      unit.Save();
                      unit.Dispose();
                      EndAction();
                  },(obj) => !User.HasErrors));

            }
        }
    }
}
