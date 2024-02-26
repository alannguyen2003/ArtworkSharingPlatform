using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Packages;
using ArtworkSharingPlatform.Domain.Entities.Transactions;

namespace ArtworkSharingPlatform.Domain.Entities.Users;

public class User: BaseEntity
{
     private string? _name;
     private string? _telephone;
     private string? _email;
     private string? _password;
     private int _roleId;
     private int _packageId;
     private byte _status;
     private int _remainingCredit;
     public ICollection<Artwork>? Artworks;
     public ICollection<PackageBilling>? PackageBillings;
     public ICollection<Transaction>? Transactions; 
     public Role? Role;

     public string? Name
     {
          get => _name;
          set => _name = value;
     }

     public string? Telephone
     {
          get => _telephone;
          set => _telephone = value;
     }

     public string? Email
     {
          get => _email;
          set => _email = value;
     }

     public string? Password
     {
          get => _password;
          set => _password = value;
     }

     public int RoleId
     {
          get => _roleId;
          set => _roleId = value;
     }

     public int PackageId
     {
          get => _packageId;
          set => _packageId = value;
     }

     public byte Status
     {
          get => _status;
          set => _status = value;
     }

     public int RemainingCredit
     {
          get => _remainingCredit;
          set => _remainingCredit = value;
     }
     
     
     
}