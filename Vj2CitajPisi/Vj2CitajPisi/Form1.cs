using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vj2CitajPisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Ucitaj_iz_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //otaranje File Dialoga i odabir datoteke
                string putanja1 = openFileDialog1.FileName;
                upisPutanje1.Text = putanja1;

                string readText = File.ReadAllText(putanja1);
                poljeIspisa.Text = readText;
            }
            else
            {
                upisPutanje1.Text = "Datoteka nije odabrana";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Folder = new FolderBrowserDialog();

            if (Folder.ShowDialog() == DialogResult.OK)
            {
                //otvaranje File Dialoga i odabir datoteke
                string putanja2 = Folder.SelectedPath;
                upisPutanje2.Text = putanja2;

              
               
            }
            else
            {
                upisPutanje2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string putanjaSadrzaj;
            string imeDokSadrzaj;
           
            string path;

            putanjaSadrzaj = upisPutanje2.Text;
            imeDokSadrzaj = imeDokumenta.Text;
            
            path = putanjaSadrzaj+@"\"+imeDokSadrzaj+@".txt";


            try
            {
                if ((string.IsNullOrEmpty(imeDokSadrzaj)||imeDokSadrzaj.Trim().Length==0)&&(string.IsNullOrEmpty(putanjaSadrzaj)||putanjaSadrzaj.Trim().Length==0))
                    //(imeDokSadrzaj.Length == 0 && putanjaSadrzaj.Length == 0)
                {
                    MessageBox.Show("Ime dokumenta i putanja datoteke nisu upisani!");
                }
                else if (string.IsNullOrEmpty(imeDokSadrzaj) || imeDokSadrzaj.Trim().Length == 0)
                //(imeDokSadrzaj.Length == 0)

                {
                    MessageBox.Show("Ime dokumenta nije upisano!");
                }
                else if (string.IsNullOrEmpty(putanjaSadrzaj) || putanjaSadrzaj.Trim().Length == 0)
                //(putanjaSadrzaj.Length == 0)
                {
                    MessageBox.Show("Putanja datoteke nije upisana!");
                }
                else
                {

                    {
                        using (StreamWriter sw = new StreamWriter(path, true))
                        {

                            sw.WriteLine(poljeUpisa.Text);
                            MessageBox.Show("Uspješno upisano u datoteku: " + imeDokSadrzaj + @".txt");


                        }

                    }
                }
             

            }
            catch(DirectoryNotFoundException)
            {
                MessageBox.Show("Polje unosa putanje datoteke nije upisano!");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Polje unosa putanje datoteke ili imena dokumenta nije upisano!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: {0}",ex.Message);
            }

        }
            




    }
}
