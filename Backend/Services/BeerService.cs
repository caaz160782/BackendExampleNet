using Backend.DTOs;
using Backend.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private StoreContext _context;
        

        public BeerService(StoreContext context )
        {
            _context = context;            
        }

        public async Task<IEnumerable<BeerDto>> Get() =>
                   await _context.Beers.Select(b => new BeerDto
                   {
                       Id = b.BeerID,
                       Name = b.BeerName,
                       Alcohol = b.Alcohol,
                       BrandID = b.BrandID
                   }).ToListAsync();

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

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

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

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
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            beer.BeerName = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beer.BrandID;
            await _context.SaveChangesAsync();

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
            var beer = await _context.Beers.FindAsync(id);

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

          

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        

            return beerDto;
        }

    }
}
