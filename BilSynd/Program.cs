using System.Globalization;

namespace BilSynd
{
    //TODO Tell customer that a full list of cars every time menu is shown is overkill.

    internal class Program
    {
        static Random random = new Random();
        //static Person[] people = new Person[2];
        //static List<Person> peopleList = new List<Person>();
        static List<Car> carList = new List<Car>();
        static List<Car> defectCarList = new List<Car>();

        static void Main(string[] args)
        {
            DefectCars();
            while (true) { Menu(); }
        }

        #region Menu
        static void Menu()
        {
            Console.WriteLine("\n*** Menu ***\n");
            Console.WriteLine("Select 1 for Show all cars");
            Console.WriteLine("Select 2 for Create car");
            Console.WriteLine("Select 3 for Search car");
            Console.WriteLine("Select 4 for Gen LP");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowCars();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SomethingCar();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    SearchCar();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    for (int i = 0; i < 100; i++) Console.WriteLine(GenerateRandomLicensePlate());
                    break;
                default:
                    Console.WriteLine("Not understood");
                    break;
            }
        }

        private static void SearchCar()
        {
            Console.WriteLine("Søg på nummerplade: ");
            string input = Console.ReadLine();

            foreach (var car in carList)
                if (input == car.LicensePlate)
                {
                    ShowCustomer(car.Owner);
                }

            
        }

        static void ShowCustomer(Person owner)
        {
            Console.WriteLine($"Owner: {owner.Firstname} {owner.Lastname} \tPhone number: {owner.PhoneNumber}");
        }

        private static void SomethingCar()
        {
            Person owner = CreatePerson();
            Car car = CreateCar();
            car.Owner = owner;
            carList.Add(car);

            //Variable   Condition           True                 False
            string str = NeedInspection(car) ? "Bilen skal synes" : "Bilen skal IKKE synes";
            Console.WriteLine(str);

            string? str2 = IsCarDefect(car);
            if (str2 != null) Console.WriteLine("Bilen har følgende fabriksfejl: " + str2);
        }
        #endregion

        static string GenerateRandomLicensePlate()
        {
            char a1 = (char)(random.Next(25) + 65);
            char a2 = (char)(random.Next(25) + 65);
            string b = random.Next(100).ToString("00");
            string c = random.Next(1000).ToString("000");
            return a1.ToString() + a2.ToString() + " " + b + " " + c;
        }

        static void ShowCars()
        {
            Console.WriteLine("*** List of cars ***");
            foreach (Car car in carList)
            {
                ShowCar(car);
            }
        }

        static bool NeedInspection(Car car)
        {
            //If car year + 5 is more than now year, then no inspection
            if (car.DateofRegistration.AddYears(5) >= DateTime.Now) return false;
            //If last inspeciton + 2 is more than now year, then no inspection
            if (car.LastInspection.AddYears(2) >= DateTime.Now) return false;
            return true;
        }

        static void ShowCar(Car car)
        {
            Console.WriteLine($"\nCar: {car.Brand} {car.Model} \tLicense plate: {car.LicensePlate}");
            Console.WriteLine($"Reg.Date: {car.DateofRegistration.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)} \tLast inspection {car.LastInspection.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)}");
        }

        static Car CreateCar()
        {
            Console.WriteLine("\n***Create Car Menu***\n");

            Car car = new Car();
            car.DateofRegistration = RandomDay(1990);
            car.ModelYear = car.DateofRegistration.Year - random.Next(2);
            car.LastInspection = RandomDay(car.ModelYear);
            car.EngineSize = 1 + random.Next(10) / 10f;
            car.LicensePlate = GenerateRandomLicensePlate();

            Console.Write("Brand: ");
            car.Brand = Console.ReadLine();
            Console.Write("Model: ");
            car.Model = Console.ReadLine();

            return car;
        }

        static DateTime RandomDay(int year)
        {
            DateTime start = new DateTime(year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        static Person CreatePerson()
        {
            Console.WriteLine("\n*** Create Customer Menu ***\n");

            Person person = new Person();
            Console.Write("First name: ");
            person.Firstname = Console.ReadLine();
            Console.Write("Last name: ");
            person.Lastname = Console.ReadLine();
            Console.Write("Telephone number: ");
            person.PhoneNumber = Console.ReadLine();
            return person;
        }

        static string? IsCarDefect(Car car)
        {
            foreach (var defectCar in defectCarList)
            {
                if (car.Brand == defectCar.Brand &&
                    car.Model == defectCar.Model &&
                    car.ModelYear <= defectCar.ModelYear) return defectCar.ManufacturingDefects;
            }
            return null;
        }

        static void DefectCars()
        {
            Car car1 = new Car() { Brand = "Alfa Romeo", Model = "G", ModelYear = 2022, ManufacturingDefects = "Wheels are square" };
            Car car2 = new Car() { Brand = "Lamborghini", Model = "Countach", ModelYear = 1986, ManufacturingDefects = "Wiper is baaad!" };
            defectCarList.Add(car1);
            defectCarList.Add(car2);
        }
    }
}