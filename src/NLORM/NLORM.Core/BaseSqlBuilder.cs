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
        virtual public string GenCreateTableSql<T>() 
        {

            ModelDefinition md = ConvertClassToModelDef<T>();
            return SqlGen.GenCreateTableSql(md);
        }

        virtual public string GenDropTableSql<T>() 
        {
            ModelDefinition md = ConvertClassToModelDef<T>();
            return SqlGen.GenDropTableSql(md);
        }

        virtual public string GenInsertSql<T>()
        {
            ModelDefinition md = ConvertClassToModelDef<T>();
            return SqlGen.GenInsertSql(md);
        }

        private ModelDefinition ConvertClassToModelDef<T>() 
        {
            var mdc = new ModelDefinitionConverter();
            return mdc.ConverClassToModelDefinition<T>();
        }


    }
}
