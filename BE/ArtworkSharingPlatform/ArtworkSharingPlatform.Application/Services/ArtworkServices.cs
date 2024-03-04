using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
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
	}
}
