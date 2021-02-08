using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSPLib
{
    public class Lab
    {
        private static readonly int[] points = new[] { 12, 12, 12, 12, 12, 12, 12, 16 };

        private static readonly Random rnd = new Random();


        public static void Part1()
        {
            Console.WriteLine("Частина 1");
            var str = "Дмитренко Олександр - ІП-84; Матвійчук Андрій - ІВ-83; Лесик Сергій - ІО-82; Ткаченко Ярослав - ІВ-83; Аверкова Анастасія - ІО-83; Соловйов Даніїл - ІО-83; Рахуба Вероніка - ІО-81; Кочерук Давид - ІВ-83; Лихацька Юлія- ІВ-82; Головенець Руслан - ІВ-83; Ющенко Андрій - ІО-82; Мінченко Володимир - ІП-83; Мартинюк Назар - ІО-82; Базова Лідія - ІВ-81; Снігурець Олег - ІВ-81; Роман Олександр - ІО-82; Дудка Максим - ІО-81; Кулініч Віталій - ІВ-81; Жуков Михайло - ІП-83; Грабко Михайло - ІВ-81; Іванов Володимир - ІО-81; Востриков Нікіта - ІО-82; Бондаренко Максим - ІВ-83; Скрипченко Володимир - ІВ-82; Кобук Назар - ІО-81; Дровнін Павло - ІВ-83; Тарасенко Юлія - ІО-82; Дрозд Світлана - ІВ-81; Фещенко Кирил - ІО-82; Крамар Віктор - ІО-83; Іванов Дмитро - ІВ-82";

            var studentsGroups = str.Split(';').Select(t => t.Trim()).Select(x => x.Split(new[] { '-' }, 2, StringSplitOptions.None)).Select(t => t.Select(g=>g.Trim()).ToArray()).GroupBy(x => x[1], x => x[0]).OrderBy(k=>k.Key).ToDictionary(x => x.Key, x => x.OrderBy(t=>t).ToList());

            Console.WriteLine("Завдання 1");
            foreach (var p in studentsGroups)
            {
                Console.WriteLine($"{p.Key}: {string.Join(", ", p.Value)}");
            }
            Console.WriteLine();
         
            var studentPoints = studentsGroups.ToDictionary(x => x.Key, x => x.Value.ToDictionary(t => t, t => points.Select(y => RandomValue(y)).ToList()));

            Console.WriteLine("Завдання 2");
            foreach (var p in studentPoints)
            {
                Console.WriteLine($"{p.Key}:");
                foreach (var p2 in p.Value)
                {
                    Console.WriteLine($"{p2.Key}: {string.Join(", ", p2.Value)}");
                }
            }
            Console.WriteLine();
            
            var sumPoints = studentPoints.ToDictionary(x => x.Key, x => x.Value.ToDictionary(t => t.Key, t => t.Value.Sum()));

            Console.WriteLine("Завдання 3");
            foreach (var p in sumPoints)
            {
                Console.WriteLine($"{p.Key}:");
                foreach (var p2 in p.Value)
                {
                    Console.WriteLine($"{p2.Key}: {p2.Value}");
                }
            }
            Console.WriteLine();

            var groupAvg = studentPoints.ToDictionary(x => x.Key, x => (float)x.Value.Select(t => t.Value.Sum()).Average());

            Console.WriteLine("Завдання 4");
            foreach (var p in groupAvg)
            {
                Console.WriteLine($"{p.Key}: {p.Value}");
            }
            Console.WriteLine();

            var passedPerGroup = studentPoints.ToDictionary(x => x.Key, x => x.Value.Where(t => t.Value.Sum() >= 60).Select(t => t.Key).ToList());

            Console.WriteLine("Завдання 5");
            foreach (var p in passedPerGroup)
            {
                Console.WriteLine($"{p.Key}: {string.Join(", ", p.Value)}");
            }
            Console.WriteLine();
        }

        public static void Part2()
        {
            var defaultInit = new CoordinateVI();
            Console.WriteLine($"{defaultInit} ; {defaultInit.ToDecimalString()}");

            var latitude1 = new CoordinateVI(Direction.Latitude, 85, 23, 56);
            var latitude2 = new CoordinateVI(Direction.Latitude, -67, 11, 34);

            try
            {
                var invalidLatitude = new CoordinateVI(Direction.Latitude, 100, 84, 120);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"{latitude1} ; {latitude1.ToDecimalString()}");
            Console.WriteLine($"{latitude2} ; {latitude2.ToDecimalString()}");

            var avgLatitude1 = latitude1.Average(latitude2);
            var avgLatitude2 = CoordinateVI.Average(latitude2, avgLatitude1);

            Console.WriteLine($"{avgLatitude1} ; {avgLatitude1.ToDecimalString()}");
            Console.WriteLine($"{avgLatitude2} ; {avgLatitude2.ToDecimalString()}");

            var longitude1 = new CoordinateVI(Direction.Longitude, -8, 28, 47);
            var longitude2 = new CoordinateVI(Direction.Latitude, 145, 32, 13);

            try
            {
                var invalidLongitude = new CoordinateVI(Direction.Longitude, 200, 84, 120);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"{longitude1} ; {longitude1.ToDecimalString()}");
            Console.WriteLine($"{longitude2} ; {longitude2.ToDecimalString()}");

            var avgLongitude1 = longitude1.Average(longitude2);
            var avgLongitude2 = CoordinateVI.Average(longitude2, avgLongitude1);

            Console.WriteLine($"{avgLongitude1} ; {avgLongitude1.ToDecimalString()}");
            Console.WriteLine($"{avgLongitude2} ; {avgLongitude2.ToDecimalString()}");
        }

        private static int RandomValue(int maxValue)
        {
            switch (rnd.Next(6))
            {
                case 1:
                    return (int)Math.Ceiling(maxValue * 0.7);
                case 2:
                    return (int)Math.Ceiling(maxValue * 0.9);
                case 3:
                case 4:
                case 5:
                    return maxValue;
                default:
                    return 0;
            }
        }
    }
}
