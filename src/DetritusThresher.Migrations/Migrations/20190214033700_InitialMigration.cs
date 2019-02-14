using FluentMigrator;

namespace DetritusThresher.Migrations.Migrations
{
    [Migration(20190214033700)]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Log")
                .WithColumn("Id")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("Severity")
                    .AsInt32()
                    .NotNullable()
                .WithColumn("SeverityName")
                    .AsString("10")
                    .NotNullable()
                .WithColumn("Category")
                    .AsString("10")
                    .NotNullable()
                .WithColumn("Created")
                    .AsDateTimeOffset()
                    .NotNullable()
                    .WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("Message")
                    .AsString()
                    .Nullable()
                ;
        }
    }
}