using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT_BT01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuanLyNhanVien a = new QuanLyNhanVien();
            string filename = "../../DSNhanVien.xml";
            a.DocFile(filename);
            a.XuatDS();
            Console.ReadLine();
        }
    }
}
