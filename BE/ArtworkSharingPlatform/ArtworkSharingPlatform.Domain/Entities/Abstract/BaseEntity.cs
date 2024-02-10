using System.ComponentModel.DataAnnotations;

namespace ArtworkSharingPlatform.Domain.Entities.Abstract;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}