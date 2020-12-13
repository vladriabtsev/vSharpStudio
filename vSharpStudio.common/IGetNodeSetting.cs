using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public interface IGetNodeSetting
    {
        IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings);
        void GetSettings(string guidAppPrjGen, string guidSettings, Func<ITreeConfigNode, IvPluginGeneratorNodeSettings, bool> toParents);
        T GetSettings<T>(string guidAppPrjGen, string guidSettings, Func<ITreeConfigNode, T, bool> found);
        TValue GetSettingsValue<T, TValue>(string guidAppPrjGen, string guidSettings, Action<ITreeConfigNode, T, Result<TValue>> found);
        bool IsIncluded(string guidAppPrjGen, string guidSettings, bool isFromPrevStable = false);
        bool ContainsSettings(string guidAppPrjGen, string guidSettings);
    }
}
