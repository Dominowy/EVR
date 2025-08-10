
-- 1. Lista pracowników z zespołu ".NET" z urlopem w 2019
SELECT DISTINCT e.*
FROM Employee e
JOIN Team t ON e.TeamId = t.Id
JOIN Vacation v ON e.Id = v.EmployeeId
WHERE t.Name = '.NET'
  AND (YEAR(v.DateSince) = 2019 OR YEAR(v.DateUntil) = 2019);


-- 2. Lista pracowników z liczbą dni urlopowych zużytych w bieżącym roku
DECLARE @CurrentYear INT = YEAR(GETDATE());
DECLARE @Today DATE = CAST(GETDATE() AS DATE);

SELECT e.*, 
       ISNULL(SUM(
         CASE 
           WHEN YEAR(v.DateSince) = @CurrentYear OR YEAR(v.DateUntil) = @CurrentYear
                AND v.DateUntil < @Today 
           THEN DATEDIFF(DAY, v.DateSince, v.DateUntil) + 1
           ELSE 0
         END), 0) AS DaysUsed
FROM Employee e
LEFT JOIN Vacation v ON e.Id = v.EmployeeId
GROUP BY e.Id, e.Name, e.TeamId, e.PositionId, e.VacationPackageId


-- 3. Lista zespołów, w których pracownicy nie mieli żadnego urlopu w 2019
SELECT t.*
FROM Team t
WHERE NOT EXISTS (
    SELECT 1
    FROM Employee e
    JOIN Vacation v ON e.Id = v.EmployeeId
    WHERE e.TeamId = t.Id
      AND (YEAR(v.DateSince) = 2019 OR YEAR(v.DateUntil) = 2019)
);
