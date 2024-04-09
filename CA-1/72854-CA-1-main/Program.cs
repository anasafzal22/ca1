using System;

class GaragePark
{
    private const double MinimumFee = 2.00;
    private const double MaximumTotalCharge = 10.00;
    private double hourlyFee;
    private int totalCars;
    public double TotalReceipts { get; private set; }

    public GaragePark(double hourlyFee, int totalCars)
    {
        this.hourlyFee = hourlyFee;
        this.totalCars = totalCars;
        TotalReceipts = 0;
    }

    public double CalculateCharges(double hours)
    {
        if (hours <= 3)
        {
            return MinimumFee;
        }
        else
        {
            double fee = MinimumFee + ((Math.Ceiling(hours) - 3) * hourlyFee);
            return fee > MaximumTotalCharge ? MaximumTotalCharge : fee;
        }
    }

    public void CarProcessing()
    {
        Random random = new Random();
        for (int i = 0; i < totalCars; i++)
        {
            string registrationNumber = GenerateRegistrationNumber(random);
            double hoursParked = random.NextDouble() * 24; // Random double between 0 and 24
            double charge = CalculateCharges(hoursParked);
            TotalReceipts += charge;
            Console.WriteLine($"Registration: {registrationNumber}, Hours Parked: {hoursParked:F2}, Charge: ${charge:F2}");
        }
    }

    private static string GenerateRegistrationNumber(Random random)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] registrationArray = new char[7];
        for (int i = 0; i < registrationArray.Length; i++)
        {
            registrationArray[i] = chars[random.Next(chars.Length)];
        }
        return new string(registrationArray).ToLower();
    }
}

class Program
{
    static void Main()
    {
        GaragePark[] garageArr = {
            new GaragePark(0.50, 10),
            new GaragePark(0.60, 6),
            new GaragePark(0.75, 8)
        };

        double grandTotal = 0;
        for (int i = 0; i < garageArr.Length; i++)
        {
            Console.WriteLine($"Processing Car Park {i + 1}:");
            garageArr[i].CarProcessing();
            Console.WriteLine($"Total Receipts for Garage {i + 1}: ${garageArr[i].TotalReceipts:F2}\n");
            grandTotal += garageArr[i].TotalReceipts;
        }

        Console.WriteLine($"Grand Total for All Garages: ${grandTotal:F2}");
    }
}