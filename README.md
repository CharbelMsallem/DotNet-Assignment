# .NET Assignment Solution

This is my backend solution for the Customer Registration and Migration system. I built it using .NET 10 Web API, Entity Framework Core, and SQL Server.

I organized the project using Clean Architecture to keep the API, Logic, and Data parts separate and clean.

## 🛠️ My Setup & Database

**My Computer:** macOS (Apple Silicon)

**Why I used Docker:**

Because I am on a Mac, I cannot install the normal Microsoft SQL Server directly. So, I used Docker to run the database.

* Image: `azure-sql-edge` (It works great on Mac chips).

**Note for Windows Users:**

If you want to use a normal installed SQL Server on Windows, you don't need Docker. You just need to change the Connection String in `Assignment.Api/appsettings.json` to match your local server name.

## 📋 Prerequisites

To run this, you need:

1. Docker Desktop (If you are running the database in a container like me).
2. .NET 10.0 SDK (Since I used the latest version).
3. Azure Data Studio (To check the data tables).

## 🐳 Step 1: Start the Database

I made a `docker-compose.yml` file to make it easy to start the database.

1. Open the terminal in the project folder.
2. Run this command:
```
docker-compose up -d
```

This starts the SQL Server.

* User: `sa`
* Password: `Assignment123$`
* Database: `AssignmentDb`

**Note:** I know that database passwords should not be shared in production or committed to version control. This password is included here only for the assignment submission so you can run and test the project easily.

## 🗄️ Step 2: Create the Tables

Now we need to create the tables in the database.

Run these commands:
```
# Install the tool if you don't have it
dotnet tool install --global dotnet-ef

# Update the database
dotnet ef database update --project Assignment.Data --startup-project Assignment.Api
```

## 🚀 Step 3: Run the API

Now you can start the app:
```
dotnet run --project Assignment.Api
```

Open this link to test the API:

* Swagger UI: http://localhost:5279/swagger

## 🧪 How to Test

There are two main things to test.

### Flow A: Register a New User

1. Register (`POST /register`) -> Enter Name, IC (12 numbers), Mobile (8 numbers).
2. Verify Phone (`POST /verify-phone`) -> Use the OTP you get from step 1.
3. Verify Email (`POST /verify-email`) -> Use the OTP you get from step 1.
4. Accept Privacy (`POST /accept-privacy`).
5. Set PIN (`POST /set-pin`) -> Must be 6 numbers.
6. Biometrics (`POST /set-biometric`).

### Flow B: Migrate Existing User

1. Start Migration (`POST /api/RegisteredUser/migrate`)
   * Enter the IC Number of a user who already exists.
   * The system will reset their data (like PIN and checks) and give a new OTP.
2. Finish Setup:
   * Go back to Flow A steps (Verify Phone, Verify Email, Set PIN, etc.) to set up the account again.

## 📂 Project Structure

* `Assignment.Api`: The main API project with Controllers.
* `Assignment.Services`: The logic code (Interfaces and Services).
* `Assignment.Data`: The Database code (Tables and Migrations).