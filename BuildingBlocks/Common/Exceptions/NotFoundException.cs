using BuildingBlocks.Common.Exceptions.Base;

namespace BuildingBlocks.Common.Exceptions
{
    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message) { }
        public override int StatusCode => 404;
    }
}
