namespace INFAS_CORTES_PO.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //public bool IsValidGmail()
        //{
        //    return !string.IsNullOrWhiteSpace(Email) &&
        //           Email.Trim().EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase) &&
        //           Email.Trim().Length > "@gmail.com".Length;
        //}

        //public string _sql(string fullname, string email, string username, string password, string confirmPassword, string tb)
        //{
        //    string _query = $"INSERT INTO User" + tb + "VALUES(fullname, username, password, confirmPassword)";
        //    return _query;
        //}

        public string _sql(string[] fields, string[] values, string tb)
        {
            string fieldList = "";
            string valueList = "";

            for(int i = 0; i < fields.Length; i++)
            {
                fieldList = fieldList + fields[i];
                if(i!= fields.Length - 1)
                {

                    fields[i] = fieldList;
                    
                }
            }
            for (int i = 0; i < values.Length; i++)
            {
                valueList = valueList + values[i];
                if (i != values.Length - 1)
                {
                    values[i] = values[i];
                }
            }

            string _query = "INSERT INTO User" + tb + "(" + fieldList + ") VALUES (" + valueList + ")";
            return _query;
        }
       


        //public string GetRegistrationSummary()
        //{
        //    return "Registration Successful!\n\n" +
        //           "Full Name: " + FullName + "\n" +
        //           "Email: " + Email + "\n" +
        //           "Username: " + Username;
        //}

    }
}
