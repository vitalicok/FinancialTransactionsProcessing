IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'Transactions' AND TABLE_SCHEMA = 'dbo'
)
BEGIN
    CREATE TABLE dbo.Transactions (
        TransactionId UNIQUEIDENTIFIER  PRIMARY KEY,
        UserId UNIQUEIDENTIFIER  NOT NULL,          
        Date DATETIME NOT NULL,                    
        Amount DECIMAL(18, 2) NOT NULL,            
        Category NVARCHAR(100) NOT NULL,           
        Description NVARCHAR(MAX) NULL,           
        Merchant NVARCHAR(200) NULL     
       );
END