namespace SH.APIProblemDetails
{
    [Serializable]
    public class ProblemDetailsException(string error, string message) : Exception
    {
        public string Error { get; set; } = error;
        public string Message { get; set; } = message;
    }


}
