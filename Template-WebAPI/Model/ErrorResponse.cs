using System;
namespace Template_WebAPI.Model
{
  public class ErrorResponse
  {
    public ErrorResponse(double code, string message)
    {
      ResponseCode = code;
      ResponseMessage = message;
    }
    public string ResponseMessage { get; set; }
    public double ResponseCode { get; set; }
  }
}
