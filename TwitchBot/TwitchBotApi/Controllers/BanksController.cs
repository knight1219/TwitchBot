﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TwitchBotApi.Models;

namespace TwitchBotApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BanksController : Controller
    {
        private readonly TwitchBotContext _context;

        public BanksController(TwitchBotContext context)
        {
            _context = context;
        }

        // GET: api/banks
        //[HttpGet]
        //public IEnumerable<Bank> GetBank()
        //{
        //    return _context.Bank;
        //}

        // GET: api/banks/5
        // GET: api/banks/5?username=simple_sandman
        [HttpGet("{broadcasterId:int}")]
        public async Task<IActionResult> GetBank([FromRoute] int broadcasterId, [FromQuery] string username = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bank = await _context.Bank.Where(m => m.Broadcaster == broadcasterId).ToListAsync();

            if (!string.IsNullOrEmpty(username))
                bank = bank.Where(m => m.Username == username).ToList();

            if (bank == null)
            {
                return NotFound();
            }

            return Ok(bank);
        }

        // PUT: api/banks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank([FromRoute] int id, [FromBody] Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bank.Id)
            {
                return BadRequest();
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
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

        // POST: api/banks
        [HttpPost]
        public async Task<IActionResult> PostBank([FromBody] Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bank.Add(bank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBank", new { id = bank.Id }, bank);
        }

        //// DELETE: api/banks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBank([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var bank = await _context.Bank.SingleOrDefaultAsync(m => m.Id == id);
        //    if (bank == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Bank.Remove(bank);
        //    await _context.SaveChangesAsync();

        //    return Ok(bank);
        //}

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.Id == id);
        }
    }
}