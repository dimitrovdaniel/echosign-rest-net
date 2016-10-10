# Echosign REST API .NET Client

This is the initial version of Echosign REST API .NET Client. It contains only the minimum required methods for uploading and sending an echosign document/agreement to people for signing.

**UPDATE: Added document listing and retrieving for agreement documents. Also extracted an interface for the client for when unit tests are needed.**    
**UPDATE: Added Widget creation/personalization methods in latest version.**    
**UPDATE: Added additional parameters to DocumentCreationInfo. Callback URL can now be added to notify you when documents are signed.**

## What you need to use the client
To use this client, you first need to create a new Application in your Echosign account (make sure you have a Developer account created, otherwise you cannot create applications). After you have created an application and obtained an Application ID/Client ID and a Client Secret, you can use them to obtain an authorization code, that will be used for getting an OAuth access token to use in the API.

**To request an authorization code**, you need to call the echosign public oauth URL with the following parameters:

https://salesforceintegration.na1.echosign.com/public/oauth?redirect_uri={REDIRECT_URI}&response_type=code&client_id={CLIENT_ID}&scope={SCOPES}

where:

**{REDIRECT_URI}** - the URL Echosign will send the authorization code back to (callback URL, via query string)  

**{CLIENT_ID}** - the client ID/application ID from your created application in the Echosign account panel  

**{SCOPES**} - permission scopes, seperated by "+". For example: agreement_send+agreement_read+agreement_write+widget_read+widget_write+library_read+library_write+workflow_read+workflow_write

More on getting an authorization code and setting scopes can be found here: https://secure.na1.echosign.com/public/static/oauthDoc.jsp

## Basic usage
After you have the authorization code, you can construct the client like so:

    EchosignREST client = new EchosignREST([API_URL], [CLIENT_ID], [SECRET_ID]);    
    client.Authorize([AUTH_CODE], [REDIRECT_URI]);`

All the required parameters are received from the callback URL call after Echosign authorization.

**That's it, you can now call all API methods after the Authorize call.**

_Note: The obtained Access Token lasts only 1 hour after which it expires and becomes invalid. You can refresh it and continue using the client by calling Authorize again, this time with the Refresh Token as a parameter._

    client.Authorize([REFRESH_TOKEN]);

This will refresh your Access Token and you will be able to call the API again.

**Refresh Tokens expire after 60 days of idleness. If you use a Refresh Token, the expiration period will be reset to after 60 days, which means you can use it indefinately, as long as you use it at least once every 60 days.**

**More info on available API methods can be found here: ** https://secure.na1.echosign.com/redirect/latestRestApiMethods

## Uploading a simple document and sending it to recipients for signing
To upload a simple PDF document and send it to people for signing, use the following code:

    ...
    TransientDocument document = await client.UploadTransientDocument("sample.pdf", fileBytes, "application/pdf");
    
    // create an AgreementMinimalRequest object and fill the data
    AgreementMinimalRequest request = new AgreementMinimalRequest();

    request.options = new InteractiveOptions()
    {
        authoringRequested = true,
        autoLoginUser = false
    };

    request.documentCreationInfo = new DocumentCreationInfo
    {
        fileInfos = new List<EchosignRESTClient.Models.FileInfo>() {
           new EchosignRESTClient.Models.FileInfo()
           {
                transientDocumentId = document.transientDocumentId
           }
        },
        name = "Sample Agreement",
        recipientSetInfos = new List<RecipientInfo>()
        {
           new RecipientInfo()
           {
               recipientSetMemberInfos = new List<RecipientMemberInfo>()
               {
                     new RecipientMemberInfo()
                     {
                         email = "recipient@somewhere.com"
                     }
               },
               recipientSetRole = "SIGNER"
          }
       },
       signatureFlow = "SENDER_SIGNATURE_NOT_REQUIRED",
       signatureType = "ESIGN"
    };
    
    AgreementCreationResponse response = await client.CreateAgreement(request);
    
This will create an agreement with the uploaded document. The "authoringRequested" property will cause the API to return a JavaScript snippet to include in your webpage. This will display an editor widget, which will allow you to place Signature and Initials fields on your document and then send it for signing.
