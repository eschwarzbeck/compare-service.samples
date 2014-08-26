using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareServer.Domain
{
    public class ServerFile
    {
        public ServerFile()
        { }

        public ServerFile(string clientName, string serverName, DateTime expireTime)
        {
            ClientName = clientName;
            ServerName = serverName;
            ExpireTime = expireTime;
        }

        public ServerFile(string clientName, string serverName):
            this (clientName, serverName, DateTime.Now.AddMinutes(AppConfig.TempFilesTimeout))
        {
            
        }

        public string ClientName { get; set; }
        public string ServerName { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
