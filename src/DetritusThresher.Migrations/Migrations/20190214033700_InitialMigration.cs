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

            Create.Table("Log")
                .WithColumn("Id")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("Severity")
                    .AsInt32()
                    .NotNullable()
                .WithColumn("SeverityName")
                    .AsString(10)
                    .NotNullable()
                .WithColumn("Category")
                    .AsString(10)
                    .NotNullable()
                .WithColumn("Created")
                    .AsDateTime()
                    .NotNullable()
                    .WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("Message")
                    .AsString()
                    .Nullable()
                ;

            Create.Table("Scans")
                .WithColumn("Id")
                    .AsInt32()
                    .PrimaryKey()
                    .Identity()
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