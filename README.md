# Expense & Savings Tracker

A desktop **personal finance** application built with **C# Windows Forms** and **SQLite**. Track daily expenses, set a monthly budget, manage savings goals, and view category-wise reports — all in one app.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078D4?style=flat)
![SQLite](https://img.shields.io/badge/Database-SQLite-003B57?style=flat&logo=sqlite)

## Features

- **User registration & login** — sign up with name, mobile number, and password
- **Dashboard** — monthly expenses, total savings, and quick navigation
- **Monthly budget** — set a spending limit and track usage with a progress bar
- **Expense tracking** — add expenses with category, amount, date, and note
- **Expense history** — view all expenses for the current month
- **Savings goals** — create goals, deposit money, withdraw, and track progress
- **Monthly reports** — category-wise expense breakdown with percentages

## Tech Stack

| Layer        | Technology                          |
|-------------|--------------------------------------|
| Language    | C#                                   |
| UI          | Windows Forms (.NET 10)              |
| Database    | SQLite (`Microsoft.Data.Sqlite`)     |
| Architecture| Models + Forms + Database helper     |

## Project Structure

```
Expense & Savings Tracker/
├── Program.cs                    # Application entry point
├── ExpenseSavingsTracker.csproj  # Project file
├── Forms/                        # UI screens — logic (.cs) + designer (.Designer.cs)
│   ├── AuthForm                  # Login
│   ├── SignupForm                # Registration
│   ├── DashboardForm             # Summary, budget, navigation
│   ├── ExpenseForm               # Add expense
│   ├── ExpenseHistoryForm        # Expense list
│   ├── SavingsForm               # Savings goals
│   └── ReportsForm               # Category report
├── Models/                       # User, Expense, SavingGoal, etc.
└── Database/                     # SQLite setup and data access
```

## Prerequisites

- [**.NET 10 SDK**](https://dotnet.microsoft.com/download) (or compatible SDK for `net10.0-windows`)
- **Windows** (Windows Forms target)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/abdohooglitch/E-wallet-app.git
cd E-wallet-app
```

### 2. Run the application

```bash
cd "Expense & Savings Tracker"
dotnet restore
dotnet run
```

Or open `Expense & Savings Tracker/ExpenseSavingsTracker.csproj` in **Visual Studio** and press **F5**.

### 3. First-time use

1. **Sign Up** with name, mobile number, and password
2. **Login** with the same credentials
3. On the **Dashboard**, set your **monthly budget**
4. Use **Add Expense** to record spending
5. Create **Savings Goals** and add deposits
6. Check **Monthly Report** for category breakdown

> The SQLite database file (`finance.db`) is created automatically on first run.

## Screens Overview

| Screen            | Description                                           |
|-------------------|-------------------------------------------------------|
| Login / Sign Up   | Authentication and account creation                   |
| Dashboard         | Expense & savings summary, budget tracker, navigation |
| Add Expense       | Record a new expense with category and date           |
| Expense History   | List of expenses for the current month                |
| Savings Goals     | Create goals, deposit, withdraw, view progress        |
| Monthly Report    | Category-wise expense totals and percentages          |

## Expense Categories

Food & Dining · Transport · Bills & Utilities · Shopping · Entertainment · Health · Education · Other

## Database

SQLite tables:

- **Users** — `Id`, `Name`, `MobileNumber`, `Password`, `MonthlyBudget`
- **Expenses** — `Id`, `UserId`, `Category`, `Amount`, `ExpenseDate`, `Note`
- **SavingGoals** — `Id`, `UserId`, `Title`, `TargetAmount`, `SavedAmount`, `CreatedAt`

All database operations are handled in `Database/DatabaseHelper.cs`.

## Development Notes

- Each form uses a **partial class**: `.cs` for logic, `.Designer.cs` for UI layout and styling.
- Project namespace: `ExpenseSavingsTracker` | Assembly output: `ExpenseSavingsTracker.exe`
- Build artifacts (`bin/`, `obj/`) and local `finance.db` are excluded via `.gitignore`.

## Author

**Abdul Hooglitch** — [GitHub @abdohooglitch](https://github.com/abdohooglitch)

## License

This project is submitted as an academic / portfolio work. Use and modify as needed for learning purposes.
