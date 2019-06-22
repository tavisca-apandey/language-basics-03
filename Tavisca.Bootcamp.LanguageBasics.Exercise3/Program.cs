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
            int[] M=new int[4];
            int[][] selection=new int[4][];
            
            //Total Calorie for a Diet
            for(int i=0;i<protein.Length;i++)
                calorie[i] = fat[i] * 9 + 5*(carbs[i] + protein[i]);
    
            String plan="";
            int j,k;
            for(int i=0;i<dietPlans.Length;i++)
            {
                plan=dietPlans[i];
                for(j=0;j<4;j++)
                if(j<plan.Length)
                {
                    M[j] = char.IsLower(plan[j]) ? 1 : -1;
                    switch(char.ToLower(plan[j]))
                    {
                        case 'c': 
                        {
                            selection[j] = carbs;
                            break;
                        }
                        case 'p':
                        {
                            selection[j] = protein;
                            break;
                        }
                        case 'f':
                        {
                            selection[j] = fat;
                            break;
                        }
                        case 't':
                        {
                            selection[j] = calorie;
                            break;
                        }
                    }
                }
                else
                {
                    M[j]=0;
                    selection[j]=null;
                }
        
                int best=0;
                for(j=0;j<protein.Length;j++)
                {
                    int b=best;
                    for(k=0;k<4;k++)
                    {
                        if(M[k]==0)
                        {
                            best = Math.Min(j,b);
                            break;
                        }
                        if(M[k]*selection[k][j].CompareTo(selection[k][b])<0)
                        {
                            best=j;
                            break;
                        }
                        if(M[k]*selection[k][j].CompareTo(selection[k][b])>0)
                        {
                            best=b;
                            break;
                        }
                    }
                }
            finalD[i]=best;
            }
        return finalD;
        throw new NotImplementedException();
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