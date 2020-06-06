CREATE DATABASE clinica_teste
USE clinica_teste

CREATE TABLE cliente(
id int NOT NULL AUTO_INCREMENT,
nome varchar(50) NOT NULL,
sexo varchar(10) NOT NULL,
cpf varchar(11) NOT NULL,
data_nascimento datetime NOT NULL,
endereco varchar(60) NOT NULL,
email varchar(50),
profissao varchar(20),
historico varchar(500),
PRIMARY KEY (id)
)

select * from cliente

insert into cliente values (null, "ana", "feminino", 12121212, "1988-06-30", "rua nenhuma", "ana@ana.com", null, null)

-- criar novo usuário
CREATE USER 'niceface'@'localhost' IDENTIFIED WITH mysql_native_password BY '1234567';

-- dar permissão a esse usuário a mexer no banco db_ead
GRANT ALL PRIVILEGES ON clinica_teste.* TO 'niceface'@'localhost' WITH GRANT OPTION;