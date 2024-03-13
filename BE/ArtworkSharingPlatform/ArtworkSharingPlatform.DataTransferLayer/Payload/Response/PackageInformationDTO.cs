using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response
{
    public class PackageInformationDTO
    {
        public string Name { get; set; }
        public int Credit { get; set; }
        [Column(TypeName = "decimal(10,2)")] public decimal Price { get; set; }
        public string? Status { get; set; }
    }
}
