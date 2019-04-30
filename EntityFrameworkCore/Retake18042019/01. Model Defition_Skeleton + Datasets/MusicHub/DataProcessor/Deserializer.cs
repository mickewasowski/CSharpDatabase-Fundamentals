namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writersDtos = JsonConvert.DeserializeObject<ImportWriterDto[]>(jsonString);

            var writers = new List<Writer>();
            var sb = new StringBuilder();

            foreach (var writerDto in writersDtos)
            {
                var isValidDto = IsValid(writerDto);

                if (!isValidDto)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var writer = new Writer
                {
                    Name = writerDto.Name,
                    Pseudonym = writerDto.Pseudonym
                };

                writers.Add(writer);

                sb.AppendLine(string.Format(SuccessfullyImportedWriter, writer.Name));
            }

            context.Writers.AddRange(writers);
            context.SaveChanges();

            var result = sb.ToString();

            return result;
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producersAlbumsDtos = JsonConvert.DeserializeObject<ImportProducersAlbumsDto[]>(jsonString);

            var producersAlbums = new List<Producer>();
            var sb = new StringBuilder();

            foreach (var prodAlbDto in producersAlbumsDtos)
            {
                var isValidDto = IsValid(prodAlbDto);

                if (!isValidDto)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producerAlbum = new Producer();

                if (prodAlbDto.PhoneNumber != null)
                {
                    producerAlbum = new Producer
                    {
                        Name = prodAlbDto.Name,
                        Pseudonym = prodAlbDto.Pseudonym,
                        PhoneNumber = prodAlbDto.PhoneNumber
                    };
                }
                else
                {
                    producerAlbum = new Producer
                    {
                        Name = prodAlbDto.Name,
                        Pseudonym = prodAlbDto.Pseudonym
                    };
                }

                bool invalidAlbumName = false;

                foreach (var tokens in prodAlbDto.Albums)
                {
                    string dateTime = tokens.ReleaseDate.ToString();

                    if (tokens.Name.Length < 3 || tokens.Name.Length > 40)
                    {
                        sb.AppendLine(ErrorMessage);
                        invalidAlbumName = true;
                        break;
                    }

                    producerAlbum.Albums.Add(new Album
                    {
                        Name = tokens.Name,
                        ReleaseDate = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    });
                }

                if (invalidAlbumName)
                {
                    continue;
                }

                producersAlbums.Add(producerAlbum);

                if (producerAlbum.PhoneNumber == null)
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone,producerAlbum.Name,producerAlbum.Albums.Count));
                }
                else
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone,producerAlbum.Name, producerAlbum.PhoneNumber, producerAlbum.Albums.Count));
                }
            }

            context.Producers.AddRange(producersAlbums);
            context.SaveChanges();

            var result = sb.ToString();

            return result;
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSongsDto[]), new XmlRootAttribute("Songs"));

            var songsDto = (ImportSongsDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var songs = new List<Song>();
            var sb = new StringBuilder();

            foreach (var songDto in songsDto)
            {
                var albumId = context.Albums.Find(songDto.AlbumId);
                var writerId = context.Writers.Find(songDto.WriterId);

                if (!IsValid(songDto) || !Enum.IsDefined(typeof(Genre), songDto.Genre) || albumId == null || writerId == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var song = new Song
                {
                    Name = songDto.Name,
                    Duration = TimeSpan.ParseExact(songDto.Duration, "c", CultureInfo.InvariantCulture),
                    CreatedOn = DateTime.ParseExact(songDto.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Genre = Enum.Parse<Genre>(songDto.Genre),
                    AlbumId = songDto.AlbumId,
                    WriterId = songDto.WriterId,
                    Price = songDto.Price
                };

                sb.AppendLine(string.Format(SuccessfullyImportedSong, song.Name,song.Genre,song.Duration));

                songs.Add(song);
            }

            context.Songs.AddRange(songs);
            context.SaveChanges();
            return sb.ToString();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSongPerformersDto[]), new XmlRootAttribute("Performers"));

            var performersSongsDto = (ImportSongPerformersDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var performers = new List<Performer>();
            var sb = new StringBuilder();

            foreach (var performerDto in performersSongsDto)
            {
                var ids = performerDto.PerformersSongs.Select(s => s.Id);

                foreach (var id in ids)
                {
                    var songId = context
                    .Songs
                    .FirstOrDefault(s => s.Id == id);

                    if (songId == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }

                if (!IsValid(performerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var performer = new Performer
                {
                    FirstName = performerDto.FirstName,
                    LastName = performerDto.LastName,
                    Age = performerDto.Age,
                    NetWorth = performerDto.NetWorth
                };

                foreach (var id in ids)
                {
                    performer.PerformerSongs.Add(new SongPerformer
                    {
                        SongId = id
                    });
                }

                sb.AppendLine(string.Format(SuccessfullyImportedPerformer, performer.FirstName, performer.PerformerSongs.Count));

                performers.Add(performer);
            }

            context.Performers.AddRange(performers);
            context.SaveChanges();
            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}