INSERT INTO Autori (Nume, Prenume, TaraOrigine) VALUES
('Rebreanu', 'Liviu', 'România'),
('Eminescu', 'Mihai', 'România'),
('Bacovia', 'George', 'România'),
('Reynolds', 'Anthony', 'SUA'),
('Sorescu', 'Marin', 'România');
INSERT INTO Carti (Titlu, AnPublicare, Gen, AutorID) VALUES
('Ion', 1920, 'Roman', 1),
('Luceafărul', 1883, 'Poezie', 2),
('Portret de iarnă', 1930, 'Poezie', 3),
('Ruination', 2022, 'Fantasy', 4),
('Iona', 1968, 'Teatru', 5);
INSERT INTO Membri (Nume, Prenume, DataInscriere, Email, Telefon) VALUES
('Popescu', 'Ana', '2023-01-10', 'ana.popescu@email.com', '0721123456'),
('Ionescu', 'Mihai', '2023-03-22', 'mihai.ionescu@email.com', '0721987654'),
('Georgescu', 'Elena', '2023-05-05', 'elena.g@email.com', '0734567890'),
('Vasilescu', 'Ion', '2023-06-15', 'vasilescu.ion@email.com', '0712345678'),
('Dumitrescu', 'Maria', '2023-09-10', 'maria.d@email.com', '0700112233');
INSERT INTO Imprumuturi (MembruID, CarteID, DataImprumut, DataScadenta) VALUES
(1, 1, '2024-01-10', '2024-01-24'),
(2, 3, '2024-02-01', '2024-02-15'),
(3, 2, '2024-03-10', '2024-03-24'),
(4, 5, '2024-04-12', '2024-04-26'),
(5, 4, '2024-05-01', '2024-05-15');
INSERT INTO Returnari (ImprumutID, DataReturnare, Observatii) VALUES
(1, '2024-01-22', 'Returnat în stare bună'),
(2, '2024-02-16', 'Ușor deteriorată coperta'),
(3, '2024-03-22', 'Returnat cu întârziere'),
(4, '2024-04-25', 'Ok'),
(5, '2024-05-14', 'Returnat fără observații');
