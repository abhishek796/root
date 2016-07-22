using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace BuildReport
{    
    public class RandomCode
    {
        public void RandomCoding(TfsTeamProjectCollection tfs, IBuildServer bs)
        {
            /*var def = bs.GetBuildDefinition("valhalla","CDS_CR");
            for (int i = 0; i < 20; i++)
                bs.QueueBuild(def);*/
            // Get the object used to communicate with the server.
        }
    }
}
