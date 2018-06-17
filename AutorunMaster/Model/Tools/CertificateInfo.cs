using System;
using System.Security.Cryptography.X509Certificates;

namespace AutorunMaster.Model.Tools
{
    public class CertificateInfo
    {
        public bool IsExistsCertificate { get; set; }

        public bool IsCorrectCertificate { get; set; }

        public string CompanyName { get; set; }

        public CertificateInfo(string filePath)
        {
            try
            {
                var cert = X509Certificate.CreateFromSignedFile(filePath);            
                var x509Certificate2 = new X509Certificate2(cert.Handle);
                CompanyName = x509Certificate2.GetNameInfo(X509NameType.SimpleName, false);

                IsExistsCertificate = true;
                IsCorrectCertificate = x509Certificate2.Verify();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                IsExistsCertificate = false;
            }
        }
    }
}
