# Console Bank App

A simple console-based banking application built in C# (.NET), simulating core banking operations: account creation, login, deposits, withdrawals, and transfers between accounts.

## Features

- User signup and login (passwords are hashed with SHA256, never stored in plain text)
- Two account types per user: Everyday and Saver
- Deposit, withdraw, and transfer funds between accounts
- Input validation (invalid amounts, insufficient funds, duplicate usernames/emails)
- Limited login attempts (3) with retry handling

## Try it

The app includes a demo account, so you can try it immediately without signing up:
- **Username:** Demo.User
- **Password:** Demo1234

## Project structure

- `Program.cs` — application entry point and menu logic
- `BankClient.cs` — client class (credentials, accounts, password hashing)
- `BankAccount.cs` — bank account class (type, balance)
- `Menu.cs` — console menu display

## How to run

```bash
dotnet build
dotnet run
```

Requires [.NET SDK](https://dotnet.microsoft.com/download) (9.0 or later).

## Author

Giada Arosio
Developed as part of my BSc Software Engineering (AI specialisation) coursework at Torrens University.
