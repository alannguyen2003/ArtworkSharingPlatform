﻿using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

			if (userParams.GenreId != 0)
			{
				query = query.Where(x => x.GenreId == userParams.GenreId);
			}

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
        public async Task UpdateArtwork(ArtworkUpdateDTO _artwork)
        {

                var artwork = _mapper.Map<Artwork>(_artwork);

                await _artworkRepository.UpdateArtwork(artwork);

        }

        public async Task UserFollow(UserFollowDTO follow)
        {

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
        public async Task AddArtworkImage(ArtworkImageToAddDTO _artwork)
        {

            var artwork = _mapper.Map<ArtworkImage>(_artwork);

            await _artworkRepository.AddArtworkImage(artwork);

        }
        public async Task UpdateArtworkImage(ArtworkImageToAddDTO _artwork)
        {

            var artwork = _mapper.Map<ArtworkImage>(_artwork);

            await _artworkRepository.UpdateArtworkImage(artwork);
        }

        public async Task ReportArtwork(ReportDTO _report)
        {
            var report = _mapper.Map<Report>(_report);
            await _artworkRepository.ArtworkReport(report);
        }
    }
}
