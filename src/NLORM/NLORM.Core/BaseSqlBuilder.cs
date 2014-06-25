using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLORM.Core.BasicDefinitions;
using System.Diagnostics;
using System.Dynamic;

namespace NLORM.Core
{
    public class BaseSqlBuilder : ISqlBuilder
    {
        protected ISqlGenerator SqlGen;

        private string _sqlString="";
        private string whereString = "";
        private dynamic whereConsPara;
        private int pCount = 0;
        public string SQLString
        {
            get
            {
                return _sqlString;
            }
            set
            {
                _sqlString = value;
            }
        }
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

        virtual public string GenSelect(Type t)
        {
            StringBuilder sql = new StringBuilder();
            whereConsPara = new ExpandoObject();
            ModelDefinition md = ConvertClassToModelDef(t);
            string ret = SqlGen.GenSelectSql(md);
            _sqlString += ret;
            return ret;
        }

        public string GenWhereCons(FliterObject fo)
        {
            string ret = "";
            switch (fo.Fliter)
            {
                case FliterType.EQUAL_AND:
                    ret = GenWhereConsEqual(fo,"AND");
                    break;
                case FliterType.EQUAL_OR:
                    ret = GenWhereConsEqual(fo,"OR");
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            whereString+=" "+ret +" ";
            return ret;
        }

        public string GetWhereSQLString()
        {
            return whereString;
        }

        private string GenWhereConsEqual(FliterObject fo,string op)
        {
            string ret = " ";
            var fliterInfo = fo.Cons.GetType().GetProperties();
            int i = 1;
            foreach (var info in fliterInfo)
            {
                pCount++;
                ret += " " + info.Name + "=@P" + pCount+" ";
                object v = info.GetValue(fo.Cons, null);
                AddParaToConstObject("P"+pCount,v);
                if (i < fliterInfo.Length)
                {
                    ret += " " + op + " ";
                }
            }
            return ret;
        }


        private ModelDefinition ConvertClassToModelDef<T>() 
        {
            var mdc = new ModelDefinitionConverter();
            return mdc.ConverClassToModelDefinition<T>();
        }
        private ModelDefinition ConvertClassToModelDef(Type t)
        {
            var mdc = new ModelDefinitionConverter();
            return mdc.ConverClassToModelDefinition(t);
        }

        private void AddParaToConstObject(string s, object o)
        {
            IDictionary<string, object> wherDic = whereConsPara;
            wherDic.Add(s, o);
        }



        public dynamic GetWhereParas()
        {
            return whereConsPara;
        }
    }
}
