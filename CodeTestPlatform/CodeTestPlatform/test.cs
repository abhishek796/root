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
using Microsoft.Build.Execution;

namespace BuildReport
{
    class test
    {
        private BuildHttpClient buildClient = null;
        private List<Build> builds;
        private List<string> defs;
        public async Task DownloadLatestAsync(TfsTeamProjectCollection tfs, IBuildServer bs)
        {
            buildClient = tfs.GetClient<BuildHttpClient>();
            DefinitionReference[] def = (await buildClient.GetDefinitionsAsync("valhalla")).ToArray();
            if (def != null)
            {
                foreach(var d in def)
                {
                    defs.Add(d.Name);
                }
                builds = await buildClient.GetBuildsAsync(projectName: "valhalla");
                var latest = from build in builds
                             orderby build.Id descending
                             select build;
            }
        }
    }
}
