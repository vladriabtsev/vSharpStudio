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

namespace vSharpStudio.vm.ViewModels.Shared //   7, ""  --- File: NameSpace.tt Line: 24
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface ISharedAcceptVisitor //   7, ""  --- File: NameSpace.tt Line: 30
    {
        void AcceptSharedNodeVisitor(SharedVisitor visitor);
    }
    //   8, ""  --- File: Class.tt Line: 7
    //       IsWithParent: False 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class TestSharedMesssageValidator : ValidatorBase<TestSharedMesssage, TestSharedMesssageValidator> { } //   8, ""  --- File: Class.tt Line: 16
    public partial class TestSharedMesssage : VmValidatableWithSeverity<TestSharedMesssage, TestSharedMesssageValidator>, ITestSharedMesssage //   8, ""  --- File: Class.tt Line: 17
    {
        public override string ToDebugString()
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            p = t.GetProperty("IsNew");
            if (p != null)
                if ((bool?)p.GetValue(this) == true)
                    mes = mes + " New";
            p = t.GetProperty("IsHasNew");
            if (p != null)
                if ((bool?)p.GetValue(this) == true)
                    mes = mes + " HasNew";
            OnDebugStringExtend(ref mes);
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR
        /*public TestSharedMesssage() //   8, ""  --- File: Class.tt Line: 42
        {
            this.OnCreating();
        }*/
        public TestSharedMesssage() 
            : base(TestSharedMesssageValidator.Validator) //   8, ""  --- File: Class.tt Line: 69
        {
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static TestSharedMesssage Clone(ITestSharedMesssage from, bool isDeep = true) //   9, ""  --- File: Clone.tt Line: 31
        {
            Debug.Assert(from != null);
            TestSharedMesssage vm = new TestSharedMesssage(); //   9, ""  --- File: Clone.tt Line: 38
            vm._BoolValue = from.BoolValue; //   9, ""  --- File: Clone.tt Line: 66
            vm._StringValue = from.StringValue; //   9, ""  --- File: Clone.tt Line: 66
            return vm;
        }
        public static void Update(TestSharedMesssage to, ITestSharedMesssage from, bool isDeep = true) //   9, ""  --- File: Clone.tt Line: 76
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._BoolValue = from.BoolValue; //   9, ""  --- File: Clone.tt Line: 140
            to._StringValue = from.StringValue; //   9, ""  --- File: Clone.tt Line: 140
        }
        //   9, ""  --- File: Clone.tt Line: 146
        #region IEditable
        public override TestSharedMesssage Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return TestSharedMesssage.Clone(this); //   9, ""  --- File: Clone.tt Line: 156
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
        public static TestSharedMesssage ConvertToVM(Proto.Config2.test_shared_messsage m, TestSharedMesssage vm) //   9, ""  --- File: Clone.tt Line: 170
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._BoolValue = m.BoolValue; //   9, ""  --- File: Clone.tt Line: 216
            vm._StringValue = m.StringValue; //   9, ""  --- File: Clone.tt Line: 216
            return vm;
        }
        // Conversion from 'TestSharedMesssage' to 'test_shared_messsage'
        public static Proto.Config2.test_shared_messsage ConvertToProto(TestSharedMesssage vm) //   9, ""  --- File: Clone.tt Line: 229
        {
            Debug.Assert(vm != null);
            Proto.Config2.test_shared_messsage m = new Proto.Config2.test_shared_messsage(); //   9, ""  --- File: Clone.tt Line: 232
            m.BoolValue = vm.BoolValue; //   9, ""  --- File: Clone.tt Line: 269
            m.StringValue = vm.StringValue; //   9, ""  --- File: Clone.tt Line: 269
            return m;
        }
        
        public void AcceptSharedNodeVisitor(SharedVisitor visitor) //   9, ""  --- File: AcceptNodeVisitor.tt Line: 9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); //   9, ""  --- File: AcceptNodeVisitor.tt Line: 36
        }
        #endregion Procedures
        #region Properties
        
        public bool BoolValue //   9, ""  --- File: Property.tt Line: 8
        { 
            get { return this._BoolValue; }
            set
            {
                // Use 'OnBoolValueChanging' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._BoolValue, value, (t) => { /*this.OnBoolValueChanging(ref value);*/ this._BoolValue = value; this.OnBoolValueChanged(); })) //   9, ""  --- File: Property.tt Line: 15
                {
                    this.ValidateProperty(); //   9, ""  --- File: Property.tt Line: 18
                    this.IsChanged = true; //   9, ""  --- File: Property.tt Line: 21
                }
            }
        }
        private bool _BoolValue; //   9, ""  --- File: Property.tt Line: 41
        //partial void OnBoolValueChanging(ref bool to); //   9, ""  --- File: Property.tt Line: 43
        partial void OnBoolValueChanged();
        
        public string StringValue //   9, ""  --- File: Property.tt Line: 8
        { 
            get { return this._StringValue; }
            set
            {
                // Use 'OnStringValueChanging' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._StringValue, value, (t) => { /*this.OnStringValueChanging(ref value);*/ this._StringValue = value; this.OnStringValueChanged(); })) //   9, ""  --- File: Property.tt Line: 15
                {
                    this.ValidateProperty(); //   9, ""  --- File: Property.tt Line: 18
                    this.IsChanged = true; //   9, ""  --- File: Property.tt Line: 21
                }
            }
        }
        private string _StringValue = string.Empty; //   9, ""  --- File: Property.tt Line: 41
        //partial void OnStringValueChanging(ref string to); //   9, ""  --- File: Property.tt Line: 43
        partial void OnStringValueChanged();
        #endregion Properties
    }
    
    public interface IVisitorProto //   8, ""  --- File: IVisitorProto.tt Line: 8
    {
        void Visit(Proto.Config2.test_shared_messsage p);
    }
    
    public partial class ValidationSharedVisitor : SharedVisitor //   8, ""  --- File: ValidationVisitor.tt Line: 8
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(TestSharedMesssage p) //   8, ""  --- File: ValidationVisitor.tt Line: 16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(TestSharedMesssage p) //   8, ""  --- File: ValidationVisitor.tt Line: 50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class SharedVisitor : IVisitorSharedNode //   8, ""  --- File: NodeVisitor.tt Line: 8
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
    
    public interface IVisitorSharedNode //   8, ""  --- File: IVisitorConfigNode.tt Line: 8
    {
        System.Threading.CancellationToken Token { get; }
    }
}