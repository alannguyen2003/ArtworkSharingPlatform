using ArtworkSharingPlatform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        [Required]
        public string? ReportName { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }
        [Required]
        public int SenderId { get; set; }
    }
}
