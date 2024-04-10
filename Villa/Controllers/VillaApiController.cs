using System.Data.Entity;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Villa.Data;
using Villa.Models.Dto;
using Villa.Properties.Repository.IRepository;

namespace Villa.Controllers;

[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly IVillaRepository _db;
    private readonly IMapper _mapper;

    public VillaApiController(IVillaRepository db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
        IEnumerable<Models.Villa> villaList = await _db.GetAllAsync();
        return Ok(_mapper.Map<List<VillaDTO>>(villaList));
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VillaDTO>> GetVillaId(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var villa = await _db.Get(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<VillaDTO>(villa));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreatedDTO createdDTO)
    {
        if (await _db.GetAsync(u => u.Name == createdDTO.Name != null) != null)
        {
            ModelState.AddModelError("CustomerError", "Villa already Exists!");
            return BadRequest(ModelState);
        }

        if (createdDTO == null)
        {
            return BadRequest(createdDTO);
        }

        if (createdDTO.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        Models.Villa model = _mapper.Map<Models.Villa>(createdDTO);

        await _db.CreateAsync(model);

        return CreatedAtAction("GetVilla", new { id = model.Id }, model);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = await _db.GetAsync(u => u.Id == id);
        if (villa==null)
        {
            return NotFound();
        }

        await _db.RemoveAsync(villa);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDto)
    {
        if (updateDto == null || id != updateDto.Id)
        {
            return BadRequest();
        }

        Models.Villa model = _mapper.Map<Models.Villa>(updateDto);
        // Models.Villa model = new()
        // {
        //     Id = updateDto.Id,
        //     Details = updateDto.Details,
        //     ImageUrl = updateDto.ImageUrl,
        //     Name = updateDto.Name,
        //     Rate = updateDto.Rate,
        //     Square = updateDto.Square
        // };
        _db.Villas.Update(model);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }

        var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        if (villa == null)
        {
            return BadRequest();
        }

        //patchDTO.ApplyTo(VillaDTO, ModelState);
        Models.Villa model = _mapper.Map<Models.Villa>(typeof(VillaDTO));

        _db.Villas.Update(model);
        await _db.SaveChangesAsync();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
}