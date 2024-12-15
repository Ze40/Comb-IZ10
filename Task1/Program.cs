using MyCombLib;

namespace Task1
{
    class Project
    {

        static void Main()
        {
            //Задаем алфавит
            char[] alpabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K' };
            //Создаем комбинаторный класс с заданным алфавитом
            Combinatorics<char> comb = new Combinatorics<char>(alpabet);

            //Объявляем переменные которые вводит пользователь
            Console.Write("Введите n: ");
            int n = Convert.ToInt32(Console.ReadLine());//9, 9, 7

            Console.Write("Введите k: ");
            int k = Convert.ToInt32(Console.ReadLine());//3, 4, 1

            //Проверям на ошибки и просим ввести k и n заново до тех пор пока они не будут коректны
            while (true)
            {
                //Если n=7 то единственный вариант при котором существуют слова удовлетворяющие условию это при k=1
                if (n == 7 && k != 1 || k==1 && n!=7)
                {
                    Console.WriteLine("Ошибка: при k=1 должно быть n=7");
                }
                //Если k равна 0 это значит что в слове нет только одной буквы, следовательно n равно 9
                else if (k == 0 && n != 12)
                {
                    Console.WriteLine("Ошибка: при k=0 должно быть n=10");
                }
                //Оба параметра не могут быть отрицательными и длина слова не равна нулю
                else if (k < 0 || n <= 0)
                {
                    Console.WriteLine("Ошибка: k и n должны быть положительные");
                }
                //Длина слова должна быть меньше или равна k+12 т.к. по условию максимальная длина слова k+2*3+6
                else if (n > k + 12)
                {
                    Console.WriteLine("Ошибка: n <= k+12");
                }
                //k не должно равнятся 2 т.к. это создает противоречие в условие что 3 символа повторяются 2 раза и 1 символ k раз
                else if (k == 2)
                {
                    Console.WriteLine("Ошибка: k != 2");
                }
                //Минимальная длина слова больше или равна 6+k
                else if (n < 6 + k && k > 2)
                {
                    Console.WriteLine("Ошибка: при k > 2 => n >= 6+k");  
                }
                //Если все корректно выходим из цикла
                else break;
                Console.Write("Введите n: ");
                n = Convert.ToInt32(Console.ReadLine());

                Console.Write("Введите k: ");
                k = Convert.ToInt32(Console.ReadLine());

            }

            //Обознеачаем что поиск слов в процессе
            Console.WriteLine("In progress...");
            //Получем список слов удовлетворяющих условию
            List<char[]> words = comb.GetAllPermutations(n, (char[] word) => { 
                //Создаем словарь с ключем символом и значением количество этого символа в слове
                Dictionary<char, int> map = new Dictionary<char, int>();
                //Заполняем словарь
                foreach (char c in word)
                {
                    if (map.ContainsKey(c)) map[c]++;
                    else map[c] = 1;
                }

                //Колличество символов повторяющихся 2 раза
                int countOfTwo = 3;
                //Колличество символов повторяющихся k раз
                int countOfK = 1;
                if (k == 0) countOfK = 0;

                //Прохожусь по словарю
                foreach(KeyValuePair<char, int> kvp in map)
                {
                    //Если количество символов равно два уменьшаю счетчик 2
                    if (kvp.Value == 2) countOfTwo--;
                    //Если колличество символов равно k уменьшаю счетчки k
                    else if (kvp.Value == k) countOfK--;
                    //Иначе если символ повторяется возвращаю false
                    else if (kvp.Value > 1) return false;
                }
                //После прохода возвращаю true если счетчики по нулям и false иначе
                return countOfTwo == 0 && countOfK == 0;
            });

            //Вывожу колличество найденных слов
            Console.WriteLine(words.Count);

            //Заношу слова в файлик
            MyCombLib.File<char> file = new MyCombLib.File<char>("out.txt");
            file.WriteToFile(words);

            //Сообщаю о завершении алгоритма
            Console.WriteLine("Done");
        }
    }
}