using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.DbContexts;
using MusicApi.Domain.Interfaces;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/Tracks")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly MusicContext _context = new MusicContext();
        private readonly IFileManager _fileManager;
        private readonly IUrlBuilder _urlBuilder;

        public TracksController(
            IFileManager fileManager
            , IUrlBuilder urlBuilder)
        {
            _fileManager = fileManager;
            _urlBuilder = urlBuilder;
        }

        #region Routes

        // GET: api/Tracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return await _context.Tracks.ToListAsync();
        }

        // GET: api/Tracks/5
        [HttpGet("{id}", Name=nameof(GetTrack))] //Name must be set for CreatedAtAction
        public async Task<ActionResult<Track>> GetTrack(int id)
        {
            var track = await _context.Tracks.FindAsync(id);

            if (track == null)
            {
                return NotFound();
            }

            return track;
        }

        // PUT: api/Tracks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(int id, Track track)
        {
            if (id != track.Id)
            {
                return BadRequest();
            }

            _context.Entry(track).State = EntityState.Modified;

            if (! await SaveFilesToDisk(track))
            {
                return Problem("Unable to save to disk.", 
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            SetUrls(track);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(id))
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

        // POST: api/Tracks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Track>> PostTrack([FromForm]Track track)
        {           
            var files = Request.Form.Files;
            foreach(var file in files)
            {
                using (var content = new StreamContent(file.OpenReadStream()))
                {
                    var data = await content.ReadAsByteArrayAsync();
                    if (file.Name.Equals(nameof(Track.FileData)))
                    {
                        track.FileData = data;
                    }
                    if (file.Name.Equals(nameof(Track.ImageData)))
                    {
                        track.ImageData = data;
                    }
                }                
            }
            

            if (!await SaveFilesToDisk(track))
            {
                return Problem("Unable to save to disk.", 
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            SetUrls(track);

            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();

            track.FileData = null; //Don't send
            track.ImageData = null; //Don't send
            return CreatedAtAction(nameof(GetTrack), new { id = track.Id }, track);
        }

        // DELETE: api/Tracks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Track>> DeleteTrack(int id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();

            return track;
        }

        #endregion

        #region Helper Methods

        private bool TrackExists(int id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }

        private async Task<bool> SaveFilesToDisk(Track track)
            => await _fileManager.SaveTrackToDisk(track) 
            && await _fileManager.SaveImageToDisk(track);

        private void SetUrls(Track track)
        {
            track.FileUrl = _urlBuilder.GetTrackUrl(track);
            track.ImageUrl = _urlBuilder.GetImageUrl(track);
        }

        private string GetSecurityHeader()
        {
            if (Request.Headers.TryGetValue("SecurityToken", out var resultSet))
            {
                return resultSet.FirstOrDefault();
            }
            return string.Empty;
        }

        #endregion
    }
}
