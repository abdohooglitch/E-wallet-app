# E-Wallet App

A desktop **E-Wallet** application built with **C# Windows Forms** and **SQLite**. Users can register, log in, check balance, send money to other users, deposit funds, and view transaction history.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078D4?style=flat)
![SQLite](https://img.shields.io/badge/Database-SQLite-003B57?style=flat&logo=sqlite)

## Features

- **User registration & login** — sign up with name, mobile number, and password
- **Account dashboard** — view current balance and welcome message
- **Send money** — transfer funds to another user by mobile number
- **Deposit money** — add funds to your own wallet
- **Transaction history** — view past sends and deposits in a data grid
- **Consistent UI** — shared theme via `UiTheme` (colors, fonts, button styles)

## Tech Stack

| Layer        | Technology                          |
|-------------|--------------------------------------|
| Language    | C#                                   |
| UI          | Windows Forms (.NET 10)              |
| Database    | SQLite (`Microsoft.Data.Sqlite`)     |
| Architecture| Forms + Models + Database helper     |

## Project Structure

```
E-Wallet App/
├── Program.cs              # Application entry point
├── EWalletApp.csproj       # Project file
├── Forms/                  # UI screens (login, dashboard, transactions, etc.)
├── Models/                 # User and Transaction models
├── Database/               # SQLite setup and data access
└── UI/
    └── UiTheme.cs          # Central styling (colors, fonts, buttons)
```

## Prerequisites

- [**.NET 10 SDK**](https://dotnet.microsoft.com/download) (or compatible SDK for `net10.0-windows`)
- **Windows** (Windows Forms target)
- **Git** (optional, for cloning)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/abdohooglitch/E-wallet-app.git
cd E-wallet-app
```

### 2. Run the application

```bash
cd "E-Wallet App"
dotnet restore
dotnet run
```

Or open `E-Wallet App/EWalletApp.csproj` in **Visual Studio** and press **F5**.

### 3. First-time use

1. Open the app → **Sign Up** with name, mobile, and password  
2. **Login** with the same mobile and password  
3. Use **Send / Receive Money** or **Deposit** from the dashboard  
4. Check **Transaction History** for past activity  

> The SQLite database file (`app.db`) is created automatically in the build output folder on first run.

## Screens Overview

| Screen              | Description                                      |
|---------------------|--------------------------------------------------|
| Login / Sign Up     | Authentication and new account creation          |
| Account Balance     | Dashboard with balance and navigation            |
| Send & Receive      | Tabs for sending money and depositing            |
| Transaction History | List of all user transactions                    |

## Database

SQLite tables:

- **Users** — `Id`, `Name`, `MobileNumber`, `Password`, `Balance`
- **Transactions** — `Id`, `UserId`, `Type`, `Amount`, `Timestamp`, `Details`

All database operations are handled in `Database/DatabaseHelper.cs`.

## Development Notes

- UI is built **programmatically** in each form’s `InitializeComponents()` method (no separate `.Designer.cs` files).
- Styling is centralized in `UI/UiTheme.cs` for a consistent look across forms.
- Build artifacts (`bin/`, `obj/`) and local `app.db` are excluded via `.gitignore`.

## Author

**Abdul Hooglitch** — [GitHub @abdohooglitch](https://github.com/abdohooglitch)

## License

This project is submitted as an academic / portfolio work. Use and modify as needed for learning purposes.
