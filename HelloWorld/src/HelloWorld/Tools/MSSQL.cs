using Dapper;
using HelloWorld.FrontModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HelloWorld.Tools
{
    /// <summary>
    /// mssql
    /// </summary>
    public class MSSQL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        //public const string CONNECTIONSTRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HelloWorld;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string _connectionstring = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = HelloWorld; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <typeparam name="T">数据对象模型</typeparam>
        /// <param name="model">存储过程参数模型</param>
        /// <returns></returns>
        public static FrontModel.PageDataModel<T> GetDataList<T>(PageDataStoredModel model) where T : class
        {
            var data = new PageDataModel<T>();
            if (model == null)
            {
                return data;
            }
            try
            {
                using (var conn = new SqlConnection(_connectionstring))
                {
                    DynamicParameters dp = new DynamicParameters();
                    dp.Add("@TblName", model.TblName);
                    dp.Add("@FldName", model.FldName);
                    dp.Add("@FldSort", model.FldSort);
                    dp.Add("@strCondition", model.StrCondition);

                    dp.Add("@strGroup", model.StrGroup);
                    dp.Add("@strHaving", model.StrHaving);
                    dp.Add("@Dist", model.Dist);
                    dp.Add("@strOrder", model.StrOrder);
                    dp.Add("@OnlyCounts", model.OnlyCounts);
                    dp.Add("@PageSize", model.PageSize);
                    dp.Add("@Page", model.Page);

                    dp.Add("@PageCounts", "", DbType.Int32, ParameterDirection.Output);
                    dp.Add("@Counts", "", DbType.Int32, ParameterDirection.Output);
                    data = new PageDataModel<T>();
                    data.Page = model.Page;
                    data.PageSize = model.PageSize;
                    data.DataList = conn.Query<T>("[MyPageRead]", dp, null, true, null, System.Data.CommandType.StoredProcedure).ToList();
                    data.PageCounts = dp.Get<int>("PageCounts");
                    data.Counts = dp.Get<int>("Counts");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        /// <summary>
        /// 返回jq分页模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static JQDataTableModel GetJQDatatableModel<T>(PageDataStoredModel model) where T : class
        {
            var data = GetDataList<T>(model);
            Type type = typeof(T);
            var finfos = type.GetProperties();
            JQDataTableModel jqmodel = new JQDataTableModel();
            jqmodel.data = new List<List<object>>();
            jqmodel.draw = model.Draw;
            jqmodel.recordsTotal = data.Counts;
            jqmodel.recordsFiltered = data.Counts;
            foreach (var item in data.DataList)
            {
                List<object> lst = new List<object>();
                foreach (PropertyInfo finfo in finfos)
                {
                    lst.Add(finfo.GetValue(item,null));
                }
                jqmodel.data.Add(lst);
            }
            return jqmodel;
        }

        public static T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionstring))
                {
                    return conn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionstring))
                {
                    return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
