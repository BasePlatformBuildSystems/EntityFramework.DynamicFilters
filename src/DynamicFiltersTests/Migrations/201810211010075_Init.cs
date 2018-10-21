namespace DynamicFiltersTests.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 100),
                        RemappedDBProp = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Account_ConceptualNameTest", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DerivedAccount_ConceptualNameTest", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BlogEntries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AccountID = c.Guid(nullable: false),
                        Body = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        IntValue = c.Int(),
                        StringValue = c.String(maxLength: 100),
                        DateValue = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BlogEntry_BlogEntryFilter", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.AccountID, cascadeDelete: true)
                .Index(t => t.AccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogEntries", "AccountID", "dbo.Accounts");
            DropIndex("dbo.BlogEntries", new[] { "AccountID" });
            DropTable("dbo.BlogEntries",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BlogEntry_BlogEntryFilter", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Accounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Account_ConceptualNameTest", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DerivedAccount_ConceptualNameTest", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
