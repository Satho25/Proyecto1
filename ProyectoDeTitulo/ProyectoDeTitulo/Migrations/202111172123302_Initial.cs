namespace ProyectoDeTitulo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "RUT", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Usuarios", "Apellido", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Usuarios", "Correo", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Usuarios", "Contraseña", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Contraseña", c => c.String(unicode: false));
            AlterColumn("dbo.Usuarios", "Correo", c => c.String(unicode: false));
            AlterColumn("dbo.Usuarios", "Apellido", c => c.String(unicode: false));
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String(unicode: false));
            AlterColumn("dbo.Usuarios", "RUT", c => c.String(unicode: false));
        }
    }
}
