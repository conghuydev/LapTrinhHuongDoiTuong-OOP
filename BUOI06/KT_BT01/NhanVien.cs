using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KT_BT01
{
    internal class NhanVien
    {
        private string maNV;
        private string tenNV;
        private int namVL;
        private double heSoLuong;
        private int soNgayNghi;

        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public int NamVL { get => namVL; set => namVL = value; }
        public double HeSoLuong { get => heSoLuong; set => heSoLuong = value; }
        public int SoNgayNghi { get => soNgayNghi; set => soNgayNghi = value; }

        public NhanVien(string maNV, string tenNV, int namVL, double heSoLuong, int soNgayNghi)
        {
            MaNV = maNV;
            TenNV = tenNV;
            NamVL = DateTime.Today.Year;
            HeSoLuong = heSoLuong;
            SoNgayNghi = 0;
        }

        public NhanVien()
        {
            MaNV = "NV01";
            TenNV = "Ho ten NV";
            NamVL = 2006;
            SoNgayNghi = 1;
            HeSoLuong = 3.0;
        }

        public NhanVien(string maNV, string tenNV, int namVL, int soNgayNghi, double heSoLuong)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.namVL = namVL;
            this.soNgayNghi = soNgayNghi;
            this.heSoLuong = heSoLuong;
        }

        public static int LuongCB = 1150;
        public double TinhPCTN()
        {
            int thamNien = DateTime.Now.Year - NamVL;
            if (thamNien >= 5)
                return thamNien * LuongCB / 100.0;
            return 0;
        }

        public virtual char XepLoai()
        {
            if (SoNgayNghi <= 1)
                return 'A';
            if (SoNgayNghi <= 3)
                return 'B';
            return 'C';
        }

        public virtual double Luong()
        {
            double hsThiDua = 0.5;
            char xl = XepLoai();
            if (xl == 'A')
                hsThiDua = 1.0;
            if (xl == 'B')
                hsThiDua = 0.75;
            return LuongCB * HeSoLuong * hsThiDua + TinhPCTN();
        }

        public virtual void NhapThongTin()
        {
            Console.WriteLine("Nhap ma nhan vien: ");
            MaNV = Console.ReadLine();
            Console.WriteLine("Nhap ten nhan vien: ");
            TenNV = Console.ReadLine();
            Console.WriteLine("Nhap nam vao lam: ");
            NamVL = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhap he so luong: ");
            HeSoLuong = double.Parse(Console.ReadLine());
            Console.WriteLine("Nhap so ngay nghi: ");
            SoNgayNghi = int.Parse(Console.ReadLine());
        }

        public virtual void XuatThongTin()
        {
            Console.WriteLine(
                $"|{MaNV,-10}" +
                $"|{TenNV,-25}" +
                $"|{NamVL,-15}" +
                $"|{HeSoLuong,-15:F2}" +
                $"|{SoNgayNghi,-15}" +
                $"|{TinhPCTN(),-15:F2}" +
                $"|{XepLoai(),-10}" +
                $"| {Luong(),-15:F2}"
            );
        }
    }

    class CanBo : NhanVien
    {
        string chucVu;
        string phongBan;
        double phuCapLD;

        public string ChucVu { get => chucVu; set => chucVu = value; }
        public string PhongBan { get => phongBan; set => phongBan = value; }
        public double PhuCapLD { get => phuCapLD; set => phuCapLD = value; }

        public CanBo(string maNV, string tenNV, double heSoLuong, string chucVu, double phuCapLD)
            : base(maNV, tenNV, DateTime.Today.Year, 1, heSoLuong)
        {
            this.chucVu = chucVu;
            this.phuCapLD = phuCapLD;
        }

        public CanBo() : base()
        {
            chucVu = "Truong Phong";
            phongBan = "Phong Hanh Chinh";
            phuCapLD = 5.0;
        }


        public CanBo(
            string maNV, string tenNV, int namVL, int soNgayNghi, double heSoLuong, string chucVu, string phongBan, double phuCapLD)
            : base(maNV, tenNV, namVL, soNgayNghi, heSoLuong)
        {
            this.chucVu = chucVu;
            this.phongBan = phongBan;
            this.phuCapLD = phuCapLD;
        }

        public override char XepLoai()
        {
            return 'A';
        }

        public double TinhPCLD()
        {
            return phuCapLD * LuongCB;
        }

        public override double Luong()
        {
            return base.Luong() + TinhPCLD();
        }

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.WriteLine("Nhap chuc vu: ");
            ChucVu = Console.ReadLine();
            Console.WriteLine("Nhap phong ban: ");
            PhongBan = Console.ReadLine();
            Console.WriteLine("Nhap he so phu cap lanh dao: ");
            PhuCapLD = double.Parse(Console.ReadLine());
        }

        public override void XuatThongTin()
        {
            base.XuatThongTin();

            Console.WriteLine(
                $"   Chuc vu: {ChucVu} | " +
                $"Phong ban: {PhongBan} | " +
                $"Phu cap lanh dao: {TinhPCLD():N2}"
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
            XmlNodeList nodeList = doc.SelectNodes("/DanhSach/NhanVien");
            foreach (XmlNode node in nodeList)
            {
                //NhanVien a = new NhanVien();
                //a.MaNV = node["MaNV"].InnerText;
                //a.TenNV = node["HoTen"].InnerText;
                //a.NamVL = int.Parse(node["NamVaoLam"].InnerText);
                //a.HeSoLuong = double.Parse(node["HeSoLuong"].InnerText);
                //a.SoNgayNghi = int.Parse(node["SoNgayNghi"].InnerText);
                //DsNV.Add(a);
                string loai = node["Loai"].InnerText;
                string ma = node["MaNV"].InnerText;
                string ten = node["TenNV"].InnerText;
                int nam = int.Parse(node["NamVaoLam"].InnerText);
                double hsl = double.Parse(node["HeSoLuong"].InnerText);
                int ngayNghi = int.Parse(node["SoNgayNghi"].InnerText);


                if (loai == "CanBo")
                {
                    string chucVu = node["ChucVu"].InnerText;
                    string phongBan = node["PhongBan"].InnerText;
                    double phuCapLD = double.Parse(node["HeSoPhuCapLanhDao"].InnerText);

                    CanBo cb = new CanBo(ma, ten, nam, ngayNghi, hsl, chucVu, phongBan, phuCapLD);

                    DsNV.Add(cb);
                }
                else
                {
                    NhanVien nv = new NhanVien(ma, ten, nam, ngayNghi, hsl);
                    DsNV.Add(nv);
                }
            }
        }

        public void XuatDS()
        {
            Console.WriteLine(
                $"{"Ma NV",-10}" +
                $"{"Ten nhan vien",-25}" +
                $"{"Nam vao lam",-12}" +
                $"{"He so luong",-12}" +
                $"{"Ngay nghi",-12}" +
                $"{"PCTN",-15}" +
                $"{"Xep loai",-10}" +
                $"{"Luong",-15}"
            );

            Console.WriteLine(new string('-', 111));

            foreach (var a in DsNV)
            {
                a.XuatThongTin();
            }
        }
    }
}


