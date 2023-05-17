USE STGenetics

SELECT *
FROM Animal
WHERE Sex = 'Female' AND BirthDate < DATEADD(YEAR, -2, GETDATE())
ORDER BY Name ASC;