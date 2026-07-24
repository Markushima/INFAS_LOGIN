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

        public static List<Product> Products = new List<Product>()
        {
            new Product
            {
                ProductID = 1,
                ProductName = "Keyboard",
                Price = 1200,
                Stocks = 10
            },
            new Product
            {
                ProductID = 2,
                ProductName = "Mouse",
                Price = 500,
                Stocks = 20
            }
        };
    }
}
