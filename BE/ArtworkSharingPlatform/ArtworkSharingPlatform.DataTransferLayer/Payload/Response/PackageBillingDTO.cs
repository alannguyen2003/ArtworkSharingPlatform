using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response
{
    public class PackageBillingDTO
    {
        [Column(TypeName = "decimal(10,2)")] public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
    }
}
