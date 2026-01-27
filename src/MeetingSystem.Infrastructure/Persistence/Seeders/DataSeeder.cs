using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Companies.CreateCompany;
using MeetingSystem.Application.Reservations.Create;
using MeetingSystem.Application.Resources.AddMeetingRoom;
using MeetingSystem.Application.Resources.AddParkingSpot;
using MeetingSystem.Application.Users.Register;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.Persistence.Seeders
{
    public sealed class DataSeeder : IDataSeeder
    {
        private readonly IDispatcher _dispatcher;
        private readonly IApplicationDbContext _dbContext;

        public DataSeeder(IDispatcher dispatcher, IApplicationDbContext dbContext)
        {
            _dispatcher = dispatcher;
            _dbContext = dbContext;
        }

        public async Task SeedAsync(CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.AnyAsync(cancellationToken)) return;

            Console.WriteLine("🌱 Seeding: Vytvářím uživatele...");

            var managerEmail = "manager@firma.cz";
            await _dispatcher.Send(new RegisterUserCommand(managerEmail, "Jan", "Novák", "Password123!"), cancellationToken);
            await _dispatcher.Send(new RegisterUserCommand("admin@firma.cz", "Admin", "System", "Password123!"), cancellationToken);

            var managerUser = await _dbContext.Users.FirstAsync(u => u.Email == managerEmail, cancellationToken);
            var managerId = managerUser.Id;

            Console.WriteLine("🌱 Seeding: Vytvářím společnosti, místnosti a parkování...");

            var companyIds = new List<Guid>();

            
            var brnoCmd = new CreateCompanyCommand(
                managerId,
                "Coworking Club Brno",
                "Moderní sdílené kanceláře v centru města s podzemním parkováním.",
                null,
                "Kounicova",
                "6",
                "Brno",
                "Jihomoravský kraj"
            );
            var brnoRes = await _dispatcher.Send(brnoCmd, cancellationToken);

            if (brnoRes.IsSuccess)
            {
                var brnoId = brnoRes.Data;
                companyIds.Add(brnoId);

                await _dispatcher.Send(new AddMeetingRoomCommand(
                    managerId,
                    "Zasedací místnost 'Vision'",
                    brnoId,
                    "Velká místnost s 4K projektorem a video-konferenčním systémem.",
                    500.0m, 
                    null,
                    12,     
                    new List<string> { "4K Projektor", "Video-konference", "Klimatizace", "Odhození hluku" }
                ), cancellationToken);

                await _dispatcher.Send(new AddMeetingRoomCommand(
                    managerId,
                    "Interview Room A",
                    brnoId,
                    "Malá tichá místnost ideální pro pohovory nebo 1-on-1 schůzky.",
                    200.0m,
                    null,
                    4,
                    new List<string> { "Whiteboard", "Soukromí" }
                ), cancellationToken);

                await _dispatcher.Send(new AddParkingSpotCommand(
                    managerId,
                    "VIP Stání A1",
                    brnoId,
                    "Vyhrazené kryté stání v podzemní garáži, blízko výtahu.",
                    100.0m, 
                    null,
                    1,      
                    true    
                ), cancellationToken);
            }

            
            var ostravaCmd = new CreateCompanyCommand(
                managerId,
                "OSU Inovační Inkubátor",
                "Technologické centrum pro studenty a startupy v Ostravě-Porubě.",
                null,
                "17. listopadu",
                "15",
                "Ostrava-Poruba",
                "Moravskoslezský kraj"
            );
            var ovaRes = await _dispatcher.Send(ostravaCmd, cancellationToken);

            if (ovaRes.IsSuccess)
            {
                var ovaId = ovaRes.Data;
                companyIds.Add(ovaId);

                var roomRes = await _dispatcher.Send(new AddMeetingRoomCommand(
                    managerId,
                    "Přednáškový sál 'Nikola Tesla'",
                    ovaId,
                    "Prostorný sál pro workshopy a přednášky.",
                    800.0m,
                    null,
                    50,
                    new List<string> { "Ozvučení", "2x Projektor", "Mikrofon", "Řadové sezení" }
                ), cancellationToken);

                await _dispatcher.Send(new AddParkingSpotCommand(
                    managerId,
                    "Venkovní stání P2",
                    ovaId,
                    "Parkování na venkovní ploše za budovou.",
                    20.0m, 
                    null,
                    1,
                    false 
                ), cancellationToken);

                if (roomRes.IsSuccess)
                {
                    await _dispatcher.Send(new CreateReservationCommand(
                        roomRes.Data, 
                        managerId,
                        DateTime.UtcNow.AddDays(2).AddHours(9), 
                        DateTime.UtcNow.AddDays(2).AddHours(12),
                        "Workshop: Úvod do .NET 10",
                        new List<string> { "student1@osu.cz", "student2@osu.cz" }
                    ), cancellationToken);
                }
            }



            for (int i = 3; i <= 20; i++)
            {
                var isOstrava = i % 2 == 0;
                var mesto = isOstrava ? "Ostrava" : "Brno";

                var street = isOstrava ? "Stodolní" : "Masarykova";

                var loopCmd = new CreateCompanyCommand(
                    managerId,
                    $"IT Solutions {mesto} {i}",
                    $"Pobočka číslo {i}. Poskytujeme cloudové služby.",
                    null,
                    street,
                    $"{i * 10}",
                    mesto,
                    "Česká republika"
                );

                var loopRes = await _dispatcher.Send(loopCmd, cancellationToken);

                if (loopRes.IsSuccess && i % 3 == 0)
                {
                    await _dispatcher.Send(new AddParkingSpotCommand(
                        managerId,
                        $"Parkování pro hosty {i}",
                        loopRes.Data,
                        "Standardní parkovací místo.",
                        50.0m,
                        null,
                        1,
                        false
                    ), cancellationToken);
                }
            }

            Console.WriteLine("Seeding dokončen: Data pro Brno i Ostravu jsou připravena!");
        }
    }
}
