using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konzola
{
    class Program
    {

        private static void SortirajPrioritet(String putanja)
        {
            try
            {
                Process[] procesi = Process.GetProcesses().OrderByDescending(p => p.BasePriority).ToArray();
                using (StreamWriter sw = new StreamWriter(putanja, false))
                { 
                Console.WriteLine("Naziv".ToString().PadRight(70)+"ID".ToString().PadRight(10)+"Prioritet");
                sw.WriteLine("Naziv".ToString().PadRight(70) + "ID".ToString().PadRight(10) + "Prioritet");

                Console.WriteLine("=".ToString().PadRight(69,'=')+" " +"=".ToString().PadRight(9, '=')+" "+ "=".ToString().PadRight(11, '='));
                sw.WriteLine("=".ToString().PadRight(69, '=') + " " + "=".ToString().PadRight(9, '=') + " " + "=".ToString().PadRight(11, '='));


                    foreach (var p in procesi)
                {
                    try
                    {
                        Console.WriteLine(p.ProcessName.PadRight(70) + p.Id.ToString().PadRight(10) + p.BasePriority.ToString());
                        sw.WriteLine(p.ProcessName.PadRight(70) + p.Id.ToString().PadRight(10) + p.BasePriority.ToString());



                        }
                    catch (Exception e)
                    {
                    }
                }

               }


            }
            catch (Exception e)
            {
                Console.WriteLine("Greška: {0}", e.Message);
            }

        }

        private static void Detalji(String putanjaPID, int PID)
        {

            try

            {   

                String lokacija = putanjaPID+@"\"+PID+@".txt";
                Process procesi = Process.GetProcessById(PID);
                using (StreamWriter sw = new StreamWriter(lokacija, false))

                {
                    Console.WriteLine("Naziv: " + procesi.ProcessName);
                    Console.WriteLine("Broj thredova: " + procesi.Threads.Count);
                    Console.WriteLine("Vrijeme pokretanja: " + procesi.StartTime);
                    Console.WriteLine("Naziv računala na kojem je pokrenut: " + procesi.MachineName);
                    Console.WriteLine("Ukupno iskorišteno vrijeme procesora: " + procesi.TotalProcessorTime);
                    Console.WriteLine("Peak memorije: " + procesi.PeakPagedMemorySize64 / 1024 + " MB");

                    sw.WriteLine("Naziv: " + procesi.ProcessName);
                    sw.WriteLine("Broj thredova: " + procesi.Threads.Count);
                    sw.WriteLine("Vrijeme pokretanja: " + procesi.StartTime);
                    sw.WriteLine("Naziv računala na kojem je pokrenut: " + procesi.MachineName);
                    sw.WriteLine("Ukupno iskorišteno vrijeme procesora: " + procesi.TotalProcessorTime);
                    sw.WriteLine("Peak memorije: " + procesi.PeakPagedMemorySize64 / 1024 + " MB");

                    

                }
                
            }

            catch (Exception e)
            {
                Console.WriteLine("Greška: {0}" + e.Message);
            }

            
        }

        private static void ZauzimajuViseOd(int v)
        {
            try
            {
                Process[] procesi = Process.GetProcesses().Where(p => p.PeakPagedMemorySize64 / 1024 > v).ToArray();
                foreach (var p in procesi)
                {
                    try
                    {
                        Console.WriteLine(p.ProcessName.PadRight(60) + p.Id.ToString().PadRight(10) + p.PeakPagedMemorySize64 / 1024);

                    }


                    catch (Exception e)
                    {
                        Console.WriteLine("Greška - zauzimajuVišeOd: " + e.Message);
                    }


                }

            }

            catch (Exception e)
            {
                Console.WriteLine("Greška: " + e.Message);
            }

           
        }

        private static void ProcitajTxt(String putanjaTxt)
        {
            

            try
            {
              string[] zapisi = File.ReadAllLines(putanjaTxt);
              int brojac = 0;
              foreach (var z in zapisi)
               {
                 try

                  {
                        Process.Start(z);
                        Console.WriteLine("Proces {0} je uspješno pokrenut!", z);
                        brojac++;
                                 
                  }

                  catch (Exception e)
                 {
                        Console.WriteLine("Greška prilikom čitanja/pokretanja zapisa: " + e.Message);
                 }

              }
                Console.WriteLine("Ukupno je pokrenuto {0} procesa čija su se imena nalazila u {1}", brojac,putanjaTxt);


            }

             catch (Exception e)
             {
                Console.WriteLine("Greška - ProcitajTxt :" + e.Message);

             }

        }

        private static void FiltrirajPremaSlovu(String a)
        {
            try
            {
                Process[] procesi = Process.GetProcesses().Where(p => p.ProcessName.StartsWith(a)).ToArray();
                int brojac = 0;

                Console.WriteLine("Naziv".ToString().PadRight(70) + "ID".ToString().PadRight(10) + "Prioritet");
                Console.WriteLine("=".ToString().PadRight(69, '=') + " " + "=".ToString().PadRight(9, '=') + " " + "=".ToString().PadRight(11, '='));

                foreach (var p in procesi)

                {
                    try
                    {
                        Console.WriteLine(p.ProcessName.PadRight(70) + p.Id.ToString().PadRight(10) + p.BasePriority.ToString());
                        brojac++;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Greška: {0}",e.Message);
                    }


                }
                Console.WriteLine();
                Console.WriteLine("Ukupno {0} procesa počinje sa slovom {1}.", brojac, a);

            }
            catch(Exception e)
            {
                Console.WriteLine("Greška kod filtriranja: {0}", e.Message);

            }
          
        }


        static void Main(string[] args)
        {
            if (args[0] == "sortirajPrioritet" && args.Length > 1)
            {
                SortirajPrioritet(args[1]);

            }
            else if (args[0] == "detalji" && args.Length > 2)
            {
                try
                {
                    int j = Int32.Parse(args[1]);
                    // Detalji(args[2],Int32.TryParse(args[1], out int j));
                    Detalji(args[2], j);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Greška {0}:" + e.Message);
                }

            }

            else if (args[0] == "zauzimajuViseOd" && args.Length > 1)
            {
                try
                {
                    int v = Int32.Parse(args[1]);
                    ZauzimajuViseOd(v);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Greška: " + e.Message);
                }
            }

            else if (args[0] == "procitajTxt" && args.Length > 1)
            {
                ProcitajTxt(args[1]);

            }

            else if ((args[0] == "filtrirajPremaSlovu" && args.Length > 1))
            {
                try
                {
                    if (args[1].Length == 1)
                    {
                        FiltrirajPremaSlovu(args[1]);
                    }
                    else
                    {
                        Console.WriteLine("Filtiranje je moguće samo po jednom zadanom slovu!");
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Greška filtiranja: {0}" + e.Message);
                       
                 }
            }

            Console.ReadKey();

        }
    }
}
