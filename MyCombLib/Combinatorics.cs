using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCombLib
{
    public class Combinatorics<T>
    {
        //Хранение алфавита
        T[] alphabet;

        //Объявление конструктора, который устанавливает алфовит
        public Combinatorics(params T[] alpabet) {
            this.alphabet = alpabet;
        }

        //Функция генерации размещений с повторениями удовлетворяющих условию Condition
        public List<T[]> GetAllPermutations(int k, Func<T[], bool> Condition)
        {
            //Список для хранения подходящих размещений
            List<T[]> answer = new List<T[]>();

            //Массив хранения индексов элементов алфавита и задание начального значения
            int[] indexes = new int[k];
            for (int i = 0; i < k; i++) indexes[i] = 0;

            //Объявления массива, который представляет собой рассматриваемое размещение
            T[] currPermutaion = new T[k];

            //Создание первого размещения и добавление его, если оно подходит под условие
            for (int i = 0; i < k; i++) currPermutaion[i] = alphabet[0];
            if (Condition(currPermutaion)) answer.Add(currPermutaion);

            //Индекс рассматриваемой позиции в размещении
            int index = k - 1;
            //Цикл до тех пор пока индекс элемента алфавита первой позиции размещения находится в переделе размера самого алфавита 
            while (indexes[0] < alphabet.Length)
            {
                //Цикл до тех пора пока элемент на поз. index равен последнему элементу в алфавите
                while (index >= 0 && indexes[index] == alphabet.Length - 1)
                {
                    //Обнуление позиции символа в алфавите
                    indexes[index] = 0;
                    //Устанавливаем элемент в размещении на поз. index на первый из алфавита
                    currPermutaion[index] = alphabet[0];
                    //Смещаемся на предыдущий символ алфавита
                    index--;
                }
                //Если позиция в размещении меньше нуля значит найдены все размещения, следовательно выходим из цикла
                if (index < 0) break;

                //Переход к следующему символу в алфавите на позиции index в размещении
                indexes[index]++;
                //Замена на соответствующее значение
                currPermutaion[index] = alphabet[indexes[index]];
                //Проверяем удовлетворяет ли полученное размещение заданному условию
                if (Condition(currPermutaion))
                {
                    //Если до то создаем новое размещение и копируем в него элементы из currPermutation, чтобы избежать конфликтов памяти
                    T[] newPermutation = new T[k];
                    for (int i = 0; i < k; i++) newPermutation[i] = currPermutaion[i];
                    //Добавляем эелемент в ответ
                    answer.Add(newPermutation);
                }
                //Вновь устанавливаем index на последнюю позицию
                if (index != k - 1) index = k - 1;
            }

            //Возвращаем ответ
            return answer;

        }

        public List<int[]> GetAllSolution(int n, int answer, Dictionary<string, int>[] condition)
        {
            //Массив корней
            int[] indexes = new int[n];
            for (int i = 0; i < n; i++)
            {
                //Проверка существования условий
                if (condition[i] == null) throw new ArgumentNullException("Не все хi содержат ограничения");
                if (!condition[i].ContainsKey("max")) throw new ArgumentNullException("Xi не содержит max");
                if (!condition[i].ContainsKey("min")) throw new ArgumentNullException("Xi не содержит min");

                //Формирование начального значения корней по их минимальному значению
                indexes[i] = condition[i]["min"];
            }

            //Создаю список ответов
            List<int[]> answerList = new List<int[]>();
            //Устанавливаю указатель
            int index = n - 1;
            while (indexes[0] <= condition[0]["max"])
            {
                //Расчитываю нынешнюю сумму
                int sum = 0;
                for (int i = 0; i < n-1; i++)
                {
                    sum += indexes[i];
                }
                //Устанавливаю если по ограничениям это возможно последний корень как разницу суммы и answer если же нет то проверяю можно ли установить её на пердыдущий корень
                int fraq = answer - sum;
                indexes[n - 1] = fraq;
                if (fraq <= condition[n - 1]["max"] && fraq >= condition[n - 1]["min"])
                {
                    int[] x = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        x[i] = indexes[i];
                    }
                    //добовляю решение в список
                    answerList.Add(x);
                    index--;
                }
                //проверяю на выполнение всех условий
                while (indexes[index] > condition[index]["max"] || indexes[index] < condition[index]["min"])
                {
                    indexes[index] = condition[index]["min"];
                    index--;
                }
                //перевожу указатель на последний элемент
                indexes[index]++;
                index = n - 1;
            }
            //Возвращаю ответ
            return answerList;
        }
        
    }
}
