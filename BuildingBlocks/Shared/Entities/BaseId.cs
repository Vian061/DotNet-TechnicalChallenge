using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Shared.Entities
{
    public class BaseId
    {
        public BaseId()
        {
            Id = 0;
            Alias = Guid.NewGuid().ToString();
        }

        public int Id { get; set; }
        public string Alias { get; set; }
    }
}
