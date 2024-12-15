using MyCombLib;

namespace Task2
{
    class Project
    {

        static void Main()
        {
            //Задаем алфавит
            char[] alpabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k'};
            //Создаем объект комбинаторного класса, куда передаем алфавит
            Combinatorics<char> comb = new Combinatorics<char>(alpabet);

            //Ввод длины слова
            Console.Write("Введите n: ");
            int n = Convert.ToInt32(Console.ReadLine());//6 6 8 
            //Ввод k
            Console.Write("Введите k: ");
            int k = Convert.ToInt32(Console.ReadLine());//1 2 1
            //Ввод m
            Console.Write("Введите m: ");
            int m = Convert.ToInt32(Console.ReadLine());//0 2 3

            //Проверка на корректность
            while (true)
            {
                //Длина слова и k должны быть больше нуля, а m - не может быть положительным
                if (k <= 0 || m < 0 || n <= 0)
                {
                    Console.WriteLine("Ошибка: все параметры должны быть > 0");
                }
                //Длина слова должна быть строго больше чем 2k+m
                else if (n <= 2 * k + m)
                {
                    Console.WriteLine("Ошибка: n должно быть больше чем 2k + m");
                }
                //Иначе если корректно выходим из цикла
                else break;
                Console.Write("Введите n: ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите k: ");
                k = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите m: ");
                m = Convert.ToInt32(Console.ReadLine());
            }

            //Обозначаем что генерация слов в процессе
            Console.WriteLine("In progress...");
            //Получаем список слов вызовом функции в которую передаем длину и условие
            List<char[]> words = comb.GetAllPermutations(n, (char[] word) => {
                //Создаем словарь где ключ это символ слова, а значение его количество в слове
                Dictionary<char, int> map = new Dictionary<char, int>();
                //Заполняем словарь
                foreach (char c in word)
                {
                    if (map.ContainsKey(c)) map[c]++;
                    else map[c] = 1;
                }

                //Проверяем на условия задачаи
                foreach (KeyValuePair<char, int> pair in map)
                {
                    if (pair.Key == 'a' && pair.Value >= k) return false;
                    else if ((pair.Key == 'b' || pair.Key == 'c') &&  pair.Value != k) return false;
                    else if (pair.Key == 'd' &&  pair.Value <= m) return false;
                    else if ((pair.Key != 'a' && pair.Key != 'b' && pair.Key != 'c' && pair.Key != 'd') &&  pair.Value > 1) return false;
                }
                return map.ContainsKey('b') && map.ContainsKey('c') && map.ContainsKey('d');
                
            });
            //Заносим слова в файл
            MyCombLib.File<char> file = new MyCombLib.File<char>("out.txt");
            file.WriteToFile(words);

            //Выводим их колличество
            Console.WriteLine(words.Count);
            //Сообщаем о конце программы
            Console.WriteLine("Done");
        }
    }
}