# 📝 DriveDocs / gdocify

```text
   ____                 ____              _            
  |  _ \ __ _ ___ ___  |  _ \ ___  ___ __| | ___ _ __ 
  | | | / _` / __/ __| | |_) / _ \/ __/ _` |/ _ \ '__|
  | |_| | (_| \__ \__ \ |  _ <  __/ (_| (_| |  __/ |   
  |____/ \__,_|___/___/ |_| \_\___|\___\__,_|\___|_|

```


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
```

## Install required packages for the library:

dotnet add DriveDocs.Client package Google.Apis.Drive.v3
dotnet add DriveDocs.Client package Google.Apis.Auth

## 🔐 Setup

 - Create a project in Google Cloud Console

 - Enable the Google Drive API

 - Create an OAuth Client ID (Desktop App)

 - Download credentials.json and place it in the project root

## 💻 CLI Usage
dotnet run --project DriveDocs.Cli -- "C:\path\file.docx"

Example output:
https://docs.google.com/document/d/1XxXxXxXxXx/edit


## 💻 SDK / Library Usage
```code
using DriveDocs.Client.Services;

var uploader = new DriveUploader();
var link = await uploader.UploadDocxAsync(
    @"C:\Docs\Resume.docx",
    "credentials.json",
    "your-folder-id",
    "example@email.com");

Console.WriteLine(link);
```

## 📊 UploadDocxAsync Parameters
<img width="1080" height="350" alt="image" src="https://github.com/user-attachments/assets/13f28156-5516-4d42-bfee-2b97f8551010" />


## Token Folder

On first run, a browser window will open for OAuth authentication
The token will be saved at:
%APPDATA%\DriveDocsToken
To re-authenticate, delete this folder

⚠ Errors & Solutions
<img width="1129" height="279" alt="image" src="https://github.com/user-attachments/assets/5c3dff73-6a4e-4e52-bb5d-b4ae5741e5df" />

