CREATE PROCEDURE GetSituatieImprumuturi
AS
BEGIN
    SELECT
        m.MembruID,
        m.Nume,
        m.Prenume,
        c.Titlu AS Carte,
        i.DataImprumut,
        i.DataScadenta,
        r.DataReturnare,
        r.Observatii
    FROM Membri m
    LEFT JOIN Imprumuturi i ON m.MembruID = i.MembruID
    LEFT JOIN Carti c ON i.CarteID = c.CarteID
    LEFT JOIN Returnari r ON i.ImprumutID = r.ImprumutID
END;

EXEC GetSituatieImprumuturi;