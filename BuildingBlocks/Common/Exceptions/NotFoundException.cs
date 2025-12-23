using BuildingBlocks.Common.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Exceptions
{
	public sealed class NotFoundException : AppException
	{
		public NotFoundException(string message) : base(message) { }
		public override int StatusCode => 404;
	}
}
