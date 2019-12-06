﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Forum.Services;
using Forum.DTOin;
using Forum.DTOout;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Forum.Extensions;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ThreadController : ControllerBase
    {
        private readonly IThreadService _threadService;
        private readonly IMapper _mapper;
        public ThreadController(IThreadService threadService, IMapper mapper)
        {
            _threadService = threadService;
            _mapper = mapper;
        }
        // GET: api/Thread
        [HttpGet("get/", Name = "GetThreads")]
        [ProducesResponseType(typeof(IEnumerable<ThreadDTOout>), 200)]
        [Authorize(Roles = "User, Admin")]
        public async Task<IEnumerable<ThreadDTOout>> Get()
        {
            var thread = await _threadService.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadDTOout>> (thread);

            return dto;
        }

        // GET: api/Thread/5
        [HttpGet("get/{id}", Name = "GetThread")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ThreadDTOout> Get(string id)
        {
            var thread = await _threadService.GetAsync(id);
            var dto = _mapper.Map<Thread, ThreadDTOout> (thread);

            return dto;
        }

        // POST: api/Thread
        [HttpPost("post", Name = "PostThread")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ThreadDTOin threadDto)
        {
            var thread = _mapper.Map<ThreadDTOin, Thread>(threadDto);
            var result = await _threadService.AddAsync(thread);

            if(!result.Success)
            {
                return BadRequest(new ErrorViewModel());
            }

            var threadDTO = _mapper.Map<Thread, ThreadDTOout>(result.Resource);
            return Ok(threadDTO);

        }

        // PUT: api/Thread/5
        [HttpPut("put/{id}", Name = "PutThread")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(string id, [FromBody] ThreadDTOin threadDto)
        {
            var thread = _mapper.Map<ThreadDTOin, Thread>(threadDto);
            var result = await _threadService.UpdateAsync(id, thread);

            if(!result.Success)
            {
                return BadRequest(new ErrorViewModel());
            }

            var threadDTO = _mapper.Map<Thread, ThreadDTOout>(result.Resource);
            return Ok(threadDTO);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("delete/{id}", Name = "DeleteThread")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _threadService.RemoveAsync(id);

            if(!result.Success)
            {
                return BadRequest(new ErrorViewModel());
            }

            var threadDTO = _mapper.Map<Thread, ThreadDTOout>(result.Resource);
            return Ok(threadDTO);
        }
        [HttpPost("subscribe/{id}", Name = "SubscribeThread")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Subscribe(string id)
        {
            var UserId = HttpContext.GetUserId();

            var result = await _threadService.Subscribe(UserId, id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok("Succesfuly subscribed!");
        }
        [HttpDelete("unsubscribe/{id}", Name = "UnSubscribeThread")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UnSubscribe(string id)
        {
            var UserId = HttpContext.GetUserId();

            var result = await _threadService.UnSubscribe(UserId, id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok("Succesfuly unsubscribed!");
        }
    }
}
