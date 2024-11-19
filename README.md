# Activity Manager API

**Activity Manager API** allows users to manage their activities. This API enables users to create, track, and assign activities. It also supports administrators in assigning specific activities to other users.

## Features

- User management
- Create, update, and delete activities
- Assign activities to users
- Track activity completion status

## Technologies

- **.NET 8.0: For backend development.
- **Entity Framework Core**: For database management and ORM operations.
- **SQL Server**: As the database.
- **JWT Authentication**: For secure API access.
- **AutoMapper**: For mapping DTOs to models.
- **Swagger**: For API documentation and testing.

## Setup

### Requirements

- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Steps

1. **Clone the project**:

    ```bash
    git clone https://github.com/yourusername/activity-manager-api.git
    cd activity-manager-api
    ```

2. **Install dependencies**:

    ```bash
    dotnet restore
    ```

3. **Configure the database connection string**:

    Open the `appsettings.json` file and update the connection string:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=ActivityManager;User Id=sa;Password=yourpassword;"
    }
    ```

4. **Apply database migrations**:

    ```bash
    dotnet ef database update
    ```

5. **Run the project**:

    ```bash
    dotnet run
    ```

    The API will run by default on `https://localhost:5001`.

### Testing the API

- **Swagger UI**: Access the API documentation and testing tools at `https://localhost:5001/swagger`.

## API Endpoints

### Users

- **GET /api/users**: Get all users
- **GET /api/users/{id}**: Get a specific user by ID
- **POST /api/users**: Create a new user
- **PUT /api/users/{id}**: Update an existing user
- **DELETE /api/users/{id}**: Delete a user

### Activities

- **GET /api/activities**: Get all activities
- **GET /api/activities/{id}**: Get a specific activity by ID
- **POST /api/activities**: Create a new activity
- **PUT /api/activities/{id}**: Update an existing activity
- **DELETE /api/activities/{id}**: Delete an activity

### Activity Assignments

- **POST /api/userActivities**: Assign an activity to a user
- **GET /api/userActivities/{userId}/{activityId}**: Get the relationship between a user and an activity
- **PUT /api/userActivities/{userId}/{activityId}**: Update the activity status for a user
- **DELETE /api/userActivities/{userId}/{activityId}**: Remove an activity from a user

## Authentication

This API uses **JWT (JSON Web Token)** for authentication.

- **Getting a token**: Send a **POST** request to **/api/authenticate** with a username and password.
  
    Request body:
    ```json
    {
      "username": "yourusername",
      "password": "yourpassword"
    }
    ```

    Upon successful login, you will receive a token. This token must be included in the **Authorization** header of every request as a `Bearer` token.

## Error Handling

The API returns appropriate HTTP status codes for all errors:

- **200 OK**: Successful request
- **400 Bad Request**: Invalid request
- **401 Unauthorized**: Authentication error
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server error