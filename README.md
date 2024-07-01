# Banking

This project contains an API for Baking Company

## Set up your development environment

### Dependencies

This project requires you to have the following installed on your machine.

- Docker
- dotnet core cli

### Notes for Windows

If you have issues getting Docker for windows running, checkout these docs:

- https://docs.docker.com/docker-for-windows/troubleshoot/
- https://docs.docker.com/docker-for-windows/troubleshoot/#virtualization

### Setup

1. Extract the Banking.zip to a folder

### Starting the project

If you are using OSX or a linux based operating system, you can use the Makefile to run `make` in the terminal. This will spin up a PostgreSQL docker container, start the .NET api.

If you are not using a linux/unix based operating system (ex: Windows/DOS), you should be able to use the following commands to start the app.

- Navigate to [`Banking`] folder
- Run `docker-compose up -d` in powershell or git bash. This will start a PostgreSQL database and API server using docker.

You will know everything is running correctly when you go to https://localhost:44340/swagger/index.html in your browser, and see a swagger page.

## Development

We recommend below development tools:
- Visual Studio 2022 for Backend developmentt

Please select "docker-compose" project as startup while debugging the code in the Visual Studio 2022

This Banking API has the below features:

- User can create a bank account.
- User Can login to a bank account.
- User Can see his account details.
- User can transfer money to another bank account.
- User can see his transaction history.
- User can update his contact details.

If you get stuck getting your development environment setup, reach out to er.sahilmehta89@gmail.com


To handle the below considerations, various steps can be taken as explained below:
-	Mobile app internet connectivity can be flaky
-	Data volume could be very large

Step 1: Paginaion: It is essential to handle large datasets using pagination. It involves fetching and returning data in chunks rather than loading everything at once. For example, the API to get all transactions for a given account-number, is configured to return only a fixed number of records (i.e pageSize = 10).

Step 2: Use Retry Logic: Polly is a .NET resilience and transient-fault-handling library that provides various resilience strategies such as retries, circuit breakers, and fallback. In case 

Step 3: Implement Circuit Breaker: A circuit breaker can be used to stop making requests for a certain period if the system detects frequent failures, thus giving the service time to recover.

Step 4: Implement transactions at database level.