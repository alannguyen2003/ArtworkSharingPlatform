using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Application.Services
{
	public class ArtworkServices : IArtworkService
	{
		private readonly IArtworkRepository _artworkRepository;
		private readonly IMapper _mapper;

		public ArtworkServices(IArtworkRepository artworkRepository, IMapper mapper)
        {
			_artworkRepository = artworkRepository;
			_mapper = mapper;
		}


        public async Task<ArtworkDTO> GetArtworkAsync(int id)
		{
			var query = _artworkRepository.GetArtworksAsQueryable();

			var artwork = await query.Where(x => x.Id == id).ProjectTo<ArtworkDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
			return artwork;
		}

		public async Task<PagedList<ArtworkDTO>> GetArtworksAsync(UserParams userParams)
		{
			var query = _artworkRepository.GetArtworksAsQueryable();

			query = query.Where(x => x.OwnerId != userParams.CurrentUserId);

			query = query.Where(x => x.Price >= userParams.MinPrice && x.Price <= userParams.MaxPrice);

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
			try
			{
				var artworkLike = _mapper.Map<Like>(like);

				await _artworkRepository.UserLike(artworkLike);
			} catch (Exception ex) { }
		}

        public async Task UserRating(ArtworkRatingDTO rating)
        {
            try
            {
                var artworkRate = _mapper.Map<Rating>(rating);

                await _artworkRepository.UserRating(artworkRate);
            }
            catch (Exception ex) { }
        }

        public async Task AddArtwork(ArtworkDTO _artwork)
        {
            try
            {
                var artwork= _mapper.Map<Artwork>(_artwork);

                await _artworkRepository.AddArtwork(artwork);
            }
            catch (Exception ex) { }
        }

        public async Task DeleteArtwork(int artworkId)
        {
            try
            {
                await _artworkRepository.DeleteArtwork(artworkId);
            }
            catch (Exception ex) { }
        }
        public async Task UpdateArtwork(ArtworkDTO _artwork)
        {
            try
            {
                var artwork = _mapper.Map<Artwork>(_artwork);

                await _artworkRepository.UpdateArtwork(artwork);
            }
            catch (Exception ex) { }
        }

        public async Task UserFollow(UserFollowDTO follow)
        {
            try
            {
                var artworkFollow = _mapper.Map<Follow>(follow);

                await _artworkRepository.UserFollow(artworkFollow);
            }
            catch (Exception ex) { }
        }
    }
}
