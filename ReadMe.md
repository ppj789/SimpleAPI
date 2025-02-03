# SimpleAPI
LLM was used in creation of this README

## Overview
SimpleAPI is a lightweight web API built using **ASP.NET Core** with **Entity Framework (EF) Core** and **SQLite** for data persistence. It provides functionality for managing users and task items, with full CRUD operations and authentication via API keys.

## Features
- **User Management**: Create, retrieve, update, and delete users.
- **Task Management**: CRUD operations on task items with support for assignees and due dates.
- **Authentication**: Uses API keys for authorization.
- **Middleware for Error Handling**: Custom error handling for better API responses.
- **AutoMapper Integration**: Converts models to DTOs for clean API responses.
- **Unit Testing**: Comprehensive tests using **xUnit** and **Moq**.

## Technologies Used
- **ASP.NET Core**
- **Entity Framework Core** (with SQLite)
- **AutoMapper**
- **xUnit & Moq** (for unit testing)
- **Swagger** (for API documentation)

---

## API Endpoints
To start please use 'admin' for the API Key

### Users
| Method | Endpoint         | Description              |
|--------|-----------------|--------------------------|
| GET    | `/api/users`     | Get all users           |
| GET    | `/api/users/{id}` | Get user by ID         |
| POST   | `/api/users`     | Create a new user      |
| PUT    | `/api/users/{id}` | Update an existing user |
| DELETE | `/api/users/{id}` | Delete a user          |
| GET | `/api/users/newApiKey/{id}` | Generates a new api key for the user          |

### Task Items
| Method | Endpoint           | Description               |
|--------|-------------------|---------------------------|
| GET    | `/api/tasks`       | Get all tasks            |
| GET    | `/api/tasks/{id}`   | Get task by ID          |
| POST   | `/api/tasks`       | Create a new task       |
| PUT    | `/api/tasks/{id}`   | Update an existing task |
| DELETE | `/api/tasks/{id}`   | Delete a task           |
| GET    | `/api/taskitems/expired`   | Get expired tasks           |
| GET    | `/api/taskitems/active`   | Get active tasks           |
| GET    | `/api/taskitems/fromDate/{date}`   | Get tasks from a specific date           |

### Authentication
Following the guide  https://www.camiloterevinto.com/post/simple-and-secure-api-keys-using-asp-net-core

All requests must include an API key as a header:
```sh
Authorization: Bearer {your-api-key}
```

---

### Models
**User Model**:
```sh
Id
Username
Email
Password
API Key
```

**TaskItem Model**:
```sh
Id
Title
Description
DueDate
Assignee
```

---

## Error Handling
Custom middleware handles errors globally, returning 404 JSON responses:
```json
{
  "message": "Task Item not found."
}
```

---

## Running Tests
Unit tests are written using **xUnit** and **Moq**.


# Further Improvements
## Password
hash the password, and store that

## API Key
Add a login, to verify API key
Expire/Rotate key

## Add roles
Add roles, such as admin can see all, but a user can only see tasks and information about themselves or under them in the hierarchy. 

## Pagenation and filtering
If you have 1000 ItemTasks returned, make it only return a 100 at a time with pages or filtering.





