using System.Runtime.InteropServices;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.User;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;

namespace ArtworkSharingPlatform.Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Like, ArtworkLikeDTO>().ReverseMap();
            CreateMap<Follow, UserFollowDTO>().ReverseMap();
            CreateMap<Comment, ArtworkCommentDTO>().ReverseMap();
            CreateMap<Rating, ArtworkRatingDTO>().ReverseMap();

            CreateMap<User, ArtworkUserDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
                .ReverseMap();
			CreateMap<User, UserDTO>()
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
				.ReverseMap();
			CreateMap<Like, ArtworkLikeDTO>().ReverseMap();

            CreateMap<ArtworkImage, ArtworkImageDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkToAddDTO>().ReverseMap();
            CreateMap<ArtworkImage, ArtworkImageToAddDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkDTO>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ArtworkImages.SingleOrDefault(x => x.IsThumbnail.Value).ImageUrl))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Owner))
                .ReverseMap();
            CreateMap<Message, MessageDTO>();
            CreateMap<User, UserInfoDTO>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.UserRoles.Any() ? src.UserRoles.First().Role.Name : null))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
                .ReverseMap();
            CreateMap<User, UserAdminDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.First().Role.Name))
                .ReverseMap();
            CreateMap<User, UserAdminCreateDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.First().Role.Name))
                .ReverseMap();
            CreateMap<User, UserInfoAudienceDTO>()
                .ReverseMap();
            CreateMap<User, UserDetailUpdateDTO>()
                .ReverseMap();
            CreateMap<User, UserProfileDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
                .ReverseMap();
            CreateCommissionRequestToCommissionEntityMap();
            CommissionEntityToCommissionDTOMap();
        }


        private void CreateCommissionRequestToCommissionEntityMap()
        {
            CreateMap<CreateCommissionRequestDTO, CommissionRequest>()
                .ForMember(dest => dest.MinPrice,
                    opt => opt.MapFrom(
                        src => src.MinPrice
                    ))
                .ForMember(dest => dest.MaxPrice,
                    opt => opt.MapFrom(
                        src => src.MaxPrice
                    ))
                .ForMember(dest => dest.RequestDescription,
                    opt => opt.MapFrom(
                        src => src.RequestDescription
                    ))
                .ForMember(dest => dest.NotAcceptedReason,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.RequestDate,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.IsProgressStatus,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.Sender,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.Receiver,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.Genre,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.CommissionStatus,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.CommissionImages,
                    opt => opt.Ignore()
                );
        }

        private void CommissionEntityToCommissionDTOMap()
        {
            CreateMap<CommissionRequest, CommissionDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.Id
                    ))
                .ForMember(dest => dest.MinPrice,
                    opt => opt.MapFrom(
                        src => src.MinPrice
                    ))
                .ForMember(dest => dest.MaxPrice,
                    opt => opt.MapFrom(
                        src => src.MaxPrice
                    ))
                .ForMember(dest => dest.RequestDescription,
                    opt => opt.MapFrom(
                        src => src.RequestDescription
                    ))
                .ForMember(dest => dest.NotAcceptedReason,
                    opt => opt.MapFrom(
                        src => src.NotAcceptedReason)
                )
                .ForMember(dest => dest.RequestDate,
                    opt => opt.MapFrom(
                        src => src.RequestDate)
                )
                .ForMember(dest => dest.IsProgressStatus,
                    opt => opt.MapFrom(
                        src => src.IsProgressStatus))
                .ForMember(dest => dest.SenderName,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.ReceiverName,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.GenreName,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.CommissionStatus,
                    opt => opt.Ignore()
                );
        }
    }
}