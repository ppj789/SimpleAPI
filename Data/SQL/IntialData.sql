INSERT INTO Users (Username, Email, Password, ApiKey)
SELECT 'admin', 'admin@example.com', 'admin', 'admin'
WHERE NOT EXISTS (SELECT 1 FROM Users WHERE id = 1);