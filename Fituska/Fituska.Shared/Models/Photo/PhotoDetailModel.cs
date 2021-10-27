using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fituska.Shared.Models.Photo;

public record PhotoDetailModel : ModelBase
{
    public byte[]? Content { get; set; }
}
