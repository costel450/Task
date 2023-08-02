using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using Tesseract;
using System.Text.RegularExpressions;

namespace Task
{
    public partial class Form1 : Form
    {
        private string adobe = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        public void ExtractImagesAndTextFromPDFPage(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            using (var reader = new PdfReader(filePath))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, page, new SimpleTextExtractionStrategy());

                    PdfDictionary pageDict = reader.GetPageN(page);
                    PdfDictionary resources = (PdfDictionary)PdfReader.GetPdfObject(pageDict.Get(PdfName.RESOURCES));
                    ExtractImagesFromPDFPage(resources, sb);

                    sb.Append(pageText);
                }
            }

            string mess = sb.ToString();
            SearchValuesInText(mess);
        }

        public void ExtractImagesFromPDFPage(PdfDictionary resources, StringBuilder sb)
        {
            if (resources == null)
            {
                return;
            }

            PdfDictionary xObjects = (PdfDictionary)PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT));

            if (xObjects == null)
            {
                return;
            }

            foreach (var key in xObjects.Keys)
            {
                var obj = xObjects.Get(key);
                if (obj.IsIndirect())
                {
                    var xObjectDict = (PdfDictionary)PdfReader.GetPdfObject(obj);
                    string objectType = xObjectDict.Get(PdfName.SUBTYPE).ToString();
                    if (objectType.Equals(PdfName.IMAGE.ToString()))
                    {
                        try
                        {

                            PdfImageObject pdfImage = new PdfImageObject((PRStream)xObjectDict);
                            using (MemoryStream memoryStream = new MemoryStream(pdfImage.GetImageAsBytes()))
                            {
                                using (Bitmap bmp = new Bitmap(memoryStream))
                                {
                                    using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                                    {
                                        using (var page = engine.Process(bmp))
                                        {
                                            string text = page.GetText();
                                            sb.AppendLine(text);
                                        }
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        public string extractFEature(string text, string[] feature)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string value in feature)
            {
                int index = text.IndexOf(value, StringComparison.OrdinalIgnoreCase);
                if (index != -1)
                {
                    int startIndex = index;
                    int endIndex = text.IndexOf(' ', startIndex);
                    if (endIndex == -1)
                    {
                        endIndex = text.Length;
                    }
                    string test = text.Substring(startIndex, endIndex - startIndex);
                    if (test.Length >= 19)
                    {
                        test = test.Substring(0, 19);
                    }
                    sb.Append(test);


                }
            }
            string mrn = sb.ToString();
            sb.Clear();
            return mrn;
        }
        public string searchnext(string text, string[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string searchValue in array)
            {
                int index = text.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase);
                if (index != -1)
                {
                    int startIndex = index + searchValue.Length;
                    int endIndex = text.IndexOf(' ', startIndex);
                    if (endIndex == -1)
                    {
                        endIndex = text.Length;
                    }

                    string nextValue = text.Substring(startIndex, endIndex - startIndex).Trim();
                    sb.Append(nextValue);
                }
            }
            string value = sb.ToString();
            return value;
        }
        /*
        public string reg(string text, string pattern)
        {
            StringBuilder sb = new StringBuilder();
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(text);
            sb.Clear();
            foreach (Match match in matches)
            {
                string data = match.Value;
                sb.Append(data);
                break;
            }
            string value= sb.ToString();
            return value;
        }*/
        static List<string> REGVAL(string text, string pattern)
        {
            List<string> results = new List<string>();
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(text);
            foreach (Match match in matches)
            {
                results.Add(match.Value);
            }
            return results;

        }

        static List<string> ExtractValuesFromText(string text, string[] valueArray)
        {
            List<string> extractedValues = new List<string>();

            foreach (string value in valueArray)
            {
                int startIndex = 0;
                while (true)
                {
                    startIndex = text.IndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase);
                    if (startIndex == -1)
                        break;

                    int endIndex = startIndex + value.Length;

                    extractedValues.Add(text.Substring(startIndex, endIndex - startIndex));

                    startIndex += 1;
                }
            }

            return extractedValues;
        }

        public void SearchValuesInText(string text)
        {
            TextBox textBox1 = new TextBox();
            textBox1.Location = new Point(10, 10);
            textBox1.Multiline = true;
            textBox1.Height = 250;
            textBox1.Width = 500;
            textBox1.Visible = true;
            textBox1.Text = text;
            Controls.Add(textBox1);
            string[] searchtext = { "23ROBV" };
            string mrn = extractFEature(text, searchtext);
            StringBuilder sb = new StringBuilder();
            //string[] marfa = { "cod marfa" , "martc", " Cod martc ", "Cod mart: " };
            string pattern = @"\b[83]\d{7}\b";
            List<string> valuesStartingWith8 = REGVAL(text, pattern);

            foreach (string value in valuesStartingWith8)
            {
                sb.Append(value); break;

            }
            string cod_marfa = sb.ToString();
            sb = sb.Clear();
            //string cod_marfa=searchnext(text, marfa);
            //string cod_marfa = reg(text, marfa);

            string patern = @"\b\d{2}/\d{2}/\d{4}\b";
            List<string> data = REGVAL(text, patern);

            foreach (string value in data)
            {
                sb.Append(value); break;

            }
            string date = sb.ToString();
            sb = sb.Clear();
            //string[] provider = { "TURVO", "GENTHERM", "USHIKUBO", "CWB" };
            string[] provider = { "TURVO INTERNATIONAL CO LTD", "GENTHERM INCORPORATED", " USHIKUBO TSZUKIKU", "CWB AUTOMOTIVE ELECTRONICS CO LTD" };

            List<string> providValues = ExtractValuesFromText(text, provider);

            foreach (string value in providValues)
            {
                sb.Append(value); break;

            }
            string provid = sb.ToString();
            sb.Clear();


            string[] tara = { "China", "Statele Unite", "Japonia" };
            List<string> extractedValues = ExtractValuesFromText(text, tara);

            foreach (string value in extractedValues)
            {
                sb.Append(value);
                break;
            }
            string exp = sb.ToString();
            sb = sb.Clear();
    
            string model = @"\b(?<currency>USD|EUR|GBP|JPY|CAD|AUD|CNY)\b";
            List<string> moned = REGVAL(text, model);

            foreach (string value in moned)
            {
                sb.Append(value); break;

            }
            string moneda_plata=sb.ToString();
            sb=sb.Clear();
            /*
            string mod = @"Pret articol\s+(\d+(\.\d{1,2})?)";
            List<string> valo = REGVAL(text, mod);

            foreach (string value in valo)
            {
                sb.Append(value); break;

            }
           
            string valaore = sb.ToString().Replace("Pret articol", "").Trim();

            sb = sb.Clear();

            MessageBox.Show(valaore);
            */
           string mod = @"(?i)(Pret\s*articol|Pretarticol)\s*(\d+(\.\d{1,2})?)";
            List<string> valo = REGVAL(text, mod);

            foreach (string value in valo)
            {
                sb.Append(value); break;

            }
            string valaore=sb.ToString().Replace("Pret articol","").Trim();
            // Create a Regex object with the pattern
            Form2 form2 = new Form2(mrn, date, cod_marfa, provid, exp, moneda_plata, valaore) ;
            form2.Show();

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPdf = new OpenFileDialog();
            openPdf.Filter = "PDF files|*.pdf|All files|*.*";

            if (openPdf.ShowDialog() == DialogResult.OK)
            {
                string filePath = openPdf.FileName;
                Process.Start(adobe, filePath);

                ExtractImagesAndTextFromPDFPage(filePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}