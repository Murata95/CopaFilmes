namespace CopaFilmes.Web.Api
{
    public class ApiError
    {
        public const string ErrorMessage = "Ocorreu um erro.";

        public ApiError(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}
