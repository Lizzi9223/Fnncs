using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.ViewModel
{
    public class AddImage
    {
        public static string file_image = "";
        static public void Open()
        {
            file_image = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            Nullable<bool> result = openFileDialog.ShowDialog();
            file_image = openFileDialog.FileName;
        }
        static public byte[] Convert(string image)
        {
            try
            {
                byte[] imageData;
                using (FileStream file = new FileStream(image, FileMode.Open))
                {
                    imageData = new byte[file.Length];
                    file.Read(imageData, 0, imageData.Length);
                }
                return imageData;

            }
            catch
            {
                return null;
            }
        }
    }
}
