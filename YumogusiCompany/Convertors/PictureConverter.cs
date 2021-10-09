using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace YumogusiCompany.Convertors
{
    internal class PictureConverter : IValueConverter
    {
        private readonly BitmapImage _defaultLogo;

        /// <summary>
        /// Подгружает картинку по умолчанию, чтобы в последствии её использовать. Такое решение необходимо для оптимизации,
        /// т.к. происходит много вызовов преобразования байтов в картинку, что негативно влияет на объем используемой памяти.
        /// </summary>
        private PictureConverter(BitmapImage defaultLogo)
        {
            _defaultLogo = defaultLogo;
        }

        public PictureConverter() : this(ConvertByteToImage(Properties.Resources.none_picture_logo)) { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not byte[] bytes)
            {
                return _defaultLogo;
            }

            return ConvertByteToImage(bytes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static BitmapImage ConvertByteToImage(byte[] bytes)
        {
            var image = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

#pragma warning disable IDE0051 // Удалите неиспользуемые закрытые члены
        private static BitmapImage ConvertUriToImage(string uri)
#pragma warning restore IDE0051 // Удалите неиспользуемые закрытые члены
        {
            byte[] bytes;
            using (var fs = new FileStream(uri, FileMode.Open))
            {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
            }
            return ConvertByteToImage(bytes);
        }
    }
}
