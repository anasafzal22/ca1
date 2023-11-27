// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using Bogus;

class CarParking
{
    private const decimal MinimumFee = 2.00m;
    private const decimal MaximumCharge = 10.00m;
    private decimal hourlyRate;
    private int totalCars;
    public decimal TotalReceipts { get; private set; }

    

    public decimal CalculateCharges(decimal hoursParked)
    {
        if (hoursParked <= 3)
        {
            return MinimumFee;
        }
        else
        {
            decimal fee = MinimumFee + ((Math.Ceiling(hoursParked) - 3) * hourlyRate);
            return fee > MaximumCharge ? MaximumCharge : fee;
        }
    }
    public CarParking(decimal hourlyRate, int totalCars)
    {
        this.hourlyRate = hourlyRate;
        this.totalCars = totalCars;
        TotalReceipts = 0;
    }
    public void ProcessCars()
    {
        var faker = new Faker();
        for (int i = 0; i < totalCars; i++)
        {
            string registrationNumber = faker.Random.AlphaNumeric(7).ToLower();
            decimal hoursParked = faker.Random.Decimal(0, 24);
            decimal charge = CalculateCharges(hoursParked);
            TotalReceipts += charge;
            Console.WriteLine($"Registration: {registrationNumber}, Hours Parked: {hoursParked:F2}, Charge: €{charge:F2}");
        }
    }
}

class Program
{
    static void Main()
    {
        CarParking[] parkingGarages = {
            new CarParking(0.50m, 10),
            new CarParking(0.60m, 6),
            new CarParking(0.75m, 8)
        };

        decimal grandTotal = 0;
        for (int i = 0; i < parkingGarages.Length; i++)
        {
            Console.WriteLine($"Processing Car Park {i + 1}:");
            parkingGarages[i].ProcessCars();
            Console.WriteLine($"Total Receipts for Garage {i + 1}: €{parkingGarages[i].TotalReceipts:F2}\n");
            grandTotal += parkingGarages[i].TotalReceipts;
        }

        Console.WriteLine($"Grand Total for All Garages: €{grandTotal:F2}");
    }
}