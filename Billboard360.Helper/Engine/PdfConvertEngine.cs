using System;
using System.Collections.Generic;
using System.Text;
using IronPdf;
using System.IO;

namespace Billboard360.Helper.Engine
{
    public class PdfConvertEngine
    {
        public string ConvertHTMLToPDF(string content, string path, string fileName)
        {
            
            HtmlToPdf renderer = new HtmlToPdf();

            CheckDirectory(path);

            var Renderer = new IronPdf.HtmlToPdf();

            var PDF = Renderer.RenderHtmlAsPdf(content);
            var OutputPath = path + fileName + ".pdf";
            PDF.SaveAs(OutputPath);

        

            return OutputPath;
        }

        private void CheckDirectory(string path)
        {
            if(!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
        }

        private string writeHTML(string body, string pathFile, string fileName)
        {
            string path = pathFile + fileName + ".html";

            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(body);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }

            return path;
            
        }
    }
}
