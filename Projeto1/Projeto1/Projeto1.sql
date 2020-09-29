create database EmprestaArduino;
use EmprestaArduino;

create table Alunos (
ID INT PRIMARY KEY AUTO_INCREMENT,
NOME VARCHAR(100) NOT NULL,
MATRICULA INT NOT NULL
);

create table Equipamentos (
ID INT PRIMARY KEY AUTO_INCREMENT,
DENOMINACAO VARCHAR(200) NOT NULL,
CUSTO DECIMAL(10,2) NOT NULL
);

create table equip_saldo (
ID INT PRIMARY KEY AUTO_INCREMENT,
ID_EQUIPAMENTO INT NOT NULL,
SALDO INT NOT NULL
);

create table reg_mov (
ID INT PRIMARY KEY AUTO_INCREMENT,
ID_ALUNO INT NOT NULL,
ID_EQUIPAMENTO INT NOT NULL,
DATA_ENTREGA DATETIME NOT NULL,
DATA_DEVOLUCAO DATETIME,
FOREIGN KEY (ID_ALUNO) REFERENCES Alunos(ID),
FOREIGN KEY (ID_EQUIPAMENTO) REFERENCES Equipamentos(ID)
);