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
            string[] searchtext = {"23BV" };
            /*
             "PRETTL ELECTRIC MEXIC","SFS GROUP CH", "SHANGHAI FL DAI ICHI CHINA","PRETTL VN","BENO PLASTIK TURCIA",
                "CWB AUTOMOTIVE CHINA","FORD OTOMOTIV TR","ALLEGRO MICROSYSTEM PH","KYUNGWON KOREA","ROBERT BOSCH US", "BOSCH LIMITED BANGALORE IN",
                "PRETTL ELECTRIC CN","TE CONECTIVITY HONG KONG","ROBERT BOSCH LLC MX", "UAES CN", "AMPHENOL CHINA","METAL SEAL PRECISION LTD US", "BOSCH AUTOMOTIVE PRODUCTS CHENGDU",
                "PRETTL ELECTRIC CHINA","LOYOUNG ELECTRIC&CABLE SHENZEN CN","SHANGHAI VICO CHINA","ALLEGRO PH","ROBERT BOSCH TH","SHANGHAI FL DAI ICHI CN",
                "PRETTLE ELECTRIC CN","ELFO AG ELVETIA", "FORD MOTOR BR","PRETTL AUTOMOTIVE VIETNAM","KERN LIEBERS CHINA","INOVAN CONTACT CN","BOSCH CORPORATION JP",
                "BOSCH AUTOMOTIVE SYSTEMS WUXI CN","SIGMA&HEARTS TH","BAUMANN SPRINGS CH","BAOTOU TIAHNE MAGNETICS CN","GENTHERM INCORP USA","RPK MX",
                "PRETTL KR","ALLEGRO MICROSYSTEMS PH","JIANGYIN CN","JAGUAR LAND ROVER","ROBERT BOSCH LLC USA","ROBERT BOSCH KR","TURVO INTERN.TAIWAN",
                "LOYOUNG ELECTRIC & CABLE CN","UNITED AUTOMOTIVE CHINA","HONG KONG  LOROM","HEINZ CH","ROBERT BOSCH MY","BOSCH SANYIAI VE TICARET TR",
                "ROBERT BOSCH ID","AMPHENOL MOBILE CONNECTOR CN","BOSCH SANAYI TR","BEIJING ZHONG CN","ROBERT BOSCH GMBH GB","BOSCH CORP JAP","HANGZHOU AMPHENOL",
                "CWB AUTOMOTIVE CN","BOSCH AUTOMOTIVE PRODUCTS SUZHOU","ROBERT BOSCH LLC US","HANGZHOU AMPHENOL CHINA","LOROM CN","TTM ASIA","EMS CHEMIE AG ELVETIA",
                "OSKAR RUEGG ELVETIA","ROBERT BOSCH LLC PIEMONT"
             */

            StringBuilder sb = new StringBuilder();

            foreach (string value in searchtext)
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
                    test = test.Substring(0, 19);
                    sb.Append(test);
                }
            }
            string mrn = sb.ToString();
            sb.Clear();


            string[] textData = {"cod marfa"};

            foreach (string searchValue in textData)
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
            string cod_marfa = sb.ToString();

            string pattern = @"\b\d{2}/\d{2}/\d{4}\b";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(text);
            sb.Clear();
            foreach (Match match in matches)
            {
                string data = match.Value;
                sb.Append(data);
                break;
            }

            string date = sb.ToString();

            Form2 form2 = new Form2(mrn, date, cod_marfa);
            form2.ShowDialog();
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