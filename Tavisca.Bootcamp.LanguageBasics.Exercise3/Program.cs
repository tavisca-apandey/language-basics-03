using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int[] calorie=new int[protein.Length];
            int[] finalD=new int[dietPlans.Length];
            int[] multipleSelection=new int[4];
            int[][] selection=new int[4][];
            
            //Total Calorie for a Diet
            for(int index=0;index<protein.Length;index++)
                calorie[index] = fat[index] * 9 + 5*(carbs[index] + protein[index]);
    
            string plan="";
            int indexSelection,tempIndex;
            for(int index=0;index<dietPlans.Length;index++)
            {
                plan=dietPlans[index];
                for(indexSelection=0;indexSelection<4;indexSelection++)
                if(indexSelection<plan.Length)
                {
                    multipleSelection[indexSelection] = char.IsLower(plan[indexSelection]) ? 1 : -1;
                    switch(char.ToLower(plan[indexSelection]))
                    {
                        case 'c': 
                        {
                            selection[indexSelection] = carbs;
                            break;
                        }
                        case 'p':
                        {
                            selection[indexSelection] = protein;
                            break;
                        }
                        case 'f':
                        {
                            selection[indexSelection] = fat;
                            break;
                        }
                        case 't':
                        {
                            selection[indexSelection] = calorie;
                            break;
                        }
                    }
                }
                else
                {
                    multipleSelection[indexSelection]=0;
                    selection[indexSelection]=null;
                }
        
                int best=0;
                for(indexSelection=0;indexSelection<protein.Length;indexSelection++)
                {
                    int b=best;
                    for(tempIndex=0;tempIndex<4;tempIndex++)
                    {
                        if(multipleSelection[tempIndex]==0)
                        {
                            best = Math.Min(indexSelection,b);
                            break;
                        }
                        if(multipleSelection[tempIndex]*selection[tempIndex][indexSelection].CompareTo(selection[tempIndex][b])<0)
                        {
                            best=indexSelection;
                            break;
                        }
                        if(multipleSelection[tempIndex]*selection[tempIndex][indexSelection].CompareTo(selection[tempIndex][b])>0)
                        {
                            best=b;
                            break;
                        }
                    }
                }
            finalD[index]=best;
            }
        return finalD;
        }
    }
}
/*
Last login: Sun Jun 23 00:38:55 on ttys000
Anikets-MacBook-Pro:~ aniket$ cd /Users/aniket/language-basics-03/Tavisca.Bootcamp.LanguageBasics.Exercise3
Anikets-MacBook-Pro:Tavisca.Bootcamp.LanguageBasics.Exercise3 aniket$ dotnet run
Proteins = [3, 4]
Carbs = [2, 8]
Fats = [5, 2]
Diet plan = [P, p, C, c, F, f, T, t]
PASS
Proteins = [3, 4, 1, 5]
Carbs = [2, 8, 5, 1]
Fats = [5, 2, 4, 4]
Diet plan = [tFc, tF, Ftc]
PASS
Proteins = [18, 86, 76, 0, 34, 30, 95, 12, 21]
Carbs = [26, 56, 3, 45, 88, 0, 10, 27, 53]
Fats = [93, 96, 13, 95, 98, 18, 59, 49, 86]
Diet plan = [f, Pt, PT, fT, Cp, C, t, , cCp, ttp, PCFt, P, pCt, cP, Pc]
PASS

[1]+  Stopped                 dotnet run
Anikets-MacBook-Pro:Tavisca.Bootcamp.LanguageBasics.Exercise3 aniket$ 
*/