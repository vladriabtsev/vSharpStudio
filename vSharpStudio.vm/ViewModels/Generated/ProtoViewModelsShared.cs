using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vSharpStudio.common;
using vSharpStudio.common.ViewModels;
using Google.Protobuf;
using System.Diagnostics;

namespace vSharpStudio.vm.ViewModels.Shared // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\NameSpace.tt Line:24
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface ISharedAcceptVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\NameSpace.tt Line:30
    {
        void AcceptSharedNodeVisitor(SharedVisitor visitor);
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: False 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class TestSharedMesssageValidator : ValidatorBase<TestSharedMesssage, TestSharedMesssageValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class TestSharedMesssage : VmValidatableWithSeverity<TestSharedMesssage, TestSharedMesssageValidator>, ITestSharedMesssage // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        /*public TestSharedMesssage() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:21
        {
            this.OnCreating();
        }*/
        public TestSharedMesssage() 
            : base(TestSharedMesssageValidator.Validator) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:50
        {
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static TestSharedMesssage Clone(ITestSharedMesssage from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:31
        {
            Debug.Assert(from != null);
            TestSharedMesssage vm = new TestSharedMesssage(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:38
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm._BoolValue = from.BoolValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:69
            vm._StringValue = from.StringValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:69
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:78
            return vm;
        }
        public static void Update(TestSharedMesssage to, ITestSharedMesssage from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:82
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._BoolValue = from.BoolValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:146
            to._StringValue = from.StringValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:146
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        #region IEditable
        public override TestSharedMesssage Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return TestSharedMesssage.Clone(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:162
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(TestSharedMesssage from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            TestSharedMesssage.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'test_shared_messsage' to 'TestSharedMesssage'
        public static TestSharedMesssage ConvertToVM(Proto.Config2.test_shared_messsage m, TestSharedMesssage vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:176
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:184
            vm._BoolValue = m.BoolValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:225
            vm._StringValue = m.StringValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:225
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:236
            return vm;
        }
        // Conversion from 'TestSharedMesssage' to 'test_shared_messsage'
        public static Proto.Config2.test_shared_messsage ConvertToProto(TestSharedMesssage vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:241
        {
            Debug.Assert(vm != null);
            Proto.Config2.test_shared_messsage m = new Proto.Config2.test_shared_messsage(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:244
            m.BoolValue = vm.BoolValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:281
            m.StringValue = vm.StringValue; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:281
            return m;
        }
        
        public void AcceptSharedNodeVisitor(SharedVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool BoolValue // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
        { 
            get { return this._BoolValue; }
            set
            {
                if (this._BoolValue != value)
                {
                    this.OnBoolValueChanging(ref value);
                    this._BoolValue = value;
                    this.OnBoolValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _BoolValue;
        partial void OnBoolValueChanging(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnBoolValueChanged();
        
        public string StringValue // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
        { 
            get { return this._StringValue; }
            set
            {
                if (this._StringValue != value)
                {
                    this.OnStringValueChanging(ref value);
                    this._StringValue = value;
                    this.OnStringValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _StringValue = string.Empty;
        partial void OnStringValueChanging(ref string to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnStringValueChanged();
        #endregion Properties
    }
    
    public interface IVisitorProto // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\IVisitorProto.tt Line:8
    {
        void Visit(Proto.Config2.test_shared_messsage p);
    }
    
    public partial class ValidationSharedVisitor : SharedVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:8
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(TestSharedMesssage p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(TestSharedMesssage p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class SharedVisitor : IVisitorSharedNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\NodeVisitor.tt Line:8
    {
        public CancellationToken Token { get { return _cancellationToken; } }
        protected CancellationToken _cancellationToken;
    
        public void Visit(TestSharedMesssage p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(TestSharedMesssage p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(TestSharedMesssage p) { }
        protected virtual void OnVisitEnd(TestSharedMesssage p) { }
    }
    
    public interface IVisitorSharedNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\IVisitorConfigNode.tt Line:8
    {
        System.Threading.CancellationToken Token { get; }
    }
}