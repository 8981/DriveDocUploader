# 📝 DriveDocs / gdocify

| _ \ __ _ ___ ___ | _ \ ___ ___ | | ___ _ __
| | | / _ / __/ __| | |_) / _ \/ __/ _ |/ _ \ '|
| || | (| __ __ \ | _ < / (| (| | / |
|/ _,|// |_| __|__,|_|_|


![.NET](https://img.shields.io/badge/.NET-6+-blue)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-stable-success)

**DriveDocs / gdocify** — an open-source C# SDK and CLI tool for:

- uploading local files to Google Drive,  
- converting `.docx` to Google Docs,  
- assigning user permissions,  
- generating shareable links.  

---

## 🚀 Features

- OAuth 2.0 authentication  
- Upload `.docx` → Google Docs  
- Specify target folder on Google Drive  
- Assign editor permissions to specific users  
- Return a view/edit link  
- Use as a library in any project or via CLI  

---

## ⚙ Installation

```bash
git clone https://github.com/8981/DriveDocs.git
cd DriveDocs
dotnet restore


Install required packages for the library:

dotnet add DriveDocs.Client package Google.Apis.Drive.v3
dotnet add DriveDocs.Client package Google.Apis.Auth

🔐 Setup

Create a project in Google Cloud Console

Enable the Google Drive API

Create an OAuth Client ID (Desktop App)

Download credentials.json and place it in the project root

💻 CLI Usage
dotnet run --project DriveDocs.Cli -- "C:\path\file.docx"

Example output:
https://docs.google.com/document/d/1XxXxXxXxXx/edit


💻 SDK / Library Usage
using DriveDocs.Client.Services;

var uploader = new DriveUploader();
var link = await uploader.UploadDocxAsync(
    @"C:\Docs\Resume.docx",
    "credentials.json",
    "your-folder-id",
    "example@email.com");

Console.WriteLine(link);


📊 UploadDocxAsync Parameters
Parameter	Type	Description
filePath	string	Local path to the .docx file
credentialsPath	string	Path to credentials.json
folderId	string	Google Drive folder ID (empty = root)
email	string	Email to assign editor permission (optional)


Token Folder

On first run, a browser window will open for OAuth authentication
The token will be saved at:
%APPDATA%\DriveDocsToken
To re-authenticate, delete this folder

⚠ Errors & Solutions
Error	Cause	Solution
File not found	Invalid file path	Check the file path
Credentials not found	credentials.json missing	Download and place it
Upload failed	Google API error	Check permissions and folder access
