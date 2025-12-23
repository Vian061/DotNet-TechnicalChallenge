using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Exceptions.Base
{
	public abstract class AppException : Exception
	{
		protected AppException(string message) : base(message) { }

		public abstract int StatusCode { get; }
	}
}
