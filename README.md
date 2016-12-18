# Xamarin Forms 應用程式使用 Azure Notification Hubs 做推播通知

這個專案在示範 Xamarin Forms 的應用程式如何使用 Azure Notification Hubs 做推播通知的功能。並且以一個 ASP.NET 的網站示範如何送出推送訊息。

## 範例操作步驟

  1. [在 Microsoft Azure 建立 Azure 通知中樞](SetupAzureNotificationHubs.md)。
  2. 參考下列參考文章 2，分別在 Google Firebase Console 及 Apple Developer Portal 中處理好關於推播通知的 API Key/Sender ID (Android) 以及 Apple Certificate，並且在 Azure Notification Hubs 中將它們設定好。
  
### Xamarin.Android 專案

  1. 注意是否已安裝 _Azure Messaging_ 以及 _Google Cloud Messaging Client_ 這兩個 Xamarin 元件。
  
  2. 在 Xamarin.Android 專案中，修改 **Constants.cs** 檔案，將 ```SenderID``` 填寫在 Google Firebase Console 中取得的 Sender ID；其餘 Azure Notification Hubs 的連接字串（至少要擁有 Listen 權限）以及 Hub 名稱也填寫好。

  3. 在 **MyBroadcastReceiver.cs** 檔案的 ```OnRegistered``` 函式中，```registrationId``` 變數代表的是與 GCM 註冊的 handle，而向 Azure Notification Hub 註冊完的 ```hubRegistration.RegistrationId``` 則是跟 Azure Notification Hub 註冊的 ID，兩者用途不同。

  4. 因為 Azure Notification Hub 的 API 是用 tag （唯一值）來區分要送訊息給特定的使用者，所以跟 Hub 註冊時一定要填上 tag 的值才能被送特定訊息，這裡可以修改 ```tags``` 的資料來設定。

  5. 啟動後，注意 TAG 為 ```MyBroadcastReceiver-GCM``` 的 log 看看有無錯誤發生。

### Xamarin.iOS 專案

  1. 注意是否已安裝 _Azure Messaging_ 的 Xamarin 元件。

  2. 同 Xamarin.Android 專案所述，修改 **Constants.cs** 的檔案以填入正確的連接字串。

  3. 在 **AppDelegate.cs** 中注意修改 ```tags``` 的值（理由同上）。

### Xamarin.UWP 專案

_尚未實作_

### Web (ASP.NET) 專案

  1. 透過 NuGet 安裝 _Microsoft.Azure.NotificationHubs_ 元件。

  2. 修改 **Models/Notification.cs** 檔案，填入正確的連接字串以及 hub 名稱，**注意連接字串須有 Send 的權限**。

  3. 啟動 web 測試。

## 參考文章

  1. [Azure Notification Hubs Notify Users for Android with .NET backend](https://docs.microsoft.com/zh-tw/azure/notification-hubs/notification-hubs-aspnet-backend-gcm-android-push-to-user-google-notification)
  2. [Add push notifications to your Xamarin.Forms app](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-push)

# Code of Conduct
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.