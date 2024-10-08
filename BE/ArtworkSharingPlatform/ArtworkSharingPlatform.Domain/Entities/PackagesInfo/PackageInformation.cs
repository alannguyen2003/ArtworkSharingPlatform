﻿using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Configs;

namespace ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

public class PackageInformation : BaseEntity
{
    public string Name { get; set; }
    public int Credit { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Price { get; set; }
    public byte? Status { get; set; }
    public ICollection<PackageBilling>? PackageBillings { get; set; }
    public ICollection<ConfigManager>? ConfigManagers { get; set; }
}