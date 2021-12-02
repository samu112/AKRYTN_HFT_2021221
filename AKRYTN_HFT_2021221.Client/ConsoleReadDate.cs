using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AKRYTN_HFT_2021221.Client
{
    /// <summary>
    /// UI for date input
    /// </summary>
    static class ConsoleReadDate
    {

        //Used the different date parts(year, month, day) to give back a year based on user input
        public static string GetDate(bool required, DateTime minimumDate, DateTime maximumDate)
        {
            ShowHintMessage();
            //Part 0
            string year = "", month = "", day = "";
            DatePart1(ref year, ref month, ref day, required, minimumDate, maximumDate);
            Console.WriteLine();

            //Return
            if (year == "" && required == false) { return null; } //if not required and cancelled return null
            else { return year + "." + month + "." + day; }       
        }

        private static void ShowHintMessage()
        {
            int cursorXPosition = Console.CursorLeft;
            int cursorYPosition = Console.CursorTop;
            Console.CursorTop++;
            Console.CursorLeft -= 8;
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Format: yyyy.mm.dd.");
            Console.ForegroundColor = defaultColor;
            Console.CursorLeft = cursorXPosition;
            Console.CursorTop = cursorYPosition;
        }

        //Part1:Year
        private static void DatePart1(ref string year, ref string month, ref string day, bool required, DateTime minimumDate, DateTime maximumDate)
        {
            int partNumber = 0;
            year = DatePartBuilder(4, minimumDate.Year, maximumDate.Year, ref partNumber, required);
            if (!(year == "" && required == false))
            {
                partNumber++;
                DatePart2(ref partNumber, ref year, ref month, ref day, required, minimumDate, maximumDate);
            }


        }
        //Part2:Month
        private static void DatePart2(ref int partNumber, ref string year, ref string month, ref string day, bool required, DateTime minimumDate, DateTime maximumDate)
        {
            //Check year, because if it is minimum or maximum, we have to change upper or lower limit
            if (year == maximumDate.Year.ToString())
            {
                month = DatePartBuilder(2, 1, maximumDate.Month, ref partNumber, required);
            }
            else if (year == minimumDate.Year.ToString())
            {
                month = DatePartBuilder(2, minimumDate.Month, 12, ref partNumber, required);
            }
            else
            {
                month = DatePartBuilder(2, 1, 12, ref partNumber, required);
            }


            if (month == "" && partNumber == 0)//It was cancelled, go back to part1
            {
                //Fix cursor and remove previous data
                Console.CursorLeft -= 5;
                Console.Write("     ");
                Console.CursorLeft -= 5;
                //Back to Part1
                DatePart1(ref year, ref month, ref day, required, minimumDate, maximumDate);
            }
            else
            {
                partNumber++;
                DatePart3(ref partNumber, ref year, ref month, ref day, required, minimumDate, maximumDate);
            }

        }
        //Part3:Day
        private static void DatePart3(ref int partNumber, ref string year, ref string month, ref string day, bool required, DateTime minimumDate, DateTime maximumDate)
        {
            int[] daysInMonth;

            //decide if febrauary has 28 or 29 day
            if (double.Parse(year) % 4 == 0)
            {
                daysInMonth = new int[12] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }
            else
            {
                daysInMonth = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }

            //Check year, because if it is minimum or maximum, we have to change upper or lower limit
            if (year == maximumDate.Year.ToString())
            {
                day = DatePartBuilder(2, 1, maximumDate.Day, ref partNumber, required);
            }
            else if (year == minimumDate.Year.ToString())
            {
                day = DatePartBuilder(2, minimumDate.Day, daysInMonth[int.Parse(RemoveLeadingZeros(month)) - 1], ref partNumber, required);
            }
            else
            {
                day = DatePartBuilder(2, 1, daysInMonth[int.Parse(RemoveLeadingZeros(month)) - 1], ref partNumber, required);
            }

            if (day == "" && partNumber == 1)//It was cancelled, go back to part2
            {
                //Fix cursor and remove previous data
                Console.CursorLeft -= 3;
                Console.Write("   ");
                Console.CursorLeft -= 3;
                //Back to Part2
                DatePart2(ref partNumber, ref year, ref month, ref day, required, minimumDate, maximumDate);
            }
            else
            {
                ConsoleKeyInfo key = default(ConsoleKeyInfo);
                int cursorPosition = Console.CursorLeft;
                do
                {
                    //wait for cancel(backspace) or confirm(enter)
                    if (key != default(ConsoleKeyInfo) && Console.CursorLeft > cursorPosition)
                    {
                        Console.CursorLeft -= 1;
                        Console.Write(" ");
                        Console.CursorLeft -= 1;
                    }
                    key = Console.ReadKey();

                } while (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter);

                if (key.Key == ConsoleKey.Backspace)//It was cancelled, go back to part3
                {
                    //Fix cursor and remove previous data
                    Console.CursorLeft -= 2;
                    Console.Write("   ");
                    Console.CursorLeft -= 3;
                    //Back to Part2
                    DatePart3(ref partNumber, ref year, ref month, ref day, required, minimumDate, maximumDate);
                }
            }
        }


        //Extend the number with 0 to desired digits(Example: ExtendNumber(1,4,true,0)  => 0001)
        //                                                    ExtendNumber(1,4,false,9) => 1999)
        private static string ExtendNumber(double number, int numbeOfdigit, bool begin_end, int numberToAdd)
        {
            string numberString = number.ToString();

            int loopCounter = numbeOfdigit - numberString.Length;

            int insertLocation = 0;

            if (begin_end == false)
            {
                insertLocation = numberString.Length;
            }
            for (int i = 0; i < loopCounter; i++)
            {
                numberString = numberString.Insert(insertLocation, numberToAdd.ToString());
            }

            return numberString;
        }

        //Removes the zeros from the beggining of the string. (Example: RemoveLeadingZeros(0004) => 4)
        private static string RemoveLeadingZeros(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] != 0)
                {
                    return number.Substring(i, number.Length);
                }
            }
            return number;
        }

        //Gives back the index of the first non-zero character
        private static int GetFirstNonZero(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] != '0')
                {
                    return i;
                }
            }
            //if string empty return 0
            if (string.IsNullOrEmpty(number)) { return 0; }
            //if string not empty return 1
            else { return 1; }
        }

        //Use it to builds one date part (eg. the year)
        private static string DatePartBuilder(int digitLimit, int lowerLimit, int upperLimit, ref int part, bool required)
        {
            string datepart = "";
            int datePartDigitCounter = 0;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                //If it is NOT backspace
                if (required==false && datePartDigitCounter == 0 && key.Key == ConsoleKey.Enter)
                {
                    return datepart;
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    double val = 0;
                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                    double extendeNumber = double.Parse(ExtendNumber(double.Parse(RemoveLeadingZeros(datepart + val.ToString())), digitLimit - GetFirstNonZero(datepart), false, 0));
                    double loverExtendedNumber = double.Parse(ExtendNumber(double.Parse(RemoveLeadingZeros(datepart + val.ToString())), digitLimit - GetFirstNonZero(datepart), false, 9));

                    if (_x && upperLimit >= extendeNumber && lowerLimit <= loverExtendedNumber)
                    {
                        datepart += key.KeyChar;
                        //Auto-increment if there is no other possibily
                        if (double.Parse(ExtendNumber(double.Parse(datepart), digitLimit, false, 0)) == upperLimit)
                        {
                            Console.Write(ExtendNumber(double.Parse(datepart), digitLimit, false, 0).Substring(datePartDigitCounter) + ".");
                            return upperLimit.ToString();
                        }
                        Console.Write(key.KeyChar);
                        datePartDigitCounter++;
                    }
                }
                else
                {
                    //If it is backspace
                    if (key.Key == ConsoleKey.Backspace && datepart.Length > 0)
                    {
                        datepart = datepart.Substring(0, (datepart.Length - 1));
                        Console.Write("\b \b");
                        datePartDigitCounter--;
                    }
                    else if (part > 0)
                    {
                        part--;
                        return "";
                    }
                }
            }
            // Stops Receving Keys Once digit limit acquired
            while (!(datePartDigitCounter >= digitLimit));
            if (int.Parse(datepart) > digitLimit)
            {
                datepart = datepart.Substring(0, digitLimit);
            }

            Console.Write(".");
            return datepart;
        }
    }
}
