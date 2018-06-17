using System.Windows.Media;
using AutorunMaster.Model.Enums;

namespace AutorunMaster.Model.Entities
{
    public class AutorunObject
    {
        private ImageSource _icon { get; set; }
        public ImageSource Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                _icon.Freeze();
            }
        }

        public string FileName { get; set; }

        private string _commandParameters;
        public string CommandParameters
        {
            get=> _commandParameters;
            set => _commandParameters = string.IsNullOrWhiteSpace(value) ? "[Null]" : value;
        }

        public string FilePath { get; set; }

        public AutorunTypes AutorunType { get; set; }

        public bool HasCertificate { get; set; }

        public bool IsVerifiedCertificate { get; set; }

        private string _manufacturerName;
        public string ManufacturerName
        {
            get => _manufacturerName;
            set => _manufacturerName = string.IsNullOrWhiteSpace(value) ? "[Null]" : value;
        }
    }
}
