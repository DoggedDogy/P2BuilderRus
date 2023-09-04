using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace P2BuilderRus.Services
{
    public class IModelService
    {
        public async Task<DataTable> GetDataFromDB(string target, string tableQuery)
        {
            DataTable dataTable = new DataTable();
            await using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-4DHE83OFTCF;Initial Catalog=Pf;Trusted_Connection=true"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT {target} FROM {tableQuery}", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static List<string> ConvertToString(DataTable dataTable)
        {
            List<string> list = dataTable.Select().AsEnumerable().Select(r => r.Field<string>("Name")).ToList();
            return list;
        }
        public static List<string[]> ConvertToStringList(DataTable dataTable)
        {
            List<string[]> results = dataTable.Select().Select(dr => dr.ItemArray.Select(x => x.ToString()).ToArray()).ToList();
            return results;
        }
    }
}
