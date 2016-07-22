/* In order to add a new class file, add a suitable name in the enumerator, 
 * then instatiate that class and call the desired method in the switch below.
 * THis project purely ment for testing and "playing around" with code
*/

using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BuildReport
{
    public enum TestClass
    {
        ReadShelveset,
        BuildData,
        Serialization,
        RandomCode,
        ReadFile
    }

    class MainProgram
    {
        public static void Main(string[] args)
        {
            //Initialization
            TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri("https://valhalla-dev01.visualstudio.com/DefaultCollection"));
            tfs.EnsureAuthenticated();
            IBuildServer bs = tfs.GetService<IBuildServer>();
            VersionControlServer vc = tfs.GetService<VersionControlServer>();
            int option = 0;
            Console.WriteLine("~~~ Welcome to CodeTestPlatform ~~~\n");
            Console.WriteLine("Author: Abhishek Chaturvedi");
            Console.WriteLine("Last Modified: 1/14/2016\n\n");
            Console.WriteLine("Classes available to execute:\n\n1. ReadShelveset\n2. BuildData\n3. Serialization\n4. RandomCode\n5. ReadFile");
            Console.WriteLine("Enter your cohice (number):");
            string number = Console.ReadLine();
            option = Convert.ToInt16(number);
            Console.WriteLine("\nYou selected option : " + option);
            Console.WriteLine("\nStarting...");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            switch (option)
            {
                case 1:
                    ReadShelveset rsa = new ReadShelveset();
                    rsa.readShelveset(tfs, bs, vc);
                    break;

                case 2:
                    GetData gd = new GetData();
                    gd.Report(tfs, bs);
                    break;

                case 3:
                    Serialization s = new Serialization();
                    s.writeXml("build.config");
                    //LabEnvironment x = s.ReadXml("environment.properties");
                    break;

                case 4:
                    RandomCode rc = new RandomCode();
                    rc.RandomCoding(tfs, bs);
                    break;

                case 5:
                    ReadFile rf = new ReadFile();
                    rf.readMyFile(@"\\3.45.223.169\BuildDrop\CDS_CI\CDS_CI_20160520.4\RuleEngine\CoverageReports\frame-summary.html");
                    break;

                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            Console.WriteLine("\nEnd!! Press any key to exit");
            Console.Read();
        }
    }
}

