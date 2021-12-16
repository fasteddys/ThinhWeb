using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Service
{
    public interface ICrawlingStockService
    {
        //lấy dữ liệu ngành nghề
        Task GetNhomNganh();
        Task LoginPage();
        Task GetDanhSachCoPhieu();
        Task GetNganh();
    }
}
