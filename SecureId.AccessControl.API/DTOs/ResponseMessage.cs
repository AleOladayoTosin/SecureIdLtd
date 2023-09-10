namespace SecureId.AccessControl.API.DTOs
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

   
}
