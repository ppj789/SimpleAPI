INSERT INTO Users (Username, Email, Password, ApiKey)
SELECT 'admin', 'admin@example.com', 'admin', 'admin'
WHERE NOT EXISTS (SELECT 1 FROM Users WHERE id = 1);

INSERT INTO TaskItems (Title, Description, AssigneeId, DueDate)
SELECT 'Test Task', 'This is a test task for user assignment', 1, '2025-02-02 10:00:00'
WHERE NOT EXISTS (SELECT 1 FROM TaskItems WHERE id = 1);