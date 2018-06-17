using System.Windows;
using System.Windows.Media;

namespace AutorunMaster.Model.Tools
{
    public static class FileToImageIconConverter
    {
        public static ImageSource ToImageSource(this string filePath)
        {
           ImageSource icon;

            if (!System.IO.File.Exists(filePath)) return null;

            using (var sysicon = System.Drawing.Icon.ExtractAssociatedIcon(filePath))
            {
                icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    sysicon.Handle,
                    new Int32Rect(0,0,32,32),
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }

            return icon;
        }
    }
}
