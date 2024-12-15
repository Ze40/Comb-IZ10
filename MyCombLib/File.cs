using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCombLib
{
    public class File<T>
    {
        string? path { get; }
        public File(string name)
        {
            string? appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path =  Path.Combine(appDir, name);
        }

        public void WriteToFile(List<T[]> data)
        {
            if (data == null) throw new ArgumentNullException("data is null");
            if (path == null) throw new ArgumentNullException("path is null");
            try
            {
                StreamWriter sw = new StreamWriter(path);
                foreach (T[] item in data)
                {
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (i == item.Length - 1)
                        {
                            sw.Write(item[i].ToString());
                            continue;
                        }
                        sw.Write(item[i].ToString() + " ");
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
            catch (Exception ex) { Console.WriteLine("EX: " + ex.Message); }
        }

    }
}
