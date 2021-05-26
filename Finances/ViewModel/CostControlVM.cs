using Finances.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Finances.ViewModel
{
    public class CostControlVM : BaseVM
    {
        private string categorytext = "";
        private Cost newState = null;


        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Cost> Costs { get; set; }

        private readonly CollectionViewSource _CVCosts = new CollectionViewSource();
        public ICollectionView CVCosts => _CVCosts?.View;

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
        private Cost selecteditem = null;
        public Cost SelectedItem
        {
            get
            {
                return selecteditem;
            }
            set
            {
                selecteditem = value;
                OnPropertyChanged("SelectedItem");
                _CVCosts.Source = Costs;
                OnPropertyChanged(nameof(CVCosts));
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

        public CostControlVM()
        {
            unit = new UnitOfWork();
            Categories = unit.Categories.GetItems();
            Costs = unit.Users.GetCosts();
            _CVCosts.Source = Costs;
            _CVCosts.Filter += OnCostsFiltred;
            service = new MessageBoxService();

            SelectCommand = new RelayCommand((obj) =>
            {
                ListBox list = obj as ListBox;
                if (SelectedItem != null)
                {
                    if (prev != null)
                    {
                        prev.Visibility = Visibility.Hidden;
                    }
                    if (prevEd != null)
                    {
                        prevEd.Visibility = Visibility.Hidden;
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
                            int id = SelectedItem.ID_Cost;
                            int index = Costs.IndexOf(SelectedItem);
                            SelectedItem.date_when = Date;
                            SelectedItem.Descrip = Desc;
                            Sum = Sum.Replace(',', '.');
                            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                            SelectedItem.sum = double.Parse(Sum, formatter);
                            SelectedItem.CategoryName = SelectedCat.CategoryName;
                            unit.Users.UpdateCost(SelectedItem);
                            unit.Save();
                            Costs.RemoveAt(index);
                            Costs.Insert(index, unit.Users.GetCost(id));
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
                        if (SelectedItem != null)
                        {
                            bool result = service.ShowMessage("Уверены?", "Удаление", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                            if (result)
                            {
                                unit.Users.DeleteCost(SelectedItem);
                                Costs.Remove(SelectedItem);
                                unit.Save();
                            }
                        }
                    }));
            }
        }
        private RelayCommand addcostCommand;
        public RelayCommand AddCostCommand
        {
            get
            {
                return addcostCommand ??
                    (addcostCommand = new RelayCommand((obj) =>
                    {
                        newState = new Cost();
                        newState.date_when = Date;
                        newState.CategoryName = SelectedCat.CategoryName;
                        newState.Descrip = Desc;
                        Sum = Sum.Replace(',', '.');
                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        newState.sum = double.Parse(Sum, formatter);
                        newState.UserLogin = User.current.Login;
                        unit.Users.CreateCost(newState);
                        unit.Save();
                        //if (Incomes.Count == 0)
                        //{
                        //    Incomes = new ObservableCollection<Income>();
                        //    OnPropertyChanged("Incomes");
                        //}
                        Costs.Add(newState);
                        newState = null;
                        Sum = "";
                        Desc = "";

                    }, (obj) => Date != null && Sum != "" && SelectedCat != null));
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









        private int comboboxSelectedItemIndex;
        public int ComboboxSelectedItemIndex
        {
            get => comboboxSelectedItemIndex;
            set
            {
                comboboxSelectedItemIndex = value;
                OnPropertyChanged("ComboboxSelectedItemIndex");
                _CVCosts.SortDescriptions.Clear();
                switch (comboboxSelectedItemIndex)
                {
                    case 0:
                        break;
                    case 1:
                        _CVCosts.SortDescriptions.Add(new SortDescription("date_when", ListSortDirection.Ascending));
                        break;
                    case 2:
                        _CVCosts.SortDescriptions.Add(new SortDescription("sum", ListSortDirection.Ascending));
                        break;
                    case 3:
                        _CVCosts.SortDescriptions.Add(new SortDescription("CategoryName", ListSortDirection.Ascending));
                        break;
                }
            }
        }



        private string costsFilterText;
        public string CostsFilterText
        {
            get
            {
                if (costsFilterText == "") return null;
                return costsFilterText;
            }
            set
            {
                costsFilterText = value;
                OnPropertyChanged("CostsFilterText");
                _CVCosts.View.Refresh();
            }
        }

        private void OnCostsFiltred(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Cost cost))
            {
                e.Accepted = false;
                return;
            }

            var filter_text = costsFilterText;
            if (string.IsNullOrWhiteSpace(filter_text)) return;

            if (cost.CategoryName.Contains(filter_text)) return;
            if (cost.Descrip.Contains(filter_text)) return;
            if ((cost.date_when).ToShortDateString().Contains(filter_text)) return;
            if ((cost.sum).ToString().Contains(filter_text)) return;

            e.Accepted = false;
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
