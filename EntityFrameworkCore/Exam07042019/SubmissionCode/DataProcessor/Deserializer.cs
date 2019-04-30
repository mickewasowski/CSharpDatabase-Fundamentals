namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var movies = JsonConvert.DeserializeObject<Movie[]>(jsonString)
                .ToArray();

            var valid = new List<Movie>();

            var movie = new Movie();

            var sb = new StringBuilder();

            foreach (var m in movies)
            {
                if (m.Title == null || m.Title.Length < 3 || m.Title.Length > 20 || valid.Contains(m) || Enum.IsDefined(typeof(GenreType), m.Genre) == false || m.Duration == null || m.Rating < 1 || m.Rating > 10 || m.Director == null || m.Director.Length < 3 || m.Director.Length > 20)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                movie = new Movie
                {
                    Title = m.Title,
                    Genre = m.Genre,
                    Duration = m.Duration,
                    Rating = m.Rating,
                    Director = m.Director
                };
                valid.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating + ".00"));
            }
            context.Movies.AddRange(valid);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallSeats = JsonConvert.DeserializeObject<ImportHallSeatsDto[]>(jsonString)
                .ToArray();

            var valid = new List<Hall>();

            var sb = new StringBuilder();

            foreach (var dto in hallSeats)
            {
                var seats = new List<Seat>();

                for (int i = 0; i < dto.Seats; i++)
                {
                    seats.Add(new Seat());
                }

                if (dto.Name == null || dto.Name.Length < 3 || dto.Name.Length > 20 || dto.Seats <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall
                {
                    Name = dto.Name,
                    Is4Dx = dto.Is4Dx,
                    Is3D = dto.Is3D,
                    Seats = seats
                };

                var projectionType = "";

                if (hall.Is4Dx && hall.Is3D)
                {
                    projectionType = "4Dx/3D";
                }
                else if (hall.Is4Dx)
                {
                    projectionType = "4Dx";
                }
                else if (hall.Is3D)
                {
                    projectionType = "3D";
                }
                else
                {
                    projectionType = "Normal";
                }
                valid.Add(hall);
                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count()));
            }

            context.AddRange(valid);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProjectionsDto[]), new XmlRootAttribute("Projections"));

            var users = (ImportProjectionsDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var valid = new List<Projection>();

            var sb = new StringBuilder();

            foreach (var u in users)
            {
                var movie = context.Movies.Find(u.MovieId);
                var hall = context.Halls.Find(u.HallId);

                if (movie == null || hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var input = new Projection
                {
                    MovieId = u.MovieId,
                    HallId = u.HallId,
                    DateTime = DateTime.Parse(u.DateTime)
                };

                valid.Add(input);
                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, input.DateTime.ToString("MM/dd/yyyy")));
            }

            context.AddRange(valid);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));

            var customerTicketsDto = (CustomerDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var validCustomers = new List<Customer>();

            var sb = new StringBuilder();

            foreach (var ct in customerTicketsDto)
            {
                var ticketPrice = ct.Tickets.Select(x => x.Price).FirstOrDefault();
                var projectionId = ct.Tickets.Select(x => x.ProjectionId).FirstOrDefault();

                var projection = context.Projections.Find(projectionId);

                if (ct.FirstName == null || ct.FirstName.Length < 3 || ct.FirstName.Length > 20 || ct.LastName == null || ct.LastName.Length < 3 || ct.LastName.Length > 20 || ct.Age < 12 || ct.Age > 110 || ct.Balance < 0.01M || ticketPrice < 0.01M || projection == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var tickets = new List<Ticket>();

                for (int i = 0; i < ct.Tickets.Count(); i++)
                {
                   // var customerId = context.Customers.Where(x => x.FirstName == ct.FirstName && x.LastName == ct.LastName).Select(x => x.Id).FirstOrDefault();

                    var ticket = new Ticket
                    {
                        Price = ticketPrice,
                        ProjectionId = projectionId,
                        //CustomerId = customerId
                    };

                    tickets.Add(ticket);
                }

                var customer = new Customer
                {
                    FirstName = ct.FirstName,
                    LastName = ct.LastName,
                    Age = ct.Age,
                    Balance = ct.Balance,
                    Tickets = tickets
                };

                validCustomers.Add(customer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count()));
            }

            context.AddRange(validCustomers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
    }
}