using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Shared.DTOs
{
    internal class BaseObjectDTO
    {
        public BaseObjectDTO()
        {
            DateCreated = DateModified = DateTime.Now;
        }

        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; set; }

        public string? Remarks { get; set; }
    }
}
