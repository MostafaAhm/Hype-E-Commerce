namespace Product.API.Contracts
{
    public static class ApiRoute
    {
        public static class Account
        {
            public const string Register = "account/register";
            public const string Login = "account/login";
            public const string AddRole = "account/addrole";
        }
            public static class Product
        {
            public const string CreateProduct = "Product/create";
            public const string GetProduct = "Product/get";
        }
    }
}
