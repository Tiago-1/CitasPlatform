select * from usuario;


insert into usuario (Nombre,Apellidos,Matricula,Correo,Telefono,Rol)
values('Fernanda','Torres','767267','fernanda@ite.edu.mx','6461791223','1');

ALTER TABLE usuario
ADD Pass VARCHAR (50);

UPDATE usuario 
set Pass = 'psicologa'
where UsuarioId = 1003;



select * from cita 
where Fecha = Convert(date, getdate())  
order by Hora_Inicio;

UPDATE cita
set Fecha = '2022-05-20',
Hora_Inicio = 10.00,
Hora_Final = 11.00
where citaId = 1;

insert into cita (UsuarioId,Fecha, Matricula, Hora_Inicio,Hora_Final,Tipo,Estatus,Descripcion)
values (
3,
'2022-05-24',
'15760547',
13.00,
14.00,
'Grupo',
'Pendiente',
'test'
);