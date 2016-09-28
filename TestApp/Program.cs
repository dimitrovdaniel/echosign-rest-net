using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchosignRESTClient;
using System.IO;
using EchosignRESTClient.Models;
using EchosignRESTClient.ErrorHandling;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EchosignREST client = new EchosignREST("https://api.na1.echosign.com:443", "CBJCHBCAABAALgK5oFzABJQopTR85IUlm6jEVNu2D4io", "6vLYfhsapYkhJWD3AFpfOj3yQRJwDgfg");

            client.Authorize("3AAABLblqZhAAoYyGQWKSTvyC2AM6qXGzKH4GPSZA0cwc0644VqUXfRf5DOhzlx06HFE90BtOvKE*").Wait();

            try
            {
                //byte[] file = File.ReadAllBytes("C:/Users/dimit/Desktop/sample.pdf");
                //TransientDocument document = client.UploadTransientDocument("sample", file).Result;
                //AgreementMinimalRequest request = new AgreementMinimalRequest();

                //request.options = new InteractiveOptions()
                //{
                //    authoringRequested = true,
                //    autoLoginUser = false
                //};

                //request.documentCreationInfo = new DocumentCreationInfo
                //{
                //    fileInfos = new List<EchosignRESTClient.Models.FileInfo>() {
                //        new EchosignRESTClient.Models.FileInfo()
                //        {
                //            transientDocumentId = document.transientDocumentId
                //        }
                //    },
                //    name = "Sample Agreement",
                //    recipientSetInfos = new List<RecipientInfo>()
                //    {
                //        new RecipientInfo()
                //        {
                //            recipientSetMemberInfos = new List<RecipientMemberInfo>()
                //            {
                //                new RecipientMemberInfo()
                //                {
                //                    email = "dimitrovdaniel@outlook.com"
                //                }
                //            },
                //            recipientSetRole = "SIGNER"
                //        }
                //    },
                //    signatureFlow = "SENDER_SIGNATURE_NOT_REQUIRED",
                //    signatureType = "ESIGN"
                //};

                //AgreementCreationResponse response = client.CreateAgreement(request).Result;

                //Console.WriteLine("Agreement id: " + response.agreementId);
                //Console.WriteLine("Embed code: " + response.embeddedCode);
                //Console.WriteLine("Doc ID: " + document.transientDocumentId);

                //AlternateParticipantResponse resp = client.AddParticipant("3AAABLblqZhADb2ggOKEG28JAGy6t2Xj3tT34L56lFkj1nQTkvTyWIFkzWEUhsSFifdAArklqP38M6IpvKj34A91h2sORnHtQ", 
                //"NonSigners", "DANSIGNER", info).Result;
                UserAgreements agreements = client.GetAgreements().Result;

                AgreementStatusUpdateResponse response = client.CancelAgreement(agreements.userAgreementList.First().agreementId, "Game over man", true).Result;
                client.DeleteAgreement(agreements.userAgreementList.First().agreementId).Wait();
                Console.WriteLine("Cancel status: " + response.result);
            }
            catch (Exception ex)
            {
            }
            Console.ReadLine();
        }
    }
}
