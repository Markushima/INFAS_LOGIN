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

        public List<User> ViewAll()
        {
            return FakeDB.Users;
        }

       

    }
}
