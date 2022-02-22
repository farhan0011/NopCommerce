using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.Books.Domains;

namespace Nop.Plugin.Widgets.Books.Infrastructure
{
    [NopMigration("2022/02/16 02:00:00", "base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Book>();
        }
        


    }
}
