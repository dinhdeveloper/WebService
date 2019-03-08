using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HinhNen.Controllers
{
    public class LoaiSanPhamController : ApiController
    {

        //Lấy tất cả sản phẩm
        [HttpGet]
        [ActionName("getAllLSP")]
        public List<LoaiSP> GetAllLSP()
        {
            HinhNenDataContext bh = new HinhNenDataContext();
            return bh.LoaiSPs.ToList();
        }
        //Lấy một sản phẩm
        [HttpGet]
        [ActionName("getOneLSP")]
        public LoaiSP GetOneLoaiSanPham(int id)
        {
            HinhNenDataContext bh = new HinhNenDataContext();
            var bien = bh.LoaiSPs.FirstOrDefault(x => x.maLSP == id);
            return bien;
        }
        //Lấy tên Loại sản phẩm
        [HttpGet]
        [ActionName("getTenLSP")]
        public IHttpActionResult getTenLSP()
        {
            try
            {
                HinhNenDataContext db = new HinhNenDataContext();
                var list = from sp in db.LoaiSPs
                           select new
                           {
                               sp.maLSP,
                               sp.tenLSP,
                               sp.hinhSLP
                           };

                if (list == null)
                {
                    return NotFound();
                }
                Console.WriteLine(list);
                return Ok(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Thêm loại sản phẩm
        [HttpPost]
        [ActionName("themLSP")]
        public bool InsertSanPham([FromBody] LoaiSP lsp)
        {
            try
            {
                HinhNenDataContext bh = new HinhNenDataContext();
                bh.LoaiSPs.InsertOnSubmit(lsp);
                bh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //sửa loại sản phẩm
        [HttpPut]
        [ActionName("suaLSP")]
        public bool UpdateSanPham([FromBody] LoaiSP lsp)
        {
            try
            {
                HinhNenDataContext bh = new HinhNenDataContext();
                LoaiSP loaisanpham = bh.LoaiSPs.FirstOrDefault(x => x.maLSP == lsp.maLSP);
                if (loaisanpham == null) return false;
                bh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Xóa loại sản phẩm
        [HttpGet]
        [ActionName("xoaLSP")]
        public bool deleteSanPham(int id)
        {
            HinhNenDataContext bh = new HinhNenDataContext();
            LoaiSP loaisanPham = bh.LoaiSPs.FirstOrDefault(x => x.maLSP == id);
            bh.LoaiSPs.DeleteOnSubmit(loaisanPham);
            bh.SubmitChanges();
            return true;
        }
    }
}
