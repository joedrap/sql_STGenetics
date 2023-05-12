DECLARE @i INT = 0
WHILE @i < 5000
BEGIN
    INSERT INTO Animal (Name, Breed, BirthDate, Sex, Price, Status)
    VALUES ('Name' + CAST(@i AS VARCHAR), 'Breed' + CAST(@i AS VARCHAR), '2000-01-01', 'Male', 1000.00, 'Active');
    SET @i = @i + 1
END