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
                .WithColumn("ScanId")
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
                .WithColumn("StartingFolderId")
                    .AsInt64()
                    .Nullable()
                ;

            Create.Table("FolderScans")
                .WithColumn("FolderScanId")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("ScanId")
                    .AsInt32()
                    .NotNullable()
                .WithColumn("ParentId")
                    .AsInt64()
                    .Nullable()
                .WithColumn("ScanCreated")
                    .AsDateTime()
                    .NotNullable()
                    .WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("ScanFinished")
                    .AsDateTime()
                    .Nullable()
                .WithColumn("Uri")
                    .AsString()
                    .NotNullable()
                .WithColumn("Name")
                    .AsString()
                    .NotNullable()
                .WithColumn("Created")
                    .AsDateTime()
                    .Nullable()
                .WithColumn("Modified")
                    .AsDateTime()
                    .Nullable()
                ;

            Create.Table("FileScans")
                .WithColumn("FileScanId")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("ScanCreated")
                    .AsDateTime()
                    .NotNullable()
                    .WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("ScanFinished")
                    .AsDateTime()
                    .Nullable()
                .WithColumn("Uri")
                    .AsString()
                    .NotNullable()
                .WithColumn("Name")
                    .AsString()
                    .NotNullable()
                .WithColumn("Created")
                    .AsDateTime()
                    .Nullable()
                .WithColumn("Modified")
                    .AsDateTime()
                    .Nullable()
                .WithColumn("Bytes")
                    .AsInt64()
                    .Nullable()
                ;

        }
    }
}