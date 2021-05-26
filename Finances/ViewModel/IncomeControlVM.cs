using Finances.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Finances.ViewModel
{
    public class IncomeControlVM : BaseVM
    {
        private string categorytext = "";
        private Income newState = null;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Income> Incomes { get; set; }
        private MessageBoxService service;
        private UnitOfWork unit;
        private string desc = "";
        private string sum = "";
        
        private Category selectedcat;
        public string Desc
        {
            get
            {
                return desc;
            }
            set
            {
                desc = value;
                OnPropertyChanged("Desc");
            }
        }
        public string Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }
        public DateTime Date { get; set; } = DateTime.Today;
        public Category SelectedCat
        {
            get
            {
                return selectedcat;
            }
            set
            {
                selectedcat = value;
                OnPropertyChanged("SelectedCat");
            }
        }
        private Income selecteditem = null;
        public Income SelectedItem
        {
            get
            {
                return selecteditem;
            }
            set
            {
                selecteditem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public string CategoryText
        {
            get
            {
                return categorytext;
            }
            set
            {
                categorytext = value;
                OnPropertyChanged("CategoryText");
            }
        }
        private bool isopen = false;
        public bool IsOpen
        {
            get
            {
                return isopen;
            }
            set
            {
                isopen = value;
                OnPropertyChanged("IsOpen");
            }
        }

        public RelayCommand SelectCommand { get; set; }

        public IncomeControlVM()
        {
            unit = new UnitOfWork();
            Categories = unit.Categories.GetItems();
            Incomes = unit.Users.GetIncomes();
            service = new MessageBoxService();

            SelectCommand = new RelayCommand((obj) =>
            {
                ListBox list = obj as ListBox;
                if(SelectedItem != null)
                {
                    if (prev != null)
                    {
                        prev.Visibility = Visibility.Hidden;
                    }
                    ListBoxItem boxItem = (ListBoxItem)(list.ItemContainerGenerator.ContainerFromItem(list.SelectedItem));
                    if (boxItem != null)
                    {
                        ContentPresenter presenter = FindVisualChild<ContentPresenter>(boxItem);
                        DataTemplate template = presenter.ContentTemplate;
                        Image img = (Image)template.FindName("del", presenter);
                        Image ed = (Image)template.FindName("edit", presenter);
                        img.DataContext = this;
                        ed.DataContext = this;
                        prev = img;
                        prevEd = ed;
                        img.Visibility = Visibility.Visible;
                        ed.Visibility = Visibility.Visible;
                    }
                }
            });
        }



        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand((obj) =>
                    {
                        if (isopen)
                            IsOpen = false;
                        else
                            IsOpen = true;
                        SelectedItem = null;
                    }));
            }
        }
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((obj) =>
                    {
                        if (SelectedItem != null)
                        {
                            IsOpen = true;
                            Date = SelectedItem.date_when;
                            OnPropertyChanged("Date");
                            Desc = SelectedItem.Descrip;
                            Sum = SelectedItem.sum.ToString();
                            SelectedCat = SelectedItem.Category;

                        }
                    }));
            }
        }
        private RelayCommand endedCommand;
        public RelayCommand EndEdCommand
        {
            get
            {
                return endedCommand ??
                    (endedCommand = new RelayCommand((obj) =>
                    {
                        if (SelectedItem != null)
                        {
                            int id = SelectedItem.ID_Income;
                            int index = Incomes.IndexOf(SelectedItem);
                            SelectedItem.date_when = Date;
                            SelectedItem.Descrip = Desc;
                            Sum = Sum.Replace(',', '.');
                            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                            SelectedItem.sum = double.Parse(Sum, formatter);
                            SelectedItem.CategoryName = SelectedCat.CategoryName;
                            unit.Users.UpdateIncome(SelectedItem);
                            unit.Save();
                            Incomes.RemoveAt(index);
                            Incomes.Insert(index, unit.Users.GetIncome(id));
                            SelectedItem = null;
                            IsOpen = false;
                            Sum = "";
                            Desc = "";
                        }
                    }));
            }
        }


        private RelayCommand delCommand;
        public RelayCommand DelCommand
        {
            get
            {
                return delCommand ??
                    (delCommand = new RelayCommand((obj) =>
                    {
                        if(SelectedItem != null)
                        {
                            bool result = service.ShowMessage("Уверены?", "Удаление", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                            if(result)
                            {
                                unit.Users.DeleteIncome(SelectedItem);
                                Incomes.Remove(SelectedItem);
                                unit.Save();
                            }
                        }
                    }));
            }
        }
        private RelayCommand addincomeCommand;
        public RelayCommand AddIncomeCommand
        {
            get
            {
                return addincomeCommand ??
                    (addincomeCommand = new RelayCommand((obj) =>
                    {
                        newState = new Income();
                        newState.date_when = Date;
                        newState.CategoryName = SelectedCat.CategoryName;
                        newState.Descrip = Desc;
                        Sum = Sum.Replace(',', '.');
                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        newState.sum = double.Parse(Sum, formatter);
                        newState.UserLogin = User.current.Login;
                        unit.Users.CreateIncome(newState);
                        unit.Save();
                        //if (Incomes.Count == 0)
                        //{
                        //    Incomes = new ObservableCollection<Income>();
                        //    OnPropertyChanged("Incomes");
                        //}
                        Incomes.Add(newState);
                        newState = null;
                        Sum = "";
                        Desc = "";

                    },(obj) => Date != null && Sum != "" && SelectedCat != null));
            }
        }
        private RelayCommand catCommand;
        public RelayCommand CatCommand
        {
            get
            {
                return catCommand ??
                    (catCommand = new RelayCommand((obj) =>
                    {
                        if (CategoryText != "")
                        {
                            Category temp = new Category();
                            temp.CategoryName = CategoryText;
                            service.ShowMessage("Выберите картинку для категории!", "Category", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                            AddImage.Open();
                            if (AddImage.file_image != "")
                            {
                                temp.Image = AddImage.Convert(AddImage.file_image);
                            }
                            //if (Categories.Count == 0)
                            //{
                            //    Categories = new ObservableCollection<Category>();
                            //    OnPropertyChanged("Categories");
                            //}

                            if (unit.Categories.GetItem(temp.CategoryName) == null)
                            {
                                Categories.Add(temp);
                                unit.Categories.Create(temp);
                                unit.Save();
                            }
                            else
                                service.ShowMessage("Такая категория уже есть", "Категория", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                            CategoryText = "";
                        }

                    }));
            }
        }







        private Image prev = null;
        private Image prevEd = null;

        private childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
