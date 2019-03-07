using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HinhNen.Controllers
{
    public class TestController : ApiController
    {
        [HttpDelete]
        [ActionName("xoaSP")]
        public IHttpActionResult deleteSanPham(int id)
        {
            try
            {
                HinhNenDataContext bh = new HinhNenDataContext();
                SanPham sp = bh.SanPhams.FirstOrDefault(x => x.maSP == id);
                if (sp == null)
                {
                    return NotFound();
                }
                bh.SanPhams.DeleteOnSubmit(sp);
                bh.SubmitChanges();

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
