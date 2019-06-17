using Dapper;
using Mathy.Model.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.DAL
{
    public abstract class BaseDAL
    {
        private string ReadConnectionString;
        private string WriteConnectionString;

        public BaseDAL()
        {
            ReadConnectionString = ConfigurationManager.AppSettings["ConnectionString_Read"];
            WriteConnectionString = ConfigurationManager.AppSettings["ConnectionString_Write"];
        }

        public virtual PageList<T> QueryPage<T>(string sql, PageInfo page, object param = null, string sqlCountCustom = null)
        {
            var countstr = @" SELECT  COUNT(*) totalcount
                                FROM    ( {0}
                                        ) CountTable ";
            var sqlCount = string.IsNullOrEmpty(sqlCountCustom) ? string.Format(countstr, sql) : sqlCountCustom;
            var sqlPage = sql + " Order By " + page.OrderField + " " + page.DescString;
            sqlPage += " OFFSET " + page.OffSetRows + " ROWS FETCH NEXT " + page.PageSize + " ROWS ONLY";

            using (var conn = GetDbConnection())
            {
                var p = new PageList<T>();
                p.Data = conn.Query<T>(sqlPage, param);
                page.TotalCount = conn.QueryFirst<int>(sqlCount, param);
                p.Page = page;
                return p;
            }
        }

        public virtual IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var conn = GetDbConnection())
            {
                return conn.Query<T>(sql, param);
            }
        }

        public virtual T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            using (var conn = GetDbConnection())
            {
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }

        public virtual T SaveAndReturnID<T>(string sql, object param = null, SqlTransaction transaction = null)
        {
            if (transaction == null)
                using (var conn = GetDbConnection(false))
                {
                    return conn.QueryFirst<T>(sql, param);
                }
            else
                return transaction.Connection.QueryFirst<T>(sql, param, transaction);
        }

        public virtual Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = GetDbConnection())
                {
                    return conn.Query<T>(sql, param);
                }
            });
        }

        public virtual Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = GetDbConnection())
                {
                    return conn.QueryFirstOrDefault<T>(sql, param);
                }
            });
        }

        public virtual bool Excute(string sql, object param = null, SqlTransaction transaction = null)
        {
            if (transaction == null)
                using (var conn = GetDbConnection(false))
                {
                    return conn.Execute(sql, param) >= 0;
                }
            else
                return transaction.Connection.Execute(sql, param, transaction) >= 0;
        }

        public virtual Task<int> ExcuteAsync(string sql, object param = null, SqlTransaction transaction = null)
        {
            if (transaction == null)
            {
                return Task.Factory.StartNew(() =>
                {
                    using (var conn = GetDbConnection())
                    {
                        return conn.Execute(sql, param);
                    }
                });
            }
            else
            {
                return Task.Factory.StartNew(() =>
                {
                    return transaction.Connection.Execute(sql, param, transaction);
                });
            }
        }

        public virtual bool BulkToDB<T>(IEnumerable<T> entitys, string tableName, SqlTransaction tran = null)
        {
            DataTable dt = GetTable(entitys);
            using (var conn = GetDbConnection(false))
            {
                SqlBulkCopy bulkCopy;
                if (tran != null)
                {
                    bulkCopy = new SqlBulkCopy(tran.Connection, SqlBulkCopyOptions.Default, tran);
                }
                else
                {
                    bulkCopy = new SqlBulkCopy(conn);
                }
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = dt.Rows.Count;
                try
                {
                    if (dt != null && dt.Rows.Count != 0)
                        bulkCopy.WriteToServer(dt);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public virtual Task<bool> BulkToDBAsync<T>(IEnumerable<T> entitys, string tableName, SqlTransaction tran = null)
        {
            return Task.Factory.StartNew(() =>
            {
                return BulkToDB(entitys, tableName, tran);
            });
        }

        private DataTable GetTable<T>(IEnumerable<T> entitys)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] propertys = t.GetProperties();
            foreach (var n in propertys)
            {
                var column = new DataColumn();
                column.AllowDBNull = true;
                column = new DataColumn(n.Name);
                dt.Columns.Add(column);
            }
            foreach (var n in entitys)
            {
                object[] entityValues = new object[propertys.Length];
                for (int i = 0; i < propertys.Length; i++)
                {
                    entityValues[i] = propertys[i].GetValue(n, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        public virtual SqlConnection GetDbConnection(bool isRead = true)
        {
            string connectionStr = isRead ? ReadConnectionString : WriteConnectionString;
            SqlConnection conn = new SqlConnection(connectionStr);
            conn.Open();
            return conn;
        }

        public virtual SqlTransaction GetTransaction()
        {
            var conn = GetDbConnection(false);
            return conn.BeginTransaction();
        }
    }
}
