using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Finances.ViewModel
{
    public interface IMessageBoxService
    {
        bool ShowMessage(string text, string caption, MessageBoxButtons button, MessageBoxIcon image);
    }
    public class MessageBoxService : IMessageBoxService
    {
        public bool ShowMessage(string text, string caption, MessageBoxButtons button, MessageBoxIcon image)
        {
            DialogResult result = MessageBox.Show(text, caption, button, image);
            return DialogResult.Yes == result;
        }
    }
}
