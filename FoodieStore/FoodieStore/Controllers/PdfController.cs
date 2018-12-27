using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoodieStore.Models;
using System.Text;

namespace FoodieStore.Controllers
{
    public class PdfController : Controller
    {
        private App_Context db = new App_Context();

        public ActionResult ExportToExcel()
        {
            var data = (from ord in db.Orders
                        join ordDet in db.OrderDetails on ord.OrderId equals ordDet.OrderId
                        join prod in db.Products on ordDet.ProductId equals prod.ProductId
                        where ord.Username == User.Identity.Name
                        select new OrdersJoin
                        {
                            State = ord.OrderState,
                            OrderDate = ord.OrderDate,
                            Quantity = ordDet.Quantity,
                            ProductPrice = ordDet.ProductPrice,
                            ProductName = prod.ProductName
                        }).ToList();

            GridView view = new GridView();
            view.DataSource = data;
            view.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=myorders.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Charset = "";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    view.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }
    }
}