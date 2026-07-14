namespace INFAS_CORTES_PO.Models
{
    public static class FakeDB
    {
        public static List<User> Users = new List<User>()
        {
            new User
            {
                FullName = "Administrator",
                Email = "admin@gmail.com",
                Username = "admin",
                Password = "admin123"
            }
        };
    }
}
