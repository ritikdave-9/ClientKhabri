# ClientKhabri

**ClientKhabri** is a modern **.NET 8 console application** for interacting with news sources, categories, and articles. It features user authentication, personalized news feeds, and admin management — all with a rich terminal UI powered by [Spectre.Console](https://spectreconsole.net/).

---

## 🚀 Features

- **User Authentication** – Sign up, log in, and manage sessions securely.
- **Personalized News** – View news tailored to your interests.
- **Category Browsing** – Explore news by category and date range.
- **Article Actions** – Like, dislike, save, or report news articles.
- **Admin Dashboard** – Manage external servers, news sources, and categories.
- **Rich Console UI** – Styled output and interactive menus using Spectre.Console.

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or later

### Setup

1️⃣ **Clone the repository**
```
git clone https://github.com/ritikdave-9/ClientKhabri.git
cd ClientKhabri
```

2️⃣ **Restore dependencies**
```
dotnet restore
```

3️⃣ **Build the project**
```
dotnet build
```

4️⃣ **Run the application**
```
dotnet run
```

---

## 💻 Usage

- **Login/Signup**: Start the app and follow the prompts to log in or create a new account.
- **User Dashboard**: Browse news, interact with articles, and view personalized feeds.
- **Admin Dashboard**: Manage news sources, categories, and external servers.

---

## 📂 Project Structure

```
ClientKhabri/
 ├── Pages/                # UI logic for different pages (News, Admin, Auth, etc.)
 ├── Components/           # Reusable UI components (menus, headers, input, etc.)
 ├── HttpRequestManager.cs  # Handles HTTP requests and API communication
 ├── Cache.cs              # Manages user session and authentication token
 └── Utils/                # Utility classes for printing and error handling
```

---

## ⚙️ Configuration

- **API Base URL**: Configured in `HttpClient` initialization.
- **Authentication**: The app automatically sends the `AuthToken` from session cache with each request.

---

## 🤝 Contributing

Contributions are welcome! Please open an issue or submit a pull request for improvements and bug fixes.

---

## 📄 License

This project is licensed under the **MIT License**.

---

*Built with ❤️ using .NET 8 and Spectre.Console for a beautiful terminal experience.*
