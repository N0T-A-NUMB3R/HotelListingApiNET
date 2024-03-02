using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListingAPI.Data;
using HotelListingAPI.Contracts;
using HotelListingAPI.Models.Hotel;
using AutoMapper;
using HotelListingAPI.Repository;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IMapper mapper, IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            var hotel =  await _hotelRepository.GetAllAsync();
            return _mapper.Map<List<HotelDTO>>(hotel);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {

            var hotel = await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return _mapper.Map<HotelDTO>(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }

            var hotel = await _hotelRepository.GetAsync(id);
            if (hotel == null) return NotFound();

            _mapper.Map(hotelDto, hotel);  // copies content in hotelDto into hotel

            try
            {
                await _hotelRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

            // POST: api/Hotels
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(HotelDTO hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {

            var hotel = await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
