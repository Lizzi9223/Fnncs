using Finances.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Finances.Model
{
    public class UserReg : ValidatableModel 
    {
        private string name;
        private string last_name;
        private string login;
        private string password;
        private byte[] image; 

        public UserReg()
        {
            Image = AddImage.Convert(@"C:\Users\Джек\Desktop\Fnncs\Finances\Finances\View\Images\image.png");
        }

        // св-ва зависимости
        public static readonly DependencyProperty LoginProperty;
        public static readonly DependencyProperty PasswordProperty;

        static UserReg()
        {
            LoginProperty = DependencyProperty.Register("Login", typeof(string), typeof(UserReg)); //(элемент xaml-разметки, тип хранимого значения, 
                                                                                                            //класс в котором находится св-во зависимости

            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.CoerceValueCallback = new CoerceValueCallback(CorrectValue);

            PasswordProperty = DependencyProperty.Register("Password", typeof(int), typeof(UserReg), metadata, new ValidateValueCallback(ValidateValue));
        }

        private static object CorrectValue(DependencyObject d, object baseValue)
        {
            int currentValue = ((string)baseValue).Length;
            if (currentValue > 4)
                return ((string)baseValue).Remove(4);
            return currentValue;
        }

        private static bool ValidateValue(object value)
        {
            int currentValue = ((string)value).Length;
            if (currentValue >= 0)
                return true;
            return false;
        }

        // ---------------------------

        public UserReg(string name, string lastName, string login, string password, byte[] image)
        {
            Name = name;
            LastName = lastName;
            Login = login;
            Password = password;
            Image = image;
        }

        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([А-Я]{1})([а-я]*)$", ErrorMessage = "Имя неверно введено!")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([А-Я]{1})([а-я]*)$", ErrorMessage = "Фамилия неверно введена!")]
        public string LastName
        {
            get
            {
                return last_name;
            }
            set
            {
                last_name = value;
                RaisePropertyChanged("LastName");
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Логин без русских символов")]
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Пароль без русских символов")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        public byte[] Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }
    }
}
