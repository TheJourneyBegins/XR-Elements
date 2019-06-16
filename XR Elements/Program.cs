using System;

namespace XRElements
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter quantity and product code");
            string inputText = Console.ReadLine();
            packCalculator pc = new packCalculator(inputText);
            Console.WriteLine(pc.calculatePacks());
            Console.ReadLine();
        }
    }
}
