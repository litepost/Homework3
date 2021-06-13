using System.Collections.Generic;
using System;

namespace hw3.code {
    class Program {

      public struct backpack {
          public List<int> items;
          public int totalValue;

          public backpack(int initialValue) {
              items = new List<int>();
              totalValue = initialValue;
          }

            public override string ToString() {
                string output = "";

                foreach (int item in items) {
                    if (output.Equals("")) 
                        output = item.ToString();
                    else
                        output += $", {item}";
                }
                return "[" + output + "]";
            }
  
      }

        public static int BruteForceStringMatch(string text, string pattern) {
            if (pattern is null || text is null)
                throw new ArgumentException("The string and substring cannot be null.");
            if (text.Trim().Equals("") || pattern.Trim().Equals(""))
                throw new ArgumentException("The string and substring cannot be empty strings.");


            int tLength = text.Length;
            int pLength = pattern.Length;
            int j = 0;

            for (int i = 0; i <= tLength - pLength; i++) {
                j = 0;
                while (j < pLength && pattern[j].Equals(text[i+j])) {
                    j++;
                }
                if (j == pLength) return i;
            }
            return -1;
        }

        public static backpack Knapsack(int[] weights, int[] values, int maxWeight) {                
            int wLength = weights.Length;
            int subWeight = 0;
            int subTotal = 0;
            List<int> items = new List<int>();
            backpack bp = new backpack(0);

            if (wLength != values.Length)
                throw new ArgumentException("weights and values must have the same number of elements");
            if (maxWeight < 0)
                throw new ArgumentException("The max weight must be nonnegative.");


            for (int i = 0; i < wLength; i++) {                        
                items.Clear();
                if (weights[i] <= maxWeight) {
                    subWeight = weights[i];
                    subTotal = values[i];
                    items.Add(weights[i]);
                }
                else {
                    subWeight = 0;
                    subTotal = 0;
                }

                for (int j = i+1; j < wLength; j++) {
                    if (subWeight + weights[j] <= maxWeight) {
                        subWeight += weights[j];
                        subTotal += values[j];
                        items.Add(weights[j]);
                    }
                }                
                if (subTotal > bp.totalValue) {
                    List<int> deepCopy = new List<int>(items);
                    bp.items = deepCopy;
                    bp.totalValue = subTotal;
                }
            }

            return bp;
        }
        static void Main(string[] args) {
            string[] text = new string[] {"Happy happy joy joy", "Where is the dog?", "fun fun fun", "I love coding!"};
            string[] pattern = new string[] {"happy", "cat", "fun", "Me too!"};

            /**************** string match ****************/
            for (int i = 0; i < text.Length; i++) {
                try {
                    int index = BruteForceStringMatch(text[i], pattern[i]);
                    Console.WriteLine($"string: {text[i], -30}substring: {pattern[i], -20}position: {index}");
                }
                catch (Exception e) {
                    System.Console.WriteLine(e.Message);;
                }
            }

            System.Console.WriteLine("");

            /**************** knapsack ****************/
            int[] w = new int[] {7, 3, 4, 5};
            //int[] w = new int[] {1, 3, 4, 5};
//            int[] w = new int[] {70, 30, 40, 5};
//            int[] w = new int[] {7, 30, 40, 50};
//            int[] w = new int[] {70, 30, 40, 50};
            int[] d = new int[] {42, 12, 40, 25};

            backpack knapsack = Knapsack(w, d, 10);
            Console.WriteLine($"Total value in the knapsack is ${knapsack.totalValue}");
            Console.WriteLine($"Its weights are {knapsack.ToString()}");
        }
    }
}
