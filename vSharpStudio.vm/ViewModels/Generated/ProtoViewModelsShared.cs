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

namespace vSharpStudio.vm.ViewModels.Shared // NameSpace.tt Line: 23
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface ISharedAcceptVisitor // NameSpace.tt Line: 29
    {
        void AcceptSharedNodeVisitor(SharedVisitor visitor);
    }
    // Class.tt Line: 6
    //       IsWithParent: False 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class TestSharedMesssageValidator : ValidatorBase<TestSharedMesssage, TestSharedMesssageValidator> { } // Class.tt Line: 15
    public partial class TestSharedMesssage : VmValidatableWithSeverity<TestSharedMesssage, TestSharedMesssageValidator>, ITestSharedMesssage // Class.tt Line: 16
    {
        #region CTOR
        /*public TestSharedMesssage() // Class.tt Line: 20
        {
            this.OnCreating();
        }*/
        public TestSharedMesssage() 
            : base(TestSharedMesssageValidator.Validator) // Class.tt Line: 51
        {
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
        }
        partial void OnCreating();
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static TestSharedMesssage Clone(ITestSharedMesssage from, bool isDeep = true) // Clone.tt Line: 30
        {
            Debug.Assert(from != null);
            TestSharedMesssage vm = new TestSharedMesssage(); // Clone.tt Line: 37
            vm.IsNotifying = false; // Clone.tt Line: 39
            vm.IsValidate = false;
            vm.BoolValue = from.BoolValue; // Clone.tt Line: 67
            vm.StringValue = from.StringValue; // Clone.tt Line: 67
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(TestSharedMesssage to, ITestSharedMesssage from, bool isDeep = true) // Clone.tt Line: 79
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to.BoolValue = from.BoolValue; // Clone.tt Line: 143
            to.StringValue = from.StringValue; // Clone.tt Line: 143
        }
        // Clone.tt Line: 149
        #region IEditable
        public override TestSharedMesssage Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return TestSharedMesssage.Clone(this); // Clone.tt Line: 158
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
        public static TestSharedMesssage ConvertToVM(Proto.Config2.test_shared_messsage m, TestSharedMesssage vm) // Clone.tt Line: 172
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.BoolValue = m.BoolValue; // Clone.tt Line: 220
            vm.StringValue = m.StringValue; // Clone.tt Line: 220
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'TestSharedMesssage' to 'test_shared_messsage'
        public static Proto.Config2.test_shared_messsage ConvertToProto(TestSharedMesssage vm) // Clone.tt Line: 235
        {
            Debug.Assert(vm != null);
            Proto.Config2.test_shared_messsage m = new Proto.Config2.test_shared_messsage(); // Clone.tt Line: 238
            m.BoolValue = vm.BoolValue; // Clone.tt Line: 275
            m.StringValue = vm.StringValue; // Clone.tt Line: 275
            return m;
        }
        
        public void AcceptSharedNodeVisitor(SharedVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
    #if !DEBUG
        [Browsable(false)]
    #endif
        
        public bool BoolValue // Property.tt Line: 55
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
        partial void OnBoolValueChanging(ref bool to); // Property.tt Line: 79
        partial void OnBoolValueChanged();
        
        public string StringValue // Property.tt Line: 55
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
        partial void OnStringValueChanging(ref string to); // Property.tt Line: 79
        partial void OnStringValueChanged();
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
        void Visit(Proto.Config2.test_shared_messsage p);
    }
    
    public partial class ValidationSharedVisitor : SharedVisitor // ValidationVisitor.tt Line: 7
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(TestSharedMesssage p) // ValidationVisitor.tt Line: 15
        {
            Debug.Assert(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(TestSharedMesssage p) // ValidationVisitor.tt Line: 48
        {
            Debug.Assert(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
    }
    
    public partial class SharedVisitor : IVisitorSharedNode // NodeVisitor.tt Line: 7
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
    
    public interface IVisitorSharedNode // IVisitorConfigNode.tt Line: 7
    {
        System.Threading.CancellationToken Token { get; }
    }
}