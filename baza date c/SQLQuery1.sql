-- 1. Tabela Autori
CREATE TABLE Autori (
    AutorID INT PRIMARY KEY IDENTITY(1,1),
    Nume VARCHAR(100) NOT NULL,
    Prenume VARCHAR(100),
    TaraOrigine VARCHAR(100)
);

-- 2. Tabela Carti
CREATE TABLE Carti (
    CarteID INT PRIMARY KEY IDENTITY(1,1),
    Titlu VARCHAR(255) NOT NULL,
    AnPublicare INT,
    Gen VARCHAR(100),
    AutorID INT,
    FOREIGN KEY (AutorID) REFERENCES Autori(AutorID)
);

-- 3. Tabela Membri
CREATE TABLE Membri (
    MembruID INT PRIMARY KEY IDENTITY(1,1),
    Nume VARCHAR(100) NOT NULL,
    Prenume VARCHAR(100),
    DataInscriere DATE,
    Email VARCHAR(150),
    Telefon VARCHAR(20)
);

-- 4. Tabela Imprumuturi
CREATE TABLE Imprumuturi (
    ImprumutID INT PRIMARY KEY IDENTITY(1,1),
    MembruID INT,
    CarteID INT,
    DataImprumut DATE NOT NULL,
    DataScadenta DATE,
    FOREIGN KEY (MembruID) REFERENCES Membri(MembruID),
    FOREIGN KEY (CarteID) REFERENCES Carti(CarteID)
);

-- 5. Tabela Returnari
CREATE TABLE Returnari (
    ReturnareID INT PRIMARY KEY IDENTITY(1,1),
    ImprumutID INT,
    DataReturnare DATE NOT NULL,
    Observatii TEXT,
    FOREIGN KEY (ImprumutID) REFERENCES Imprumuturi(ImprumutID)
);
