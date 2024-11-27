// See https://aka.ms/new-console-template for more information
using System.Text;

Console.OutputEncoding = UTF8Encoding.UTF8;
Console.InputEncoding = UTF8Encoding.UTF8;


void bubble_sort(int[] array, bool from_more_to_less)
{
    bool is_changed = true;
    int a;
    while (is_changed)
    {
        is_changed = false;
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (from_more_to_less == false && array[i] > array[i + 1])
            {
                a = array[i];
                array[i] = array[i + 1];
                array[i + 1] = a;
                is_changed = true;
            }
            else if (from_more_to_less == true && array[i] < array[i + 1])
            {
                a = array[i];
                array[i] = array[i + 1];
                array[i + 1] = a;
                is_changed = true;
            }
        }
    }
}
void print_array(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        Console.Write($"{ array[i]}, ");
    }
    Console.Write("\b\b.");
}
void random_array(int[] array)
{
    Random rand = new Random();
    for (int i = 0; i < array.Length; i++)
    {
        array[i] = rand.Next(1, 11);
    }
    
}
int even(int[] array)
{
    int count = 0;
    for (int i = 0; i < array.Length; i++)
        if (array[i] % 2 == 0) 
            count++;

    return count;
}
int odd(int[] array)
{
    int count = 0;
    for (int i = 0; i < array.Length; i++)
        if (array[i] % 2 == 1)
            count++;

    return count;
}

int count_of_solo_numb(int[] array)
{
    int count = 0;
    bool is_solo = true;
    for (int i = 0; i < array.Length; i++)
    {
        is_solo = true;
        for (int j = 0; j < i; j++)
            if (array[i] == array[j])
            {
                is_solo = false;
                break;
            }
        if (is_solo)        
            count++;
    }

    return count;
}

string shifr(string str, int offset)
{
    int a;
    string encoded_text = "";
    for (int i = 0; i < str.Length; i++) { 
        if (str[i] > 90)
            a = ((str[i] + offset) - 'a') % 26 + 'a';
        else
            a = ((str[i] + offset) - 'A') % 26 + 'A';
        
        encoded_text += Convert.ToChar(a);
    }
    return encoded_text;
}

string deshifr(string str, int offset)
{
    int a;
    string encoded_text = "";
    for (int i = 0; i < str.Length; i++)
    {
        if (str[i] > 90)
            a = ((((str[i] - 'a') + 26) - offset) % 26) + 'a';
        else
            a = ((((str[i] - 'A') + 26) - offset) % 26) + 'A';
        

        encoded_text += Convert.ToChar(a);
    }
    return encoded_text;
}

/*


int len = 0;
Console.Write("len array:");
len = int.Parse(Console.ReadLine());
int[] array = new int[len];


random_array(array);
Console.WriteLine($"odd: {odd(array)}");
Console.WriteLine($"even: {even(array)}");
Console.WriteLine($"Solo numb: {count_of_solo_numb(array)}");
print_array(array);
*/
int a = 'a' + 1 - 1;
Console.Write(a);

Console.Write("1. шифровать 2. дешифровать: ");
int vibor = int.Parse(Console.ReadLine());
Console.Write("text: ");
string str = Console.ReadLine();
Console.Write("offset: ");
int offset = int.Parse(Console.ReadLine());
if (vibor == 1)
    Console.Write(shifr(str, offset % 26));
else if (vibor == 2)
    Console.Write(deshifr(str, offset % 26));
