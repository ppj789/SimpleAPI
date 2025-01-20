TODO:

1, just stick to API key (misread the question)
2, implement a controller -> service -> repository services 
3. add testing



Assignment
**Part 1: API Development**

1. Create a new ASP.NET Core Web API project.
2. Define the following models:
- User: ID, Username, Email, Password
- Task: ID, Title, Description, Assignee (UserID), DueDate
3. Implement controllers and endpoints for CRUD operations for both users and tasks.

**Part 2: Authentication**

1. Implement authentication for the API using either API key or bearer token.
2. For API key authentication:
- Generate a unique API key for each user.
- Require API key for accessing API endpoints.
3. For bearer token authentication:
- Implement JWT (JSON Web Tokens) authentication.
- Generate and validate JWT tokens for users.
- 
**Part 3: Database Interaction**

1. Choose either Entity Framework Core or Dapper for database interaction.
2. Set up a local database (SQL Server, SQLite, etc.) for storing users and tasks information.
3. Implement repository patterns to interact with the database for CRUD operations.

**Part 4: Filtering**

1. On the Tasks, filtering on due dates should be possible:
a. Get all expired tasks.
b. Get all active tasks.
c. Get all tasks from a certain date.

**Part 5: Testing**

1. Write unit tests for the API controllers and repository methods.
2. Use an appropriate testing framework (e.g., MSTest, NUnit, xUnit) for writing tests.
**Part 6: Swagger Documentation**
1. Integrate Swagger to generate API documentation.
2. Ensure that all API endpoints are documented and properly described.

**Part 7: Submission**

Provide a GitHub link (public repository) of the completed test.

Zip files and google drive links will not be accepted.