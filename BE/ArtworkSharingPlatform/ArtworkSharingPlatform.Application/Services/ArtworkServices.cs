using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Artwork;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Interfaces;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ArtworkSharingPlatform.Application.Services
{
	public class ArtworkServices : IArtworkService
	{
		private readonly IArtworkRepository _artworkRepository;
		private readonly IUserRepository _userRepository;
		private readonly IUserImageRepository _userImageRepository;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public ArtworkServices(
            IArtworkRepository artworkRepository, 
            IUserRepository userRepository,
            IUserImageRepository userImageRepository,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
			_artworkRepository = artworkRepository;
			_userRepository = userRepository;
			_userManager = userManager;
			_userImageRepository = userImageRepository;
			_mapper = mapper;
		}


        public async Task<ArtworkDTO> GetArtworkAsync(int id)
		{
			var query = _artworkRepository.GetArtworksAsQueryable();

			var artwork = await query.Where(x => x.Id == id).ProjectTo<ArtworkDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
			return artwork;
		}
		public async Task<List<ArtworkDTO>> GetArtworksByGenre(int genreId)
		{
			var query = _artworkRepository.GetArtworksAsQueryable();

			var artwork = await query.Where(x => x.GenreId == genreId).ProjectTo<ArtworkDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
			return artwork;
		}
        public async Task<List<ArtworkAdminDTO>> GetArtworkAdmin()
        {
            var artworks = await _artworkRepository.GetArtworksAsync();
            var query = artworks.AsQueryable();

            return query.ProjectTo<ArtworkAdminDTO>(_mapper.ConfigurationProvider).ToList();
        }


        public async Task<PagedList<ArtworkDTO>> GetArtworksAsync(UserParams userParams)
		{
			var query = _artworkRepository.GetArtworksAsQueryable();
			query = query.Where(x => x.Status == 1);
			query = query.Where(x => x.Price >= userParams.MinPrice && x.Price <= userParams.MaxPrice);

			if (userParams.GenreIds != null && userParams.GenreIds.Length > 0)
			{
				query = query.Where(x => userParams.GenreIds.Contains(x.GenreId));
			}

			if(!string.IsNullOrEmpty(userParams.Search))
				query = query.Where(x => x.Title.Contains(userParams.Search));

			query = userParams.OrderBy switch
			{
				"lowPrice" => query.OrderBy(x => x.Price),
				_ => query.OrderByDescending(x => x.Price)
			};

			return await PagedList<ArtworkDTO>.CreateAsync(query.AsNoTracking().ProjectTo<ArtworkDTO>(_mapper.ConfigurationProvider),
															userParams.PageNumber,
															userParams.PageSize);
		}

        public async Task UserLike(ArtworkLikeDTO like)
		{

			var artworkLike = _mapper.Map<Like>(like);
			artworkLike.User = null;

			await _artworkRepository.UserLike(artworkLike);

		}

		public async Task UserRating(ArtworkRatingDTO rating)
        {

                var artworkRate = _mapper.Map<Rating>(rating);

                await _artworkRepository.UserRating(artworkRate);

        }

        public async Task AddArtwork(ArtworkToAddDTO _artwork)
        {

                var artwork= _mapper.Map<Artwork>(_artwork);

                await _artworkRepository.AddArtwork(artwork);

        }

        public async Task DeleteArtwork(int artworkId)
        {

                await _artworkRepository.DeleteArtwork(artworkId);

        }
        public async Task UpdateArtwork(ArtworkUpdateDTO artwork)
        {

                var _artwork = _mapper.Map<Artwork>(artwork);

                await _artworkRepository.UpdateArtwork(_artwork);

        }

        public async Task UserFollow(int sourceUserId, string email)
        {
			var user = await _userManager.FindByEmailAsync(email);
			var follow = new UserFollowDTO
			{
				SourceUserId = sourceUserId,
				TargetUserId = user.Id
			};
			var artworkFollow = _mapper.Map<Follow>(follow);

			await _artworkRepository.UserFollow(artworkFollow);

		}
		public async Task ArtworkComment(ArtworkCommentDTO comment)
        {
            var artwork = _mapper.Map<Comment>(comment);
            await _artworkRepository.UserComment(artwork);
        }

        public async Task<IEnumerable<ArtworkLikeToShowDTO>> GetArtworksLike(int userId)
        {
            IList<ArtworkLikeToShowDTO> artworkLikeDTOList = new List<ArtworkLikeToShowDTO>();
            var artworks = await _artworkRepository.GetArtworksAsync();


            foreach (var artwork in artworks)
            {
                if(await _artworkRepository.HasUserLikedArtwork(userId, artwork.Id))
                {
					var artworkLikeDTO = new ArtworkLikeToShowDTO
					{
						ArtworkId = artwork.Id,
						IsLiked = true
					};
					artworkLikeDTOList.Add(artworkLikeDTO);
				}
            }
            return artworkLikeDTOList;
        }
        public async Task<IList<ArtworkDTO>> SearchArtworkByTitle(string search)
        {
            var artworks = await _artworkRepository.SearchArtwork(search);
            var artworkDTOs = _mapper.Map<IList<ArtworkDTO>>(artworks);
            return artworkDTOs;
        }
        public async Task<IEnumerable<ArtworkDTO>> SearchArtworkByGenre(int genreId)
        {
            var artworks = await _artworkRepository.SearchArtworkByGenre(genreId);
            var artworkDTOs = _mapper.Map<IList<ArtworkDTO>>(artworks);
            return artworkDTOs;
        }

		public async Task<bool> ConfirmSell(int artworkId, int userId, string buyUserEmail)
		{
			return await _artworkRepository.ConfirmSell(artworkId, userId, buyUserEmail);
		}

		public async Task<IList<ArtworkDTO>> GetArtistArtwork(int artistId)
		{
			var query = _artworkRepository.GetArtworksAsQueryable();
			query = query.Where(x => x.OwnerId == artistId && x.Status == 1).OrderByDescending(x => x.CreatedDate);
			return await query.ProjectTo<ArtworkDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<bool> SetThumbnail(int id)
		{
			return await _artworkRepository.SetThumbNail(id);
		}

		public async Task<ArtworkImage> AddImageToArtwork(ArtworkImageDTO artworkImageDTO)
		{
			var artworkImage = _mapper.Map<ArtworkImage>(artworkImageDTO);
			return await _artworkRepository.AddImageToArtwork(artworkImage);
		}

        public async Task ReportArtwork(ReportDTO _report)
        {
            var report = _mapper.Map<Report>(_report);
            await _artworkRepository.ArtworkReport(report);
        }
    
		public async Task<bool> DeleteArtworkImage(ArtworkImageDTO imageDTO)
		{
			var image = _mapper.Map<ArtworkImage>(imageDTO);
			return await _artworkRepository.DeleteArtworkImage(image);
		}

        public Task AddArtworkImage(ArtworkImageToAddDTO _artwork)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArtworkImage(ArtworkImageToAddDTO _artwork)
        {
            throw new NotImplementedException();
        }

		public async Task<int> GetArtworkRatingForUser(int userId, int artworkId)
		{
			return await _artworkRepository.GetArtworkRatingForUser(userId, artworkId);
		}
	
		public async Task<IEnumerable<GetArtworkCommentDTO>> GetArtworkComments(int artworkId)
		{
			var comments = await _artworkRepository.ListArtworkComments(artworkId);
			List<GetArtworkCommentDTO> res = new List<GetArtworkCommentDTO>();
			foreach (var comment in comments)
			{
				User user = _userRepository.GetById(comment.UserId);
				UserImage userImage =  _userImageRepository.GetByUserId(user.Id);
				string userName = user.UserName;
				string avatarUrl = null;
				if (userImage != null)
				{
					avatarUrl = userImage.Url;
				}
				
				res.Add(new GetArtworkCommentDTO()
				{
					ArtworkId = comment.ArtworkId,
					AvatarUrl = avatarUrl,
					UserId = comment.UserId,
					UserName = userName,
					Content = comment.Content
				});
			}
			return res;
		}
		public async Task ActiveArtworkStatus(int artworkId, int userId)
		{
			await _artworkRepository.ActiveArtworkStatus(artworkId, userId);
		}
  }
}
