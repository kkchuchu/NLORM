using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public class BaseSqlBuilder : ISqlBuilder
    {
        protected ISqlGenerator SqlGen;

        public BaseSqlBuilder()
        {
            SqlGen = new BaseSqlGenerator();
        }
        virtual public string GenCreateTableSql<T>() where T : new()
        {
            var mdc = new ModelDefinitionConverter(null);
            ModelDefinition md = mdc.ConverClassToModelDefinition<T>();
            return SqlGen.GenCreateTableSql(md);
        }

        virtual public string GenDropTableSql<T>() where T : new()
        {
            //TODO
            return "";
        }


    }
}
