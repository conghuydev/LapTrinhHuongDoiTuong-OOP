using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;

namespace KT_BT02
{
    internal class NhanVien
    {
        string maNV;
        string tenNV;
        double hsl;

        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public double Hsl { get => hsl; set => hsl = value; }

        public NhanVien()
        {
            MaNV = "NV001";
            TenNV = "Ho va ten sinh vien";
            Hsl = 2.34;
        }

        public NhanVien(string maNV, string tenNV, double hsl)
        {
            if (maNV != null && maNV.StartsWith("NV"))
                MaNV = maNV;
            else
                MaNV = "NV001";

            TenNV = tenNV;
            Hsl = hsl;
        }

        public static double LCB = 1150;

        public  virtual double TinhThuNhap()
        {
            return Hsl * LCB;
        }

        public virtual void Xuat()
        {
            Console.WriteLine(
                $"{maNV,-15}" +
                $"{tenNV,-25}" +
                $"{hsl,-15}" +
                $"{TinhThuNhap(),-15:F2}"
            );
        }
    }

    class CBLanhDao: NhanVien
    {
        string chucVu;
        int thamNien;

        public string ChucVu { get => chucVu; set => chucVu = value; }
        public int ThamNien { get => thamNien; set => thamNien = value; }

        public CBLanhDao(): base("NV009", "DieuHien", 4.67)
        {
            ChucVu = "GiamDoc";
            ThamNien = 10;
        }

        public CBLanhDao(string maNV, string tenNV, double hsl, string chucVu, int thamNien) : base(maNV, tenNV, hsl)
        {
            ChucVu = chucVu;
            ThamNien = thamNien;
        }

        public double HeSoLanhDao()
        {
            if (ChucVu == "GiamDoc")
                return 7.0;

            else if (ChucVu == "TruongPhong")
                return 6.0;

            else if (ChucVu == "PhoPhong")
                return 4.5;

            else
                return 1.0;
        }

        public override double TinhThuNhap()
        {
            double phuCapLanhDao = 1500 * HeSoLanhDao();
            return base.TinhThuNhap() + phuCapLanhDao;
        }

        public override void Xuat()
        {
            Console.WriteLine(
                $"{MaNV,-15}" +
                $"{TenNV,-25}" +
                $"{Hsl,-15:F2}" +
                $"{TinhThuNhap(),-15:F2}" +
                $"{ChucVu,-20}" +
                $"{ThamNien,-10}"
            );
        }
    }

    class QuanLyNhanVien
    {
        List<NhanVien> dsNV;

        internal List<NhanVien> DsNV { get => dsNV; set => dsNV = value; }

        public QuanLyNhanVien()
        {
            DsNV = new List<NhanVien>();
        }

        public void DocFile(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNodeList nodeList = doc.SelectNodes("/DanhSachNhanVien/NhanVien");
            foreach (XmlNode node in nodeList)
            {
                string loai = node["Loai"].InnerText;
                string ma = node["MaNV"].InnerText;
                string ten = node["TenNV"].InnerText;
                double hsl = double.Parse(
                    node["Hsl"].InnerText,
                    CultureInfo.InvariantCulture
                );
                if (loai == "CBLanhDao")
                {
                    string chucVu = node["ChucVu"].InnerText;
                    int thamNien = int.Parse(node["ThamNien"].InnerText);
                    CBLanhDao a = new CBLanhDao(ma, ten, hsl, chucVu, thamNien);
                    DsNV.Add(a);
                }
                else if(loai =="NhanVien")
                {
                    NhanVien a = new NhanVien(ma, ten, hsl);
                    DsNV.Add(a);
                }
            }
            Console.WriteLine("Doc file thanh cong!");
        }

        public void XuatDS()
        {
            Console.WriteLine(
                $"{"Ma NV",-15}" +
                $"{"Ten NV",-25}" +
                $"{"Hs luong",-15}" +
                $"{"Thu nhap",-15}" +
                $"{"Chuc vu",-20}" +
                $"{"Tham nien",-10}"
            );

            foreach (var a in DsNV)
                a.Xuat();
        }



    }

}
