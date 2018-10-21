

Enable-Migrations

Enable-Migrations -ContextTypeName DynamicFiltersTests.AccountTest+TestContext


add-migration   'Init'

Update-Database  -Script -SourceMigration:0