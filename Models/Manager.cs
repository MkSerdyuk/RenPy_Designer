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
        //public static Dictionary<Block, Page> BlockPages = new Dictionary<Block, Page>();

        public static string Path = "";

        public static Block Selected;

        public static Block Last = null;

        public static void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string label in Labels.Values)
                {
                    writer.Write(label.Replace("\t", "    "));
                }
                writer.Close();
            }
        }        
        
        public static void Save_As()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
            string path = saveFileDialog.FileName;
            Path = path;
            Save(path);
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
                line = reader.ReadLine();
                if (line == null)
                {
                    return;
                }
                line = line.Replace("    ", "\t");
                while (true)
                {


                    int ind = line.IndexOf(' ');
                    try
                    {
                        Type type = null;
                        if (ind != -1)
                        {
                            if (ComToBlock.ContainsKey(line.Substring(0, ind)))
                            {
                                type = ComToBlock[line.Substring(0, ind)];
                            }
                            else
                            {
                                type = typeof(DialogBlock);
                            }
                        }
                        else
                        {
                            if (ComToBlock.ContainsKey(line))
                            {
                                type = ComToBlock[line];
                            }
                            else
                            {
                                type = typeof(DialogBlock);
                            }
                        }

                        if (type == typeof(LabelBlock))
                        {
                            if (!LabelBlocks.ContainsKey(line.Substring(line.IndexOf(" ") + 1, line.IndexOf(":") - line.IndexOf(" ") - 1)))
                            {
                                Block newBlock = ((Block)Activator.CreateInstance(type));
                                newBlock.Parse(bl, line.Replace("    ", "\t"));
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

                            i++;
                            line = reader.ReadLine();

                            if (line == null)
                            {
                                break;
                            }

                            line = line.Replace("    ", "\t");

                            continue;
                        }

                        #region Old

                        /*
                        if (type == typeof(IfBlock))
                        {
                            int notCondCounter = 0;
                            string ifLine = line + "\n";
                            while (notCondCounter != 2)
                            {
                                if ((line = reader.ReadLine()).Contains("if"))
                                {
                                    notCondCounter = 0;
                                }
                                else
                                {
                                    notCondCounter++;
                                    if (notCondCounter == 2)
                                    {
                                        break;
                                    }
                                }
                                ifLine += line + "\n";
                            }
                            Block newBlock = ((Block)Activator.CreateInstance(typeof(IfBlock)));
                            newBlock.Parse(bl, ifLine);
                            bl = newBlock;
                            PortManager.Canvas.Children.Add(newBlock);
                            Canvas.SetTop(newBlock, 10);
                            Canvas.SetLeft(newBlock, i * 150);
                            i++;

                            ind = line.IndexOf(' ');
                            if (ind != -1)
                            {
                                type = ComToBlock[line.Substring(0, ind)];
                            }
                            else
                            {
                                type = ComToBlock[line];
                            }
                        }
                        */

                        #endregion
                        #region New Parser
                        int tabCount = line.Count(f => f == '\t');
                        string newLine = line;
                        try
                        { 
                            while (true)//(line = reader.ReadLine()).Count(f => f == '\t') > tabCount || ((Block)Activator.CreateInstance(type)).StrContinues(line))
                            {
                                line = reader.ReadLine();
                                if (line == null)
                                {
                                    throw new ArgumentNullException();
                                }
                                line = line.Replace("    ", "\t");
                                if (!(line.Count(f => f == '\t') > tabCount || ((Block)Activator.CreateInstance(type)).StrContinues(line)))
                                {
                                    break;
                                }
                                newLine += "\n";
                                newLine += line;
                            }

                            Block newBlock = ((Block)Activator.CreateInstance(type));
                            newBlock.Parse(bl, newLine.Replace("    ", "\t"));
                            bl = newBlock;
                            PortManager.Canvas.Children.Add(newBlock);
                            Canvas.SetTop(newBlock, 10);
                            Canvas.SetLeft(newBlock, i * 150);

                            i++;

                        }
                        catch (ArgumentNullException e)
                        {
                            Block newBlock = ((Block)Activator.CreateInstance(type));
                            newBlock.Parse(bl, newLine.Replace("    ", "\t"));
                            bl = newBlock;
                            PortManager.Canvas.Children.Add(newBlock);
                            Canvas.SetTop(newBlock, 10);
                            Canvas.SetLeft(newBlock, i * 150);
                            i++;
                            break;
                        }
                        catch { }
                        #endregion

                        #region Old Crunch
                        /*if (!(type == typeof(LabelBlock) && LabelBlocks.ContainsKey(line.Substring(line.IndexOf(" ") + 1, line.IndexOf(":") - line.IndexOf(" ") - 1))))
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
                        */
                        #endregion


                        if (i * 150 > PortManager.Canvas.Width)
                        {
                            PortManager.Canvas.Width = (i + 1) * 150;
                        }

                        if (line == null)
                        {
                            break;
                        }
                    }
                    catch { }
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
           {"\tmenu:", typeof(MenuBlock) },
           {"\tif", typeof(IfBlock) },
           {"\tshow", typeof(ShowBlock) },
           {"\tpython:", typeof(PythonBlock) },
           {"\thide", typeof(HideBlock) },
           {"\tdefine", typeof(CharacterBlock) }
        };
    }
}
