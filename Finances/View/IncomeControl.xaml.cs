using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finances.View
{
    /// <summary>
    /// Логика взаимодействия для IncomeControl.xaml
    /// </summary>
    public partial class IncomeControl : UserControl
    {
        private bool flag = false;
        private int length = 0;
        private bool maxL = false;

        public IncomeControl()
        {
            InitializeComponent();
            picker.DisplayDateEnd = DateTime.Today;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ValidNumericString(((TextBox)sender).Text))
            {
                Dispatcher.BeginInvoke(new Action(() => ((TextBox)sender).Undo()));
                e.Handled = true;
                flag = false;

            }
            if (flag && !maxL)
            {
                ((TextBox)sender).MaxLength = ((TextBox)sender).Text.Length + 2;
                maxL = true;
            }
            if (!flag && maxL)
            {
                ((TextBox)sender).MaxLength = 12;
                maxL = false;
            }
        }


        public bool ValidNumericString(string IPString)
        {
            char prev = ' ';
            if (!flag || IPString.Length < length)
            {
                foreach (char ch in IPString)
                {
                    if (!char.IsDigit(ch))
                    {
                        if (ch != 44)
                        {
                            if (ch != 46)
                                return false;
                        }
                        //if (flag)
                        //    return false;
                    }

                    prev = ch;
                }
            }
            else
            {
                if (IPString.Length > 0)
                {
                    char test = IPString.Last();
                    if (!char.IsDigit(test))
                        return false;
                }
            }
            length = IPString.Length;
            flag = IPString.Contains('.') || IPString.Contains(',');
            return true;

        }
    }
}
