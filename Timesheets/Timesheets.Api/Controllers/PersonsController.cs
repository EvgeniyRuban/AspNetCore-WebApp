﻿using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Repositories;
using Timesheets.Core.Services;
using Timesheets.Core.Models;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Person>> GetAsync([FromRoute] int id, CancellationToken token)
        {
            var result = await _personsService.GetAsync(id, token);
            return Ok(result);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<Person>> GetAsync ([FromQuery] string firstName, CancellationToken token)
        {
            var result = await _personsService.GetAsync(firstName, token);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken token)
        {
            var result = await _personsService.GetRangeAsync(skip, take, token);
            return Ok(result);
        }
            
        [HttpPost]
        public async Task<ActionResult> AddAsync ([FromBody] Person person, CancellationToken token)
        {
            await _personsService.AddAsync(person, token);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync (
            [FromQuery] int targetPersonId, 
            [FromBody] Person newPerson,
            CancellationToken token)
        {
            await _personsService.UpdateAsync(newPerson, token);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> RemoveAsync ([FromRoute] int id, CancellationToken token)
        {
            var result = await _personsService.RemoveAsync(id, token);
            return Ok(result);
        }
    }
}
