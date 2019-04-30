namespace MusicHub.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.DataProcessor.ExportDtos;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumsInfo = context
                .Albums
                .Where(x => x.ProducerId == producerId)
                .Select(x => new
                {
                    AlbumName = x.Name,
                    ReleaseDate = x.ReleaseDate.ToString("MM/dd/yyyy"),
                    ProducerName = x.Producer.Name,
                    Songs = x.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        Price = $"{s.Price:f2}",
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(t => t.SongName)
                    .ThenBy(t => t.Writer),
                    AlbumPrice = $"{x.Price:f2}"
                })
                .OrderByDescending(x => x.AlbumPrice);
               

            var jsonString = JsonConvert.SerializeObject(albumsInfo, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            TimeSpan span = TimeSpan.FromSeconds(duration);
            string str = span.ToString(@"hh\:mm\:ss\:fff");

            var songs = context
                .Songs
                .Where(x => x.Duration > span)
                .Select(x => new ExportSongsDto
                {
                    SongName = x.Name,
                    Writer = x.Writer.Name,
                    Performer = $"{x.SongPerformers.First(s => s.Performer.FirstName != null)} {x.SongPerformers.First(s => s.Performer.LastName != null)}",
                    AlbumProducer = x.Album.Producer.Name,
                    Duration = x.Duration.ToString("c")
                })
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.Writer)
                .ThenBy(x => x.Performer)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportSongsDto[]), new XmlRootAttribute("Songs"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), songs, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}