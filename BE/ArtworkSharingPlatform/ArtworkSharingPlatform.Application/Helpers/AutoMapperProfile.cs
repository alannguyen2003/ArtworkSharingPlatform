using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;

namespace ArtworkSharingPlatform.Application.Helpers
{
	public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<User, ArtworkUserDTO>();
            CreateMap<Artwork, ArtworkDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ArtworkImages.SingleOrDefault(x => x.IsThumbnail.Value).ImageUrl))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Owner));
        }
    }
}
