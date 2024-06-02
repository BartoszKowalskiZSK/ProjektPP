using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineHotelBooking.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

public class RentUpdateService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public RentUpdateService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var rents = dbContext.Rents.ToList(); // Pobierz wszystkie wynajmy

                foreach (var rent in rents)
                {
                    if (rent.To < DateTime.Today) // Jeśli data zakończenia wynajmu jest wcześniejsza niż dzisiaj
                    {
                        rent.IsActive = false; // Ustaw pole IsActive na false
                    }
                }

                await dbContext.SaveChangesAsync(); // Zapisz zmiany w bazie danych
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Czekaj 24 godziny przed kolejnym sprawdzeniem
        }
    }
}