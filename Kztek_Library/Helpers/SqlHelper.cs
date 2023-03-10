using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;


namespace Kztek_Library.Helpers
{
    public static class SqlHelper
    {
        public static bool ExcuteCommandToBool(string connect, string command)
        {
            var result = false;

            var k = new SqlConnection(connect);

            using (SqlConnection conn = k)
            {
                try
                {
                    
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(command, conn);

                    var dt = cmd.ExecuteReader();

                    conn.Close();

                    result = true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        public static List<T> ExcuteCommandToList<T>(string connect, string command)
        {
            List<T> list = new List<T>();

            var k = new SqlConnection(connect);

            using (SqlConnection conn = k)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);

                var dt = cmd.ExecuteReader();

                list = DataReaderMapToList<T>(dt);

                conn.Close();
            }

            return list;
        }

        public static T ExcuteCommandToModel<T>(string connect, string command)
        {
            var list = new List<T>();

            var k = new SqlConnection(connect);

            using (SqlConnection conn = k)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);

                var dt = cmd.ExecuteReader();

                list = DataReaderMapToList<T>(dt);

                conn.Close();
            }

            return list.FirstOrDefault();
        }

        private static List<T> DataReaderMapToList<T>(SqlDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static DataSet GetDataSet(string connect,string strSQL)
        {
            //Khai b??o 1 bi???n b???ng ????? ch???a d??? li???u
            var ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connect))
            {
                try
                {
                    //M??? k???t n???i
                    conn.Open();
                    //Khai b??o 1 ?????i t?????ng ????? th???c hi???n c??ng vi???c
                    //SqlCommand comm = new SqlCommand(strSQL, conn);
                    SqlCommand comm = new SqlCommand();
                    //Th???c hi???n c??ng vi???c tr??n database n??o
                    comm.Connection = conn;
                    //N???u s??? d???ng th??? t???c
                    comm.CommandType = CommandType.Text;
                    //C??ng vi???c c???n th???c hi???n
                    comm.CommandText = strSQL;
                    //Khai b??o 1 ?????i t?????ng ????? ch???a d??? li???u t???m th???i l???y ???????c t??? db l??n
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    //????? d??? li???u v??o b???ng

                    adapter.Fill(ds);

                    adapter.Dispose();
                    comm.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //????ng k???t n???i
                    conn.Close();
                }
            }
            return ds;
        }

        public static List<T> ConvertTo<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }

        }

        public static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        public static DataTable ToDataTableNullable<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }


            return tb;
        }


        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
    }
}