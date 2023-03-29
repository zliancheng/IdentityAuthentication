namespace Mongo.Web.Models
{
    public class ResponseDto
    {
        public bool isSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public string ErrorMessage { get; set; }
    }
}
