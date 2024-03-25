using System.Net;

namespace ElasticSearch.Api.DTOs
{
    public record ResponseDto<T>
    {
        public T? Data { get; set; }
        public List<string>? Erros { get; set; }
        public HttpStatusCode Status { get; set; }

        //static factory method
        public static ResponseDto<T> Success(T data, HttpStatusCode status)
        {
            return new ResponseDto<T> { Data = data, Status = status };
        }

        public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode status)
        {
            return new ResponseDto<T> { Erros = errors, Status = status };
        }

        public static ResponseDto<T> Fail(string errors, HttpStatusCode status)
        {
            return new ResponseDto<T> { Erros = new List<string> { errors }, Status = status };
        }
    }

}