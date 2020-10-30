using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Track
    {

        public int? Id { get; set; }

        [FromForm(Name = "Title")]
        public string Title { get; set; }

        [FromForm(Name = "FileName")]
        public string FileName { get; set; }

        [FromForm(Name = "Description")]
        public string Description { get; set; }

        [FromForm(Name = "TrackType")]
        public TrackType TrackType { get; set; }

        [FromForm(Name = "CreationDate")]
        public DateTime? CreationDate { get; set; }

        public string FileUrl { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        [FromForm(Name = "FileData")]
        public byte[] FileData { get; set; }

        [NotMapped]
        [FromForm(Name = "ImageData")]
        public byte[] ImageData { get; set; }

    }
}
