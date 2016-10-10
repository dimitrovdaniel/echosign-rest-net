using EchosignRESTClient.Models;
using EchosignRESTClient.ErrorHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace EchosignRESTClient
{
    /// <summary>
    ///  EchosignREST client for working with the Echosign REST v5+ api
    /// </summary>
    public class EchosignREST : IDisposable, IEchosignREST
    {
        private HttpClient client;

        private string clientId;
        private string secretId;

        private string accessToken;
        private int accessTokenExpires;
        private string refreshToken;

        private string apiUrl;
        private string apiEndpointVer = "api/rest/v5";

        /// <summary>
        /// Initialize EchosignREST without Access Token. Must call Authorize() after initialization to acquire Access Token.
        /// </summary>
        /// <param name="apiUrl">API url returned from the authorization request URL</param>
        /// <param name="clientId">Application/Client ID</param>
        /// <param name="secretId">Client Secret</param>
        public EchosignREST(string apiUrl, string clientId, string secretId)
        {
            this.apiUrl = apiUrl;
            this.clientId = clientId;
            this.secretId = secretId;

            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
        }

        /// <summary>
        /// Obtain Access Token for the Echosign REST API (use only if you don't already have a Refresh Token, or if it is expired)
        /// </summary>
        /// <param name="authCode">Authorization code received from the authorization request</param>
        /// <param name="redirect_uri">Redirect URI matching the one specified in the authorization request</param>
        /// <returns></returns>
        public async Task Authorize(string authCode, string redirect_uri)
        {
            using (HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", secretId),
                new KeyValuePair<string, string>("code", authCode),
                new KeyValuePair<string, string>("redirect_uri", redirect_uri)
            }))
            {
                HttpResponseMessage result = await client.PostAsync("oauth/token", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    TokenResponse tokenObj = JsonConvert.DeserializeObject<TokenResponse>(response);

                    this.accessToken = tokenObj.access_token;
                    this.accessTokenExpires = tokenObj.expires_in;
                    this.refreshToken = tokenObj.refresh_token;

                    client.DefaultRequestHeaders.Remove("Access-Token");
                    client.DefaultRequestHeaders.Add("Access-Token", accessToken);
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, true);
                }
            }


        }

        /// <summary>
        /// Refresh existing Access Token with the Refresh Token.
        /// </summary>
        /// <param name="refreshToken">Refresh Token used to get a new Access Token.</param>
        /// <returns></returns>
        public async Task Authorize(string refreshToken)
        {
            using (HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", secretId),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            }))
            {
                HttpResponseMessage result = await client.PostAsync("oauth/refresh", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    RefreshTokenResponse tokenObj = JsonConvert.DeserializeObject<RefreshTokenResponse>(response);

                    this.accessToken = tokenObj.access_token;
                    this.accessTokenExpires = tokenObj.expires_in;

                    client.DefaultRequestHeaders.Remove("Access-Token");
                    client.DefaultRequestHeaders.Add("Access-Token", accessToken);
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, true);
                }
            }
        }

        /// <summary>
        /// Revoke Access and Refresh tokens so they cannot be used again until next authorization.
        /// </summary>
        /// <param name="token">Access or Refresh token.</param>
        /// <returns></returns>
        public async Task Revoke(string token)
        {
            using (HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token", token)
            }))
            {
                HttpResponseMessage result = await client.PostAsync("oauth/revoke", content);
                if (!result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, true);
                }
            }
        }

        /// <summary>
        /// Uploads a document and obtains the document's ID to use in an Agreement.
        /// </summary>
        /// <param name="fileName">The name for the Transient Document</param>
        /// <param name="file">The document file</param>
        /// <param name="mimeType">(Optional) The mime type for the document</param>
        /// <returns>Returns the uploaded document ID</returns>
        public async Task<TransientDocument> UploadTransientDocument(string fileName, byte[] file, string mimeType = null)
        {
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(new MemoryStream(file)), "File", "sample.pdf");
                content.Add(new StringContent(fileName), "File-Name");

                if (mimeType != null)
                    content.Add(new StringContent(mimeType), "Mime-Type");

                HttpResponseMessage result = await client.PostAsync(apiEndpointVer + "/transientDocuments", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    TransientDocument document = JsonConvert.DeserializeObject<TransientDocument>(response);

                    return document;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        /// <summary>
        /// Creates an agreement. Sends it out for signatures, and returns the agreementID in the response to the client
        /// </summary>
        /// <param name="newAgreement">Information about the agreement</param>
        /// <returns>AgreementCreationResponse</returns>
        public async Task<AgreementCreationResponse> CreateAgreement(AgreementMinimalRequest newAgreement)
        {
            string serializedObject = JsonConvert.SerializeObject(newAgreement);

            using (StringContent content = new StringContent(serializedObject, Encoding.UTF8))
            {
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");

                HttpResponseMessage result = await client.PostAsync(apiEndpointVer + "/agreements", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    AgreementCreationResponse agreement = JsonConvert.DeserializeObject<AgreementCreationResponse>(response);

                    return agreement;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        /// <summary>
        /// Creates a new alternate participant
        /// </summary>
        /// <param name="agreementId">The agreement identifier, as returned by the agreement creation API or retrieved from the API to fetch agreements</param>
        /// <param name="participantSetId">The participant set identifier</param>
        /// <param name="participantId">The participant identifier</param>
        /// <param name="participantInfo">Information about the alternate participant</param>
        /// <returns>AlternateParticipantResponse</returns>
        public async Task<AlternateParticipantResponse> AddParticipant(string agreementId, string participantSetId, string participantId, AlternateParticipantInfo participantInfo)
        {
            string serializedObject = JsonConvert.SerializeObject(participantInfo);

            using (StringContent content = new StringContent(serializedObject, Encoding.UTF8))
            {
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");

                HttpResponseMessage result = await client.PostAsync(apiEndpointVer + "/agreements/" + agreementId + "/participantSets/" +
                                                        participantSetId + "/participants/" + participantId + "/alternateParticipants", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    AlternateParticipantResponse agreement = JsonConvert.DeserializeObject<AlternateParticipantResponse>(response);

                    return agreement;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        /// <summary>
        /// Retrieves agreements for the user
        /// </summary>
        /// <returns>UserAgreements</returns>
        public async Task<UserAgreements> GetAgreements()
        {
            HttpResponseMessage result = await client.GetAsync(apiEndpointVer + "/agreements");
            if (result.IsSuccessStatusCode)
            {
                string response = await result.Content.ReadAsStringAsync();
                UserAgreements agreements = JsonConvert.DeserializeObject<UserAgreements>(response);

                return agreements;
            }
            else
            {
                string response = await result.Content.ReadAsStringAsync();
                HandleError(result.StatusCode, response, false);

                return null;
            }
        }

        /// <summary>
        /// Retrieves the latest status of an agreement
        /// </summary>
        /// <param name="agreementId">The agreement identifier, as returned by the agreement creation API or retrieved from the API to fetch agreements</param>
        /// <returns>AgreementInfo</returns>
        public async Task<AgreementInfo> GetAgreement(string agreementId)
        {
            HttpResponseMessage result = await client.GetAsync(apiEndpointVer + "/agreements/" + agreementId);
            if (result.IsSuccessStatusCode)
            {
                string response = await result.Content.ReadAsStringAsync();
                AgreementInfo agreement = JsonConvert.DeserializeObject<AgreementInfo>(response);

                return agreement;
            }
            else
            {
                string response = await result.Content.ReadAsStringAsync();
                HandleError(result.StatusCode, response, false);

                return null;
            }
        }

        /// <summary>
        /// Retrieves the IDs of all the main and supporting documents of an agreement identified by agreementid
        /// </summary>
        /// <param name="agreementId">The agreement identifier, as returned by the agreement creation API or retrieved from the API to fetch agreements</param>
        /// <returns>AgreementInfo</returns>
        public async Task<AgreementDocuments> GetAgreementDocuments(string agreementId)
        {
            HttpResponseMessage result = await client.GetAsync(apiEndpointVer + "/agreements/" + agreementId + "/documents");
            if (result.IsSuccessStatusCode)
            {
                string response = await result.Content.ReadAsStringAsync();
                AgreementDocuments agreement = JsonConvert.DeserializeObject<AgreementDocuments>(response);

                return agreement;
            }
            else
            {
                string response = await result.Content.ReadAsStringAsync();
                HandleError(result.StatusCode, response, false);

                return null;
            }
        }

        /// <summary>
        /// Retrieves the file stream of a document of an agreement
        /// </summary>
        /// <param name="agreementId">The agreement identifier, as returned by the agreement creation API or retrieved from the API to fetch agreements</param>
        /// <param name="documentId">The document identifier, as retrieved from the API which fetches the documents of a specified agreement</param>
        /// <returns>AgreementInfo</returns>
        public async Task<Stream> GetAgreementDocument(string agreementId, string documentId)
        {
            HttpResponseMessage result = await client.GetAsync(apiEndpointVer + "/agreements/" + agreementId + "/documents/" + documentId);
            if (result.IsSuccessStatusCode)
            {
                Stream response = await result.Content.ReadAsStreamAsync();

                return response;
            }
            else
            {
                string response = await result.Content.ReadAsStringAsync();
                HandleError(result.StatusCode, response, false);

                return null;
            }
        }

        /// <summary>
        /// Cancels an agreement
        /// </summary>
        /// <param name="agreementId">The agreement identifier, as returned by the agreement creation API or retrieved from the API to fetch agreements</param>
        /// <param name="comment">An optional comment describing to the recipient why you want to cancel the transaction</param>
        /// <param name="notifySigner">Whether or not you would like the recipient to be notified that the transaction has been cancelled. The notification is mandatory if any party has already signed this document. The default value is false</param>
        /// <returns>AgreementStatusUpdateResponse</returns>
        public async Task<AgreementStatusUpdateResponse> CancelAgreement(string agreementId, string comment, bool notifySigner)
        {
            AgreementStatusUpdateInfo info = new AgreementStatusUpdateInfo();
            info.value = "CANCEL";
            info.notifySigner = notifySigner;
            info.comment = comment;

            string serializedObject = JsonConvert.SerializeObject(info);

            using (HttpContent content = new StringContent(serializedObject))
            {
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");

                HttpResponseMessage result = await client.PutAsync(apiEndpointVer + "/agreements/" + agreementId + "/status", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    AgreementStatusUpdateResponse agreement = JsonConvert.DeserializeObject<AgreementStatusUpdateResponse>(response);

                    return agreement;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        /// <summary>
        /// Deletes an agreement. Agreement will no longer be visible in the user's Manage Page
        /// </summary>
        /// <param name="agreementId">The agreement identifier, as returned by the agreement creation API or retrieved from the API to fetch agreements</param>
        /// <returns>void</returns>
        public async Task DeleteAgreement(string agreementId)
        {
            HttpResponseMessage result = await client.DeleteAsync(apiEndpointVer + "/agreements/" + agreementId);
            if (!result.IsSuccessStatusCode)
            {
                string response = await result.Content.ReadAsStringAsync();
                HandleError(result.StatusCode, response, false);
            }
        }

        /// <summary>
        /// Creates a widget and returns the Javascript snippet and URL to access the widget and widgetID in response to the client
        /// </summary>
        /// <param name="newWidget">Information about the widget that you want to create</param>
        /// <returns>WidgetCreationResponse</returns>
        public async Task<WidgetCreationResponse> CreateWidget(WidgetMinimalRequest newWidget)
        {
            string serializedObject = JsonConvert.SerializeObject(newWidget);

            using (StringContent content = new StringContent(serializedObject, Encoding.UTF8))
            {
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");

                HttpResponseMessage result = await client.PostAsync(apiEndpointVer + "/widgets", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    WidgetCreationResponse widget = JsonConvert.DeserializeObject<WidgetCreationResponse>(response);

                    return widget;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        /// <summary>
        /// Personalize the widget to a signable document for a specific known user
        /// </summary>
        /// <param name="widgetId">The widget identifier, as returned by the widget creation API or retrieved from the API to fetch widgets</param>
        /// <param name="email">The email address of the person who will be receiving this widget</param>
        /// <returns></returns>
        public async Task<WidgetPersonalizedResponse> PersonalizedWidget(string widgetId, string email)
        {
            WidgetPersonalizationInfo info = new WidgetPersonalizationInfo();
            info.email = email;
            string serializedObject = JsonConvert.SerializeObject(info);

            using (StringContent content = new StringContent(serializedObject, Encoding.UTF8))
            {
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");

                HttpResponseMessage result = await client.PutAsync(apiEndpointVer + "/widgets/" + widgetId + "/personalize", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    WidgetPersonalizedResponse widget = JsonConvert.DeserializeObject<WidgetPersonalizedResponse>(response);

                    return widget;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        /// <summary>
        /// Enables or Disables a widget
        /// </summary>
        /// <param name="widgetId">The widget identifier, as returned by the widget creation API or retrieved from the API to fetch widgets</param>
        /// <param name="info">Widget status update information object</param>
        /// <returns>WidgetStatusUpdateResponse</returns>
        public async Task<WidgetStatusUpdateResponse> UpdateWidgetStatus(string widgetId, WidgetStatusUpdateInfo info)
        {
            string serializedObject = JsonConvert.SerializeObject(info);

            using (StringContent content = new StringContent(serializedObject, Encoding.UTF8))
            {
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");

                HttpResponseMessage result = await client.PutAsync(apiEndpointVer + "/widgets/" + widgetId + "/status", content);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    WidgetStatusUpdateResponse widget = JsonConvert.DeserializeObject<WidgetStatusUpdateResponse>(response);

                    return widget;
                }
                else
                {
                    string response = await result.Content.ReadAsStringAsync();
                    HandleError(result.StatusCode, response, false);

                    return null;
                }
            }
        }

        private void HandleError(HttpStatusCode statusCode, string response, bool isOAuthError = false)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    EchosignError error = JsonConvert.DeserializeObject<EchosignError>(response);
                    throw new EchosignOAuthException(error);
                case HttpStatusCode.BadRequest:
                    if (!isOAuthError) // echosign returns different json for oAuth calls
                    {
                        EchosignErrorCode errorCode = JsonConvert.DeserializeObject<EchosignErrorCode>(response);
                        throw new EchosignBadRequestException(errorCode);
                    }
                    else
                    {
                        EchosignError oAuthError = JsonConvert.DeserializeObject<EchosignError>(response);
                        throw new EchosignOAuthException(oAuthError);
                    }
                default:
                    EchosignErrorCode defaultError = JsonConvert.DeserializeObject<EchosignErrorCode>(response);
                    throw new EchosignBadRequestException(defaultError);
            }
        }

        public void Dispose()
        {
            if (this.client != null)
                this.client.Dispose();
        }

        /// <summary>
        /// Gets or sets the Access Token obtained from the last authorization request
        /// </summary>
        public string AccessToken
        {
            get
            {
                return this.accessToken;
            }
            set
            {
                this.accessToken = value;
            }
        }

        /// <summary>
        /// Returns the Refresh Token obtained from the last authorization request
        /// </summary>
        public string RefreshToken
        {
            get
            {
                return this.refreshToken;
            }
        }

        /// <summary>
        /// Get or set API endpoint and version. Default is "api/rest/v5"
        /// </summary>
        public string ApiEndpointVer
        {
            get
            {
                return this.apiEndpointVer;
            }
            set
            {
                this.apiEndpointVer = value;
            }
        }

        /// <summary>
        /// Gets the expiration time of the acquired Access Token (in seconds)
        /// </summary>
        public int AccessTokenExpires
        {
            get
            {
                return accessTokenExpires;
            }
        }
    }
}
