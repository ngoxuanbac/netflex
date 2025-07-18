using Netflex.Shared.Exceptions;

namespace Netflex.Application.Common.Exceptions;

public class InvalidOtpException : BadRequestException
{
    public InvalidOtpException() : base("Otp is not valid") { }
}