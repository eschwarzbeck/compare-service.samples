using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using CompareServer.Interfaces;
using System.Diagnostics;
using System.DirectoryServices;
using Microsoft.Web.Administration;

namespace CompareServer.Domain
{
    public class IISHelper
    {

        private static ILogger Log;

        static IISHelper()
        {
            Log = UnityContainer.UnityManager.Resolve<ILogger>();
        }

        private static Version GetIISVersion()
        {
            using (RegistryKey iisKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\InetStp", false))
            {
                if (iisKey != null)
                {
                    int majorVersion = (int)iisKey.GetValue("MajorVersion", -1);
                    int minorVersion = (int)iisKey.GetValue("MinorVersion", -1);
                    if (majorVersion != -1 && minorVersion != -1)
                    {
                        return new Version(majorVersion, minorVersion);
                    }
                }
                return new Version(0, 0);
            }
        }

        private static IEnumerable<Site> GetWebSitesIIS7()
        {
            ServerManager mgr = new ServerManager();
           
            return mgr.Sites;
        }

        private static IEnumerable<DirectoryEntry> GetWebSitesIIS6()
        {
            List<DirectoryEntry> sites = new List<DirectoryEntry>();
            DirectoryEntry entries = new DirectoryEntry(IIS.W3SvcPath);

            if (entries != null)
            {
                if (entries.Children != null)
                {
                    foreach (DirectoryEntry entry in entries.Children)
                    {
                        if (entry != null && entry.SchemaClassName == IIS.WebServer)
                        {
                            sites.Add(entry);
                        }
                    }
                }
            }

            return sites;
        }

        public static IEnumerable<WebSite> GetWebSites()
        {
            List<WebSite> result = new List<WebSite>();
            try
            {
                Version iisVer = GetIISVersion();
                if (iisVer.Major == 7)
                {
                    IEnumerable<Site> sites = GetWebSitesIIS7();
                    foreach (Site s in sites)
                    {
                        try
                        {
                            var site = new WebSite();
                            site.Identity = s.Id;
                            site.Name = s.Name;

                            try
                            {
                                site.State = (WebsiteState)Enum.Parse(typeof(WebsiteState), s.State.ToString());
                            }
                            catch
                            {
                                site.State = WebsiteState.Unknown;
                            }

                            result.Add(site);
                        }
                        catch (Exception ex)
                        {
                            Log.Write(TraceEventType.Error, "GetWebSitesIIS7: Failed to process site. Exception ({0}): '{1}' \n {2}", ex.GetType(), ex.Message, ex.StackTrace);
                        }
                    }
                    return result;
                }
                else if (iisVer.Major == 6)
                {
                    IEnumerable<DirectoryEntry> entries = GetWebSitesIIS6();
                    foreach (DirectoryEntry entry in entries)
                    {
                        if (entry != null && entry.SchemaClassName == IIS.WebServer)
                        {
                            WebSite site = new WebSite();
                            site.Identity = Convert.ToInt32(entry.Name);
                            site.Name = entry.InvokeGet("ServerComment").ToString();
                            site.State = (WebsiteState)entry.InvokeGet("ServerState");
                            result.Add(site);
                        }
                    }
                    return result;
                }
                else
                {
                    return new List<WebSite>();
                }
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Error, "{0}", ex);
                return new List<WebSite>();
            }
        }


        public static Site GetWebSiteIIS7(string name)
        {
            IEnumerable<Site> sites = GetWebSitesIIS7();
            foreach (Site site in sites)
            {
                if (site.Name == name)
                {
                    return site;
                }
            }
            return null;
        }

        public static DirectoryEntry GetWebSiteIIS6(string name)
        {
            IEnumerable<DirectoryEntry> entries = GetWebSitesIIS6();
            foreach (DirectoryEntry entry in entries)
            { 
                string siteName = entry.InvokeGet("ServerComment").ToString();
                if (siteName == name)
                {
                    return entry;
                }
            }
            return null;
        }

        public static WebSite GetWebSite(string name)
        {
            IEnumerable<WebSite> sites = GetWebSites();
            foreach (WebSite site in sites)
            {
                if (site.Name == name)
                {
                    return site;
                }
            }
            return null;
        }


        public static WebsiteState GetWebSiteState(string name)
        {
            WebSite site = GetWebSite(name);
            if (site != null)
            {
                return site.State;
            }
            return WebsiteState.Unknown;
        }

        public static WebsiteState StartWebSite(string name)
        {
            try
            {
                Version iisVer = GetIISVersion();
                if (iisVer.Major == 7)
                {
                    Site site = GetWebSiteIIS7(name);
                    ObjectState state = site.Start();
                    switch (state)
                    { 
                        case ObjectState.Started:
                            return WebsiteState.Started;
                        case ObjectState.Stopped:
                            return WebsiteState.Stopped;
                        default:
                            return WebsiteState.Unknown;
                    }
                }
                else if (iisVer.Major == 6)
                {
                    DirectoryEntry entry = GetWebSiteIIS6(name);
                    int state = (int)entry.Invoke("start", null);
                    switch (state)
                    {
                        case 2:
                            return WebsiteState.Started;
                        case 4:
                            return WebsiteState.Stopped;
                        default:
                            return WebsiteState.Unknown;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            return WebsiteState.Unknown;
        }

        public static WebsiteState StopWebSite(string name)
        {
            try 
            {
                Version iisVer = GetIISVersion();
                if (iisVer.Major == 7)
                {
                    Site site = GetWebSiteIIS7(name);
                    ObjectState state = site.Stop();
                    switch (state)
                    {
                        case ObjectState.Started:
                            return WebsiteState.Started;
                        case ObjectState.Stopped:
                            return WebsiteState.Stopped;
                        default:
                            return WebsiteState.Unknown;
                    }
                }
                else if (iisVer.Major == 6)
                {
                    DirectoryEntry entry = GetWebSiteIIS6(name);
                    int state = (int)entry.Invoke("stop", null);
                    switch (state)
                    {
                        case 2:
                            return WebsiteState.Started;
                        case 4:
                            return WebsiteState.Stopped;
                        default:
                            return WebsiteState.Unknown;
                    }
                }            
            }
            catch (Exception ex)
            {
                Log.Write(TraceEventType.Error, "{0}", ex);
            }
            return WebsiteState.Unknown;
        }
    }

    public class WebSite
    {
        public long Identity { get; set; }
        public string Name { get; set; }
        public WebsiteState State { get; set; }
        public Int32 Port { get; set; }
    }

    public enum WebsiteState
    {
        Unknown = 0,
        Starting = 1,
        Started = 2,
        Stopping = 3,
        Stopped = 4,
        Pausing = 5,
        Paused = 6,
        Continuing = 7
    }

    class IIS
    {
        public const string W3SvcPath = "IIS://localhost/W3SVC";
        public const string DefaultWebSite = W3SvcPath + "/1/Root";
        public const string WebVirtualDir = "IIsWebVirtualDir";
        public const string WebServer = "IIsWebServer";
    }
}
