using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App001
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] disk = DriveInfo.GetDrives();

            foreach (DriveInfo d in disk) {

                Console.WriteLine("Disk {0}", d.Name);
                Console.WriteLine("Disk - tip {0}", d.DriveType);
                if (d.IsReady) {
                    Console.WriteLine("Spreman!");

                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
