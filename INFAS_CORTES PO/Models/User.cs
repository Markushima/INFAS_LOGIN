namespace INFAS_CORTES_PO.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

       

        public string _sql(string[] fields, string[] values, string tb)
        {
            string fieldList = "";
            string valueList = "";

            for (int i = 0; i < fields.Length; i++)
            {
                fieldList += fields[i];

                if (i != fields.Length - 1)
                {
                    fieldList += ", ";
                }
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (int.TryParse(values[i], out _) || double.TryParse(values[i], out _))
                {
                   
                    valueList += values[i];
                }
                else
                {
                   
                    valueList += $"'{values[i]}'";
                }

                if (i != values.Length - 1)
                {
                    valueList += ", ";
                }
            }

            return $"INSERT INTO {tb} ({fieldList}) VALUES ({valueList});";
        }

        public string Update(string table, string[] fields, string[] values)
        {
            string query = $"UPDATE {table} SET ";

            for (int i = 0; i < fields.Length; i++)
            {
                query += $"{fields[i]} = '{values[i]}'";

                if (i != fields.Length - 1)
                    query += ", ";
            }

            return query;
        }

        public void UpdateObject(object obj, string[] fields, string[] values)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                var property = obj.GetType().GetProperty(fields[i]);

                if (property != null && property.CanWrite)
                {
                    property.SetValue(obj, Convert.ChangeType(values[i], property.PropertyType));
                }
            }
        }
        public string Delete(string table, string condition)
        {
            return $"DELETE FROM {table} WHERE {condition}";
        }

        public string View(string table)
        {
            return $"SELECT * FROM {table}";
        }

        public object ViewAll(string table)
        {
            var field = typeof(FakeDB).GetField(table + "s");

            if (field == null)
            {
                return new
                {
                    sql = this.View("table"),
                    data = new List<object>()
                };
            }

            return new
            {
                sql = this.View("table"),
                data = field.GetValue(null)
            };
        }
    }
}
