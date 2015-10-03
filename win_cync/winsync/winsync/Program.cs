using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace winsync
{
    class Program
    {

        List<string> fileList = new List<string>();
        List<string> dirList = new List<string>();

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StreamWriter sw = new StreamWriter(@"C:\2\Test.txt");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //todo: Удалить все лишнее

            try
            {
                string[] drives = System.IO.Directory.GetLogicalDrives();
                foreach (string driver in drives)
                {
                    sw.WriteLine(driver);


                    Program p = new Program();
                    p.CreateTree(@driver, true);

                    foreach (string path in p.fileList)
                    {

                        try
                        {
                            sw.WriteLine(path);

                        }
                        catch (Exception exc) { sw.WriteLine(exc.Message); }

                    }
                    foreach (string dir in p.dirList)
                    {
                        sw.WriteLine(dir);
                    }

                }
            }







            catch (System.IO.IOException)
            {
                MessageBox.Show("ERROR");
            }



            sw.Close();
        }

        public void CreateTree(string Dir, bool Sub)
        {

            DirectoryInfo dir1 = new DirectoryInfo(Dir);
            Regex regex = new Regex("ReparsePoint");
            if (!regex.IsMatch(dir1.Attributes.ToString()))
            {
                //  MessageBox.Show("Найден репарсе поинт");
                //   MessageBox.Show(Dir);
                try
                {

                    string[] dirs = Directory.GetDirectories(Dir);

                    foreach (string dir in dirs)
                    {
                        ///  dirList.Add(dir + "(" + dir1.Attributes.ToString() + ")");
                        fileList.Add(dir + "(" + dir1.Attributes.ToString() + ")");
                    }
                }
                catch
                {


                    // MessageBox.Show(dir1.Attributes.ToString());

                }

                try
                {



                    string[] files = Directory.GetFiles(Dir);

                    foreach (string file in files)
                    {

                        FileInfo filess = new FileInfo(file);
                        fileList.Add(file + "(" + filess.Attributes.ToString() + ")");

                    }
                }
                catch
                {
                    //  DirectoryInfo dir2 = new DirectoryInfo(Dir);
                    // Regex regexx = new Regex("ReparsePoint");
                    // MessageBox.Show(dir2.Attributes.ToString());

                }

                try
                {



                    if (Sub)
                    {
                        foreach (string folder in Directory.GetDirectories(Dir))
                        {
                            CreateTree(folder, Sub);
                        }
                    }
                }
                catch
                {
                    // MessageBox.Show(dir2.Attributes.ToString());
                }

            }


        }

    }
}
