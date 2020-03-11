using GenaratePdf.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GenaratePdf.Controllers
{
    public class PdfController : Controller
    {
        GeneratePdfDbContext db = new GeneratePdfDbContext();
        // GET: Pdf
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        [HttpGet]
        public FileStreamResult GetFile()
        {
            List<Customer> list = new List<Customer>();
            // collect data from database and store webgrid
            WebGrid grid = new WebGrid(source: db.Customers.ToList(), canPage: false, canSort:false);
            //serialige data to view formate.
            string gridhtml = grid.GetHtml(
                columns: grid.Columns(
                    grid.Column("Id", "Id"),
                    grid.Column("Name", "Name"),
                    grid.Column("Address", "Address"),
                    grid.Column("City", "City")
                    )
                    ).ToString();
            //add some style for pdf view
            string exportdata = string.Format("<html><head>{0}</head><body>{1}</body></html>", "<style>table{border-spacing:10px;border-collapse:separate;}</style>", gridhtml);
            var bytes = System.Text.Encoding.UTF8.GetBytes(exportdata);
            MemoryStream input = new MemoryStream(bytes);
            var output = new MemoryStream();
            //declare page sige and page margin
            var document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
            var writer = PdfWriter.GetInstance(document, output);
            writer.CloseStream = false;
            document.Open();
            var xml = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
            xml.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
            document.Close();
            output.Position = 0;
            return new FileStreamResult(output, "application/pdf");
        }
    }
}