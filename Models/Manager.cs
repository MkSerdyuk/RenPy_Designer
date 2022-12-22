using Flowchart_Framework.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Flowchart_Framework.View.Blocks;
using Ren_Py_Designer.Views.Blocks;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Ren_Py_Designer.Models
{


    public static class Manager
    {
        public static Dictionary<string, string> Labels = new Dictionary<string, string>();
        public static Dictionary<string, LabelBlock> LabelBlocks = new Dictionary<string, LabelBlock>();

        public static string Path = "";

        public static Block Last = null;

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
        
        public static void Save_As()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
            string path = saveFileDialog.FileName;
            if (path != "")
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

        public static void OpenBlocks(string path)
        {
            int i = 0;
            Block bl = null;
            using (StreamReader reader = new StreamReader(path))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    int ind = line.IndexOf(' ');
                    try
                    {
                        Type type = null;
                        if (ind != -1)
                        {
                            type = ComToBlock[line.Substring(0, ind)];
                        }
                        else
                        {
                            type = ComToBlock[line];
                        }
                        
                        if (!(type == typeof(LabelBlock) && LabelBlocks.ContainsKey(line.Substring(line.IndexOf(" ") + 1, line.IndexOf(":") - line.IndexOf(" ") - 1))))
                        {
                            Block newBlock = ((Block)Activator.CreateInstance(type));
                            newBlock.Parse(bl, line);
                            bl = newBlock;
                            PortManager.Canvas.Children.Add(newBlock);
                            Canvas.SetTop(newBlock, 10);
                            Canvas.SetLeft(newBlock, i * 150);
                        }
                        else
                        {
                            LabelBlock newLabel = LabelBlocks[line.Substring(line.IndexOf(" ") + 1, line.IndexOf(":") - line.IndexOf(" ") - 1)];
                            bl = newLabel;
                            PortManager.Canvas.Children.Add(newLabel);
                            Canvas.SetTop(newLabel, 10);
                            Canvas.SetLeft(newLabel, i * 150);

                        }
                        //PortManager.Canvas.Children.Add(new LabelBlock());

                    }
                    catch { }
                    i++;
                }
            }
            
        }

        public static List<Block> LoadBlocks(string path)
        {
            List<Block> list = new List<Block>();

            return list;
        }

        private static Dictionary<string, Type> ComToBlock = new Dictionary<string, Type>()
        {
           {"label", typeof(LabelBlock) },
           {"\tjump", typeof(JumpBlock) },
           {"\treturn", typeof(ReturnBlock) },
           {"\tif", typeof(IfBlock) }
        };
    }
}
