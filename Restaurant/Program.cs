namespace Project
{
    class Start
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.ShowMenu();
            Console.ReadKey();
        }
    }

    class Restaurant
    {
        private List<Table> _tables = new List<Table>();
        private Queue<Customer> _customers = new Queue<Customer>();
        

        private Menu menu = new Menu();

        public Restaurant(int tableCount)
        {
            Random random = new Random();
            for(int i = 0; i < tableCount; i++)
                _tables.Add(new Table(15));
        }

        public void OpenRestaurant()
        {
            
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

        public void ShowMenu()
        {
            for(int i =1; i<10; i++)
                Console.WriteLine(dishes[i]);
        }
    }

    class Table
    {
        public bool IsReserved
        {
            get => _minutesRemaining > 0;
        }
        private Customer _customer;
        private int _minutesRemaining;
        
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
        }
    }
}