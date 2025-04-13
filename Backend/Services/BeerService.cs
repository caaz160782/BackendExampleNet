using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private StoreContext _context;
        private IRepository<Beer> _beerRepository;
        public BeerService(
            StoreContext context,
            IRepository<Beer> beerRepository
            )
        {
            _context = context;
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get() 
        {
            var beers = await _beerRepository.Get();
            return beers.Select(beer => new BeerDto() {
                Id = beer.BeerID,
                Name = beer.BeerName,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            });
        }
        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetbyId(id);

            if (beer == null)
            {
                return null;
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.BeerName,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDto;

        }
        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer()
            {
                BeerName = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.BeerName,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDto;
        }
        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetbyId(id);

            if (beer == null)
            {
                return null;
            }

            beer.BeerName = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beer.BrandID;
                        
            _beerRepository.Update(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.BeerName,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDto;
        }
        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetbyId(id);

            if (beer == null)
            {
                return null;
            }
           
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.BeerName,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };
            _beerRepository.Delete(beer);
            await _beerRepository.Save();

            return beerDto;
        }

    }
}
