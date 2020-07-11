CREATE DATABASE db_niceface
USE db_niceface

-- criar novo usuário
CREATE USER 'niceface'@'localhost' IDENTIFIED BY '1234567';

-- dar permissão a esse usuário a mexer no banco db_ead
GRANT ALL PRIVILEGES ON db_niceface.* TO 'niceface'@'localhost';
FLUSH PRIVILEGES;


CREATE TABLE funcionario(
id_funcionario int NOT NULL AUTO_INCREMENT,
nome varchar(60) NOT NULL,
cargo varchar(30) NOT NULL,
telefone varchar(11),
cpf varchar(11) NOT NULL UNIQUE,
email varchar(60),
rua varchar(80) NOT NULL,
numero int NOT NULL,
bairro varchar(30) NOT NULL,
cidade varchar(30) NOT NULL,
cep varchar(9) NOT NULL,
PRIMARY KEY (id_funcionario)
)

select * from funcionario

insert into funcionario (nome, cargo, telefone, cpf, email, rua, numero, bairro, cidade, cep) 
	values('João da Silva', 'Gerente', '11985784930', '37485837284', 'joao@gmail.com', 'Rua das Casas', 42, 'Vila Leopoldina', 'São Paulo-SP', '03232-302')


CREATE TABLE login(
id_login int NOT NULL AUTO_INCREMENT,
id_funcionario int NOT NULL,
usuario varchar(30) NOT NULL,
senha varchar(30) NOT NULL,
PRIMARY KEY (id_login),
FOREIGN KEY (id_funcionario) REFERENCES funcionario(id_funcionario)
)

select * from login

insert into login (id_funcionario, usuario, senha) values (1, 'gerente', '1234567')


CREATE TABLE cliente(
id_cliente int NOT NULL AUTO_INCREMENT,
nome varchar(60) NOT NULL,
sexo varchar(10) NOT NULL,
cpf varchar(11) NOT NULL UNIQUE,
data_nascimento datetime NOT NULL,
rua varchar(80) NOT NULL,
numero int NOT NULL,
bairro varchar(30) NOT NULL,
cidade varchar(30) NOT NULL,
cep varchar(9) NOT NULL,
email varchar(60),
telefone varchar(11) NOT NULL,
profissao varchar(30),
historico varchar(500),
PRIMARY KEY (id_cliente)
)

select * from cliente

insert into cliente (nome, sexo, cpf, data_nascimento, rua, numero, bairro, cidade, cep, email, telefone, profissao, historico) 
	values ('Joaquina Souza', 'feminino', '12312312312', '1980-10-23', 'Rua da Cidade', 100, 'Jardim das Laranjeiras', 'São Paulo-SP', '04594-969', 'joaquina@gmail.com', '11958374757', null, null)



CREATE TABLE procedimento(
id_procedimento int NOT NULL AUTO_INCREMENT,
nome varchar(30) NOT NULL UNIQUE,
descricao varchar(100) NOT NULL,
tempo_estimado varchar(5) NOT NULL,
valor_proc numeric(7,2) NOT NULL,
PRIMARY KEY (id_procedimento)
)

select * from procedimento

insert into procedimento (nome, descricao, tempo_estimado, valor_proc)
	values ('Botox facial', 'Aplicação de botox através de injeções', '00:30', '100.00')
    
    

CREATE TABLE sessao(
id_sessao int NOT NULL AUTO_INCREMENT,
id_cliente int NOT NULL,
id_procedimento int NOT NULL,
id_funcionario int NOT NULL,
descricao varchar(100),
quantidade int NOT NULL,
PRIMARY KEY (id_sessao),
FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente),
FOREIGN KEY (id_procedimento) REFERENCES procedimento(id_procedimento),
FOREIGN KEY (id_funcionario) REFERENCES funcionario(id_funcionario)
)

select * from sessao

insert into sessao (id_cliente, id_procedimento, id_funcionario, descricao, quantidade)
	values (1, 1, 1, null, 4)
    
    

CREATE TABLE agenda(
id_agenda int NOT NULL AUTO_INCREMENT,
data_hora datetime NOT NULL,
estado varchar(10) NOT NULL,
agend_pago char(1) NOT NULL,
id_sessao int NOT NULL,
PRIMARY KEY (id_agenda),
FOREIGN KEY (id_sessao) REFERENCES sessao(id_sessao)
)

select * from agenda



CREATE TABLE avaliacao_diagnostica(
id_avaliacao int NOT NULL AUTO_INCREMENT,
data_hora datetime NOT NULL,
obs_cliente varchar(200),
id_procedimento int NOT NULL,
id_cliente int NOT NULL,
PRIMARY KEY (id_avaliacao),
FOREIGN KEY (id_procedimento) REFERENCES procedimento(id_procedimento),
FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
)

select * from avaliacao_diagnostica



CREATE TABLE pagamento(
id_pagamento int NOT NULL AUTO_INCREMENT,
tipo_pagamento varchar(10) NOT NULL,
quant_parcelas int NOT NULL,
valor_total numeric(8,2) NOT NULL,
id_sessao int NOT NULL,
PRIMARY KEY (id_pagamento),
FOREIGN KEY (id_sessao) REFERENCES sessao(id_sessao)
)

select * from pagamento

