using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace PackageCreator_AudioBook
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            //the first way
            this.progressBar1.Minimum = 1;
            this.progressBar1.Maximum = 5000;
            this.progressBar1.Value = 1;
            this.progressBar1.Step = 1;
            this.progressBar1.Visible = false;
            

        }
        class Item
        {
            public int Key;
            public string Value;
        }
        
        private void btn_Generate_Click(object sender, EventArgs e)
        {
            int error_Count = 0;
            this.progressBar1.Visible = true;

            //validation
            if (rdo_Simplified.Checked == false && rdo_Detailed.Checked == false && rdo_FetchMetadata.Checked == false) MessageBox.Show("Please select a process type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            string fileName = txt_FilePath.Text;
            string log = @"log/PackageCreator_AudioBook.log";
            StreamWriter file = new StreamWriter(log, false);
            string Ter_ini = @"bin/Territory_Map.ini";
            
            //Cell and terretory

            if (!File.Exists(Ter_ini))
            {
                MessageBox.Show(this, "\"bin/Territory_Map.ini\" is not found.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(log))
            {
                File.Create(log);
                file.WriteLine("-------------------------------------------");
                file.WriteLine("   Package Creator Audio Book (" + Application.ProductVersion + ")");
                file.WriteLine("-------------------------------------------");
                //file.Close();
            }
            else
            {
                file.Flush();
                file.WriteLine("-------------------------------------------");
                file.WriteLine("   Package Creator Audio Book (" + Application.ProductVersion + ")");
                file.WriteLine("-------------------------------------------");
                //file.Close();

            }


            var TerLines = File.ReadAllLines(Ter_ini);    
            

            var cusTerretory = new List<Item>();
            var dict = new Dictionary<string, string>();

            foreach (var terVal in TerLines)
            {
                var terKeyVal = terVal.Split('=');
                if (int.TryParse(terKeyVal[0].ToString(), out int a))
                {
                    cusTerretory.Add(new Item() { Key = int.Parse(terKeyVal[0]), Value = terKeyVal[1].ToString() });
                }
                else
                {
                    continue;
                }
            }


            //var cusTerretory = new List<Item>();

            //cusTerretory.Add(new Item() { Key = 7, Value = "US" });
            //cusTerretory.Add(new Item() { Key = 8, Value = "CA" });
            //cusTerretory.Add(new Item() { Key = 9, Value = "JP" });
            //cusTerretory.Add(new Item() { Key = 10, Value = "AU" });
            //cusTerretory.Add(new Item() { Key = 11, Value = "NZ" });
            //cusTerretory.Add(new Item() { Key = 12, Value = "GB" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "FR" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "AT" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "BE" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "DE" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "ES" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "FI" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "IE" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "GR" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "IT" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "LU" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "NL" });
            //cusTerretory.Add(new Item() { Key = 13, Value = "PT" });
            //cusTerretory.Add(new Item() { Key = 14, Value = "NO" });
            //cusTerretory.Add(new Item() { Key = 15, Value = "DK" });
            //cusTerretory.Add(new Item() { Key = 16, Value = "SE" });
            //cusTerretory.Add(new Item() { Key = 17, Value = "CH" });

            //simplified template
            if (rdo_Simplified.Checked)
            {
                //try
                //{

                if (!File.Exists(fileName))
                {
                    error_Count++;
                    MessageBox.Show(this, "Invalid Path.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.WriteLine("[ERROR]: Path \""+ fileName + "\" is not found.");
                    file.WriteLine("------------------ END --------------------");
                    file.Close();
                    return;
                }

                var workbook = new XLWorkbook(fileName);
                var ws1 = workbook.Worksheet(1);


                //Remove header from template
                int skipCells = 7; // Skip top header names
                int totalRows = ws1.Range("B8:B1048576").CellsUsed().Count() + skipCells;


                //MessageBox.Show(this, totalRows.ToString(), "ROW_Count", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.progressBar1.Maximum = totalRows;
                
                for (int i = (1 + skipCells); i <= totalRows; i++)
                {

                    //ProgressBar
                    progressBar1.Value += 1;

                    var row = ws1.Row(i);
                    bool empty = row.IsEmpty();

                    if (!empty)
                    {

                        var cell = row.Cell(2);
                        string value = cell.GetValue<string>();

                        if (cell.IsEmpty())
                        {
                            MessageBox.Show(this, "Empty Identifier Found.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        if (File.Exists(@"input/" + value + ".itmsp" + "/metadata.xml"))
                        {

                            Directory.CreateDirectory(@"output/" + value + ".itmsp");

                            //Load XML Schema
                            XmlDocument doc = new XmlDocument();
                            doc.Load(@"input/" + value + ".itmsp" + "/metadata.xml");

                            //Create the node name Technical Report

                            XmlNode productsNode = doc.SelectSingleNode("package/album/products");
                            //XmlNode productsNode = doc.SelectSingleNode("products");

                            XmlElement optional = doc.SelectSingleNode(@"/package/album/products/product/intervals/interval/start_date") as XmlElement;
                            string fisrtInterval = optional.InnerText;

                            var terrories = row.Cell(5);
                            String terrories_value = terrories.GetValue<string>();
                            String[] territory = terrories_value.Split(new char[0]);

                            
                            //Existing product Tags
                            var ExistingTerretory = new List<Item>();
                            XmlNodeList TerrList = doc.GetElementsByTagName("product");
                              
                            string[] existTerrList = new string[TerrList.Count];
                            int t = 0;
                            foreach (XmlNode terr in TerrList)
                            {
                                existTerrList[t++] = terr["territory"].InnerText;
                            }

                            //Mistmach Check
                            string[] AllTerrList = new string[cusTerretory.Count];
                            //int at = 0;
                            foreach (var item1 in cusTerretory)
                            {
                                //AllTerrList[at++] = item1.Key.ToString();

                                for (int c = 7; c <= 17; c++)
                                {
                                    if (c == item1.Key && !row.Cell(c).IsEmpty())
                                    {
                                        int pos2 = Array.IndexOf(territory, item1.Value);

                                        if (pos2 > -1)
                                        {
                                           
                                        }
                                        else
                                        {
                                            //If not available, pass error
                                            file.WriteLine("[ERROR]: In " + value + ", territory " + item1.Value + " is not found with the corresponding territory value in column " + item1.Key + ".");
                                            error_Count++;
                                        }
                                    }
                                    
      
                                }

                            }
                            
                            //Loop on terrertory list that requested on each ISBN
                            for (int j = 0; j < territory.Length; j++)
                            {

                                //cleared For Sale
                                var clearedForSale = row.Cell(6);
                                String clearedForSale_value = clearedForSale.GetValue<string>();

                                //sales Start Date
                                var salesStartDate = row.Cell(3);
                                String salesStartDate_value = DateTime.Parse(salesStartDate.GetValue<string>()).ToString("yyyy-MM-dd");

                                //intervals
                                var intervals = row.Cell(4);
                                String intervals_value;

                                if (fisrtInterval != null)
                                {
                                    //Get first GB value
                                    intervals_value = DateTime.Parse(fisrtInterval).ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    //Update excel GB value
                                    intervals_value = DateTime.Parse(intervals.GetValue<string>()).ToString("yyyy-MM-dd");
                                }

                                //Validate If there any terretory available in metadata XML file.

                                string checkTerr = territory[j];//Assign requested terr code

                                int pos = Array.IndexOf(existTerrList, checkTerr); //Check in XML terr list
                                if (pos > -1)
                                {
                                    //If available, skip creating tag.
                                    //file.WriteLine("[STATUS]: " + checkTerr + " skipped.");
                                }
                                else
                                {
                                    XmlElement productNode = doc.CreateElement("product");
                                    //productsNode.AppendChild(productNode);
                                    productsNode.InsertAfter(productNode, productsNode.LastChild);


                                    //XmlNode productNode = doc.CreateElement("product");
                                    //productsNode.AppendChild(productNode);

                                    //territory tag
                                    XmlNode terrNode = doc.CreateElement("territory");
                                    terrNode.AppendChild(doc.CreateTextNode(territory[j]));
                                    productNode.AppendChild(terrNode);


                                    //cleared_for_sale tag
                                    if (!clearedForSale.IsEmpty() || (clearedForSale_value == "TRUE" || clearedForSale_value == "true"))
                                    {
                                        //cleared_for_sale = true
                                        XmlNode cfsNode = doc.CreateElement("cleared_for_sale");
                                        cfsNode.AppendChild(doc.CreateTextNode("true"));
                                        productNode.AppendChild(cfsNode);
                                    }
                                    else
                                    {
                                        //cleared_for_sale = false
                                        XmlNode cfsNode = doc.CreateElement("cleared_for_sale");
                                        cfsNode.AppendChild(doc.CreateTextNode("false"));
                                        productNode.AppendChild(cfsNode);
                                    }


                                    //sales_start_date tag
                                    if (!salesStartDate.IsEmpty())
                                    {
                                        //sales_start_date = true
                                        XmlNode ssdNode = doc.CreateElement("sales_start_date");
                                        ssdNode.AppendChild(doc.CreateTextNode(salesStartDate_value));
                                        productNode.AppendChild(ssdNode);
                                    }
                                    else
                                    {
                                        //sales_start_date = false
                                        XmlNode ssdNode = doc.CreateElement("sales_start_date");
                                        ssdNode.AppendChild(doc.CreateTextNode(""));
                                        productNode.AppendChild(ssdNode);
                                    }


                                    //intervals tag
                                    if (!intervals.IsEmpty())
                                    {
                                        //intervals = true
                                        XmlNode rTntervalNode = doc.CreateElement("intervals");
                                        productNode.AppendChild(rTntervalNode);

                                        XmlNode intervalNode = doc.CreateElement("interval");
                                        rTntervalNode.AppendChild(intervalNode);

                                        //start_date tag
                                        XmlNode sdNode = doc.CreateElement("start_date");
                                        sdNode.AppendChild(doc.CreateTextNode(intervals_value));
                                        intervalNode.AppendChild(sdNode);

                                        // Use KeyValuePair to use foreach on Dictionary.
                                        foreach (var item in cusTerretory)
                                        {
                                            var priceTier = row.Cell(item.Key);

                                            //Check Loop Territory and Array Value
                                            if (territory[j].ToString() == item.Value.ToString())
                                            {
                                                
                                                String priceTier_value = priceTier.GetValue<string>();

                                                if (!priceTier.IsEmpty())
                                                {
    
                                                    //wholesale_price_tier tag
                                                    XmlNode wptNode = doc.CreateElement("wholesale_price_tier");
                                                    wptNode.AppendChild(doc.CreateTextNode(priceTier_value));
                                                    intervalNode.AppendChild(wptNode);
                                                }
                                                else
                                                {
                                                    //If not available, pass error
                                                    file.WriteLine("[ERROR]: For territory " + territory[j].ToString() + " in file " + value + ", value is not found with the corresponding territory column " + item.Key + ".");
                                                    error_Count++;
                                                }

                                            }

                                        }

                                    }
                                    else
                                    {
                                        // no interval
                                    }
                                }

                                

                            }

                            doc.Save(@"output/" + value + ".itmsp/" + "metadata.xml");
                        }
                        else
                        {
                            file.WriteLine("[ERROR]: Package " + value + ".itmsp" + " or metadata.xml is not found.");
                            error_Count++;
                        }


                        ////Process Only Product Tags
                        //XmlDocument doc = new XmlDocument();
                        //XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        //doc.AppendChild(docNode);

                        //XmlNode productsNode = doc.CreateElement("products");
                        //doc.AppendChild(productsNode);

                        //var terrories = row.Cell(5);
                        //String terrories_value = terrories.GetValue<string>();
                        //String[] territory = terrories_value.Split(new char[0]);

                        //for (int j = 0; j < territory.Length; j++)
                        //{
                        //    //cleared For Sale
                        //    var clearedForSale = row.Cell(6);
                        //    String clearedForSale_value = clearedForSale.GetValue<string>();

                        //    //sales Start Date
                        //    var salesStartDate = row.Cell(3);
                        //    String salesStartDate_value = salesStartDate.GetValue<string>();

                        //    //intervals
                        //    var intervals = row.Cell(4);
                        //    String intervals_value = intervals.GetValue<string>();


                        //    //product tag
                        //    XmlNode productNode = doc.CreateElement("product");
                        //    productsNode.AppendChild(productNode);

                        //    //territory tag
                        //    XmlNode terrNode = doc.CreateElement("territory");
                        //    terrNode.AppendChild(doc.CreateTextNode(territory[j]));
                        //    productNode.AppendChild(terrNode);

                        //    //cleared_for_sale tag
                        //    if (!clearedForSale.IsEmpty() || (clearedForSale_value == "TRUE" || clearedForSale_value == "true"))
                        //    {
                        //        //cleared_for_sale = true
                        //        XmlNode cfsNode = doc.CreateElement("cleared_for_sale");
                        //        cfsNode.AppendChild(doc.CreateTextNode("true"));
                        //        productNode.AppendChild(cfsNode);
                        //    }
                        //    else
                        //    {
                        //        //cleared_for_sale = false
                        //        XmlNode cfsNode = doc.CreateElement("cleared_for_sale");
                        //        cfsNode.AppendChild(doc.CreateTextNode("false"));
                        //        productNode.AppendChild(cfsNode);
                        //    }


                        //    //sales_start_date tag
                        //    if (!salesStartDate.IsEmpty())
                        //    {
                        //        //sales_start_date = true
                        //        XmlNode ssdNode = doc.CreateElement("sales_start_date");
                        //        ssdNode.AppendChild(doc.CreateTextNode(salesStartDate_value));
                        //        productNode.AppendChild(ssdNode);
                        //    }
                        //    else
                        //    {
                        //        //sales_start_date = false
                        //        XmlNode ssdNode = doc.CreateElement("sales_start_date");
                        //        ssdNode.AppendChild(doc.CreateTextNode(""));
                        //        productNode.AppendChild(ssdNode);
                        //    }


                        //    //intervals tag
                        //    if (!intervals.IsEmpty())
                        //    {
                        //        //intervals = true
                        //        XmlNode rTntervalNode = doc.CreateElement("intervals");                                
                        //        productNode.AppendChild(rTntervalNode);

                        //        XmlNode intervalNode = doc.CreateElement("interval");
                        //        rTntervalNode.AppendChild(intervalNode);

                        //        //start_date tag
                        //        XmlNode sdNode = doc.CreateElement("start_date");
                        //        sdNode.AppendChild(doc.CreateTextNode(intervals_value));
                        //        intervalNode.AppendChild(sdNode);


                        //        // Use KeyValuePair to use foreach on Dictionary.
                        //        foreach (var item in cusTerretory)
                        //        {
                        //            //Check Loop Territory and Array Value
                        //            if (territory[j].ToString() == item.Value.ToString())
                        //            {
                        //                var priceTier = row.Cell(item.Key);
                        //                String priceTier_value = priceTier.GetValue<string>();

                        //                if (!priceTier.IsEmpty())
                        //                {
                        //                    //wholesale_price_tier tag
                        //                    XmlNode wptNode = doc.CreateElement("wholesale_price_tier");
                        //                    wptNode.AppendChild(doc.CreateTextNode(priceTier_value));
                        //                    intervalNode.AppendChild(wptNode);
                        //                }

                        //            }

                        //        }


                        //    }
                        //    else
                        //    {
                        //        // no interval
                        //    }

                        //}

                        //doc.Save(@"output/" + value + ".itmsp/" + "metadata.xml");

                    }

                }


                this.progressBar1.Visible = false;
                progressBar1.Value = 1;
                MessageBox.Show(this, "Precess Completed.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                //catch (Exception ex)
                //{
                //    if (ex.HResult == -2147024809)
                //    {
                //        MessageBox.Show(this, "Invalid Path.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                    
                //}
            }

            //detailed template
            if (rdo_Detailed.Checked)
            {
                var workbook = new XLWorkbook(fileName);
                var ws1 = workbook.Worksheet(2);

            }

            //Fetch metadata
            if (rdo_FetchMetadata.Checked)
            {
                try
                {
                    fileName = @"bin\Audiobooks_Metadata_Template_Empty.xlsx";

                    var workbook = new XLWorkbook(fileName);
                    var ws1 = workbook.Worksheet(1);


                    string path = @"input\";

                    string[] fileArray = Directory.GetDirectories(path);

                    //ProgressBas override
                    this.progressBar1.Minimum = 0;
                    this.progressBar1.Maximum = fileArray.Length;
                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        //ProgressBar
                        progressBar1.Value = i;

                        string dir = fileArray[i];

                        if (File.Exists(dir + "/metadata.xml"))
                        {

                            //Load XML Schema
                            XmlDocument doc = new XmlDocument();
                            doc.Load(dir + "/metadata.xml");

                            //Find By Tags 
                            XmlNodeList terrtag = doc.GetElementsByTagName("territory");
                            XmlNodeList cfstag = doc.GetElementsByTagName("cleared_for_sale");
                            XmlNodeList ssdtag = doc.GetElementsByTagName("sales_start_date");
                            XmlNodeList isdtag = doc.GetElementsByTagName("start_date");
                            XmlNodeList wpttag = doc.GetElementsByTagName("wholesale_price_tier");

                            //MessageBox.Show(n.Count.ToString(),"Count");
                            int NumberOfLastRow = 8;

                            string remInput = dir.Replace("input\\", "");

                            //ISBN List
                            ws1.Cell(NumberOfLastRow + i, 2).Value = remInput.Replace(".itmsp", "");
                            ws1.Cell(NumberOfLastRow + i, 2).DataType = XLDataType.Text;

                            //Territory List 
                            if (terrtag != null)
                            {

                                ws1.Cell(NumberOfLastRow + i, 5).DataType = XLDataType.Text;

                                foreach (XmlNode terrval in terrtag)
                                {
                                    ws1.Cell(NumberOfLastRow + i, 5).Value += (terrval.InnerText) + " ";
                                }

                            }

                            //Cleared For Sale List 
                            if (cfstag != null)
                            {
                                ws1.Cell(NumberOfLastRow + i, 6).DataType = XLDataType.Text;

                                foreach (XmlNode cfsval in cfstag)
                                {
                                    ws1.Cell(NumberOfLastRow + i, 6).Value = (cfsval.InnerText);
                                }
                            }

                            //Sale Start Date List 
                            if (ssdtag != null)
                            {
                                ws1.Cell(NumberOfLastRow + i, 3).Style.DateFormat.Format = "yyyy-MM-dd";

                                foreach (XmlNode ssdval in ssdtag)
                                {
                                    ws1.Cell(NumberOfLastRow + i, 3).Value = DateTime.Parse(ssdval.InnerText).ToString("yyyy-MM-dd");
                                }
                            }

                            //Sale Start Date List 
                            if (isdtag != null)
                            {
                                ws1.Cell(NumberOfLastRow + i, 4).Style.DateFormat.Format = "yyyy-MM-dd";

                                foreach (XmlNode isdval in isdtag)
                                {
                                    ws1.Cell(NumberOfLastRow + i, 4).Value = DateTime.Parse(isdval.InnerText).ToString("yyyy-MM-dd");
                                }
                            }

                            //Whole Sale Price Tier List

                            XmlNodeList xnList = doc.GetElementsByTagName("product");

                            foreach (XmlNode xn in xnList)
                            {
                                string trVal = xn["territory"].InnerText;
                                //string wptVal = xn["wholesale_price_tier"].InnerText;

                                foreach (var item in cusTerretory)
                                {
                                    if (trVal.ToString() == item.Value.ToString())
                                    {
                                        foreach (XmlNode childNode in xn.ChildNodes)
                                        {
                                            if (childNode.Name == "intervals")
                                            {
                                                foreach (XmlNode inter in childNode.ChildNodes)
                                                {
                                                    if (inter.Name == "interval")
                                                    {
                                                        foreach (XmlNode wpt in inter.ChildNodes)
                                                        {
                                                            if (wpt.Name == "wholesale_price_tier")
                                                            {
                                                                ws1.Cell(NumberOfLastRow + i, item.Key).DataType = XLDataType.Text;
                                                                ws1.Cell(NumberOfLastRow + i, item.Key).Value = (wpt.InnerText);
                                                            }

                                                        }
                                                    }

                                                }
                                            }

                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            string remInput = dir.Replace("input\\", "");
                            file.WriteLine("[ERROR]: " + remInput + "/metadata.xml is not found.");
                            error_Count++;
                        }

                    }
                    workbook.SaveAs(@"output/Audiobooks_Metadata_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");


                    this.progressBar1.Visible = false;
                    progressBar1.Value = 1;
                    MessageBox.Show(this, "Precess Completed.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    if (ex.HResult == -2147024894)
                    {
                        MessageBox.Show(this, "\"Audiobooks_Metadata_Template_Empty.xlsx\" is not found in \"bin\" folder.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                     
            }

            file.WriteLine("------------------ END --------------------");
            file.Close();

            if (error_Count>0)
            {
                Process.Start("notepad.exe", log);
            }

        }

        //Open dialogbox
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog excelFile = new OpenFileDialog();

            excelFile.DefaultExt = ".xlsx";
            excelFile.Filter = "Microsoft Excel 2007 (*.xlsx)|*.xlsx|Excel File (*.xls)|*.xls";
            excelFile.FilterIndex = 1;
            excelFile.CheckFileExists = true;
            excelFile.ShowDialog();

            txt_FilePath.Text = excelFile.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_Generate.Enabled = false;
        }

        private void txt_FilePath_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_FilePath.Text))
            {
                btn_Generate.Enabled = false;
            }
            else
            {
                btn_Generate.Enabled = true;
            }

        }

        private void rdo_Simplified_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Simplified.Checked)
            {
                rdo_Detailed.ForeColor = SystemColors.GrayText;
                rdo_FetchMetadata.ForeColor = SystemColors.GrayText;
            }
            else
            {
                rdo_Detailed.ForeColor =DefaultForeColor;
                rdo_FetchMetadata.ForeColor = DefaultForeColor;
            }
        }

        private void rdo_Detailed_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Detailed.Checked)
            {
                rdo_Simplified.ForeColor = SystemColors.GrayText;
                rdo_FetchMetadata.ForeColor = SystemColors.GrayText;
            }
            else
            {
                rdo_Simplified.ForeColor = DefaultForeColor;
                rdo_FetchMetadata.ForeColor = DefaultForeColor;
            }
        }

        private void rdo_FetchMetadata_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_FetchMetadata.Checked)
            {
                rdo_Simplified.ForeColor = SystemColors.GrayText;
                rdo_Detailed.ForeColor = SystemColors.GrayText;
                txt_FilePath.Enabled = false;
                btn_Browse.Enabled = false;
                btn_Generate.Enabled = true;
            }
            else
            {
                rdo_Simplified.ForeColor = DefaultForeColor;
                rdo_Detailed.ForeColor = DefaultForeColor;
                txt_FilePath.Enabled = true;
                btn_Browse.Enabled = true;
                btn_Generate.Enabled = false;
            }
        }
    }

}
