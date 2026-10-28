using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TT_BT01
{
    abstract class NhanVienABC
    {
        string maNV;
        string hoTen;
        int namSinh;
        string gioiTinh;
        double hsl;
        int namVL;

        public string MaNV { get => maNV; set => maNV = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public int NamSinh { get => namSinh; set => namSinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public double Hsl { get => hsl; set => hsl = value; }
        public int NamVL { get => namVL; set => namVL = value; }

        public NhanVienABC(string maNV, string hoTen, int namSinh, string gioiTinh, double hsl, int namVL)
        {
            this.maNV = maNV;
            this.hoTen = hoTen;
            this.namSinh = namSinh;
            this.gioiTinh = gioiTinh;
            this.hsl = hsl;
            this.namVL = namVL;
        }
        public abstract string XepLoai();

        public abstract double TinhLuong();

        public double TinhThuNhap()
        {
            if (XepLoai() == "A")
                return TinhLuong() + TinhPCTN();
            else if (XepLoai() == "B")
                return 0.75 * TinhLuong() + TinhPCTN();
            else if (XepLoai() == "C")
                return 0.5 * TinhLuong() + TinhPCTN();
            else
                return TinhPCTN();
        }

        public static int LuongCB = 2340000;
        public double TinhPCTN()
        {
            int soNamLamViec = DateTime.Today.Year - namVL;
            return (soNamLamViec / 100.0) * LuongCB;
        }

        public virtual void Xuat()
        {
            Console.WriteLine(
                $"| {maNV,-15}" +
                $"| {hoTen,-25}" +
                $"| {namSinh,-10}" +
                $"| {gioiTinh,-10}" +
                $"| {hsl,-15:F2}" +
                $"| {namVL,-15}" +
                $"| {XepLoai(),-10}" +
                $"| {TinhPCTN(),-10:N0}" +
                $"| {TinhThuNhap(),-20:N0}" +
                $"| {TinhLuong(),-10:N0}"
            );
        }
    }

    class NVSX : NhanVienABC
    {
        public static double HSPCNN = 0.1;
        int soNgayNghi;

        public int SoNgayNghi { get => soNgayNghi; set => soNgayNghi = value; }

        public NVSX(string maNV, string hoTen, int namSinh, string gioiTinh, double hsl, int namVL, int soNgayNghi)
            : base(maNV, hoTen, namSinh, gioiTinh, hsl, namVL)
        {
            this.soNgayNghi = soNgayNghi;
        }

        public override string XepLoai()
        {
            if (soNgayNghi == 0) return "A";
            else if (soNgayNghi <= 3) return "B";
            else if (soNgayNghi <= 5) return "C";
            else return "D";
        }

        public override double TinhLuong()
        {
            return Hsl * LuongCB * (1 + HSPCNN);
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine("So ngay nghi: " + SoNgayNghi);
        }
    }
    class NVKD : NhanVienABC
    {
        double doanhSoBanHangToiThieu;
        double doanhSoBanHangThucTe;

        public double DoanhSoBanHangToiThieu { get => doanhSoBanHangToiThieu; set => doanhSoBanHangToiThieu = value; }
        public double DoanhSoBanHangThucTe { get => doanhSoBanHangThucTe; set => doanhSoBanHangThucTe = value; }

        public NVKD(string maNV, string hoTen, int namSinh, string gioiTinh, double hsl, int namVL, double doanhSoBanHangToiThieu, double doanhSoBanHangThucTe)
            : base(maNV, hoTen, namSinh, gioiTinh, hsl, namVL)
        {
            this.doanhSoBanHangToiThieu = doanhSoBanHangToiThieu;
            this.doanhSoBanHangThucTe = doanhSoBanHangThucTe;
        }

        public override string XepLoai()
        {
            if (DoanhSoBanHangThucTe > 2 * DoanhSoBanHangToiThieu)
            {
                return "A";
            }
            else if (DoanhSoBanHangThucTe >= DoanhSoBanHangToiThieu)
            {
                return "B";
            }
            else if (DoanhSoBanHangThucTe < 0.5 * DoanhSoBanHangToiThieu)
            {
                return "D";
            }
            else
            {
                return "C";
            }
        }

        public double TinhHoaHong()
        {
            if (DoanhSoBanHangThucTe <= DoanhSoBanHangToiThieu)
            {
                return 0;
            }

            return 0.15 * (DoanhSoBanHangThucTe - DoanhSoBanHangToiThieu);
        }

        public override double TinhLuong()
        {
            return Hsl * LuongCB + TinhHoaHong();
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine("Doanh so ban hang toi thieu: " + DoanhSoBanHangToiThieu);
            Console.WriteLine("Doaanh so ban hang thuc te: " + DoanhSoBanHangThucTe);
            Console.WriteLine("Hoa hong: " + TinhHoaHong());
        }
    }

    class CBQL : NhanVienABC
    {
        string chucVu;
        double hsChucVu;

        public string ChucVu { get => chucVu; set => chucVu = value; }
        public double HsChucVu { get => hsChucVu; set => hsChucVu = value; }

        public CBQL(string maNV, string hoTen, int namSinh, string gioiTinh, double hsl, int namVL, string chucVu, double hsChucVu)
            : base(maNV, hoTen, namSinh, gioiTinh, hsl, namVL)
        { 
            this.chucVu = chucVu;
            this.hsChucVu = hsChucVu;
        }

        public override string XepLoai()
        {
            return "A";
        }

        public double PCCV()
        {
            return hsChucVu * 1350;
        }

        public override double TinhLuong()
        {
            return Hsl * LuongCB + PCCV();
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine("Chuc vu: " + ChucVu);
            Console.WriteLine("He so phu cap chuc vu: " + HsChucVu);
            Console.WriteLine("Phu cap chuc vu: " + PCCV());
        }
    }
    class DsNhanVien
    {   
        List<NhanVienABC> lstNhanVien;

        internal List<NhanVienABC> LstNhanVien { get => lstNhanVien; set => lstNhanVien = value; }

        public DsNhanVien()
        {
            LstNhanVien = new List<NhanVienABC>();
        }

        public void DocFileXML(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNodeList nodeList = doc.SelectNodes("/DanhSachNhanVien/NhanVien");
            foreach(XmlNode node in nodeList)
            {
                NhanVienABC nhanVien;

                // Loai laa mot element
                int loai = int.Parse(node["LoaiNhanVien"].InnerText);

                // Loai la mot attributes cua XmlNode NhanVien
                // int loai = int.Parse(node.Attributes["Loai"].Value.ToString());

                string maNV = node["MaNV"].InnerText;
                string hoTen = node["HoTen"].InnerText;
                int namSinh = int.Parse(node["NamSinh"].InnerText);
                string gioiTinh = node["GioiTinh"].InnerText;
                double hsl = double.Parse(node["Hsl"].InnerText);
                int namVL = int.Parse(node["NamVL"].InnerText);

                if (loai == 1)
                {
                    int soNgayNghi = int.Parse(node["SoNgayNghi"].InnerText);
                    nhanVien = new NVSX(maNV,  hoTen, namSinh, gioiTinh, hsl, namVL, soNgayNghi);
                }
                else if (loai == 2)
                {
                    double dsToiThieu = double.Parse(node["DoanhSoBanHangToiThieu"].InnerText);
                    double dsThucTe = double.Parse(node["DoanhSoBanHangThucTe"].InnerText);
                    nhanVien = new NVKD(maNV, hoTen, namSinh, gioiTinh, hsl, namVL, dsToiThieu, dsThucTe);
                }
                else
                {
                    string chucVu = node["ChucVu"].InnerText;
                    double hsChucVu = double.Parse(node["HsChucVu"].InnerText);
                    nhanVien = new CBQL(maNV, hoTen, namSinh, gioiTinh, hsl, namVL, chucVu, hsChucVu);
                }

                LstNhanVien.Add(nhanVien);
            }
            Console.WriteLine("Doc file thanh cong!");
        }

        public void xuatDSNV()
        {
            Console.WriteLine(
                $"| {"Ma NV",-15}" +
                $"| {"Ho ten",-25}" +
                $"| {"Nam sinh",-10}" +
                $"| {"Gioi tinh",-10}" +
                $"| {"He so luong",-15}" +
                $"| {"Nam vao lam",-15}" +
                $"| {"Xep loai",-10}" +
                $"| {"PCTN",-10}" +
                $"| {"Thu nhap",-20}" +
                $"| {"Luong",-10}"
            );
            foreach (var a in LstNhanVien)
            {

                a.Xuat();
                Console.WriteLine(new string('-', 150));
            }
        }

        public double TinhTongThuNhap()
        {
            return LstNhanVien.Sum(t => t.TinhThuNhap());
        }
    }






}
