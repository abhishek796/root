using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildReport
{
    class ReadFile
    {
        public void readMyFile(string filename)
        {
            
            List<string> l = File.ReadLines(filename).ToList();
            String s = File.ReadAllText(filename);
            //foreach(string s in l)
            {
                //if (s.Contains("<b>All Packages</b>"))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(s);
                    foreach (XmlNode childrenNode in xmlDoc.GetElementsByTagName("td"))
                    {
                        if (childrenNode.Attributes.Count !=0 )
                        {
                            if (childrenNode.Attributes["class"].Value.Equals("percentgraph"))
                            {
                                Console.WriteLine("Coverage value: " + childrenNode.InnerText);
                            } 
                        }
                    } 
                    //Console.WriteLine("Line: " + s);
                    //break;
                }
            }
            Console.Read();
        }
    }
}
