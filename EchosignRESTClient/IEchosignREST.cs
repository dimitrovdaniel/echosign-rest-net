using System.Threading.Tasks;
using EchosignRESTClient.Models;
using System.IO;

namespace EchosignRESTClient
{
    public interface IEchosignREST
    {
        string AccessToken { get; set; }
        int AccessTokenExpires { get; }
        string ApiEndpointVer { get; set; }
        string RefreshToken { get; }

        Task<AlternateParticipantResponse> AddParticipant(string agreementId, string participantSetId, string participantId, AlternateParticipantInfo participantInfo);
        Task Authorize(string refreshToken);
        Task Authorize(string authCode, string redirect_uri);
        Task<AgreementStatusUpdateResponse> CancelAgreement(string agreementId, string comment, bool notifySigner);
        Task<AgreementCreationResponse> CreateAgreement(AgreementMinimalRequest newAgreement);
        Task<WidgetCreationResponse> CreateWidget(WidgetMinimalRequest newWidget);
        Task DeleteAgreement(string agreementId);
        void Dispose();
        Task<AgreementInfo> GetAgreement(string agreementId);
        Task<UserAgreements> GetAgreements();
        Task<Stream> GetAgreementDocument(string agreementId, string documentId);
        Task<AgreementDocuments> GetAgreementDocuments(string agreementId);
        Task<WidgetPersonalizedResponse> PersonalizedWidget(string widgetId, string email);
        Task Revoke(string token);
        Task<WidgetStatusUpdateResponse> UpdateWidgetStatus(string widgetId, WidgetStatusUpdateInfo info);
        Task<TransientDocument> UploadTransientDocument(string fileName, byte[] file, string mimeType = null);
        Task<ReminderCreationResult> SendReminders(ReminderCreationInfo info);
    }
}