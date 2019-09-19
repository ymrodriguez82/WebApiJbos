﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJbos.Modele;

namespace WebApIJbos.Controllers
{
    [Route("api/candidat")]
    [ApiController]
    public class CandidatController : ControllerBase
    {
        private readonly AplicationDbContext context;

        public CandidatController(AplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Candidat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidat>>> GetAll()
        {
            return await context.Candidats.ToListAsync();
        }

        // GET: api/Candidat/5
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Candidat>> GetById(long id)
        {
            var candidat = await context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }
            return candidat;
        }

        // POST: api/Candidat
        [HttpPost]
        public async Task<ActionResult<Candidat>>Post([FromBody] Candidat candidat)
        {
            if (ModelState.IsValid)
            {
                context.Candidats.Add(candidat);
                await context.SaveChangesAsync();
                //Retour OK et un objet de tipo candidat, Alors on fait redirection au method GetById
                //return CreatedAtAction(nameof(GetById), new { id = candidat.Id }, candidat);
                return new CreatedAtRouteResult("GetById", new { id = candidat.Id }, candidat);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Candidat/5
        [HttpPut("{id}")]
        public async Task<IActionResult>Put(long id, [FromBody] Candidat candidat)
        {
            if (id != candidat.Id)
            {
                return BadRequest();
            }
            context.Entry(candidat).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            //IActionResult interface que permettre de retourner entre autres NotFound
            var candidat = await context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }
            context.Candidats.Remove(candidat);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
