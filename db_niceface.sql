CREATE DATABASE db_niceface_tcc
USE db_niceface_tcc

-- criar novo usuário
CREATE USER 'nicefacetcc'@'localhost' IDENTIFIED BY '1234567';

-- dar permissão a esse usuário a mexer no banco
GRANT ALL PRIVILEGES ON db_niceface_tcc.* TO 'nicefacetcc'@'localhost';
FLUSH PRIVILEGES;


CREATE TABLE funcionario(
id_funcionario int NOT NULL AUTO_INCREMENT,
nome varchar(60) NOT NULL,
cargo varchar(30) NOT NULL,
telefone varchar(11),
cpf varchar(11) NOT NULL UNIQUE,
email varchar(60),
PRIMARY KEY (id_funcionario)
)

select * from funcionario


CREATE TABLE cliente(
id_cliente int NOT NULL AUTO_INCREMENT,
nome varchar(60) NOT NULL,
sexo varchar(10) NOT NULL,
cpf varchar(11) NOT NULL UNIQUE,
data_nascimento datetime NOT NULL,
email varchar(60),
telefone varchar(11) NOT NULL,
profissao varchar(30),
historico varchar(500),
PRIMARY KEY (id_cliente)
)

select * from cliente


CREATE TABLE endereco(
id_endereco int NOT NULL AUTO_INCREMENT,
rua varchar(80) NOT NULL,
numero int NOT NULL,
complemento varchar(50),
bairro varchar(30) NOT NULL,
cidade varchar(30) NOT NULL,
estado varchar(20) NOT NULL,
cep varchar(9) NOT NULL,
id_cliente int,
PRIMARY KEY (id_endereco),
FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
)

select * from endereco


CREATE TABLE login(
id_login int NOT NULL AUTO_INCREMENT,
id_funcionario int,
id_cliente int,
usuario varchar(30) NOT NULL UNIQUE,
senha varchar(30) NOT NULL,
nivel_acesso char(1) NOT NULL,
PRIMARY KEY (id_login),
FOREIGN KEY (id_funcionario) REFERENCES funcionario(id_funcionario),
FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
)

select * from login


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


SELECT count(id_sessao) FROM agenda WHERE id_sessao = 1

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





DELIMITER $$
create procedure cad_cliente(
in nome varchar(60),
in sexo varchar(10),
in cpf_cli varchar(11),
in data_nascimento datetime,
in email varchar(60),
in telefone varchar(11),
in profissao varchar(30),
in historico varchar(500),
in rua varchar(80),
in numero int,
in complemento varchar(50),
in bairro varchar(30),
in cidade varchar(30),
in estado varchar(20),
in cep varchar(9),
in usuario varchar(30),
in senha varchar(30)
)
begin
	insert into cliente (nome, sexo, cpf, data_nascimento, email, telefone, profissao, historico) values
		(nome, sexo, cpf_cli, data_nascimento, email, telefone, profissao, historico);
	set @id_cliente_novo = (select id_cliente from cliente where cpf = cpf_cli);
    insert into endereco (rua, numero, complemento, bairro, cidade, estado, cep, id_cliente) values
		(rua, numero, complemento, bairro, cidade, estado, cep, @id_cliente_novo);
	insert into login (id_cliente, usuario, senha, nivel_acesso) values
		(@id_cliente_novo, usuario, senha, '3');
end$$
DELIMITER ;

call cad_cliente('Joaquina', 'feminino', '33333333333', '1980-10-20', 'joaquina@gmail.com', '11989898989', 'arquiteta', null, 'Rua Dois', 200, null, 'Casa Verde', 'São Paulo', 'SP', '01234123', 'joaquina', '1234567')


DELIMITER $$
create procedure cad_cliente_sem_senha(
in nome varchar(60),
in sexo varchar(10),
in cpf_cli varchar(11),
in data_nascimento datetime,
in email varchar(60),
in telefone varchar(11),
in profissao varchar(30),
in historico varchar(500),
in rua varchar(80),
in numero int,
in complemento varchar(50),
in bairro varchar(30),
in cidade varchar(30),
in estado varchar(20),
in cep varchar(9)
)
begin
	insert into cliente (nome, sexo, cpf, data_nascimento, email, telefone, profissao, historico) values
		(nome, sexo, cpf_cli, data_nascimento, email, telefone, profissao, historico);
	set @id_cliente_novo = (select id_cliente from cliente where cpf = cpf_cli);
    insert into endereco (rua, numero, complemento, bairro, cidade, estado, cep, id_cliente) values
		(rua, numero, complemento, bairro, cidade, estado, cep, @id_cliente_novo);
