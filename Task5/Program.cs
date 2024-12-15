using MyCombLib;

namespace Task5
{
    class Project
    {

        static void Main()
        {
            //Создание алфавита
            char[] alpabet = { 'К', 'О', 'Н', 'Т', 'Е', 'Й', 'Р' };
            //Создание комбинаторного объекта
            Combinatorics<char> comb = new Combinatorics<char>(alpabet);
            //Длина слова
            int n = 9;
            //Объявляю о начале процесса
            Console.WriteLine("In progress...");
            //Получаю список слов
            List<char[]> words = comb.GetAllPermutations(n, (char[] word) => {
                //Создаю словарь в котором ключ это символ слова, а значение его количество
                Dictionary<char, int> map = new Dictionary<char, int>();
                //Заполняю словарь
                foreach (char c in word)
                {
                    if (map.ContainsKey(c)) map[c]++;
                    else map[c] = 1;
                }
                //Проверка на содержание всех символов
                if (map.Count != 7) return false;
                //Проверка на необходимые повторы
                if (map['Е'] != 2 || map['Н'] != 2) return false;
                return true;

            });
            //Записываю в файл
            MyCombLib.File<char> file = new MyCombLib.File<char>("out.txt");
            file.WriteToFile(words);
            //Вывожу количество
            Console.WriteLine(words.Count);
            //Сообщаю о конце
            Console.WriteLine("Done");
        }
    }
}