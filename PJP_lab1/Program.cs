using System;

namespace PJP_lab1
{
    internal class Program
    {
        static double sum(string first, string second)
        {
            try
            {
                double res = int.Parse(first) + int.Parse(second);
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static double dif(string first, string second)
        {
            try
            {
                double res = int.Parse(first) - int.Parse(second);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static double div(string first, string second)
        {
            try
            {
                double res = int.Parse(first) / int.Parse(second);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static double mul(string first, string second)
        {
            try
            {
                double res = int.Parse(first) * int.Parse(second);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static double pow(string first, string second)
        {
            try
            {
                double res = Math.Pow(double.Parse(first), double.Parse(second));
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static string compute(string expr)
        {
            Console.WriteLine("Expr: {0}", expr);
            string first = string.Empty;
            string second = string.Empty;
            string oper = string.Empty;
            int iter = 0;
            int parCount = 0;
            expr = expr.Replace(" ", string.Empty);

            while (true)
            {
                //nacteni dvou cisel, operatoru a kontrola dalsiho znaku
                while (iter != expr.Length)
                {
                    //prvni cislo
                    if (((expr[iter] >= '0') && (expr[iter] <= '9')) && (first == string.Empty))
                    {
                        while ((expr[iter] >= '0') && (expr[iter] <= '9'))
                        {
                            first += expr[iter++];
                            if (iter >= expr.Length)
                                break;
                        }
                    }

                    //druhe cislo
                    else if (((expr[iter] >= '0') && (expr[iter] <= '9')) && (second == string.Empty))
                    {
                        while ((expr[iter] >= '0') && (expr[iter] <= '9'))
                        {
                            second += expr[iter++];
                            if (iter >= expr.Length)
                            {
                                break;
                            }
                        }
                    }

                    //operator
                    else if (((expr[iter] == '+') || (expr[iter] == '-') || (expr[iter] == '*') || (expr[iter] == '/')) && (oper == string.Empty))
                    {
                        oper = expr[iter++].ToString();
                        if (iter >= expr.Length)
                        {
                            Console.WriteLine("ERROR - Unexpected end");
                            return "ERROR";
                        }

                        if(expr[iter] == '*')
                        {
                            oper = "**";
                        }
                    }

                    //dalsi znak
                    else if (((oper == "+") || (oper == "-")) && ((expr[iter] == '*') || (expr[iter] == '/')))
                    {
                        first = second;
                        second = string.Empty;
                        oper = expr[iter].ToString();
                        iter++;

                        //hot fix pro mocninu
                        if(expr[iter] == '*')
                        {
                            oper = "**";
                        }

                        if (iter >= expr.Length)
                        {
                            Console.WriteLine("ERROR - Unexpected end");
                            return "ERROR";
                        }
                    }

                    //zacatek zavorky
                    else if(expr[iter] == '(')
                    {
                        first = string.Empty;
                        second = string.Empty;
                        oper = string.Empty;

                        parCount++;
                        iter++;
                    }

                    else
                    {
                        iter++;
                    }
                }

                //kontrola konce
                if (second == string.Empty)
                {
                    Console.WriteLine("Result: {0}\n", first);
                    return first;
                }

                //reseni
                double res = 0;
                if (oper == "+")
                    res = sum(first, second);
                else if (oper == "-")
                    res = dif(first, second);
                else if (oper == "*")
                    res = mul(first, second);
                else if (oper == "/")
                    res = div(first, second);
                else if (oper == "**")
                    res = pow(first, second);

                //konci kdyz vysledkem je zaporne cislo a return -1
                if (res < 0)
                {
                    Console.WriteLine("ERROR - Bad expression");
                    return "ERROR";
                }

                //Console.WriteLine("{0} {1} {2} = {3}", first, oper, second, res);
                string resStr = first + oper + second;

                //nahrazeni vyrazu vypoctem
                expr = expr.Replace(resStr, res.ToString());

                //odstraneni zavorky u vyreseneho expr
                if ((parCount > 0) && expr.Contains("(" + res + ")"))
                {
                    expr = expr.Replace("(" + res.ToString() + ")", res.ToString());
                    parCount--;
                }

                //kontrolni vyraz
                //Console.WriteLine("New expr: {0}", expr);


                first = string.Empty;
                second = string.Empty;
                oper = string.Empty;
                iter = 0;

            }
        }


        static void Main(string[] args)
        {
            List<string> results = new List<string>();
            string[] exprs;

            if (args.Length < 1)
            {
                Console.WriteLine("Input file path missing!");
                return;
            }

            try
            {
                exprs = File.ReadAllLines(args[0]);
                for (int i = 1; i <= int.Parse(exprs[0].ToString()); i++)
                {
                    results.Add(compute(exprs[i]));
                }

                if(args.Length > 1)
                {
                    File.WriteAllLines(args[1], results);
                    Console.WriteLine("\nResults saved to file {0}", args[1]);
                }

                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return;
        }
    }
}