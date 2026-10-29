using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TL_BT01
{
    abstract class HoaDon
    {
        string maKH;
        string tenKH;
        int soLuong;
        double gia;

        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public double Gia { get => gia; set => gia = value; }

        public HoaDon()
        {

        }

        public HoaDon(string maKH, string tenKH, int soLuong, double gia)
        {
            MaKH = maKH;
            TenKH = tenKH;
            SoLuong = soLuong;
            Gia = gia;
        }

        public double TinhThueVAT()
        {
            return 0.1 * SoLuong * Gia;
        }

        public abstract double ChietKhau();
        
        public virtual double TinhThanhTien()
        {
            return SoLuong * Gia - ChietKhau() + TinhThueVAT();
        }

        public void Xuat()
        {
            Console.WriteLine(
                "{0,-12}{1,-25}{2,-12}{3,-15:N0}{4,-15:N0}{5,-15:N0}{6,-15:N0}",
                MaKH,
                TenKH,
                SoLuong,
                Gia,
                SoLuong * Gia,
                TinhThueVAT(),
                TinhThanhTien()
            );
        }
    }

    class HDDaiLy : HoaDon
    {
        int soNamHT;

        public HDDaiLy(): base()
        {
            SoNamHT = 0;
        }

        public HDDaiLy(string maKH, string tenKH, int soLuong, double gia, int soNamHT)
            : base(maKH, tenKH, soLuong, gia)
        {
            this.SoNamHT = soNamHT;
        }

        public int SoNamHT { get => soNamHT; set => soNamHT = value; }

        public override double ChietKhau()
        {
            double phanTramCK = 0.3;
            if (SoNamHT > 5)
                phanTramCK = phanTramCK + SoNamHT * 0.01;
            if (phanTramCK > 0.35)
                phanTramCK = 0.35;
            return phanTramCK * Gia * SoLuong;
        }

        public override double TinhThanhTien()
        {
            return base.TinhThanhTien();
        }
    }

    public interface IHoTro
    {   
        double HoTroGia();
    }

    class HDCaNhan : HoaDon, IHoTro
    {
        int khoangCach;

        public HDCaNhan(): base()
        {
            khoangCach = 0;
        }

        public HDCaNhan(string maKH, string tenKH, int soLuong, double gia, int khoangCach) : base(maKH, tenKH, soLuong, gia)
        {
            this.khoangCach = khoangCach;
        }

        public override double ChietKhau()
        {
            double chietKhau = 0;
            if (SoLuong > 7)
                chietKhau += 0.05 * SoLuong * Gia;
            if (khoangCach < 10)
                chietKhau += 1000 * SoLuong;
            return chietKhau;
   
        }

        public double HoTroGia()
        {
            double hoTro = 0;
            hoTro = 0.02 * SoLuong * Gia;
            if (SoLuong > 2)
                hoTro += 100000;
            return hoTro;
        }

        public override double TinhThanhTien()
        {
            return base.TinhThanhTien();
        }

    }

    class HDCongTy : HoaDon, IHoTro
    {
        int SLNV;

        public HDCongTy(): base ()
        {
            SLNV = 0;
        }

        public HDCongTy(string maKH, string tenKH, int soLuong, double gia, int SLNV) : base(maKH, tenKH, soLuong, gia)
        {
            this.SLNV = SLNV;
        }

        public override double ChietKhau()
        {
            if (SLNV > 500)
                return 0.05 * Gia * SoLuong;
            if (SLNV > 100)
                return 0.03 * Gia * SoLuong;
            return 00;
        }

        public double HoTroGia()
        {
            return 12000 * SoLuong;
        }

        public override double TinhThanhTien()
        {
            return base.TinhThanhTien() - HoTroGia();
        }
    }

    class CongTyABC
    {
        public string TenCty;
        public string DiaChi;
        List<HoaDon> dsHoaDon;

        public List<HoaDon> DsHoaDon { get => dsHoaDon; set => dsHoaDon = value; }

        public CongTyABC()
        {
            DsHoaDon = new List<HoaDon>();
        }

        public CongTyABC(List<HoaDon> dsHoaDon)
        {
            DsHoaDon = dsHoaDon;
        }

        public void DocFileXML(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            TenCty = doc.SelectSingleNode("/CongTyABC/TenCty").InnerText;
            DiaChi = doc.SelectSingleNode("/CongTyABC/DiaChi").InnerText;
            XmlNodeList nodeList = doc.SelectNodes("/CongTyABC/DanhSachHoaDon/HoaDon");
            foreach(XmlNode node in nodeList)
            {
                HoaDon hd = null;
                string maKH = node["MaKH"].InnerText;
                string tenKH = node["TenKH"].InnerText;
                int soLuong = int.Parse(node["SoLuong"].InnerText);
                double gia = double.Parse(node["Gia"].InnerText);

                string loai = node.Attributes["type"].Value;

                if (loai == "HDDaiLy")
                {
                    int soNamHT = int.Parse(node["SoNamHT"].InnerText);
                    hd = new HDDaiLy(maKH, tenKH, soLuong, gia, soNamHT);
                }
                else if (loai == "HDCaNhan")
                {
                    int khoangCach = int.Parse(node["KhoangCach"].InnerText);

                    hd = new HDCaNhan(
                        maKH, tenKH, soLuong, gia, khoangCach);
                }
                else if (loai == "HDCongTy")
                {
                    int SLNV = int.Parse(node["SLNV"].InnerText);

                    hd = new HDCongTy(
                        maKH, tenKH, soLuong, gia, SLNV);
                }
                if (hd != null)
                    DsHoaDon.Add(hd);
            }
        }

        public void XuatDS()
        {
            Console.WriteLine("TEN CONG TY: " + TenCty);
            Console.WriteLine("DIA CHI: " + DiaChi);
            Console.WriteLine();

            Console.WriteLine(
                "{0,-12}{1,-25}{2,-12}{3,-15}{4,-15}{5,-15}{6,-15}",
                "Ma KH",
                "Ten KH",
                "So luong",
                "Gia",
                "Tien hang",
                "Thue VAT",
                "Thanh tien"
            );

            foreach (HoaDon k in DsHoaDon)
            {
                k.Xuat();
                if (k is IHoTro)
                {
                    IHoTro h = (IHoTro)k;
                    Console.WriteLine("Ho tro gia: {0:N0}", h.HoTroGia());
                }
                Console.WriteLine("\n");
            }


        }

        public double TinhTongThanhTien()
        {
            return DsHoaDon.Sum(x => x.TinhThanhTien());
        }

        public double TinhTongHoTro()
        {
            return DsHoaDon.OfType<IHoTro>().Sum(x => x.HoTroGia());
        }

        public void XuatMuaNhieuNhat()
        {
            int muaMax = DsHoaDon.Max(x => x.SoLuong);

            var dsMuaMax = DsHoaDon.Where(x => x.SoLuong == muaMax);

            Console.WriteLine("\nKHACH HANG MUA NHIEU NHAT");
            Console.WriteLine(
               "{0,-12}{1,-25}{2,-12}{3,-15}{4,-15}{5,-15}{6,-15}",
               "Ma KH",
               "Ten KH",
               "So luong",
               "Gia",
               "Tien hang",
               "Thue VAT",
               "Thanh tien"
           );
            foreach (HoaDon hd in dsMuaMax)
                hd.Xuat();
        }

        public void XuatThanhTienMax()
        {
            double thanhTienMax = DsHoaDon.Max(x => x.TinhThanhTien());

            var dsMax = DsHoaDon.Where(x => x.TinhThanhTien() == thanhTienMax);
            Console.WriteLine("\nTHANH TIEN NHIEU NHAT");
            Console.WriteLine(
               "{0,-12}{1,-25}{2,-12}{3,-15}{4,-15}{5,-15}{6,-15}",
               "Ma KH",
               "Ten KH",
               "So luong",
               "Gia",
               "Tien hang",
               "Thue VAT",
               "Thanh tien"
           );
            foreach (HoaDon hd in dsMax)
                hd.Xuat();
        }

        public double TongTienChietKhauCty()
        {
            return DsHoaDon.OfType<HDCongTy>().Sum(x => x.ChietKhau());
        }

        public void SapXepGiamTheoSoLuong()
        {
            DsHoaDon = DsHoaDon.OrderByDescending(x => x.SoLuong).ThenBy(x => x.TinhThanhTien()).ToList();
        }

        public int SoDaiLyCap1()
        {
            return DsHoaDon.OfType<HDDaiLy>().Count(x => x.SoNamHT == 1);
        }
    }
}
