using System;
using System.Collections.Generic;
using System.Linq;

// Клас, що представляє автомобіль (може використовуватися як тип T або як база)
public class Car
{
    public string LicensePlate { get; set; } // Державний номер
    public string Model { get; set; }        // Модель авто
    public string Owner { get; set; }        // Власник

    public Car(string licensePlate, string model, string owner)
    {
        LicensePlate = licensePlate;
        Model = model;
        Owner = owner;
    }

    public override string ToString()
    {
        return $"Авто: {Model}, Номер: {LicensePlate}, Власник: {Owner}";
    }
}

// Узагальнений клас паркування
public class Parking<T> where T : Car
{
    private List<T> parkedCars;
    public int Capacity { get; private set; } // Максимальна місткість парковки

    public Parking(int capacity)
    {
        Capacity = capacity;
        parkedCars = new List<T>();
    }

    // Метод додавання авто на парковку
    public bool AddCar(T car)
    {
        if (parkedCars.Count >= Capacity)
        {
            Console.WriteLine("Помилка: На парковці немає вільних місць!");
            return false;
        }

        // Перевірка, чи авто з таким номером вже припарковане
        if (parkedCars.Any(c => c.LicensePlate.Equals(car.LicensePlate, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"Помилка: Авто з номером {car.LicensePlate} вже на парковці.");
            return false;
        }

        parkedCars.Add(car);
        Console.WriteLine($"Успішно припарковано: {car}");
        return true;
    }

    // Метод пошуку автомобіля за номером
    public T FindByLicensePlate(string licensePlate)
    {
        return parkedCars.FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
    }

    // Метод перевірки кількості вільних місць
    public int GetFreeSpaces()
    {
        return Capacity - parkedCars.Count;
    }

    // Додатковий метод для виведення всіх припаркованих машин
    public void ShowParkedCars()
    {
        Console.WriteLine($"--- Список авто на парковці (Зайнято: {parkedCars.Count}/{Capacity}) ---");
        if (parkedCars.Count == 0)
        {
            Console.WriteLine("Парковка порожня.");
            return;
        }

        foreach (var car in parkedCars)
        {
            Console.WriteLine(car);
        }
    }
}

// Демонстрація роботи програми
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо парковку місткістю 3 автомобілі
        Parking<Car> parking = new Parking<Car>(3);

        // Перевірка вільних місць
        Console.WriteLine($"Вільних місць: {parking.GetFreeSpaces()}\n");

        // Додаємо автомобілі
        parking.AddCar(new Car("AA1234BB", "Toyota Camry", "Олександр"));
        parking.AddCar(new Car("BC5678EE", "BMW X5", "Марія"));

        Console.WriteLine($"\nВільних місць після додавання двох авто: {parking.GetFreeSpaces()}\n");

        // Спроба додати ще одне авто
        parking.AddCar(new Car("KA9999OO", "Audi A6", "Іван"));

        // Спроба додати перевищення ліміту (на парковку місткістю 3)
        parking.AddCar(new Car("AI0001XX", "Skoda Octavia", "Петро"));

        Console.WriteLine();
        parking.ShowParkedCars();

        // Пошук авто за номером
        Console.WriteLine("\n--- Пошук автомобіля ---");
        string searchPlate = "BC5678EE";
        var foundCar = parking.FindByLicensePlate(searchPlate);

        if (foundCar != null)
        {
            Console.WriteLine($"Знайдено! -> {foundCar}");
        }
        else
        {
            Console.WriteLine($"Авто з номером {searchPlate} не знайдено.");
        }
    }
}