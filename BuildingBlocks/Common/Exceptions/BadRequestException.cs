using BuildingBlocks.Common.Exceptions.Base;

namespace BuildingBlocks.Common.Exceptions
{
    public sealed class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message) { }
        public override int StatusCode => 400;
    }

}
