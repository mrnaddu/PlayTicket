ABP Microservice Template
Description:
This template sets up two microservices using the ABP framework and MySQL database:

CashVoucherService: Handles cash voucher functionality.
UserService: Manages user-related functionality.
MySQL and Redis are run using Docker, and Tye is used to manage and run both microservices. A PowerShell script (playticket.ps1) is provided to automate the installation, migration, and running of the services.

Features:
Two microservices: CashVoucherService (port 7001) and UserService (port 7002).
Docker for MySQL and Redis.
Tye for managing and running the microservices.
PowerShell script for automating tasks.
Getting Started:
Prerequisites:
Docker installed.
.NET 8+.
Tye installed.
PowerShell.

Installation:
Clone the repository:

bash
Copy code
git clone https://github.com/your-username/abp-microservice-template.git
cd abp-microservice-template
Restore NuGet packages for both services:

bash
Copy code
dotnet restore
Run playticket.ps1 Script:

The PowerShell script playticket.ps1 has three commands:

install: Starts Docker containers (MySQL and Redis) and applies database migrations.
run: Runs both microservices using Tye.
migrate: Applies the database migrations.
Usage:
To install (Docker + Migrations):

powershell
Copy code
.\playticket.ps1 install
To run microservices:

powershell
Copy code
.\playticket.ps1 run
To apply migrations only:

Running Swagger UI for Both Microservices:
CashVoucherService: http://localhost:7001/swagger
UserService: http://localhost:7002/swagger
