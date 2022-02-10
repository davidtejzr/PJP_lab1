using System;

namespace PJP_lab1
{
    internal class Program
    {
        static float sum(string first, string second)
        {
            try
            {
                float res = int.Parse(first) + int.Parse(second);
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static float dif(string first, string second)
        {
            try
            {
                float res = int.Parse(first) - int.Parse(second);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static float div(string first, string second)
        {
            try
            {
                float res = int.Parse(first) / int.Parse(second);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static float mul(string first, string second)
        {
            try
            {
                float res = int.Parse(first) * int.Parse(second);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        static void compute(string expr)
        {
            Console.WriteLine("Nacteny vyraz: {0}\n", expr);
            string first = string.Empty;
            string second = string.Empty;
            string oper = string.Empty;
            int iter = 0;
            bool reachedEnd = false;
            expr = expr.Replace(" ", string.Empty);

            while (true)
            {
                //read two numbers + operator
                while (/*iter != expr.Length*/true)
                {
                    //first number
                    if (((expr[iter] >= '0') && (expr[iter] <= '9')) && (first == string.Empty))
                    {
                        while ((expr[iter] >= '0') && (expr[iter] <= '9'))
                        {
                            first += expr[iter++];
                            if (iter >= expr.Length)
                                break;
                        }
                    }

                    //second number
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
                            return;
                        }
                    }

                    //next
                    else if (((oper == "+") || (oper == "-")) && ((expr[iter] == '*') || (expr[iter] == '/')))
                    {
                        first = second;
                        second = string.Empty;
                        oper = expr[iter].ToString();
                        iter++;
                        if (iter >= expr.Length)
                        {
                            Console.WriteLine("ERROR - Unexpected end");
                            return;
                        }
                    }

                    //solve
                    else
                    {
                        float res = 0;
                        if (oper == "+")
                            res = sum(first, second);
                        else if (oper == "-")
                            res = dif(first, second);
                        else if (oper == "*")
                            res = mul(first, second);
                        else if (oper == "/")
                            res = div(first, second);

                        if (res == -1)
                        {
                            Console.WriteLine("ERROR - Bad expression");
                            return;
                        }

                        Console.WriteLine("{0} {1} {2} {3}", first, oper, second, res);
                        string resStr = first + oper + second;

                        //nahrazeno vyrazu vypoctem
                        expr = expr.Replace(resStr, res.ToString());

                        first = string.Empty;
                        second = string.Empty;
                        oper = string.Empty;
                        iter = 0;

                    }

                }
            }
        }


        static void Main(string[] args)
        {
            string expr;
            while (true)
            {
                Console.WriteLine("Zadej vyraz:");
                expr = Console.ReadLine();
                if (expr != null)
                {
                    compute(expr);
                }
            }
        }
    }
}