using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Entity.Interface
{
    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }
        int CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        int UpdatedBy { get; set; }
    }
}
