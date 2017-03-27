using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Web;
using System.IO;

public class CreateExcelDoc
{
    /// <summary>
    /// https://www.codeproject.com/Articles/20228/Using-C-to-Create-an-Excel-Document
    /// </summary>
    /// 
    private Application app = null;
    private Workbook workbook = null;
    private Worksheet worksheet = null;
    private Range workSheet_range = null;

    public CreateExcelDoc()
    {
        createDoc();
    }

    public void createDoc()
    {
        try
        {
            app = new Application();
            app.Visible = false;
            workbook = app.Workbooks.Add(1);
            worksheet = (Worksheet)workbook.Sheets[1];


            //workbook.Close();
        }
        catch (Exception e)
        {
            Console.Write("Error");
        }
        finally
        {
        }
    }

    public void createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
    {
        worksheet.Cells[row, col] = htext;
        workSheet_range = worksheet.get_Range(cell1, cell2);
        workSheet_range.Merge(mergeColumns);
        switch (b)
        {
            case "YELLOW":
                workSheet_range.Interior.Color = System.Drawing.Color.Yellow.ToArgb();
                break;
            case "GRAY":
                workSheet_range.Interior.Color = System.Drawing.Color.Gray.ToArgb();
                break;
            case "GAINSBORO":
                workSheet_range.Interior.Color =
        System.Drawing.Color.Gainsboro.ToArgb();
                break;
            case "Turquoise":
                workSheet_range.Interior.Color =
        System.Drawing.Color.Turquoise.ToArgb();
                break;
            case "PeachPuff":
                workSheet_range.Interior.Color =
        System.Drawing.Color.PeachPuff.ToArgb();
                break;
            default:
                //  workSheet_range.Interior.Color = System.Drawing.Color..ToArgb();
                break;
        }

        workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
        workSheet_range.Font.Bold = font;
        workSheet_range.ColumnWidth = size;
        if (fcolor.Equals(""))
        {
            workSheet_range.Font.Color = System.Drawing.Color.White.ToArgb();
        }
        else
        {
            workSheet_range.Font.Color = System.Drawing.Color.Black.ToArgb();
        }
    }

    public void addData(int row, int col, string data, string cell1, string cell2, string format)
    {
        worksheet.Cells[row, col] = data;
        workSheet_range = worksheet.get_Range(cell1, cell2);
        workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
        workSheet_range.NumberFormat = format;
    }

    public void closeExcel()
    {
        string currentDateTime = "Bills_Report" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string path = HttpContext.Current.Server.MapPath("Bills" + "\\" + currentDateTime + ".xlsx");
        workbook.SaveAs(path);
        workbook.Close();
        app.DisplayAlerts = false;
        app.Quit();

        FileInfo file = new FileInfo(path);
        if (file.Exists)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + currentDateTime + ".xlsx");
            HttpContext.Current.Response.AddHeader("Content-Type", "application/Excel");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.End();
        }
    }
}
