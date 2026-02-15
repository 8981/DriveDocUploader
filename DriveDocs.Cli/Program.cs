using DriveDocs.Client.Services;

if (args.Length == 0)
{
    Console.WriteLine("Usage: DriveDocs <file>");
    return;
}

var uploader = new DriveUploader();

var link = await uploader.UploadDocxAsync(
    args[0],
    "credentials.json",
    "",
    "");

Console.WriteLine(link);
