USE STGenetics

SELECT Sex, COUNT(*) AS Quantity
FROM Animal
GROUP BY Sex;