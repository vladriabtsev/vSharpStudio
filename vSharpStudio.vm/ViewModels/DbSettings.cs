﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("DbSettings: Schema={DbSchema,nq}, {IdGenerator,nq}, PkType={KeyType,nq}, PkName={KeyName, nq}, Conn={ConnectionStringName, nq}")]
    public partial class DbSettings
    {
        partial void OnInit()
        {
            this.IsNotifying = false;
            this.DbSchema = "v";
            this.PKeyName = "Id";
            this.PKeyGuid = System.Guid.NewGuid().ToString();
            //this.VersionFieldName = "Version";
            //this.VersionFieldGuid = System.Guid.NewGuid().ToString();
            this.IdGenerator = DbIdGeneratorMethod.HiLo;
            this.PKeyType = EnumPrimaryKeyType.INT;
            this.IsNotifying = true;
        }

        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        protected override void OnIsChangedChanged()
        {
            if (this.IsChanged)
                this.Parent.IsChanged = true;
        }
        [BrowsableAttribute(false)]
        public string PKeyTypeStr
        {
            get
            {
                switch (this.PKeyType)
                {
                    case EnumPrimaryKeyType.INT:
                        return "int";
                    case EnumPrimaryKeyType.LONG:
                        return "long";
                }
                throw new NotImplementedException();
            }
        }
    }
}
