select * from cita where Fecha = CONVERT(date,GETDATE()) order by Hora_Inicio;


select * from usuario;
select * from cita;
select * from cita_usuario;

insert into cita (UsuarioId, Fecha, Matricula,Hora_Inicio, Hora_Final,Tipo,Estatus,Descripcion)
values (3 ,'2022-05-21','15760547',07.30,8.30,'Grupal','Pendiente','test2');

alter table cita Drop CONSTRAINT FK__cita__UsuarioId__267ABA7A;

alter table cita Drop Column UsuarioId;

EXEC sp_help cita;


create table cita_usuario (
	citaUsuarioId int IDENTITY(1,1) Primary key,
	UsuarioId int  FOREIGN KEY REFERENCES Usuario(UsuarioId),
	CitaId int FOREIGN KEY REFERENCES Cita(CitaId)
)



insert into cita_usuario (UsuarioId,CitaId)
values (3,1);


