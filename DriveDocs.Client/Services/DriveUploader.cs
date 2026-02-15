using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using DriveData = Google.Apis.Drive.v3.Data;

namespace DriveDocs.Client.Services;

public class DriveUploader
{
    private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
    private const string AppName = "DriveDocs";

    public async Task<string> UploadDocxAsync(
        string filePath,
        string credentialsPath,
        string folderId,
        string email)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);

        if (!File.Exists(credentialsPath))
            throw new FileNotFoundException("Credentials not found", credentialsPath);

        UserCredential credential;

        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            var tokenDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DriveDocsToken");

            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(tokenDir, true));
        }

        var drive = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = AppName
        });

        var name = Path.GetFileNameWithoutExtension(filePath);

        var meta = new DriveData.File
        {
            Name = name,
            MimeType = "application/vnd.google-apps.document",
            Parents = string.IsNullOrWhiteSpace(folderId)
                ? null
                : new List<string> { folderId }
        };

        var sourceMime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        FilesResource.CreateMediaUpload upload;

        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            upload = drive.Files.Create(meta, fs, sourceMime);
            upload.Fields = "id,webViewLink";
            upload.SupportsAllDrives = true;

            await upload.UploadAsync();
        }

        var file = upload.ResponseBody ?? throw new Exception("Upload failed");

        if (!string.IsNullOrWhiteSpace(email))
        {
            await drive.Permissions.Create(new DriveData.Permission
            {
                Type = "user",
                Role = "writer",
                EmailAddress = email
            }, file.Id).ExecuteAsync();
        }

        return file.WebViewLink
               ?? $"https://docs.google.com/document/d/{file.Id}/edit";
    }
}
