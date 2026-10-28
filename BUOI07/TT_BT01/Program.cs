using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_BT01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DsNhanVien a = new DsNhanVien();
            string filename = "../../DSNhanVien.xml";
            a.DocFileXML(filename);
            a.xuatDSNV();
            Console.WriteLine(
                "Tong tien luong cong ty phai tra: " + a.TinhTongThuNhap().ToString("N0")
            );
            Console.ReadLine();
        }
    }
}
