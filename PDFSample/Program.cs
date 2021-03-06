﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSample
{
    class Program
    {
        static void Main(string[] args)
        {

            var doc = new Document(PageSize.A4.Rotate());
            var stream = new MemoryStream();
            
            var pw = PdfWriter.GetInstance(doc, stream);

            doc.Open();
            var pdfContentByte = pw.DirectContent;
            var bf = BaseFont.CreateFont(@"c:\windows\fonts\msgothic.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            // A4 595x842 pt = 210x297  
            
            pdfContentByte.SetFontAndSize(bf, 24);
            ShowTextAligned(pdfContentByte, 20, 561, "Fitness Club Membership");

            DrawLine(pdfContentByte, 2, 551, 840, 551);
            
            pdfContentByte.SetFontAndSize(bf, 11);
            ShowTextAligned(pdfContentByte, 20, 530, "You can enojy Muscle Training and high-quality protein.");

            pdfContentByte.SetFontAndSize(bf, 18);
            ShowTextAligned(pdfContentByte, 20, 480, "Name: Mr.Tsuyoshi Ushio Age: 46");
            ShowTextAligned(pdfContentByte, 20, 457, "Membership: Gold");

            doc.Close();

            using (BinaryWriter w = new BinaryWriter(File.OpenWrite(@"result.pdf"))) {
                w.Write(stream.ToArray());
            }
            Console.WriteLine("See the result.pdf");
        }
        private static void ShowTextAligned(PdfContentByte pdfContentByte, float x, float y, string text, int alignment = Element.ALIGN_LEFT, float rotaion = 0)
        {
            pdfContentByte.BeginText();
            pdfContentByte.ShowTextAligned(alignment, text, x, y, rotaion);
            pdfContentByte.EndText();
        }
        private static void DrawLine(PdfContentByte pdfContentByte, float fromX, float fromY, float toX, float toY)
        {
            pdfContentByte.MoveTo(fromX, fromY);
            pdfContentByte.LineTo(toX, toY);
            pdfContentByte.ClosePathStroke();
        }
    }
}
