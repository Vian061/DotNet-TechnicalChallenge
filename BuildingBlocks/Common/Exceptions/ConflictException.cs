using BuildingBlocks.Common.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Exceptions
{
	public sealed class ConflictException : AppException
	{
		public ConflictException(string message) : base(message) { }
		public override int StatusCode => 409;
	}

}
