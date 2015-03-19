using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EJournalManager.Controllers
{
    public class ExportDataController : HttpContextBase
    {
        //
        // GET: /ExportData/

        /// <summary>
        ///     Export datatable to excel
        /// </summary>
        /// <returns></returns>
        public void ExportToExcel(DataTable dt, string fileName, string[] arr)
        {
            DataRow[] rows = dt.Select();
            var view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, arr);
            var gv = new GridView {DataSource = distinctValues};
            DataRow[] myRows = distinctValues.Select();
            gv.DataBind();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        ///     Export datatable to pdf
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="arr"></param>
        public void ExportToPDF(DataTable dt, string fileName, string[] arr)
        {
            // 
            // For PDF export we are using the free open-source iTextSharp library.
            //
            DataRow[] rows = dt.Select();

            var view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, arr);
            var gv = new GridView();
            var pdfDoc = new Document();
            var pdfStream = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, pdfStream);

            pdfDoc.Open(); //Open Document to write
            pdfDoc.NewPage();

            Font font8 = FontFactory.GetFont("ARIAL", 7);

            var pdfTable = new PdfPTable(distinctValues.Columns.Count);
            PdfPCell pdfPCell = null;

            //Add Header of the pdf table
            for (int column = 0; column < distinctValues.Columns.Count; column++)
            {
                pdfPCell = new PdfPCell(new Phrase(new Chunk(distinctValues.Columns[column].Caption, font8)));
                pdfTable.AddCell(pdfPCell);
            }

            //How add the data from datatable to pdf table
            for (int row = 0; row < distinctValues.Rows.Count; row++)
            {
                for (int column = 0; column < distinctValues.Columns.Count; column++)
                {
                    pdfPCell = new PdfPCell(new Phrase(new Chunk(distinctValues.Rows[row][column].ToString(), font8)));
                    pdfTable.AddCell(pdfPCell);
                }
            }

            pdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table            
            pdfDoc.Add(pdfTable); // add pdf table to the document
            pdfDoc.Close();
            pdfWriter.Close();

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            HttpContext.Current.Response.BinaryWrite(pdfStream.ToArray());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        ///     Export datatable to text file
        /// </summary>
        /// <returns></returns>
        public void ExportToText(DataTable dt, string fileName, string[] arr)
        {
            var view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, arr);
            var str = new StringBuilder();

            var stringWrite = new StringWriter();
            foreach (string s in arr)
            {
                str.Append(s + "|");
            }
            str.Append(Environment.NewLine);
            for (int row = 0; row < distinctValues.Rows.Count; row++)
            {
                for (int column = 0; column < distinctValues.Columns.Count; column++)
                {
                    str.Append(distinctValues.Rows[row][column]);
                    str.Append("|");
                }
                str.Append(Environment.NewLine);
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".txt");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.text";


            var htmlWrite =
                new HtmlTextWriter(stringWrite);

            HttpContext.Current.Response.Write(str.ToString());
            HttpContext.Current.Response.End();
        }
    }
}