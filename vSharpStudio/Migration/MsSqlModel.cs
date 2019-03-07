using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace vSharpStudio.vm.Migration
{
  internal class MsSqlModel : SqlServerSchemaReader
  {
    bool IsGenerateCommonCode = false;
    string FilePrefix = "";
    string ConnectionStringName = "";
    string ConfigPath = "";
    string Namespace = "";
    string ClassPrefix = "";
    string ClassSuffix = "";
    string SchemaName = null;
    string SchemaNameGenerated = null;
    bool IncludeViews;
    bool IncludeRelationships = false;
    bool IgnoreColumnDefaultValues = true;
    string[] ExcludeTablePrefixes = new string[] { };
    bool IgnoreTablesByDefault = false;

    bool WithDAL = false;
    bool WithDALSP = false;
    bool WithService = false;

    string comma = ""; // internal usage

    UILib UILibrary = UILib.SilverlightTelerik;
    bool IsUseObservableCollection = true;

    // UI
    public enum UILib { SilverlightStandard, SilverlightTelerik };


    static string FilePos(string text = "",
                            [CallerFilePath] string file = "",
                            [CallerMemberName] string member = "",
                            [CallerLineNumber] int line = 0)
    {
      return text + " Generator: " + Path.GetFileName(file) + " Line: " + line;
    }

    string CheckNullable(Column col)
    {
      string result = "";
      if (col.IsNullable &&
      col.PropertyType != "byte[]" &&
      col.PropertyType != "string" &&
      col.PropertyType != "Microsoft.SqlServer.Types.SqlGeography" &&
      col.PropertyType != "Microsoft.SqlServer.Types.SqlGeometry"
      )
        result = "?";
      return result;
    }
    //string GetConnectionString(ref string connectionStringName, out string providerName)
    //{
    //  var _CurrentProject = GetCurrentProject();

    //  providerName = null;

    //  string result = "";
    //  ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
    //  configFile.ExeConfigFilename = GetConfigPath();

    //  if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
    //    throw new ArgumentNullException("The project does not contain App.config or Web.config file.");


    //  var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
    //  var connSection = config.ConnectionStrings;

    //  //if the connectionString is empty - which is the defauls
    //  //look for count-1 - this is the last connection string
    //  //and takes into account AppServices and LocalSqlServer
    //  if (string.IsNullOrEmpty(connectionStringName))
    //  {
    //    if (connSection.ConnectionStrings.Count > 1)
    //    {
    //      connectionStringName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].Name;
    //      result = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ConnectionString;
    //      providerName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ProviderName;
    //    }
    //  }
    //  else
    //  {
    //    try
    //    {
    //      result = connSection.ConnectionStrings[connectionStringName].ConnectionString;
    //      providerName = connSection.ConnectionStrings[connectionStringName].ProviderName;
    //    }
    //    catch
    //    {
    //      result = "There is no connection string name called '" + connectionStringName + "'";
    //    }
    //  }

    //  //	if (String.IsNullOrEmpty(providerName))
    //  //		providerName="System.Data.SqlClient";

    //  return result;
    //}
    string GetConnectionString(ref string connectionStringName, out string providerName)
    {
      var _CurrentProject = GetCurrentProject();

      providerName = null;

      string result = "";
      ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
      configFile.ExeConfigFilename = GetConfigPath();

      if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
        throw new ArgumentNullException("The project does not contain App.config or Web.config file.");


      var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
      var connSection = config.ConnectionStrings;

      //if the connectionString is empty - which is the defauls
      //look for count-1 - this is the last connection string
      //and takes into account AppServices and LocalSqlServer
      if (string.IsNullOrEmpty(connectionStringName))
      {
        if (connSection.ConnectionStrings.Count > 1)
        {
          connectionStringName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].Name;
          result = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ConnectionString;
          providerName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ProviderName;
        }
      }
      else
      {
        try
        {
          result = connSection.ConnectionStrings[connectionStringName].ConnectionString;
          providerName = connSection.ConnectionStrings[connectionStringName].ProviderName;
        }
        catch
        {
          result = "There is no connection string name called '" + connectionStringName + "'";
        }
      }

      //	if (String.IsNullOrEmpty(providerName))
      //		providerName="System.Data.SqlClient";

      return result;
    }
    string _connectionString = "";
    string _providerName = "";

    void InitConnectionString()
    {
      if (String.IsNullOrEmpty(_connectionString))
      {
        _connectionString = GetConnectionString(ref ConnectionStringName, out _providerName);

        if (_connectionString.Contains("|DataDirectory|"))
        {
          //have to replace it
          string dataFilePath = GetDataDirectory();
          _connectionString = _connectionString.Replace("|DataDirectory|", dataFilePath);
        }
      }
    }

    public string ConnectionString
    {
      get
      {
        InitConnectionString();
        return _connectionString;
      }
    }

    public string ProviderName
    {
      get
      {
        InitConnectionString();
        return _providerName;
      }
    }

    //public EnvDTE.Project GetCurrentProject()
    //{

    //  IServiceProvider _ServiceProvider = (IServiceProvider)Host;
    //  if (_ServiceProvider == null)
    //    throw new Exception("Host property returned unexpected value (null)");

    //  EnvDTE.DTE dte = (EnvDTE.DTE)_ServiceProvider.GetService(typeof(EnvDTE.DTE));
    //  if (dte == null)
    //    throw new Exception("Unable to retrieve EnvDTE.DTE");

    //  Array activeSolutionProjects = (Array)dte.ActiveSolutionProjects;
    //  if (activeSolutionProjects == null)
    //    throw new Exception("DTE.ActiveSolutionProjects returned null");

    //  EnvDTE.Project dteProject = (EnvDTE.Project)activeSolutionProjects.GetValue(0);
    //  if (dteProject == null)
    //    throw new Exception("DTE.ActiveSolutionProjects[0] returned null");

    //  return dteProject;

    //}

    //private string GetProjectPath()
    //{
    //  EnvDTE.Project project = GetCurrentProject();
    //  System.IO.FileInfo info = new System.IO.FileInfo(project.FullName);
    //  return info.Directory.FullName;
    //}

    //private string GetConfigPath()
    //{
    //  if (ConfigPath != "")
    //    return Host.ResolvePath(ConfigPath);

    //  EnvDTE.Project project = GetCurrentProject();
    //  foreach (EnvDTE.ProjectItem item in project.ProjectItems)
    //  {
    //    // if it is the app.config file, then open it up
    //    if (item.Name.Equals("App.config", StringComparison.InvariantCultureIgnoreCase) || item.Name.Equals("Web.config", StringComparison.InvariantCultureIgnoreCase))
    //      return GetProjectPath() + "\\" + item.Name;
    //  }
    //  return String.Empty;
    //}

    //public string GetDataDirectory()
    //{
    //  EnvDTE.Project project = GetCurrentProject();
    //  return System.IO.Path.GetDirectoryName(project.FileName) + "\\App_Data\\";
    //}

    static string zap_password(string connectionString)
    {
      var rx = new Regex("Password=.*;", RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
      return rx.Replace(connectionString, "Password=******;");
    }


    static bool IsExcluded(string schemaname, string tablename, string[] ExcludeTablePrefixes)
    {
      for (int i = 0; i < ExcludeTablePrefixes.Length; i++)
      {
        string s = ExcludeTablePrefixes[i];
        if (tablename.StartsWith(s)) return true;
        if ((schemaname + "." + tablename).StartsWith(s)) return true;
      }
      return false;
    }

    Tables LoadTables()
    {
      InitConnectionString();

      WriteLine("// This file was automatically generated by the Dapper.FastCRUD T4 Template");
      WriteLine("// Do not make changes directly to this file - edit the template configuration instead");
      WriteLine("// ");
      WriteLine("// The following connection settings were used to generate this file");
      WriteLine("// ");
      WriteLine("//     Connection String Name: `{0}`", ConnectionStringName);
      WriteLine("//     Provider:               `{0}`", ProviderName);
      WriteLine("//     Connection String:      `{0}`", zap_password(ConnectionString));
      WriteLine("//     Include Views:          `{0}`", IncludeViews);
      WriteLine("");

      DbProviderFactory _factory;
      try
      {
        _factory = DbProviderFactories.GetFactory(ProviderName);
      }
      catch (Exception x)
      {
        var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
        Warning(string.Format("Failed to load provider `{0}` - {1}", ProviderName, error));
        WriteLine("");
        WriteLine("// -----------------------------------------------------------------------------------------");
        WriteLine("// Failed to load provider `{0}` - {1}", ProviderName, error);
        WriteLine("// -----------------------------------------------------------------------------------------");
        WriteLine("");
        return new Tables();
      }

      try
      {
        Tables result;
        using (var conn = _factory.CreateConnection())
        {
          conn.ConnectionString = ConnectionString;
          conn.Open();

          SchemaReaderBase reader = null;

          // Assume SQL Server
          reader = new SqlServerSchemaReader();

          reader.outer = this;
          result = reader.ReadSchema(conn, _factory);

          // Remove unrequired tables/views
          for (int i = result.Count - 1; i >= 0; i--)
          {
            if (SchemaName != null && string.Compare(result[i].Schema, SchemaName, true) != 0)
            {
              result.RemoveAt(i);
              continue;
            }
            if (!IncludeViews && result[i].IsView)
            {
              result.RemoveAt(i);
              continue;
            }
          }

          conn.Close();


          var rxClean = new Regex("^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
          foreach (var t in result)
          {
            t.ClassName = ClassPrefix + t.ClassName + ClassSuffix;
            foreach (var c in t.Columns)
            {
              c.PropertyName = rxClean.Replace(c.PropertyName, "_$1");

              // Make sure property name doesn't clash with class name
              if (c.PropertyName == t.ClassName)
                c.PropertyName = "_" + c.PropertyName;
            }
          }

          return result;
        }
      }
      catch (Exception x)
      {
        var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
        Warning(string.Format("Failed to read database schema - {0}", error));
        WriteLine("");
        WriteLine("// -----------------------------------------------------------------------------------------");
        WriteLine("// Failed to read database schema - {0}", error);
        WriteLine("// -----------------------------------------------------------------------------------------");
        WriteLine("");
        return new Tables();
      }


    }

  }
}
