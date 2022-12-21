using Flowchart_Framework.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Ren_Py_Designer.Models
{


    public static class Manager
    {
        public static Dictionary<string, string> Labels = new Dictionary<string, string>();

        public static string Path = "";

        public static void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string label in Labels.Values)
                {
                    writer.Write(label);
                }
                writer.Close();
            }
        }

        public static string GetCode()
        {
            string result = "";
            foreach (string label in Labels.Values)
            {
                result += label; 
            }
            return result;
        }

        public delegate void EventHandler(object sender, ValueChangedEventArgs e);

        public static event EventHandler<ValueChangedEventArgs> ValueChanged;

        public static void ReloadCode()
        {
            ValueChanged?.Invoke(null, new ValueChangedEventArgs(""));
        }

        public static void SaveBlocks(List<Block> list, string path)
        {

        }

        public static List<Block> LoadBlocks(string path)
        {
            List<Block> list = new List<Block>();

            return list;
        }

        private static Dictionary<string, Block> ComToBlock = new Dictionary<string, Block>();
        //{
           // {"label" }
       // }
    }
}
