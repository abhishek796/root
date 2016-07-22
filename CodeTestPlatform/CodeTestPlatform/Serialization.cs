using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace BuildReport
{
    /// <summary>
    /// The following declarations is an example of how we can define custom attributes
    /// </summary>
    #region AttributeDefinitions

    [AttributeUsage(AttributeTargets.Field)]
    public class BeforeGet : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class AfterGet : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class BeforeCompile : Attribute { }

    #endregion

    public class LabEnvironment
    {
        [XmlArrayAttribute("Machines")]
        public MachineInfo[] machines;
        [XmlArrayAttribute("Dlls")]
        public DllInfo[] dlls;
        public string RevertScriptPath;
        [XmlAttribute("Name")]
        public string Name;
    }

    public class DllInfo
    {
        public string dllName;
        public string Category;
    }

    public class MachineInfo
    {
        public string VmName;
        public string IPAddress;
        public string Snapshot;
        public string ServerHostName;
        public string PathToRevertCredentialFile;
        public string UserName;
        public string Password;
        public Command[] ExecuteCommands;
    }

    public class Command
    {
        public string FileName;
        public string Arguments;
        public string WorkingDirectory;
        public int TimeOutInSeconds;
    }

    public class CustomBuildSetting
    {
        [XmlArrayAttribute("SolutionsToCompile")]
        public SolutionToCompile[] SolutionsToCompile;
        public RuleEngine RuleEngine;
        public MSTest MSTest;
        [XmlAttribute]
        public string BuildName;
    }

    public class SolutionToCompile
    {
        public string PathToSolutionFile;
        public string Platform;
        public string Configuration;
        public string MSBuildArguments;
        public string MSBuildToolPath;
    }

    public class RuleEngine
    {
        public string CoverageParseScript;
        public string RuleEngineCompileArgs;
        public string RuleEngineCompileFile;
        public int CheckStyleError;
        public string CheckStyleParseScript;
        public int CheckStyleWarning;
        public bool DoRatcheting;
        public double RatchetValue;
        public string RuleEngineWarFile;
        public double CoverageTolerance;
    }

    public class MSTest
    {
        public string Platform;
        public string Configuration;
    }

    public class BuildConfig
    {
        [XmlArrayAttribute("CustomBuildSettings")]
        public CustomBuildSetting[] CustomBuildSetting;
    }

    //---------------------------Main Code------------------------------

    public class Serialization
    {
        public void writeToXml(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(LabEnvironment));
            TextWriter writer = new StreamWriter(filename);
            LabEnvironment dp = new LabEnvironment();
            dp.RevertScriptPath = @"Source\Scripts\RevertVM.ps1";
            MachineInfo mci = new MachineInfo();
            mci.VmName = "USMKEAPSECBLD01";
            mci.Snapshot = "Clean";
            mci.ServerHostName = "uswauevcv01.eng.med.ge.com";
            mci.PathToRevertCredentialFile = @"C:\RevertVM_cred";
            mci.UserName = @"eng\vixuser";
            mci.Password = "Querty123";
            Command cmd1 = new Command();
            cmd1.FileName = "cmd.exe";
            cmd1.Arguments = @"ipconfig";
            cmd1.WorkingDirectory = @"C:\";
            cmd1.TimeOutInSeconds = 300;
            Command cmd2 = new Command();
            cmd2.FileName = "xcopy";
            cmd2.Arguments = @"C:\Test\a.txt C:\new.txt";
            cmd2.WorkingDirectory = @"C:\Test";
            cmd2.TimeOutInSeconds = 300;
            Command cmd3 = new Command();
            cmd3.FileName = "cmd.exe";
            cmd3.Arguments = @"shutdown -s";
            cmd3.WorkingDirectory = @"D:\New Folder";
            cmd3.TimeOutInSeconds = 300;
            Command[] ExecuteCommands = { cmd1, cmd2, cmd3 };
            mci.ExecuteCommands = ExecuteCommands;
            MachineInfo[] machines = { mci };
            dp.Name = "hello";
            dp.machines = machines;
            serializer.Serialize(writer, dp);
            writer.Close();
        }

        //----------------sdsd
        public void writeXml(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(BuildConfig));
            TextWriter writer = new StreamWriter(filename);
            SolutionToCompile soln = new SolutionToCompile();
            soln.Configuration = "Release";
            soln.MSBuildArguments = "/m";
            soln.MSBuildToolPath = @"C:\Program Files (x86)\MSBuild\14.0\Bin";
            soln.PathToSolutionFile = @"Source\NS\Gehc.NS.Platform.Master.sln";
            soln.Platform = "x64";
            SolutionToCompile soln2 = new SolutionToCompile();
            soln2.Configuration = "Release";
            soln2.MSBuildArguments = "/m";
            soln2.MSBuildToolPath = @"C:\Program Files (x86)\MSBuild\14.0\Bin";
            soln2.PathToSolutionFile = @"Source\NS\Gehc.NS.Platform.FunctionalTest.Master.sln";
            soln2.Platform = "x64";
            SolutionToCompile[] solns = { soln, soln2 };
            MSTest test = new MSTest();
            test.Platform = "x64";
            test.Configuration = "Release";
            CustomBuildSetting setting = new CustomBuildSetting();
            setting.SolutionsToCompile = solns;
            setting.MSTest = test;
            setting.BuildName = "NS_CI";

            SolutionToCompile sol = new SolutionToCompile();
            sol.Configuration = "Debug";
            sol.MSBuildArguments = "/m";
            sol.MSBuildToolPath = @"C:\Program Files (x86)\MSBuild\12.0\Bin";
            sol.PathToSolutionFile = @"Source\CDS\Automation\Gehc.Acds.BDD.Components.sln";
            sol.Platform = "AnyCPU";
            
            SolutionToCompile[] ss = { sol};
            MSTest t = new MSTest();
            t.Platform = "AnyCPU";
            t.Configuration = "Debug";

            RuleEngine r = new RuleEngine();
            r.CheckStyleError = 0;
            r.CheckStyleParseScript = @"Build\WriteCheckStyleDatatoBuild.ps1";
            r.CheckStyleWarning = 0;
            r.CoverageParseScript = @"Build\WriteCoverageDatatoBuild.ps1";
            r.CoverageTolerance = 0.02;
            r.DoRatcheting = true;
            r.RatchetValue = 80;
            r.RuleEngineCompileArgs = "";
            r.RuleEngineCompileFile = @"Source\CDS\RuleEngineMain\Build\run-build.bat";
            r.RuleEngineWarFile = @"Source\CDS\RuleEngineMain\GateWay\target\CDSGateWay-1.0.jar";

            CustomBuildSetting cbs = new CustomBuildSetting();
            cbs.SolutionsToCompile = ss;
            cbs.MSTest = t;
            cbs.RuleEngine = r;
            cbs.BuildName = "CDS_CI";


            BuildConfig config = new BuildConfig();

            CustomBuildSetting[] allBuilds = {setting, cbs};
            config.CustomBuildSetting = allBuilds;
            serializer.Serialize(writer, config);
            writer.Close();
        }

        //=----------------------sdsd/

        public void ReadXml(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LabEnvironment));
            // If the XML document has been altered with unknown 
            // nodes or attributes, handles them with the 
            // UnknownNode and UnknownAttribute events.
            serializer.UnknownNode += new
            XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(serializer_UnknownAttribute);
            FileStream fs = new FileStream(filename, FileMode.Open);
            LabEnvironment dp;
            dp = (LabEnvironment)serializer.Deserialize(fs);
            List<MachineInfo> m = (List<MachineInfo>)dp.machines.GetEnumerator();
            foreach (MachineInfo mci in m)
            {
                Console.WriteLine(" Machine Details: \n" +
                mci.VmName + "\n\t" +
                mci.UserName + "\n\t" +
                mci.Password);
            }

            /*foreach (DllInfo dll in dp.dlls)
            {
                Console.WriteLine(" Dll Details: \n" +
                dll.dllName + "\n\t" +
                dll.Category);
            }*/
        }

        protected void serializer_UnknownNode
        (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        protected void serializer_UnknownAttribute
        (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}