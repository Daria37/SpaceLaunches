using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportData : ControllerBase
    {
        private readonly ISpaceDevsService _spaceDevsService;

        public ImportData(ISpaceDevsService spaceDevsService)
        {
            _spaceDevsService = spaceDevsService;
        }

        [HttpPost("data")]
        public async Task<IActionResult> ImportWholeData()
        {
            var result = await _spaceDevsService.SaveToDatabaseAsync();
            return result ? Ok("Data imported successfully") : BadRequest("Import failed");
        }
    }
}