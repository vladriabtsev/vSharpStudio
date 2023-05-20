using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class GenSettingsPropertyGridVm : ObservableObject
    {
        public GenSettingsPropertyGridVm() { }
        public GenSettingsPropertyGridVm(ITreeConfigNode node, IvPluginGeneratorNodeSettings? set) : this()
        {
            this.Node = node;
            this.Settings = set;
        }
        public IvPluginGeneratorNodeSettings? Settings { get => settings; set => SetProperty(ref settings, value); }
        private IvPluginGeneratorNodeSettings? settings;
        public GridLength GridColumnWidth
        {
            get
            {
                if (this.settings == null)
                    return new GridLength(0, GridUnitType.Pixel);
                return new GridLength(250, GridUnitType.Pixel);
            }
        }
        public ITreeConfigNode? Node { get; set; }
        public string NodeName
        {
            get
            {
                if (Node == null)
                    return "";
                if (Node is Property) return $"Property: {Node.Name}";
                if (Node is Catalog) return $"Catalog: {Node.Name}";
                if (Node is Document) return $"Document: {Node.Name}";
                if (Node is Detail) return $"Detail: {Node.Name}";
                if (Node is GroupConstantGroups) return $"Constant Group: {Node.Name}";
                if (Node is Form) return $"Form: {Node.Name}";
                return Node.Name;
            }
        }
    }
}
