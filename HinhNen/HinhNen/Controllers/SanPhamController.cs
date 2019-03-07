using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HinhNen.Controllers
{
    public class SanPhamController : ApiController
    {
        //Lấy toàn bộ sản phẩm
        [HttpGet]
        [ActionName("getAllSP")]
        public List<SanPham> GetAllSanPham()
        {
            HinhNenDataContext bh = new HinhNenDataContext();
            return bh.SanPhams.ToList();
        }
        //Lấy 1 sản phẩm 
        [HttpGet]
        [ActionName("getMotSP")]
        public SanPham GetOneSanPham(int id)
        {
            HinhNenDataContext bh = new HinhNenDataContext();
            var bien = bh.SanPhams.FirstOrDefault(x => x.maSP == id);
            return bien;
        }
        //[HttpGet]
        //[ActionName("getTheoDanhMuc")]
        //public IHttpActionResult getListSanPham(int id)
        //{
        //    try
        //    {
        //        HinhNenDataContext bh = new HinhNenDataContext();
        //        List<SanPham> lsp = bh.SanPhams.Where(x => x.maLSP == id).ToList();
        //        if (lsp == null)
        //        {
        //            return NotFound();
        //        }
        //        //for (int i = 0; i < lsp.Count; i++)
        //        //{
        //        //    lsp[i].DanhMucSanPham = null;
        //        //    lsp[i].SanPhamYeuThiches = null;
        //        //    lsp[i].ChiTietDonHangs = null;
        //        //}
        //        return Ok(lsp);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //

        //lấy danh sách sản phẩm cùng mã loại sản phẩm
        [HttpGet]
        [ActionName("getDanhMucSP")]
        public IHttpActionResult getListSanPham(int id)
        {
            try
            {
                HinhNenDataContext db = new HinhNenDataContext();
                var list = from sp in db.SanPhams
                           where sp.maLSP == id
                           select new
                           {
                               sp.TenSP,
                               sp.HinhSP
                           };

                if (list == null)
                {
                    return NotFound();
                }
                Console.WriteLine(list);
                //foreach(var a in list)
                //{
                //    Console.WriteLine(a);
                //}
                return Ok(list);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //lấy 10 sản phẩm mới nhất
        [HttpGet]
        [ActionName("topMuoiSP")]
        public IHttpActionResult gettopSanPham()
        {
            try
            {
                HinhNenDataContext db = new HinhNenDataContext();
                var list = (from sp in db.SanPhams
                            select new
                            {
                                sp.maSP,
                                sp.TenSP,
                                sp.HinhSP
                            }).OrderByDescending(x => x.maSP).Take(12);

                if (list == null)
                {
                    return NotFound();
                }
                Console.WriteLine(list);
                //foreach(var a in list)
                //{
                //    Console.WriteLine(a);
                //}
                return Ok(list);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //thêm sản phẩm
        [HttpPost]
        [ActionName("themSP")]
        public bool InsertSanPham([FromBody] SanPham sp)
        {
            try
            {
                HinhNenDataContext bh = new HinhNenDataContext();
                bh.SanPhams.InsertOnSubmit(sp);
                bh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //sửa sản phẩm
        [HttpPut]
        [ActionName("suaSP")]
        public bool UpdateSanPham([FromBody] SanPham sp)
        {
            try
            {
                HinhNenDataContext bh = new HinhNenDataContext();
                SanPham sanpham = bh.SanPhams.FirstOrDefault(x => x.maSP == sp.maSP);
                if (sanpham == null) return false;
                bh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        //xóa sản phẩm
        [HttpGet]
        [ActionName("xoaSP")]
        public IHttpActionResult deleteSanPham(int id)
        {
            try
            {
                HinhNenDataContext bh = new HinhNenDataContext();
                SanPham sp = bh.SanPhams.FirstOrDefault(x => x.maSP == id);
                if(sp == null)
                {
                    return NotFound();
                }
                bh.SanPhams.DeleteOnSubmit(sp);
                bh.SubmitChanges();

                return Ok(true);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //public bool deleteSanPham(int id)
        //{
        //    HinhNenDataContext bh = new HinhNenDataContext();
        //    SanPham sanPham = bh.SanPhams.FirstOrDefault(x => x.maSP == id);
        //    bh.SanPhams.DeleteOnSubmit(sanPham);
        //    bh.SubmitChanges();
        //    return true;
        //}
    }
}
