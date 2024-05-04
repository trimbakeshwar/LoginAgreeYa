namespace LoginAgreeYa.Model
{
    public class ResposeModel
    {
        public bool IsSuccess { get; set; }
        public int StatusCode  { get; set; }
        public string? Message { get; set; }
        public dynamic? Results { get; set; }
    }
}
