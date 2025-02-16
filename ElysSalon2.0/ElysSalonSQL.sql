IF EXISTS (SELECT name FROM sys.databases WHERE name = 'elysalondb')
    DROP DATABASE elysalondb;
GO

CREATE DATABASE elysalondb;
GO
USE elysalondb;
GO

IF OBJECT_ID('dbo.bill_details', 'U') IS NOT NULL DROP TABLE dbo.bill_details;
IF OBJECT_ID('dbo.bill', 'U') IS NOT NULL DROP TABLE dbo.bill;
IF OBJECT_ID('dbo.article', 'U') IS NOT NULL DROP TABLE dbo.article;
IF OBJECT_ID('dbo.article_type', 'U') IS NOT NULL DROP TABLE dbo.article_type;
GO

CREATE TABLE article_type (
    article_type_id INT IDENTITY(1,1) PRIMARY KEY,
    article_type_name VARCHAR(50) NOT NULL
);


CREATE TABLE article (
    article_id INT IDENTITY(1,1) PRIMARY KEY,
    article_name VARCHAR(50) NOT NULL,
    article_type_id INT NOT NULL,
    price_cost DECIMAL(10,2) NOT NULL,
    price_buy DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    description VARCHAR(100) NULL,
    CONSTRAINT FK_article_article_type FOREIGN KEY (article_type_id) 
    REFERENCES article_type (article_type_id) 
    ON DELETE CASCADE
);

CREATE TABLE bill (
    bill_id VARCHAR(8) PRIMARY KEY,
    emission_date DATETIME NOT NULL,
    amount_with_tax DECIMAL(10,2) NOT NULL,
    amount_without_tax DECIMAL(10,2) NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL
);

CREATE TABLE bill_details (
    bill_details_id INT IDENTITY(1,1) PRIMARY KEY,
    bill_id VARCHAR(8) NOT NULL,
    article_id INT NOT NULL,
    quantity INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_bill_details_bill FOREIGN KEY (bill_id) 
    REFERENCES bill (bill_id) 
    ON DELETE CASCADE,
    CONSTRAINT FK_bill_details_article FOREIGN KEY (article_id) 
    REFERENCES article (article_id)
);
GO

INSERT INTO article_type (article_type_name) VALUES 
('Shampoo'),
('Acondicionador'),
('Gel para cabello'),
('Spray fijador'),
('Tratamiento capilar'),
('Herramientas de estilizado');

INSERT INTO article (article_name, article_type_id, price_cost, price_buy, stock, description) VALUES
('Shampoo Hidratante', 1, 5.00, 10.00, 50, 'Shampoo con aloe vera para hidratación intensa'),
('Shampoo Reparador', 1, 6.50, 12.00, 40, 'Shampoo con keratina para cabello dañado'),
('Acondicionador Suavizante', 2, 6.00, 11.50, 35, 'Acondicionador con aceites esenciales para suavidad'),
('Acondicionador Voluminizador', 2, 6.80, 12.30, 30, 'Acondicionador con biotina para más volumen'),
('Gel Extra Fuerte', 3, 3.75, 8.00, 30, 'Gel fijador con alta resistencia y sin residuos'),
('Gel Efecto Húmedo', 3, 4.00, 9.00, 25, 'Gel que da un look mojado con duración prolongada'),
('Spray Fijador Máxima Fijación', 4, 7.50, 14.00, 20, 'Spray que mantiene el peinado por 24 horas'),
('Spray Termoprotector', 4, 8.00, 15.00, 25, 'Protege el cabello del calor de planchas y secadores'),
('Mascarilla Nutritiva', 5, 9.00, 18.00, 15, 'Tratamiento profundo con proteínas para cabello seco'),
('Tratamiento Capilar Anticaída', 5, 10.50, 20.00, 18, 'Fortalece el cabello y reduce la caída'),
('Plancha para el Cabello', 6, 20.00, 40.00, 10, 'Plancha profesional con placas de cerámica'),
('Secador de Cabello Iónico', 6, 25.00, 50.00, 8, 'Secador con tecnología iónica para reducir frizz'),
('Cepillo Desenredante', 6, 3.00, 6.50, 25, 'Cepillo especial para desenredar sin dolor'),
('Tijeras de Precisión', 6, 8.50, 17.00, 12, 'Tijeras para cortes detallados de cabello'),
('Rizador Automático', 6, 30.00, 60.00, 5, 'Rizador con tecnología automática para ondas perfectas'),
('Shampoo Matizante', 1, 7.20, 14.50, 28, 'Neutraliza tonos amarillos en cabello rubio o decolorado'),
('Acondicionador Hidratante', 2, 6.30, 12.20, 30, 'Hidrata profundamente y aporta brillo'),
('Gel Modelador', 3, 3.50, 7.50, 40, 'Gel flexible para definir rizos y peinados'),
('Spray Voluminizador', 4, 9.00, 18.00, 22, 'Aporta volumen sin dejar residuos'),
('Mascarilla Restauradora', 5, 10.00, 19.50, 18, 'Restaura cabello dañado por tintes y calor');

 SELECT * FROM article_type;
SELECT * FROM article;
SELECT * FROM bill;
SELECT * FROM bill_details;
