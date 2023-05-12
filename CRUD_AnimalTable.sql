-- Insert script for Animal table
INSERT INTO Animal (Name, Breed, BirthDate, Sex, Price, Status)
VALUES ('Name1', 'Breed1', '2000-01-01', 'Male', 1000.00, 'Active');

-- Update script for Animal table
UPDATE Animal
SET Price = 1500.00
WHERE AnimalId = 1;

-- Delete script for Animal table
DELETE FROM Animal
WHERE AnimalId = 1;