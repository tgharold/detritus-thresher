using FluentMigrator;

namespace DetritusThresher.Migrations.Migrations
{
    [Migration(20190214033700)]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            // IfDatabase("sqlite")...
            // https://fluentmigrator.github.io/articles/multi-db-support.html

            Create.Table("Scans")
                .WithColumn("Id")
                    .AsInt32()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("Name")
                    .AsString()
                    .NotNullable()
                .WithColumn("ScanCreated")
                    .AsDateTime()
                    .NotNullable()
                    .WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("ScanFinished")
                    .AsDateTime()
                    .Nullable()
                ;
        }
    }
}