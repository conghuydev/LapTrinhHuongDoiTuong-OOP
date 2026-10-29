using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TL_BT01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CongTyABC a = new CongTyABC();
            string filename = ("../../DSHoaDon.xml");
            a.DocFileXML(filename);
            a.XuatDS();

            Console.WriteLine("Tong thanh tien: {0:N0} VND", a.TinhTongThanhTien());
            Console.WriteLine("Tong ho tro: {0:N0} VND", a.TinhTongHoTro());

            a.XuatMuaNhieuNhat();

            a.XuatThanhTienMax();

            Console.WriteLine("\nTong chiet khau doi voi cong ty: {0:N0} VND", a.TongTienChietKhauCty());

            Console.WriteLine("\nSAP XEP GIAM THEO SO LUONG");
            a.SapXepGiamTheoSoLuong();
            a.XuatDS();

            Console.WriteLine("\nSo dai ly ca C1: {0}", a.SoDaiLyCap1());

            Console.ReadLine();
        }
    }
}
