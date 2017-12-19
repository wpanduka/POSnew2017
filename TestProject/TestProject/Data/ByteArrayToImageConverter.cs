using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestProject.Data
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                /// byte[] decodedByteArray = System.Convert.FromBase64String(Encoding.UTF8.GetString(imageAsBytes, 0, imageAsBytes.Length));
                // var stream = new MemoryStream(decodedByteArray);
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return null;
            throw new NotImplementedException();
        }

        ////public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        ////{
        ////    if (value == null)
        ////        return null;
        ////    var ByteArray = value as byte[];
        ////    Stream stream = new MemoryStream(ByteArray);
        ////    ImageSource ImageSrc = ImageSource.FromStream(() => stream);
        ////    return ImageSrc;
        ////}

        ////public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        ////{
        ////    //return null;
        ////    throw new NotImplementedException();
        ////}
    }
}
