using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using CompareServer.Interfaces;
using CompareServer;
using System.Configuration;

namespace CompareServer.UnityContainer
{
    /// <summary>
    ///  The class manages the unity instance globally
    /// </summary>
    public static class UnityManager
    {
        private static readonly object Lock = new object();
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get {
                if (_container == null)
                {
                    lock (Lock)
                    {
                        if (_container == null)
                        {
                            try
                            {
                                var container = new Microsoft.Practices.Unity.UnityContainer();
                                var path = System.IO.Path.Combine(ApplicationFolders.ConfigurationFolder, "logging.config");
                                var map = new ExeConfigurationFileMap() { ExeConfigFilename = path };
                                var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                                var section = (UnityConfigurationSection)config.GetSection(UnityConfigurationSection.SectionName);
                                section.Configure(container);
                                _container = container;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                }
                return _container;
            }
        }

        private static void Dummy(Type type)
        {
            // This to ensure certain assemblies are linked with this one.
            var name = type.AssemblyQualifiedName;
            if (name != null)
            {
                System.Diagnostics.Trace.WriteIf(false, name);
            }
        }

        private static void Trace(string format, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("'{0}' : {1}", AppDomain.CurrentDomain.FriendlyName, string.Format(format, args)));
        }

        private static void TraceError(string format, params object[] args)
        {
            System.Diagnostics.Trace.TraceError(string.Format("'{0}' : {1}", AppDomain.CurrentDomain.FriendlyName, string.Format(format, args)));
        }

        public static object Resolve(Type t)
        {
            return Container.Resolve(t);
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
