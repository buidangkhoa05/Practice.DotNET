using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Entities
{
    public interface IAuditable<TKey>
    {
        DateTime CreatedDate { get; set; }
        TKey? CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        TKey? UpdatedBy { get; set; }
    }
}
