using System;
using System.Collections.Generic;
using System.Text;

namespace PowerLine
{
    /// <summary>
    /// Тип ТС
    /// </summary>
    public enum VehicleType { Unknown, Car, Truck, SportCar };

    /// <summary>
    /// Базовый тип ТС
    /// </summary>
    public class Vehicle
    {
        private double _averageConsumption; // литров на 1 км (для удобства, вместо 100 км)
        private double _tankVolume; // объём в литрах
        private double _speed; // км/ч

        public Vehicle(double averageConsumption, double tankVolume, double speed)
        {
            _averageConsumption = averageConsumption;
            _tankVolume = tankVolume;
            _speed = speed;
            Type = VehicleType.Unknown;
        }

        public VehicleType Type { get; protected set; }

        /// <summary>
        /// Запас хода для полного бака
        /// </summary>
        /// <returns></returns>
        public double CruisingRange()
        {
            return _tankVolume / _averageConsumption;
        }

        /// <summary>
        /// Запас хода при заданном объеме бака
        /// </summary>
        /// <param name="currentVolume"></param>
        /// <returns></returns>
        public double CruisingRange(double currentVolume)
        {
            return currentVolume / _averageConsumption;
        }

        /// <summary>
        /// Время поездки при заданных условиях
        /// </summary>
        /// <param name="fuelVolume">объем топлива</param>
        /// <param name="distance">расстояние</param>
        /// <returns></returns>
        public TimeSpan JourneyTime(double fuelVolume, double distance)
        {
            double consumption = distance * _averageConsumption;
            if (consumption > fuelVolume)
            {
                return TimeSpan.Zero; // если объема топлива не хватит => (проверка на равенсто Zero) автомобиль не проедет
            }
            double hours = distance / _speed;
            return TimeSpan.FromHours(hours);
        }
    }

    /// <summary>
    /// Легковой автомобиль
    /// </summary>
    public class Car : Vehicle
    {
        private const int _maxPassengerCount = 6; // предустановленное значение (?)
        private int _passengerCount; // фактическое количество пассажиров

        public Car(double averageConsumption, double tankVolume, double speed, int passengerCount) : base(averageConsumption, tankVolume, speed)
        {
            _passengerCount = passengerCount;
            Type = VehicleType.Car;
        }

        /// <summary>
        /// Допустимое ли количество пассажиров
        /// </summary>
        /// <returns></returns>
        public bool IsAcceptableCount()
        {
            return _passengerCount <= _maxPassengerCount;
        }

        /// <summary>
        /// Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            double cruisingRange = CruisingRange();
            for (int i = 0; i < _passengerCount; i++)
            {
                cruisingRange *= 0.94; // уменьшаем на 6%
            }

            return $"Запас хода составляет {cruisingRange} км";
        }
    }

    /// <summary>
    /// Грузовой автомобиль
    /// </summary>
    public class Truck : Vehicle
    {
        private const double _tonnage = 50000; // предустановленная (?) грузоподъемность в кг (для удобства, вместо тонн)
        private double _load; // фактический вес груза в кг

        public Truck(double averageConsumption, double tankVolume, double speed, double load) : base(averageConsumption, tankVolume, speed)
        {
            _load = load;
            Type = VehicleType.Truck;
        }

        /// <summary>
        /// Допустимый ли вес груза
        /// </summary>
        /// <returns></returns>
        public bool IsAcceptableLoad()
        {
            return _load <= _tonnage;
        }

        /// <summary>
        /// Метод для отображения текущей информации о состоянии запаса хода в зависимости от груза
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            double cruisingRange = CruisingRange();
            int cargoStep = (int)Math.Ceiling(_load / 200);
            for (int i = 0; i < cargoStep; i++)
            {
                cruisingRange *= 0.96; // уменьшаем на 4%
            }

            return $"Запас хода составляет {cruisingRange} км";
        }
    }

    /// <summary>
    /// Спортивный автомобиль
    /// </summary>
    public class Sportscar : Vehicle
    {
        public Sportscar(double averageConsumption, double tankVolume, double speed) : base(averageConsumption, tankVolume, speed)
        {
        }
    }
}
