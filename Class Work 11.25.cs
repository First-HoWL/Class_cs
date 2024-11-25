// See https://aka.ms/new-console-template for more information
using System.Text;

Console.OutputEncoding = UTF8Encoding.UTF8;
Console.InputEncoding = UTF8Encoding.UTF8;



void bubble_sort(int[] array, int len)
{
    bool is_changed = true;
    int a;
    while (is_changed)
    {
        is_changed = false;
        for (int i = 0; i < len - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                a = array[i];
                array[i] = array[i + 1];
                array[i + 1] = a;
                is_changed = true;
            }
        }
    }

}

double CelsToFahr(double celsius)
{
    return celsius * 1.8 + 32;
}

double FahrToCels(double fahrenheit)
{
    return (fahrenheit - 32) * 0.55555; ;
}
void color(double temp)
{
    if (temp < -10)
        Console.ForegroundColor = ConsoleColor.DarkBlue;
    else if (temp < 10)
        Console.ForegroundColor = ConsoleColor.Blue;
    else if (temp < 25)
        Console.ForegroundColor = ConsoleColor.White;
    else if (temp < 33)
        Console.ForegroundColor = ConsoleColor.DarkYellow;
    else
        Console.ForegroundColor = ConsoleColor.Red;

}

Console.Write("1. З цельсію в фарінгейт 2. З фарінгейту в цельсій: ");
int a = int.Parse(Console.ReadLine());
Console.Write("Температура: ");
double temp = double.Parse(Console.ReadLine()), temp2 = 0, b;
if (a == 1)
    temp2 = CelsToFahr(temp);
else if (a == 2)
    temp2 = FahrToCels(temp);

if (a == 1)
    b = temp;
else
    b = temp2;

Console.Write($"Температура: ");
color(b);
Console.Write(temp2);
Console.ForegroundColor = ConsoleColor.Gray;
Console.Write($" градусів!");

/* 
int[] numbers = { 1, 5, 3, 2, 2 };

bubble_sort(numbers, 5);
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(numbers[i]);
}
*/


/* 
int numb, sum = 0, max = 0, min = 10000, dob = 1;

Console.WriteLine("Enter 5 numb: ");

for (int i = 0; i < 5; i++)
{
    numb = int.Parse(Console.ReadLine());
    sum += numb;
    if (max < numb) max = numb;
    if (min > numb) min = numb;
    dob *= numb;
}

Console.WriteLine($"Sum: {sum}!\t Max: {max}!\t Min: {min}!\t multip:  {dob}!");
*/
