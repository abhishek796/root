using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildReport
{
    class ReadShelveset
    {
        public void readShelveset(TfsTeamProjectCollection tfs, IBuildServer bs, VersionControlServer vc)
        {
            var def = bs.GetBuildDefinition("valhalla", "VLH_CI");
            //var buildDetail = bs.QueryBuilds(def).Last();
            //Console.WriteLine(buildDetail.BuildNumber + " - shelveset = " + buildDetail.ShelvesetName);
            //if (buildDetail.ShelvesetName != null && buildDetail.ShelvesetName != string.Empty)
            {
                //var ShelvesetName = buildDetail.ShelvesetName.Split(';')[0];
                //var ShelvesetOwner = buildDetail.ShelvesetName.Split(';')[1];
                PendingSet[] sets = vc.QueryShelvedChanges("Removed Fakes", "abhishek.chatuvedi@ge.com");
                foreach (var pendingSet in sets)
                {
                    foreach (PendingChange change in pendingSet.PendingChanges.Where(ch => ch != null))
                    {
                        Console.Write(change.FileName + ": " + change.ChangeType);
                        if (change.ChangeType.ToString().Contains("Add"))
                        {
                            Console.WriteLine(" ------ Add exists");
                        }
                        if (change.ChangeType.ToString().IndexOf("Edit",StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            Console.WriteLine(" ------ Edit exists");
                        }
                        if (change.ChangeType.ToString().IndexOf("Delete",StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            Console.WriteLine(" ------ Delete exists");
                        }
                        if (change.ChangeType.ToString().IndexOf("Rename",StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            Console.WriteLine(" ------ Rename exists");
                        }
                        if (change.ChangeType.ToString().IndexOf("Rollback", StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            Console.WriteLine(" ------ Rollback exists");
                        }
                    }
                }
            }
            Console.WriteLine("Done");
        }
    }
}
