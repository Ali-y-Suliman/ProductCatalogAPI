namespace CategoriesProductsAPI.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string[] Errors { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
            Errors = new string[] { };
        }

        public ErrorResponse(string message, string[] errors)
        {
            Message = message;
            Errors = errors;
        }
    }
}