using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad7i8
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => LetsSayUserClickedAButtonOnGuiMethodAsync());

            Console.Read();
        }

        private static async Task LetsSayUserClickedAButtonOnGuiMethodAsync()
        {
            var resultTask = Task.Run(() => GetTheMagicNumberAsync());
            Console.WriteLine(await resultTask);
        }

        private static async Task<int> GetTheMagicNumberAsync()
        {
            var resultTask = Task.Run( () => IKnowIGuyWhoKnowsAGuyAsync());
            return await resultTask;
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuyAsync()
        {
            var resultTask1 = Task.Run(() => IKnowWhoKnowsThisAsync(10));
            var resultTask2 = Task.Run(() => IKnowWhoKnowsThisAsync(5));
            return (await resultTask1 + await resultTask2);
        }

        private static async Task<int> IKnowWhoKnowsThisAsync(int n)
        {
            var resultTask = Task.Run(() => FactorialDigitSumAsync(n));
            return await resultTask;
        }


        private static async Task<int> FactorialDigitSumAsync(int n)
        {
            var resultsTask = Task.Run(() => ComputeFactDigitSum(n));
            return await resultsTask;
        }

        private static int ComputeFactDigitSum(int n)
        {
            if (n < 2)
            {
                return 1;
            }

            int fact = 1;
            int sum = 0;

            for (int i = 2; i <= n; i++)
            {
                fact *= i;
            }

            string number = fact.ToString();
            for (int i = 0; i < number.Length; i++)
            {
                sum += Int32.Parse(number[i].ToString());
            }
            return sum;
        }
    }
}
