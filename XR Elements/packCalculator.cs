using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XRElements
{
    public class packCalculator
    {

        public int orderNumber { get; set; }
        public string productCode { get; set; }

        public string inputDetails { get; set; }
        /*Dictonary contanining pack information for each product code.
        New Products can be added to this list although in production envirnment it is assumed to be stored in a database and the
        It is assumed that the price for a pack containing more numbers is always larger (ie. 5 pack is more expensive than 4 pack)*/
        Dictionary<string, string[]> packListforProducts = new Dictionary<string, string[]>() {
            {"SH3",new string[] { "3@2.99", "5@4.49" } },
            {"YT2",new string[] { "4@4.95", "10@9.95","15@13.95" } },
            {"TR", new string[] {"3@2.95","5@4.45","9@7.99"} }
    };
        public packCalculator(string inputDetails)
        {
            this.inputDetails = inputDetails;
        }

        public string calculatePacks()
        {
            if (!inputValidator(inputDetails))
            {
                return "Input is invalid. Please input string in format 10 SH3";
            }
            string[] inputArray = inputDetails.Split(" ");
            orderNumber = int.Parse(inputArray[0]);
            productCode = inputArray[1];
            if (!packListforProducts.ContainsKey(productCode))
            {
                return "The product code doesn't exist. Please enter a valid product Key";
            }
            List<int> packNumbers = new List<int>();
            List<decimal> priceList = new List<decimal>();
            foreach (string s in packListforProducts[productCode])
            {
                string[] packAndPrice = s.Split('@');
                packNumbers.Add(int.Parse(packAndPrice[0]));
                priceList.Add(decimal.Parse(packAndPrice[1]));

            }
            packNumbers.Sort();
            priceList.Sort();
            packNumbers.Reverse();
            priceList.Reverse();
            return generateOutput(packNumbers,priceList);
        }

        private bool inputValidator(string input)
        {
            Regex regex = new Regex(@"\d\d\s[a-zA-Z0-9_.-]*\b");
            return regex.IsMatch(input);
        }
        private string generateOutput(List<int> packNumbers, List<decimal> priceList)
        {
            string output = $"{orderNumber} {productCode} ${"{0}"}";
            decimal totalPrice = 0.00M;
            int orderNumberLocal = orderNumber;
            int iterator = 0;
            while (iterator< packNumbers.Count && orderNumberLocal !=0) {
                for (int i = iterator; i < packNumbers.Count; i++)
                {
                    int orderNumberForPack = orderNumberLocal / packNumbers[i];
                    if (orderNumberForPack != 0)
                    {
                        output += $"\n {orderNumberForPack} x {packNumbers[i]} ${priceList[i]}";
                        orderNumberLocal = orderNumberLocal % packNumbers[i];
                        totalPrice += orderNumberForPack * priceList[i];
                    }
                }
                if(orderNumberLocal==0)
                {
                    break;
                }
                output = $"{orderNumber} {productCode} ${"{0}"}";
                totalPrice = 0;
                orderNumberLocal = orderNumber;
                iterator++;
            }
            /*Since the price for single unit isn't provided, It is assumed that users aren't allowed to enter quantity
            that cannot be converted into packs. In a website it can be done by only providing buttons that allows users
            to order by clicking buttons that increase quantity by specific number. However I've included a case to check 
            for this demo */
            totalPrice=Math.Round(totalPrice, 2);
            if (orderNumberLocal != 0)
            {
                output = "The input number cannot be converted into packs";
            }
            return string.Format(output,totalPrice);
        }
    }
}
