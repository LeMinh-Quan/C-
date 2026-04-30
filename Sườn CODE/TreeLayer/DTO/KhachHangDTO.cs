using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHangDTO
    {
        string sdt, hoTen, diaChi, email;


        KhachHangDTO() { }
        public KhachHangDTO(string sdt, string hoTen, string diaChi, string email)
        {
            this.Sdt = sdt;
            this.HoTen = hoTen;
            this.DiaChi = diaChi;
            this.Email = email;
        }

        public string Sdt { get => sdt; set => sdt = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string Email { get => email; set => email = value; }
    }
}
