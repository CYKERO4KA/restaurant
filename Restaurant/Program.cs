namespace Project
{
    class Start
    {
        static void Main(string[] args)
        {
            
        }
    }

    class Restaurant
    {
        private List<Tables> _tables;
        private Queue<Customer> _customers;
        private Dictionary<Menu, int> menu;
    }

    class Customer
    {
        private int _money;
        public int ReserveTime { get; private set; }

        public Customer(int reserveTime)
        {
            ReserveTime = reserveTime;
        }
    }

    class Menu
    {
         
    }

    class Tables
    {
        public bool isReserved;
    }
}