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
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.ConfigManager;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Package;
using ArtworkSharingPlatform.Domain.Entities.Configs;

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
            NewConfigManagerRequestToConfigManagerEntityMap();
            CreateMap<User, ArtworkUserDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
                .ReverseMap();
			CreateMap<User, UserDTO>()
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
				.ReverseMap();
			CreateMap<Like, ArtworkLikeDTO>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap();

            CreateMap<ArtworkImage, ArtworkImageDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkToAddDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkUpdateDTO>().ReverseMap();
            CreateMap<ArtworkImage, ArtworkImageToAddDTO>().ReverseMap();
            CreateMap<Report, ReportDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkDTO>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ArtworkImages.SingleOrDefault(x => x.IsThumbnail.Value).ImageUrl))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ReverseMap();
            CreateMap<Artwork, ArtworkAdminDTO>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ArtworkImages.SingleOrDefault(x => x.IsThumbnail.Value).ImageUrl))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.Name)).
                ReverseMap();
            CreateMap<Message, MessageDTO>();
            CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.UserImage.Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.UserImage.Url))
                .ReverseMap();
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
            CreateMap<PackageInformation, PackageInformationDTO>()
                .ReverseMap();
            CreateMap<PackageInformation, PackageUpdate>().ReverseMap();
            CreateMap<PackageBilling, PackageBillingDTO>().ReverseMap();
            CreateMap<ConfigManager, ConfigManagerAdminDTO>()
                .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src => src.Administrator != null ? src.Administrator.Name : null))
                .ReverseMap();
            CreateCommissionRequestToCommissionEntityMap();
			CreateMap<User, UpdateProfileDTO>().ReverseMap();
			CreateMap<Follow, UserProfileFollowDTO>()
                .ForMember(dest => dest.SourceUserEmail, opt => opt.MapFrom(src => src.SourceUser.Email))
                .ForMember(dest => dest.TargetUserEmail, opt => opt.MapFrom(src => src.TargetUser.Email))
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
            CreateMap<CommissionRequest, CommissionHistoryAdminDTO>()
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
        private void NewConfigManagerRequestToConfigManagerEntityMap()
        {
            CreateMap<NewConfigManagerRequest, ConfigManager>()
                .ForMember(dest => dest.ConfigDate,
                    opt => opt.Ignore()
                )
                .ForMember(dest => dest.IsServicePackageConfig,
                    opt => opt.MapFrom(
                        src => src.IsServicePackageConfig
                    ))
                .ForMember(dest => dest.IsPhysicalImageConfig,
                    opt => opt.MapFrom(
                        src => src.IsPhysicalImageConfig
                    ))
                .ForMember(dest => dest.MaxReleaseCount,
                    opt => opt.Ignore())
                .ForMember(dest => dest.IsGeneralConfig,
                    opt => opt.MapFrom(
                        src => src.IsGeneralConfig)
                )
                .ForMember(dest => dest.LogoUrl,
                    opt => opt.MapFrom(
                        src => src.LogoUrl)
                )
                .ForMember(dest => dest.MyPhoneNumber,
                    opt => opt.MapFrom(
                        src => src.MyPhoneNumber))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(
                        src => src.Address))
                .ForMember(dest => dest.IsPagingConfig,
                    opt => opt.MapFrom(
                        src => src.IsPagingConfig))
                .ForMember(dest => dest.TotalItemPerPage,
                    opt => opt.MapFrom(
                        src => src.TotalItemPerPage))
                .ForMember(dest => dest.RowSize,
                    opt => opt.MapFrom(
                        src => src.RowSize))
                .ForMember(dest => dest.IsAdvertisementConfig,
                    opt => opt.MapFrom(
                        src => src.IsAdvertisementConfig))
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(
                        src => src.CompanyName))
                .ForMember(dest => dest.CompanyPhoneNumber,
                    opt => opt.MapFrom(
                        src => src.CompanyPhoneNumber))
                .ForMember(dest => dest.CompanyEmail,
                    opt => opt.MapFrom(
                        src => src.CompanyEmail))
                .ForMember(dest => dest.AdministratorId,
                    opt => opt.MapFrom(
                        src => src.AdministratorId));
        }
    }
}