end$$
DELIMITER ;

call cad_cliente_sem_senha('Maria', 'feminino', '44444444444', '1985-04-20', 'maria@gmail.com', '11787766566', 'professora', null, 'Rua Cinco', 500, null, 'Lapa', 'São Paulo', 'SP', '01234123')



DELIMITER $$
create procedure cad_usuario_cliente(
in id_cli int,
in usuario varchar(30),
in senha varchar(30)
)
begin
	insert into login (id_cliente, usuario, senha, nivel_acesso) values
		(id_cli, usuario, senha, '3');
end$$
DELIMITER ;


call cad_usuario_cliente(3, 'maria', '1234567')



DELIMITER $$
create procedure buscar_cliente_cpf_data_tel(
in cpf_cli varchar(11),
in nasc datetime,
in tel varchar(11)
)
begin
	select * from cliente where cpf = cpf_cli and data_nascimento = nasc and telefone = tel;
end$$
DELIMITER ;

call buscar_cliente_cpf_data_tel('33333333333', '1980-10-21', '11989898977')



DELIMITER $$
create procedure cad_funcionario(
in nome varchar(60),
in cargo varchar(30) ,
in telefone varchar(11),
in cpf_func varchar(11),
in email varchar(60),
in usuario varchar(30),
in senha varchar(30),
in nivel char(1)
)
begin
	insert into funcionario (nome, cargo, telefone, cpf, email) values
		(nome, cargo, telefone, cpf_func, email);
	set @id_func_novo = (select id_funcionario from funcionario where cpf = cpf_func);
	insert into login (id_funcionario, usuario, senha, nivel_acesso) values
		(@id_func_novo, usuario, senha, nivel);
end$$
DELIMITER ;


call cad_funcionario('Ana Rizzi', 'gerente', '11912345678', '11111111111', 'ana@gmail.com', 'ana', '1234567', '1')
call cad_funcionario('Roberto', 'atendente', '11987654321', '22222222222', 'roberto@gmail.com', 'roberto', '1234567', '2')


-- para ver se o usuário existe antes do cadastro:
DELIMITER $$
create procedure buscar_usuario(
in usuario_digitado varchar(30)
)
begin
	select usuario from login where usuario = usuario_digitado;
end$$
DELIMITER ;

call buscar_usuario('a')


-- para ver se o cpf já existe antes do cadastro:
DELIMITER $$
create procedure buscar_cpf_cliente(
in cpf_digitado varchar(11)
)
begin
	select cpf from cliente where cpf = cpf_digitado;
end$$
DELIMITER ;

call buscar_cpf_cliente('33333333333')


DELIMITER $$
create procedure buscar_cpf_funcionario(
in cpf_digitado varchar(11)
)
begin
	select cpf from funcionario where cpf = cpf_digitado;
end$$
DELIMITER ;

call buscar_cpf_funcionario('11111111111')



-- exibir cliente junto com endereco:
create view cliente_completo as
select c.id_cliente, c.nome, c.sexo, c.cpf, c.data_nascimento, c.email, c.telefone, c.profissao, c.historico, 
	e.rua, e.numero, e.complemento, e.bairro, e.cidade, e.estado, e.cep
from cliente c
inner join endereco e on c.id_cliente = e.id_cliente

select * from cliente_completo



DELIMITER $$
create procedure atualizar_cliente(
in id_cli int,
in nome varchar(60),
in sexo varchar(10),
in data_nascimento datetime,
in email varchar(60),
in telefone varchar(11),
in profissao varchar(30),
in historico varchar(500),
in rua varchar(80),
in numero int,
in complemento varchar(50),
in bairro varchar(30),
in cidade varchar(30),
in estado varchar(20),
in cep varchar(9)
)
begin
	update cliente set 
		nome = nome,
        sexo = sexo,
        data_nascimento = data_nascimento,
        email = email,
        telefone = telefone,
        profissao = profissao,
        historico = historico
	where id_cliente = id_cli;
    set @id_end = (select id_endereco from endereco where id_cliente = id_cli);
    update endereco set
		rua = rua,
        numero = numero,
        complemento = complemento,
        bairro = bairro,
        cidade = cidade,
        estado = estado,
        cep = cep
	where id_endereco = @id_end;
end$$
DELIMITER ;

