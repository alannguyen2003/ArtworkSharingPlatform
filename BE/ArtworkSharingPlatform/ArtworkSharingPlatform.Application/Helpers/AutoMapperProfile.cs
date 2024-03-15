﻿using System.Runtime.InteropServices;
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
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Package;

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
    }
}