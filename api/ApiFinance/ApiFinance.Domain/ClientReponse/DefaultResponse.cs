using System;

namespace ApiFinance.Domain.ClientReponse
{
    public class DefaultResponse
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
        public dynamic ResultObject { get; set; }
        public string Status { get; set; }
    }
}