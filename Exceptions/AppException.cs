namespace CategoriesProductsAPI.ErrorException
{
    public class AppException : Exception
        {
            public AppException() : base() {}

            public AppException(string message) : base(message) {}

            public AppException(string message, params object[] args) 
                : base(String.Format(message, args))
            {
            }
        }
}