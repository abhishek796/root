using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildReport
{
    class GetData
    {
        private BuildHttpClient bld = null;

        public void Report(TfsTeamProjectCollection tfs, IBuildServer bs)
        {
            var def = bs.GetBuildDefinition("valhalla", "CDS_CI");
            IBuildDetailSpec xx = bs.CreateBuildDetailSpec(def);
            //xx.InformationTypes = null;
            xx.QueryDeletedOption = QueryDeletedOption.IncludeDeleted;
            xx.QueryOptions = QueryOptions.All;
            xx.QueryOrder = BuildQueryOrder.FinishTimeDescending;
            xx.MinFinishTime = new DateTime(2016, 5, 25);
            //xx.MaxBuildsPerDefinition = count;
            var buildDetail = bs.QueryBuilds(xx).Builds;//.Where(x => x.Reason == BuildReason.CheckInShelveset);
            TimeSpan sum = TimeSpan.Zero;
            TextWriter writer = new StreamWriter("reportVLH.csv");
            int i = 1,passed = 0, failed = 0, stopped = 0, part = 0;
            //writer.WriteLine("BuildNum Date Duration status User");
            foreach (var build in buildDetail)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(build.BuildDefinition.Description);
                Console.WriteLine(i + ")  " + build.BuildNumber + " | Date: " + build.StartTime.ToShortDateString() + " | Duration: " + (build.FinishTime - build.StartTime).Minutes + " min approx");
                sum += (build.FinishTime - build.StartTime);
                if (build.Status == Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded)
                    passed++;
                if (build.Status == Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed)
                    failed++;
                if (build.Status == Microsoft.TeamFoundation.Build.Client.BuildStatus.Stopped)
                    stopped++;
                if (build.Status == Microsoft.TeamFoundation.Build.Client.BuildStatus.PartiallySucceeded)
                    part++;
                i++;
                writer.WriteLine(build.BuildNumber + " " + build.StartTime.ToShortDateString() + " " + (build.FinishTime - build.StartTime) + " " + build.Status + " " + build.RequestedFor);
            }
            Console.WriteLine("\nTotal builds = " + buildDetail.Count() + " Passed: " + passed + " Failed: " + failed + " Stopped: " + stopped + " Partial: " + part + " Average time = " + new TimeSpan((sum.Ticks / buildDetail.Count())));
            writer.Close();
        }
    }
}
