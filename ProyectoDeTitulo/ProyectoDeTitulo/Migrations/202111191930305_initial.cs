namespace ProyectoDeTitulo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contrasena",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        strContrasena = c.String(unicode: false),
                        Fecha_creacion = c.DateTime(nullable: false, precision: 0),
                        Fecha_expiracion = c.DateTime(nullable: false, precision: 0),
                        RUT = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.RUT)
                .Index(t => t.RUT);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        RUT = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Nombre = c.String(nullable: false, unicode: false),
                        Apellido = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Correo = c.String(nullable: false, unicode: false),
                        Contraseña = c.String(nullable: false, unicode: false),
                        EstadoID = c.Int(nullable: false),
                        PerfilID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RUT)
                .ForeignKey("dbo.Estado", t => t.EstadoID, cascadeDelete: true)
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .Index(t => t.EstadoID)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, unicode: false),
                        EstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estado", t => t.EstadoID, cascadeDelete: true)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.Permisos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, unicode: false),
                        EstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estado", t => t.EstadoID, cascadeDelete: true)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.Perfil_log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        Fecha_alta = c.DateTime(nullable: false, precision: 0),
                        Fecha_creacion = c.DateTime(nullable: false, precision: 0),
                        Fecha_baja = c.DateTime(nullable: false, precision: 0),
                        Fecha_modificacion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.Perfil_permisos_log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PermisosID = c.Int(nullable: false),
                        PerfilID = c.Int(nullable: false),
                        Fecha_alta = c.DateTime(nullable: false, precision: 0),
                        Fecha_baja = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .ForeignKey("dbo.Permisos", t => t.PermisosID, cascadeDelete: true)
                .Index(t => t.PermisosID)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.Permisos_log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PermisosID = c.Int(nullable: false),
                        Fecha_alta = c.DateTime(nullable: false, precision: 0),
                        Fecha_creacion = c.DateTime(nullable: false, precision: 0),
                        Fecha_baja = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Permisos", t => t.PermisosID, cascadeDelete: true)
                .Index(t => t.PermisosID);
            
            CreateTable(
                "dbo.PermisosPerfils",
                c => new
                    {
                        Permisos_ID = c.Int(nullable: false),
                        Perfil_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permisos_ID, t.Perfil_ID })
                .ForeignKey("dbo.Permisos", t => t.Permisos_ID, cascadeDelete: true)
                .ForeignKey("dbo.Perfil", t => t.Perfil_ID, cascadeDelete: true)
                .Index(t => t.Permisos_ID)
                .Index(t => t.Perfil_ID);
            
            CreateStoredProcedure(
                "dbo.Contrasena_Insert",
                p => new
                    {
                        strContrasena = p.String(maxLength: 1073741823, unicode: false),
                        Fecha_creacion = p.DateTime(),
                        Fecha_expiracion = p.DateTime(),
                        RUT = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Contrasena`(
                      `strContrasena`, 
                      `Fecha_creacion`, 
                      `Fecha_expiracion`, 
                      `RUT`) VALUES (
                      @strContrasena, 
                      @Fecha_creacion, 
                      @Fecha_expiracion, 
                      @RUT);
                      SELECT 
                      `ID`
                       FROM `Contrasena`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Contrasena_Update",
                p => new
                    {
                        ID = p.Int(),
                        strContrasena = p.String(maxLength: 1073741823, unicode: false),
                        Fecha_creacion = p.DateTime(),
                        Fecha_expiracion = p.DateTime(),
                        RUT = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"UPDATE `Contrasena` SET `strContrasena`=@strContrasena, `Fecha_creacion`=@Fecha_creacion, `Fecha_expiracion`=@Fecha_expiracion, `RUT`=@RUT WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Contrasena_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Contrasena` WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Usuario_Insert",
                p => new
                    {
                        RUT = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                        Apellido = p.String(maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Correo = p.String(maxLength: 1073741823, unicode: false),
                        Contraseña = p.String(maxLength: 1073741823, unicode: false),
                        EstadoID = p.Int(),
                        PerfilID = p.Int(),
                    },
                body:
                    @"INSERT INTO `Usuario`(
                      `RUT`, 
                      `Nombre`, 
                      `Apellido`, 
                      `Correo`, 
                      `Contraseña`, 
                      `EstadoID`, 
                      `PerfilID`) VALUES (
                      @RUT, 
                      @Nombre, 
                      @Apellido, 
                      @Correo, 
                      @Contraseña, 
                      @EstadoID, 
                      @PerfilID);"
            );
            
            CreateStoredProcedure(
                "dbo.Usuario_Update",
                p => new
                    {
                        RUT = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                        Apellido = p.String(maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Correo = p.String(maxLength: 1073741823, unicode: false),
                        Contraseña = p.String(maxLength: 1073741823, unicode: false),
                        EstadoID = p.Int(),
                        PerfilID = p.Int(),
                    },
                body:
                    @"UPDATE `Usuario` SET `Nombre`=@Nombre, `Apellido`=@Apellido, `Correo`=@Correo, `Contraseña`=@Contraseña, `EstadoID`=@EstadoID, `PerfilID`=@PerfilID WHERE `RUT` = @RUT;"
            );
            
            CreateStoredProcedure(
                "dbo.Usuario_Delete",
                p => new
                    {
                        RUT = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"DELETE FROM `Usuario` WHERE `RUT` = @RUT;"
            );
            
            CreateStoredProcedure(
                "dbo.Estado_Insert",
                p => new
                    {
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Estado`(
                      `Nombre`) VALUES (
                      @Nombre);
                      SELECT 
                      `ID`
                       FROM `Estado`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Estado_Update",
                p => new
                    {
                        ID = p.Int(),
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                    },
                body:
                    @"UPDATE `Estado` SET `Nombre`=@Nombre WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Estado_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Estado` WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_Insert",
                p => new
                    {
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                        EstadoID = p.Int(),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Perfil`(
                      `Nombre`, 
                      `EstadoID`) VALUES (
                      @Nombre, 
                      @EstadoID);
                      SELECT 
                      `ID`
                       FROM `Perfil`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_Update",
                p => new
                    {
                        ID = p.Int(),
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                        EstadoID = p.Int(),
                    },
                body:
                    @"UPDATE `Perfil` SET `Nombre`=@Nombre, `EstadoID`=@EstadoID WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Perfil` WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Permisos_Insert",
                p => new
                    {
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                        EstadoID = p.Int(),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Permisos`(
                      `Nombre`, 
                      `EstadoID`) VALUES (
                      @Nombre, 
                      @EstadoID);
                      SELECT 
                      `ID`
                       FROM `Permisos`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Permisos_Update",
                p => new
                    {
                        ID = p.Int(),
                        Nombre = p.String(maxLength: 1073741823, unicode: false),
                        EstadoID = p.Int(),
                    },
                body:
                    @"UPDATE `Permisos` SET `Nombre`=@Nombre, `EstadoID`=@EstadoID WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Permisos_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Permisos` WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_log_Insert",
                p => new
                    {
                        PerfilID = p.Int(),
                        Fecha_alta = p.DateTime(),
                        Fecha_creacion = p.DateTime(),
                        Fecha_baja = p.DateTime(),
                        Fecha_modificacion = p.DateTime(),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Perfil_log`(
                      `PerfilID`, 
                      `Fecha_alta`, 
                      `Fecha_creacion`, 
                      `Fecha_baja`, 
                      `Fecha_modificacion`) VALUES (
                      @PerfilID, 
                      @Fecha_alta, 
                      @Fecha_creacion, 
                      @Fecha_baja, 
                      @Fecha_modificacion);
                      SELECT 
                      `ID`
                       FROM `Perfil_log`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_log_Update",
                p => new
                    {
                        ID = p.Int(),
                        PerfilID = p.Int(),
                        Fecha_alta = p.DateTime(),
                        Fecha_creacion = p.DateTime(),
                        Fecha_baja = p.DateTime(),
                        Fecha_modificacion = p.DateTime(),
                    },
                body:
                    @"UPDATE `Perfil_log` SET `PerfilID`=@PerfilID, `Fecha_alta`=@Fecha_alta, `Fecha_creacion`=@Fecha_creacion, `Fecha_baja`=@Fecha_baja, `Fecha_modificacion`=@Fecha_modificacion WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_log_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Perfil_log` WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_permisos_log_Insert",
                p => new
                    {
                        PermisosID = p.Int(),
                        PerfilID = p.Int(),
                        Fecha_alta = p.DateTime(),
                        Fecha_baja = p.DateTime(),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Perfil_permisos_log`(
                      `PermisosID`, 
                      `PerfilID`, 
                      `Fecha_alta`, 
                      `Fecha_baja`) VALUES (
                      @PermisosID, 
                      @PerfilID, 
                      @Fecha_alta, 
                      @Fecha_baja);
                      SELECT 
                      `ID`
                       FROM `Perfil_permisos_log`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_permisos_log_Update",
                p => new
                    {
                        ID = p.Int(),
                        PermisosID = p.Int(),
                        PerfilID = p.Int(),
                        Fecha_alta = p.DateTime(),
                        Fecha_baja = p.DateTime(),
                    },
                body:
                    @"UPDATE `Perfil_permisos_log` SET `PermisosID`=@PermisosID, `PerfilID`=@PerfilID, `Fecha_alta`=@Fecha_alta, `Fecha_baja`=@Fecha_baja WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Perfil_permisos_log_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Perfil_permisos_log` WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Permisos_log_Insert",
                p => new
                    {
                        PermisosID = p.Int(),
                        Fecha_alta = p.DateTime(),
                        Fecha_creacion = p.DateTime(),
                        Fecha_baja = p.DateTime(),
                    },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Permisos_log`(
                      `PermisosID`, 
                      `Fecha_alta`, 
                      `Fecha_creacion`, 
                      `Fecha_baja`) VALUES (
                      @PermisosID, 
                      @Fecha_alta, 
                      @Fecha_creacion, 
                      @Fecha_baja);
                      SELECT 
                      `ID`
                       FROM `Permisos_log`
                       WHERE  row_count() > 0 AND `ID`=last_insert_id();"
            );
            
            CreateStoredProcedure(
                "dbo.Permisos_log_Update",
                p => new
                    {
                        ID = p.Int(),
                        PermisosID = p.Int(),
                        Fecha_alta = p.DateTime(),
                        Fecha_creacion = p.DateTime(),
                        Fecha_baja = p.DateTime(),
                    },
                body:
                    @"UPDATE `Permisos_log` SET `PermisosID`=@PermisosID, `Fecha_alta`=@Fecha_alta, `Fecha_creacion`=@Fecha_creacion, `Fecha_baja`=@Fecha_baja WHERE `ID` = @ID;"
            );
            
            CreateStoredProcedure(
                "dbo.Permisos_log_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE FROM `Permisos_log` WHERE `ID` = @ID;"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Permisos_log_Delete");
            DropStoredProcedure("dbo.Permisos_log_Update");
            DropStoredProcedure("dbo.Permisos_log_Insert");
            DropStoredProcedure("dbo.Perfil_permisos_log_Delete");
            DropStoredProcedure("dbo.Perfil_permisos_log_Update");
            DropStoredProcedure("dbo.Perfil_permisos_log_Insert");
            DropStoredProcedure("dbo.Perfil_log_Delete");
            DropStoredProcedure("dbo.Perfil_log_Update");
            DropStoredProcedure("dbo.Perfil_log_Insert");
            DropStoredProcedure("dbo.Permisos_Delete");
            DropStoredProcedure("dbo.Permisos_Update");
            DropStoredProcedure("dbo.Permisos_Insert");
            DropStoredProcedure("dbo.Perfil_Delete");
            DropStoredProcedure("dbo.Perfil_Update");
            DropStoredProcedure("dbo.Perfil_Insert");
            DropStoredProcedure("dbo.Estado_Delete");
            DropStoredProcedure("dbo.Estado_Update");
            DropStoredProcedure("dbo.Estado_Insert");
            DropStoredProcedure("dbo.Usuario_Delete");
            DropStoredProcedure("dbo.Usuario_Update");
            DropStoredProcedure("dbo.Usuario_Insert");
            DropStoredProcedure("dbo.Contrasena_Delete");
            DropStoredProcedure("dbo.Contrasena_Update");
            DropStoredProcedure("dbo.Contrasena_Insert");
            DropForeignKey("dbo.Permisos_log", "PermisosID", "dbo.Permisos");
            DropForeignKey("dbo.Perfil_permisos_log", "PermisosID", "dbo.Permisos");
            DropForeignKey("dbo.Perfil_permisos_log", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.Perfil_log", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.Contrasena", "RUT", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PermisosPerfils", "Perfil_ID", "dbo.Perfil");
            DropForeignKey("dbo.PermisosPerfils", "Permisos_ID", "dbo.Permisos");
            DropForeignKey("dbo.Permisos", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Perfil", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Usuario", "EstadoID", "dbo.Estado");
            DropIndex("dbo.PermisosPerfils", new[] { "Perfil_ID" });
            DropIndex("dbo.PermisosPerfils", new[] { "Permisos_ID" });
            DropIndex("dbo.Permisos_log", new[] { "PermisosID" });
            DropIndex("dbo.Perfil_permisos_log", new[] { "PerfilID" });
            DropIndex("dbo.Perfil_permisos_log", new[] { "PermisosID" });
            DropIndex("dbo.Perfil_log", new[] { "PerfilID" });
            DropIndex("dbo.Permisos", new[] { "EstadoID" });
            DropIndex("dbo.Perfil", new[] { "EstadoID" });
            DropIndex("dbo.Usuario", new[] { "PerfilID" });
            DropIndex("dbo.Usuario", new[] { "EstadoID" });
            DropIndex("dbo.Contrasena", new[] { "RUT" });
            DropTable("dbo.PermisosPerfils");
            DropTable("dbo.Permisos_log");
            DropTable("dbo.Perfil_permisos_log");
            DropTable("dbo.Perfil_log");
            DropTable("dbo.Permisos");
            DropTable("dbo.Perfil");
            DropTable("dbo.Estado");
            DropTable("dbo.Usuario");
            DropTable("dbo.Contrasena");
        }
    }
}
