using ToolShopAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShopAPI.Models;

[Route("[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly BrandService _brandsService;
    public BrandController(BrandService brandService) =>
        _brandsService = brandService;
    [HttpGet]
    public async Task<ActionResult<List<Brand>>> Get()
    {
        var brands = await _brandsService.GetBrands();
        return Ok(brands);
    }

    [HttpGet("{Id:length(24)}")]
    public async Task<ActionResult<Brand>> Get(string id)
    {
        var tool = await _brandsService.GetBrand(id);

        if (tool is null)
        {
            return NotFound();
        }

        return tool;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Brand newBrand)
    {
        await _brandsService.CreateBrand(newBrand);
        return CreatedAtAction(nameof(Get), new { id = newBrand.Id }, newBrand);
    }

    [HttpPut("{Id:length(24)}")]
    public async Task<IActionResult> Update(string id, Brand updatedBrand)
    {
        var brand = await _brandsService.GetBrand(id);

        if (brand is null)
        {
            return NotFound();
        }

        updatedBrand.Id = brand.Id;

        await _brandsService.UpdateBrand(id, updatedBrand);

        return NoContent();
    }

    [HttpDelete("{Id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var brand = await _brandsService.GetBrand(id);

        if (brand is null)
        {
            return NotFound();
        }

        await _brandsService.RemoveBrand(id);

        return NoContent();
    }

}