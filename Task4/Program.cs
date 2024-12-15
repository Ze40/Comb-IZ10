using MyCombLib;

namespace Task4
{
    class Project
    {
        static void Main()
        {
            //Создаю комбинаторный объект
            Combinatorics<int> combinatorics = new Combinatorics<int>();
            //Создаю массив словарей условий
            Dictionary<string, int>[] map = new Dictionary<string, int>[8];
            for (int i = 0; i < map.Length; i++) map[i] = new Dictionary<string, int>();
            //Заполняю условия
            map[0].Add("min", 1);
            map[0].Add("max", 8);

            map[1].Add("min", 9);
            map[1].Add("max", 100);

            map[2].Add("min", 1);
            map[2].Add("max", 7);

            map[3].Add("min", 11);
            map[3].Add("max", 100);

            map[4].Add("min", 1);
            map[4].Add("max", 6);

            map[5].Add("min", 12);
            map[5].Add("max", 100);

            map[6].Add("min", 1);
            map[6].Add("max", 5);

            map[7].Add("min", 12);
            map[7].Add("max", 100);

            //Объявляю о начале
            Console.WriteLine("In progress...");
            List<int[]> list = combinatorics.GetAllSolution(8, 100, map);
            //Вывожу количество решений
            Console.WriteLine(list.Count);
            //Записываю в файл
            File<int> file = new File<int>("out.txt");
            file.WriteToFile(list);
            //Сообщаю о конце программы
            Console.WriteLine("Done");
        }
    }
}