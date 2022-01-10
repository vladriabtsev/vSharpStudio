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
using System.Diagnostics.Contracts;
using vSharpStudio.common;
using vSharpStudio.common.ViewModels;
using Google.Protobuf;

namespace vSharpStudio.vm.ViewModels.Shared // NameSpace.tt Line: 23
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface ISharedAcceptVisitor // NameSpace.tt Line: 29
    {
        void AcceptSharedNodeVisitor(SharedVisitor visitor);
    }
    public partial class TestSharedMesssageValidator : ValidatorBase<TestSharedMesssage, TestSharedMesssageValidator> { } // Class.tt Line: 6
    public partial class TestSharedMesssage : VmValidatableWithSeverity<TestSharedMesssage, TestSharedMesssageValidator>, ITestSharedMesssage // Class.tt Line: 7
    {
        #region CTOR
        public TestSharedMesssage() 
            : base(TestSharedMesssageValidator.Validator) // Class.tt Line: 45
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static TestSharedMesssage Clone(ITestSharedMesssage from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            TestSharedMesssage vm = new TestSharedMesssage();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.BoolValue = from.BoolValue; // Clone.tt Line: 65
            vm.StringValue = from.StringValue; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(TestSharedMesssage to, ITestSharedMesssage from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.BoolValue = from.BoolValue; // Clone.tt Line: 141
            to.StringValue = from.StringValue; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override TestSharedMesssage Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return TestSharedMesssage.Clone(this);
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
        public static TestSharedMesssage ConvertToVM(Proto.Config2.test_shared_messsage m, TestSharedMesssage vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.BoolValue = m.BoolValue; // Clone.tt Line: 221
            vm.StringValue = m.StringValue; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'TestSharedMesssage' to 'test_shared_messsage'
        public static Proto.Config2.test_shared_messsage ConvertToProto(TestSharedMesssage vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config2.test_shared_messsage m = new Proto.Config2.test_shared_messsage(); // Clone.tt Line: 239
            m.BoolValue = vm.BoolValue; // Clone.tt Line: 276
            m.StringValue = vm.StringValue; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptSharedNodeVisitor(SharedVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
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
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(TestSharedMesssage p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
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