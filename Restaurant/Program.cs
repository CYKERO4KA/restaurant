namespace Project
{
    class Start
    {
        static void Main(string[] args)
        {
            Restaurant restaurant = new Restaurant(7);
            restaurant.OpenRestaurant();
        }
    }

    class Restaurant
    {
        private List<Table> _tables = new List<Table>();
        private Queue<Customer> _customers = new Queue<Customer>();
        private int _money;
        

        private Menu menu = new Menu();

        public Restaurant(int tableCount)
        {
            Random random = new Random();
            for (int i = 0; i < tableCount; i++)
            {
                _tables.Add(new Table(random.Next(5, 15)));
            }
            CreateNewCustomer(25, random);
        }

        public void CreateNewCustomer(int count, Random random)
        {
            for (int i = 0; i < count; i++)
            {
                _customers.Enqueue(new Customer(random.Next(100,250)));
            }
        }

        public void OpenRestaurant()
        {
            while (_customers.Count > 0)
            {
                Random random = new Random();
                Customer newCustomer = _customers.Dequeue();
                Console.WriteLine($"Balance of restaurant: {_money}$. Wait for a new Client");
                Console.WriteLine($"You have a new client, he wants to buy {menu.dishes[random.Next(1,10)]}");
                ShowAllTablesStates();

                Console.Write("\nYou advice him table: ");
                string userInput = Console.ReadLine();
                if (Int32.TryParse(userInput, out int tableNumber))
                {
                    tableNumber -= 1;

                    if (tableNumber >= 0 && tableNumber < _tables.Count)
                    {
                        if (_tables[tableNumber].IsReserved)
                        {
                            Console.WriteLine("You tried to pick a client, but here is no empty place. He argued and leave your restaurant! ");
                        }
                        else
                        {
                            if (newCustomer.CheckSolvency(_tables[tableNumber]))
                            {
                                Console.WriteLine($"Client pay computer cost. And sit at the table {tableNumber+1}");
                                _money = newCustomer.Pay();
                                _tables[tableNumber].BecomeReserved(newCustomer);
                            }
                            else
                            {
                                Console.WriteLine("Client can't pay money!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can't replace client! He argued you and leave your restaurant!");
                    }
                }
                else
                {
                    CreateNewCustomer(1, new Random());
                    Console.WriteLine("Try again!");
                }
                
                Console.WriteLine("Press any key, to talk with a new client");
                Console.ReadKey();
                Console.Clear();
                SpendOneMinute();
            }
        }

        public void ShowAllTablesStates()
        {
            Console.WriteLine("\nList of all tables: ");
            for (int i = 0; i < _tables.Count; i++)
            {
                Console.Write(i +1+" - ");
                _tables[i].ShowInfo();
            }
        }

        private void SpendOneMinute()
        {
            foreach (var table in _tables)
            {
                table.SpendOneMinute();
            }
        }
    }

    class Customer
    {
        private int _money;
        private int _moneyToPay;
        public int DesiredMinutes = 10;

        public Customer(int money)
        {
            _money = money;
        }
        public bool CheckSolvency(Table table)
        {
            _moneyToPay = DesiredMinutes * table.PricePerMinute;
            if (_money >= _moneyToPay)
            {
                return true;
            }
            else
            {
                _moneyToPay = 0;
                return false;
            }
        }

        public int Pay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }
    }

    class Menu
    {
        public Dictionary<int,string> dishes = new Dictionary<int,string>()
        {
            {1,"Cesar"},
            {2,"Pasta"},
            {3,"Sushi"},
            {4,"Coffee"},
            {5,"Tea"},
            {6,"Steak"},
            {7,"Fry potato"},
            {8,"Ice-cream"},
            {9,"Hamburger"}
        };
        
    }

    class Table
    {
        private Customer _customer;
        private int _minutesRemaining;
        public bool IsReserved
        {
            get => _minutesRemaining > 0;
        }
        
        public int PricePerMinute { get; private set; }

        public Table(int pricePerMinute)
        {
            PricePerMinute = pricePerMinute;
        }

        public void BecomeReserved(Customer customer)
        {
            _customer = customer;
            _minutesRemaining = _customer.DesiredMinutes;
        }

        public void BecomeEmpty()
        {
            _customer = null;
        }

        public void SpendOneMinute()
        {
            _minutesRemaining--;
        }

        public void ShowInfo()
        {
            if(IsReserved)
                Console.WriteLine($"This table is reserved, wait: {_minutesRemaining} minute");
            else
                Console.WriteLine($"Table isn't taken, it costs: {PricePerMinute}$ ");
        }
    }
}