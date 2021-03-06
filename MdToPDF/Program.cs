﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            var md = @"
# Fitness club membership

## Mr.Tsuyoshi Ushio

You are the member of the muscle fitness.

* Zama branch membership
* Gold membershiop
* Protein service included

## Message from manger

You can enjoy muscle training with great equipments. Grow your muscle!
";
            var byteArray = Encoding.UTF8.GetBytes(md);
            var stream = new MemoryStream(byteArray);

            using (var reader = new System.IO.StreamReader(stream))
            using (var writer = new System.IO.StreamWriter("result.html"))
            {
                CommonMark.CommonMarkConverter.Convert(reader, writer);
            }

            var html = CommonMark.CommonMarkConverter.Convert(md);
            Console.WriteLine(html);
            Console.ReadLine();

            Byte[] result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                result = ms.ToArray();
            }
            using (BinaryWriter w = new BinaryWriter(File.OpenWrite(@"result.pdf")))
            {
                w.Write(result);
            }


        }
    }
}
