using BuildingBlocks.Common.Exceptions.Base;

namespace BuildingBlocks.Common.Exceptions
{
    public sealed class ConflictException : AppException
    {
        public ConflictException(string message) : base(message) { }
        public override int StatusCode => 409;
    }

}
