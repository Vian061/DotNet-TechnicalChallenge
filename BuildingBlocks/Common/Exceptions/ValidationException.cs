using BuildingBlocks.Common.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Exceptions
{
	public sealed class ValidationException : AppException
	{
		public ValidationException(string message) : base(message) { }
		public override int StatusCode => 400;
	}

}
