-- Generado por Oracle SQL Developer Data Modeler 23.1.0.087.0806
--   en:        2023-09-07 16:45:17 CST
--   sitio:      SQL Server 2012
--   tipo:      SQL Server 2012

USE [master]
GO

CREATE DATABASE [NCQ_Tareas2]
GO

use [NCQ_Tareas2] 
GO


CREATE TABLE colaborador 
    (
     Id INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     NombreCompleto VARCHAR (100) NOT NULL 
    )
GO 

EXEC sp_addextendedproperty 'MS_Description' , 'Tabla de registro de colaboradores que serán asignados a las tareas' , 'USER' , 'dbo' , 'TABLE' , 'colaborador' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Id de la tabla' , 'USER' , 'dbo' , 'TABLE' , 'colaborador' , 'COLUMN' , 'Id' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Nombre y apellidos  del colaborador' , 'USER' , 'dbo' , 'TABLE' , 'colaborador' , 'COLUMN' , 'NombreCompleto' 
GO

ALTER TABLE colaborador ADD CONSTRAINT colaborador_PK PRIMARY KEY CLUSTERED (Id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE tarea 
    (
     Id INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     Descripcion VARCHAR (300) NOT NULL , 
     ColaboradorId INTEGER , 
     Estado TINYINT NOT NULL DEFAULT 1 , 
     Prioridad TINYINT NOT NULL , 
     FechaInicio DATE NOT NULL , 
     FechaFin DATE NOT NULL , 
     Notas VARCHAR (1000) 
    )
GO 


EXEC sp_addextendedproperty 'MS_Description' , 'Tabla para administrar tareas sencillas que serán asignadas a un grupo de personas.' , 'USER' , 'dbo' , 'TABLE' , 'tarea' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Id de la tabla' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'Id' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Descripción de que trata la tarea' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'Descripcion' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Colaborador asignado a la tarea.' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'ColaboradorId' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Estado actual de la tarea. Puede tener los valores: 1 = Pendiente; 2 =  En proceso; 3 = Finalizada' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'Estado' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Nivel de importancia al atender las tareas. Puede tener los valores: 3 = Baja; 2 = Media; 1 = Alta' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'Prioridad' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Fecha de inicio de la tarea' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'FechaInicio' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Fecha de finalización de la tarea' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'FechaFin' 
GO
EXEC sp_addextendedproperty 'MS_Description' , 'Notas asociadas a la tarea' , 'USER' , 'dbo' , 'TABLE' , 'tarea' , 'COLUMN' , 'Notas' 
GO

ALTER TABLE tarea ADD CONSTRAINT tarea_PK PRIMARY KEY CLUSTERED (Id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE tarea ADD CONSTRAINT tarea_CK_1 CHECK ( estado in (1,2,3) ) 
GO

ALTER TABLE tarea ADD CONSTRAINT tarea_CK_2 CHECK ( prioridad in (1,2,3) ) 
GO

ALTER TABLE tarea 
    ADD CONSTRAINT tarea_colaborador_FK FOREIGN KEY 
    ( 
     ColaboradorId
    ) 
    REFERENCES colaborador 
    ( 
     Id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

