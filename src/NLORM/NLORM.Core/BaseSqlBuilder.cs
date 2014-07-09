using System;
using System.Collections.Generic;
using System.Text;
using NLORM.Core.BasicDefinitions;
using System.Diagnostics;
using System.Dynamic;

namespace NLORM.Core
{
    abstract public class BaseSqlBuilder : ISqlBuilder
    {
        protected ISqlGenerator SqlGen;

        private string _sqlString="";
        private string _whereString = "";
        private Dapper.DynamicParameters _whereConsPara;
        private int _pCount;
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

            var md = ConvertClassToModelDef<T>();
            return SqlGen.GenCreateTableSql(md);
        }

        virtual public string GenDropTableSql<T>() 
        {
            var md = ConvertClassToModelDef<T>();
            return SqlGen.GenDropTableSql(md);
        }

        virtual public string GenInsertSql<T>()
        {
            var md = ConvertClassToModelDef<T>();
            return SqlGen.GenInsertSql(md);
        }

        virtual public string GenSelect(Type t)
        {
            _whereConsPara = new Dapper.DynamicParameters();
            var md = ConvertClassToModelDef(t);
            var ret = SqlGen.GenSelectSql(md);
            _sqlString += ret;
            return ret;
        }

        virtual public string GenDelete(Type t)
        {
            _whereConsPara = new Dapper.DynamicParameters();
            var md = ConvertClassToModelDef(t);
            var ret = SqlGen.GenDeleteSql(md);
            _sqlString += ret;
            return ret;
        }

        virtual public string GenUpdate(Type t)
        {
            _whereConsPara = new Dapper.DynamicParameters();
            var md = ConvertClassToModelDef(t);
            var ret = SqlGen.GenUpdateSql(md);
            _sqlString += ret;
            return ret;
        }

        public string GenWhereCons(FliterObject fo)
        {
            var ret = "";
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
            _whereString+=" ("+ret +") ";
            return ret;
        }

        public string GetWhereSQLString()
        {
            return _whereString;
        }

        private string GenWhereConsEqual(FliterObject fo,string op)
        {
            var ret = " ";
            var fliterInfo = fo.Cons.GetType().GetProperties();
            var i = 1;
            foreach (var info in fliterInfo)
            {
                _pCount++;
                ret += " " + info.Name + "=@P" + _pCount+" ";
                object v = info.GetValue(fo.Cons, null);
                AddParaToConstObject("P"+_pCount,v);
                if (i < fliterInfo.Length)
                {
                    ret += " " + op + " ";
                    i++;
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
            if (_whereConsPara == null)
            {
                _whereConsPara = new Dapper.DynamicParameters();
            }
            _whereConsPara.Add("@"+s, o);
        }



        public Dapper.DynamicParameters GetWhereParas()
        {
            return _whereConsPara;
        }


        abstract public ISqlBuilder CreateOne();


    }
}
