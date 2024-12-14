using ToolShopAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShopAPI.Models;

[Route("[controller]")]
[ApiController]
public class PersonnelController : ControllerBase
{
    private readonly PersonnelService _personnelService;
    public PersonnelController(PersonnelService personnelService) =>
        _personnelService = personnelService;

    [HttpGet]
    public async Task<List<Personnel>> Get() =>
        await _personnelService.GetPersonnelList();

    [HttpGet("{Id:length(24)}")]
    public async Task<ActionResult<Personnel>> Get(string id)
    {
        var personnel = await _personnelService.GetPersonnel(id);

        if (personnel is null)
        {
            return NotFound();
        }

        return personnel;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Personnel newPersonnel)
    {
        await _personnelService.CreatePersonnel(newPersonnel);
        return CreatedAtAction(nameof(Get), new { id = newPersonnel.Id }, newPersonnel);
    }

    [HttpPut("{Id:length(24)}")]
    public async Task<IActionResult> Update(string Id, Personnel updatedPersonnel)
    {
        var personnel = await _personnelService.GetPersonnel(Id);

        if (personnel is null)
        {
            return NotFound();
        }

        updatedPersonnel.Id = personnel.Id;

        await _personnelService.UpdatePersonnel(Id, updatedPersonnel);

        return NoContent();
    }

    [HttpDelete("{Id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var personnel = await _personnelService.GetPersonnel(id);

        if (personnel is null)
        {
            return NotFound();
        }

        await _personnelService.RemovePersonnel(id);

        return NoContent();
    }

}