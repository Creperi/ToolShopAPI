using ToolShopAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShopAPI.Models;

[Route("[controller]")]
[ApiController]
public class ToolController : ControllerBase
{
    private readonly ToolsService _toolsService;
    public ToolController(ToolsService toolsService) =>
        _toolsService = toolsService;

    [HttpGet]
    public async Task<List<Tool>> Get() =>
        await _toolsService.GetTools();

    [HttpGet("{Id:length(24)}")]
    public async Task<ActionResult<Tool>> Get(string id)
    {
        var tool = await _toolsService.GetTool(id);

        if (tool is null)
        {
            return NotFound();
        }

        return tool;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Tool newTool)
    {
        await _toolsService.CreateTool(newTool);
        return CreatedAtAction(nameof(Get), new { id = newTool.Id }, newTool);
    }

    [HttpPut("{Id:length(24)}")]
    public async Task<IActionResult> Update(string Id, Tool updatedTool)
    {
        var tool = await _toolsService.GetTool(Id);

        if (tool is null)
        {
            return NotFound();
        }

        updatedTool.Id = tool.Id;

        await _toolsService.UpdateTool(Id, updatedTool);

        return NoContent();
    }

    [HttpDelete("{Id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var tool = await _toolsService.GetTool(id);

        if (tool is null)
        {
            return NotFound();
        }

        await _toolsService.RemoveTool(id);

        return NoContent();
    }

}