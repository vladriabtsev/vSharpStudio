using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public interface IGetNodeSetting
    {
        IvPluginGeneratorNodeSettings? GetSettings(string guidAppPrjGen);
        void GetSettings(string guidAppPrjGen, Func<ITreeConfigNode, IvPluginGeneratorNodeSettings, bool> toParents);
        T? GetSettings<T>(string guidAppPrjGen, Func<ITreeConfigNode, T, bool> found);
        TValue? GetSettingsValue<T, TValue>(string guidAppPrjGen, Action<ITreeConfigNode, T, Result<TValue>> found);
        bool IsIncluded(string guidAppPrjGen, bool isFromPrevStable = false);
        bool ContainsSettings(string guidAppPrjGen);
        bool GetBoolSetting(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, bool?> func, bool isFromPrevStable = false);
        string GetStringSetting(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, string> func, bool isFromPrevStable = false);
        Dictionary<string, Dictionary<string, string?>?>? DicVmExclProps { get; }
    }
}
