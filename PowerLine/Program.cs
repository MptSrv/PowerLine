using System;

namespace PowerLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            double carConsumption = 8.0 / 100; // переводим с литров на 100 км в литры на 1 км
            double truckConsumption = 27.0 / 100;

            Car car = new Car(carConsumption, 40, 60, 4);
            Truck truck = new Truck(truckConsumption, 250, 50, 9000); // 9 тонн в кг

            Console.WriteLine($"Легковой автомобиль: {car}");
            Console.WriteLine($"Грузовой автомобиль: {truck}");
        }
    }
}