call atualizar_cliente(1, 'Joaquina', 'feminino', '1980-10-21', 'joaquina@gmail.com', '11989898977', 'arquiteta', null, 'Rua Dois', 300, null, 'Casa Verde', 'São Paulo', 'SP', '01234123')


DELIMITER $$
create procedure atualizar_funcionario(
in id_func int,
in nome varchar(60),
in cargo varchar(30) ,
in telefone varchar(11),
in email varchar(60)
)
begin
	update funcionario set 
		nome = nome,
        cargo = cargo,
        telefone = telefone,
        email = email
	where id_funcionario = id_func;
end$$
DELIMITER ;

call atualizar_funcionario(1, 'Ana Rizzi', 'presidente', '11912345678', 'ana@gmail.com')


DELIMITER $$
create procedure excluir_cliente(
in id_cli int
)
begin
	delete from login where id_cliente = id_cli;
	delete from endereco where id_cliente = id_cli;
	delete from cliente where id_cliente = id_cli;    
end$$
DELIMITER ;

call excluir_cliente(2);


DELIMITER $$
create procedure excluir_funcionario(
in id_func int
)
begin
	delete from login where id_funcionario = id_func;
	delete from funcionario where id_funcionario = id_func;    
end$$
DELIMITER ;

call excluir_funcionario(5);


-- atualizar senha do login:
DELIMITER $$
create procedure atualizar_senha(
in usuario_digitado varchar(30),
in senha_antiga varchar(30),
in senha_nova varchar(30)
)
begin
	update login set 
		senha = senha_nova
	where usuario = usuario_digitado and senha = senha_antiga;
end$$
DELIMITER ;

call atualizar_senha('ana', '123456', '1234567')

select * from login



-- buscar o usuario e senha para o login:
DELIMITER $$
create procedure buscar_usuario_senha(
in usuario_digitado varchar(30),
in senha_digitada varchar(30),
in tipo varchar(15)
)
begin
	if tipo = 'cliente' then
    	select l.usuario, c.id_cliente, c.nome, l.nivel_acesso
        from login l
        inner join cliente c on l.id_cliente = c.id_cliente
        where l.usuario = usuario_digitado and l.senha = senha_digitada and l.id_funcionario is null;
	else
		select l.usuario, f.id_funcionario, f.nome, l.nivel_acesso
        from login l
        inner join funcionario f on l.id_funcionario = f.id_funcionario
        where l.usuario = usuario_digitado and l.senha = senha_digitada and l.id_cliente is null;
	end if;
end$$
DELIMITER ;


call buscar_usuario_senha('ana', '1234567', 'funcionario');




create view sessao_completa as
select sessao.id_sessao, cliente.id_cliente, cliente.nome cliente, cliente.cpf, procedimento.id_procedimento, procedimento.nome procedimento, funcionario.id_funcionario, funcionario.nome esteticista, sessao.descricao, sessao.quantidade, count(agenda.id_sessao) agendadas
	from sessao
    inner join cliente on sessao.id_cliente = cliente.id_cliente
    inner join procedimento on sessao.id_procedimento = procedimento.id_procedimento
    inner join funcionario on sessao.id_funcionario = funcionario.id_funcionario
    left join agenda on sessao.id_sessao = agenda.id_sessao
    group by sessao.id_sessao
    
select * from sessao_completa



create view agenda_completa as
select agenda.id_agenda, agenda.data_hora, agenda.estado, agenda.agend_pago, agenda.id_sessao, cliente.id_cliente, cliente.nome cliente, cliente.cpf, procedimento.id_procedimento, procedimento.nome procedimento, funcionario.id_funcionario, funcionario.nome esteticista
	from agenda
    inner join sessao on agenda.id_sessao = sessao.id_sessao
    inner join cliente on sessao.id_cliente = cliente.id_cliente
    inner join procedimento on sessao.id_procedimento = procedimento.id_procedimento
    inner join funcionario on sessao.id_funcionario = funcionario.id_funcionario
    order by agenda.data_hora

select * from agenda_completa



create view avaliacao_completa as
select av.id_avaliacao, av.data_hora, av.obs_cliente, av.id_procedimento, pr.nome procedimento, av.id_cliente, cl.nome cliente, cl.cpf
	from avaliacao_diagnostica av
    inner join procedimento pr on av.id_procedimento = pr.id_procedimento
    inner join cliente cl on av.id_cliente = cl.id_cliente
    order by data_hora

select * from avaliacao_completa

