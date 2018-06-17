using System.Diagnostics;
using System.IO;
using AutorunMaster.Model.Entities;
using AutorunMaster.Model.Enums;

namespace AutorunMaster.Model.Tools
{
    public static class AutorunObjectHelper
    {
        /// <summary>
        /// Returns the constructed <see cref="AutorunObject"/> object
        /// </summary>
        /// <param name="filePath">Path to autorun object</param>
        /// <param name="arguments">arguments to autorun object</param>
        /// <param name="autorunType">Type of place where autoplay is made</param>
        public static AutorunObject GetAutorunObject(string filePath, string arguments, AutorunTypes autorunType)
        {
            filePath = filePath.Trim('"');

            if (!File.Exists(filePath)) return null;

            CertificateInfo certificateInfo = new CertificateInfo(filePath);
            var companyName = GetCompanyName(certificateInfo, filePath);

            AutorunObject autorunObject = new AutorunObject
            {
                FileName = Path.GetFileName(filePath),
                AutorunType = autorunType,
                FilePath = filePath,
                CommandParameters = arguments,
                ManufacturerName = companyName,
                Icon = filePath.ToImageSource(),
                HasCertificate = certificateInfo.IsExistsCertificate,
                IsVerifiedCertificate = certificateInfo.IsCorrectCertificate
            };
            return autorunObject;
        }

        /// <summary>
        /// If there is a name in the certificate, it returns it. If not, then the name is returned from the file.
        /// </summary>
        public static string GetCompanyName(CertificateInfo certificateInfo, string filePath)
        {
            try
            {
                var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

                if (certificateInfo.CompanyName != null || !string.IsNullOrWhiteSpace(certificateInfo.CompanyName))
                {
                    return certificateInfo.CompanyName;
                }

                return fileVersionInfo.CompanyName;
            }
            catch (System.Exception)
            {
                if (certificateInfo.CompanyName != null || !string.IsNullOrWhiteSpace(certificateInfo.CompanyName))
                {
                    return certificateInfo.CompanyName;
                }
                else
                    return string.Empty;
            }

        }
    }
}
