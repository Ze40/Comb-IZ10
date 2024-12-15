using MyCombLib;

namespace Task3
{
    class Project
    {

        static void Main()
        {
            //Задаю афлфавит
            char[] alpabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k' };
            //Создаю комбинаторный объект
            Combinatorics<char> comb = new Combinatorics<char>(alpabet);
            //Длина слова
            int n = 9;
            //Объявляю о том что выполнение в состояние выполнения
            Console.WriteLine("In progress...");
            //Получаю список слов удовлетворяющих условию задачи
            List<char[]> words = comb.GetAllPermutations(n, (char[] word) => {
                //Инициализирую словарь, в котором в качестве ключа выступает символ слова, а в качестве значения его колличество
                Dictionary<char, int> map = new Dictionary<char, int>();
                //Заполняю словарь
                foreach (char c in word)
                {
                    if (map.ContainsKey(c)) map[c]++;
                    else map[c] = 1;
                }
                //Если количество символов равно 3 то возвращаю
                return map.Count == 3;
            });
            //Записываю в файл
            MyCombLib.File<char> file = new MyCombLib.File<char>("out.txt");
            file.WriteToFile(words);
            //Вывожу колличество
            Console.WriteLine(words.Count);
            //Сообщаю о завершении программы
            Console.WriteLine("Done");
        }
    }
}