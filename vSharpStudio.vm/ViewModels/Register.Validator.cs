using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RegisterValidator
    {
        //public static PropertyRangeValuesRequirements GetRangeValidation(Property p)
        //{
        //    return PropertyRangeValuesRequirements.GetRangeValidation(p);
        //}
        //private void ValidateRangeValuesRequirements(ValidationContext<Property> cntx, Property p)
        //{
        //    var req = PropertyValidator.GetRangeValidation(p);
        //    if (req.IsHasErrors)
        //    {
        //        foreach (var t in req.ListErrors)
        //        {
        //            var vf = new ValidationFailure(nameof(p.RangeValuesRequirementStr), t);
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        public RegisterValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.PropertyMoneyAccumulatorAccuracy).Custom((acc, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseMoneyAccumulator)
                    return;
                if (acc >= p.PropertyMoneyAccumulatorLength)
                {
                    var vf = new ValidationFailure(nameof(p.PropertyMoneyAccumulatorAccuracy),
                        $"Value greater or equal than {nameof(p.PropertyMoneyAccumulatorLength)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.PropertyMoneyAccumulatorLength).Custom((len, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseMoneyAccumulator)
                    return;
                if (len <= p.PropertyMoneyAccumulatorAccuracy)
                {
                    var vf = new ValidationFailure(nameof(p.PropertyMoneyAccumulatorLength),
                        $"Value less or equal than {nameof(p.PropertyMoneyAccumulatorAccuracy)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (len > 28)
                {
                    var vf = new ValidationFailure(nameof(p.PropertyMoneyAccumulatorLength),
                        $"Value greater than 28 is not supported");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.PropertyQtyAccumulatorAccuracy).Custom((acc, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseQtyAccumulator)
                    return;
                if (acc >= p.PropertyQtyAccumulatorLength)
                {
                    var vf = new ValidationFailure(nameof(p.PropertyQtyAccumulatorAccuracy),
                        $"Value greater or equal than {nameof(p.PropertyQtyAccumulatorLength)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.PropertyQtyAccumulatorLength).Custom((len, cntx) =>
            {
                var r = (Register)cntx.InstanceToValidate;
                if (!r.UseQtyAccumulator)
                    return;
                if (len <= r.PropertyQtyAccumulatorAccuracy)
                {
                    var vf = new ValidationFailure(nameof(r.PropertyQtyAccumulatorLength),
                        $"Value less or equal than {nameof(r.PropertyQtyAccumulatorAccuracy)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (len > 28)
                {
                    var vf = new ValidationFailure(nameof(r.PropertyQtyAccumulatorLength),
                        $"Value greater than 28 is not supported");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.UseQtyAccumulator).Custom((use, cntx) =>
            {
                var r = (Register)cntx.InstanceToValidate;
                if (!use && !r.UseMoneyAccumulator)
                {
                    var vf = new ValidationFailure(nameof(r.UseQtyAccumulator),
                        $"At least one accumulator type has to be selected");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.UseMoneyAccumulator).Custom((use, cntx) =>
            {
                var r = (Register)cntx.InstanceToValidate;
                if (!use && !r.UseQtyAccumulator)
                {
                    var vf = new ValidationFailure(nameof(r.UseMoneyAccumulator),
                        $"At least one accumulator type has to be selected");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            // register without dimensions can be usefull
            //this.RuleFor(x => x.GroupRegisterDimensions.ListDimensions).Custom((lst, cntx) =>
            //{
            //    if (lst.Count == 0)
            //    {
            //        var r = (Register)cntx.InstanceToValidate;
            //        var vf = new ValidationFailure(cntx.PropertyPath,
            //            $"Register '{r.Name}'. Dimensions are not selected.");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //    }
            //});
            this.RuleFor(x => x.ListObjectDocRefs).Custom((lst, cntx) =>
            {
                if (lst.Count == 0)
                {
                    var r = (Register)cntx.InstanceToValidate;
                    var vf = new ValidationFailure(cntx.PropertyPath,
                        $"Register '{r.Name}'. List of Document types for Register is empty.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ListObjectDocRefs).Custom((lst, cntx) =>
            {
                var r = (Register)cntx.InstanceToValidate;
                foreach (var t in lst)
                {
                    var doc = (Document)r.Cfg.DicNodes[t.ForeignObjectGuid];
                    //this.foundDic.Clear();

                    // not mapped
                    var foundDocMappings = false;
                    foreach (var dtr in r.ListDocMappings)
                    {
                        if (dtr.DocGuid == doc.Guid)
                        {
                            var found = false;
                            foreach (var rd in r.GroupRegisterDimensions.ListDimensions)
                            {
                                found = false;
                                foreach (var dpm in dtr.ListMappings)
                                {
                                    if (dpm.RegPropGuid == rd.Guid)
                                    {
                                        var p = (Property)r.Cfg.DicNodes[dpm.DocPropGuid];
                                        if (p.DataType.DataTypeEnum != EnumDataType.CATALOG)
                                        {
                                            var vf = new ValidationFailure(cntx.PropertyPath,
                                                $"Register '{r.Name}'. Dimension can mapped to catalog property only, but property '{p.Name}' of '{doc.Name}' document has type '{System.Enum.GetName<EnumDataType>(p.DataType.DataTypeEnum)}'.");
                                            vf.Severity = Severity.Error;
                                            cntx.AddFailure(vf);
                                        }
                                        if (!string.IsNullOrWhiteSpace(p.DataType.ObjectRef.ForeignObjectGuid) && p.DataType.ObjectRef.ForeignObjectGuid != rd.DimensionCatalogGuid)
                                        {
                                            var cp = (Catalog)r.Cfg.DicNodes[p.DataType.ObjectRef.ForeignObjectGuid];
                                            var crd = (Catalog)r.Cfg.DicNodes[rd.DimensionCatalogGuid];
                                            var vf = new ValidationFailure(cntx.PropertyPath,
                                                $"Register '{r.Name}'. Dimension can mapped to catalog property of type '{crd.Name}', but property '{p.Name}' of '{doc.Name}' document has catalog type '{cp.Name}'.");
                                            vf.Severity = Severity.Error;
                                            cntx.AddFailure(vf);
                                        }
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                        $"Register '{r.Name}'. Dimension '{rd.Name}' is not mapped to '{doc.Name}' document property.");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                            foreach (var ra in r.GroupProperties.ListProperties)
                            {
                                found = false;
                                foreach (var dpm in dtr.ListMappings)
                                {
                                    if (dpm.RegPropGuid == ra.Guid)
                                    {
                                        var p = (Property)r.Cfg.DicNodes[dpm.DocPropGuid];
                                        var mes = ra.CanAssignFrom(p);
                                        if (!string.IsNullOrEmpty(mes))
                                        {
                                            var vf = new ValidationFailure(cntx.PropertyPath,
                                                $"Register '{r.Name}'. {mes} Attached property '{ra.Name}' is mapped to '{p.Name}' property of '{doc.Name}' document.");
                                            vf.Severity = Severity.Error;
                                            cntx.AddFailure(vf);
                                        }
                                        found = true; break;
                                    }
                                }
                                if (!found)
                                {
                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                        $"Register '{r.Name}'. Attached property '{ra.Name}' is not mapped to '{doc.Name}' document property.");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                            if (r.UseMoneyAccumulator)
                            {
                                found = false;
                                foreach (var dpm in dtr.ListMappings)
                                {
                                    if (dpm.RegPropGuid == r.PropertyMoneyAccumulatorGuid)
                                    {
                                        if (!string.IsNullOrWhiteSpace(dpm.DocPropGuid))
                                        {
                                            var p = (Property)r.Cfg.DicNodes[dpm.DocPropGuid];
                                            if (p.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
                                            {
                                                var vf = new ValidationFailure(cntx.PropertyPath,
                                                    $"Register '{r.Name}'. Accumulator property '{r.PropertyMoneyAccumulatorName}' can be mapped to numerical property only, but property '{p.Name}' of '{doc.Name}' document has a type '{Enum.GetName<EnumDataType>(p.DataType.DataTypeEnum)}'.");
                                                vf.Severity = Severity.Info;
                                                cntx.AddFailure(vf);
                                            }
                                            else
                                            {
                                                //var mes = p.CanAssignToNumerical(r.TableTurnoverPropertyMoneyAccumulatorLength, r.TableTurnoverPropertyMoneyAccumulatorAccuracy, false);
                                                if (p.Length > r.PropertyMoneyAccumulatorLength)
                                                {
                                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                                        $"Register '{r.Name}'. Accumulator property '{r.PropertyMoneyAccumulatorName}' has length less than length '{p.Name}' property of '{doc.Name}' document.");
                                                    vf.Severity = Severity.Info;
                                                    cntx.AddFailure(vf);
                                                }
                                                if (p.Accuracy > r.PropertyMoneyAccumulatorAccuracy)
                                                {
                                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                                        $"Register '{r.Name}'. Accumulator property '{r.PropertyMoneyAccumulatorName}' has accuracy less than accuracy '{p.Name}' property of '{doc.Name}' document.");
                                                    vf.Severity = Severity.Info;
                                                    cntx.AddFailure(vf);
                                                }
                                            }
                                        }
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                        $"Register '{r.Name}'. Accumulator property '{r.PropertyMoneyAccumulatorName}' is not mapped to '{doc.Name}' document property.");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                            if (r.UseQtyAccumulator)
                            {
                                found = false;
                                foreach (var dpm in dtr.ListMappings)
                                {
                                    if (dpm.RegPropGuid == r.PropertyQtyAccumulatorGuid)
                                    {
                                        if (!string.IsNullOrWhiteSpace(dpm.DocPropGuid))
                                        {
                                            var p = (Property)r.Cfg.DicNodes[dpm.DocPropGuid];
                                            if (p.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
                                            {
                                                var vf = new ValidationFailure(cntx.PropertyPath,
                                                    $"Register '{r.Name}'. Accumulator property '{r.PropertyQtyAccumulatorName}' can be mapped to numerical property only, but property '{p.Name}' of '{doc.Name}' document has a type '{Enum.GetName<EnumDataType>(p.DataType.DataTypeEnum)}'.");
                                                vf.Severity = Severity.Info;
                                                cntx.AddFailure(vf);
                                            }
                                            else
                                            {
                                                //var mes = p.CanAssignToNumerical(r.TableTurnoverPropertyMoneyAccumulatorLength, r.TableTurnoverPropertyMoneyAccumulatorAccuracy, false);
                                                if (p.Length > r.PropertyQtyAccumulatorLength)
                                                {
                                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                                        $"Register '{r.Name}'. Accumulator property '{r.PropertyQtyAccumulatorName}' has length less than length '{p.Name}' property of '{doc.Name}' document.");
                                                    vf.Severity = Severity.Info;
                                                    cntx.AddFailure(vf);
                                                }
                                                if (p.Accuracy > r.PropertyQtyAccumulatorAccuracy)
                                                {
                                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                                        $"Register '{r.Name}'. Accumulator property '{r.PropertyQtyAccumulatorName}' has accuracy less than accuracy '{p.Name}' property of '{doc.Name}' document.");
                                                    vf.Severity = Severity.Info;
                                                    cntx.AddFailure(vf);
                                                }
                                            }
                                        }
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                        $"Register '{r.Name}'. Accumulator property '{r.PropertyQtyAccumulatorName}' is not mapped to '{doc.Name}' document property.");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                            foundDocMappings = true;
                            break;
                        }
                    }
                    if (!foundDocMappings)
                    {
                        var vf = new ValidationFailure(cntx.PropertyPath,
                            $"Register '{r.Name}'. There are no any mappings for '{doc.Name}' document.");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }

                    // existing mappings
                    var propMappings = new List<MappingBranchPath>();
                    var hashPropGuid = new HashSet<string>();
                    foreach (var dtr in r.ListDocMappings)
                    {
                        if (dtr.DocGuid != doc.Guid)
                            continue;
                        foreach (var dpm in dtr.ListMappings)
                        {
                            if (string.IsNullOrWhiteSpace(dpm.DocPropGuid))
                                continue;
                            var regPropName = "";
                            foreach (var rd in r.GroupRegisterDimensions.ListDimensions)
                            {
                                if (rd.Guid == dpm.RegPropGuid)
                                {
                                    regPropName = rd.Name;
                                    break;
                                }
                            }
                            if (string.IsNullOrEmpty(regPropName))
                            {
                                foreach (var rp in r.GroupProperties.ListProperties)
                                {
                                    if (rp.Guid == dpm.RegPropGuid)
                                    {
                                        regPropName = rp.Name;
                                        break;
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(regPropName))
                            {
                                if (r.PropertyMoneyAccumulatorGuid == dpm.RegPropGuid)
                                {
                                    regPropName = r.PropertyMoneyAccumulatorName;
                                }
                                else if (r.PropertyQtyAccumulatorGuid == dpm.RegPropGuid)
                                {
                                    regPropName = r.PropertyQtyAccumulatorName;
                                }
                                else
                                {
                                    var ndic = r.Cfg.DicNodes;
                                    var doc_p = ndic[dpm.RegPropGuid];
                                    Debug.Assert(false);
                                }
                            }
                            if (!r.Cfg.DicNodes.ContainsKey(dpm.DocPropGuid))
                            {
                                var vf = new ValidationFailure(cntx.PropertyPath,
                                    $"Register '{r.Name}'. It's property '{regPropName}' mapped to not existing property of '{doc.Name}' document.");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                            else
                            {
                                var prop = (Property)r.Cfg.DicNodes[dpm.DocPropGuid];
                                var path = this.MappingPath(prop).ToString();
                                propMappings.Add(new MappingBranchPath() { BranchPath = path, RegPropGuid = dpm.RegPropGuid });
                                if (hashPropGuid.Contains(dpm.DocPropGuid))
                                {
                                    var vf = new ValidationFailure(cntx.PropertyPath,
                                        $"Property '{regPropName}' of register '{r.Name}' is mapped to property '{prop.Name}' of document '{doc.Name}'. This document property is used for mapping more than ones.");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                                hashPropGuid.Add(dpm.DocPropGuid);
                            }
                        }
                    }
                    // All mapping expected to be on same document tree branch (from header to deepest detail record)
                    if (propMappings.Count > 0)
                    {
                        bool foundMappingError = false;
                        propMappings.Sort(delegate (MappingBranchPath x, MappingBranchPath y)
                        {
                            return x.BranchPath.CompareTo(y.BranchPath);
                        });
                        var pathPrev = propMappings[0];
                        foreach (var path in propMappings)
                        {
                            if (!path.BranchPath.StartsWith(pathPrev.BranchPath))
                            {
                                var rp1 = (Property)r.Cfg.DicNodes[pathPrev.RegPropGuid];
                                var rp2 = (Property)r.Cfg.DicNodes[path.RegPropGuid];
                                var vf = new ValidationFailure(cntx.PropertyPath,
                                    $"Register '{r.Name}'. It's property '{rp1.Name}' and  '{rp2.Name}' are mapped on different tree branches of '{doc.Name}' document.");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                                foundMappingError = true;
                            }
                        }
                        // Accumulator properties expected to be mapped on a same record as deepest dimension property map
                        if (!foundMappingError)
                        {
                            var dimMappings = new List<MappingBranchPath>();
                            foreach (var rd in r.GroupRegisterDimensions.ListDimensions)
                            {
                                foreach (var pm in propMappings)
                                {
                                    if (rd.Guid == pm.RegPropGuid)
                                    {
                                        dimMappings.Add(pm);
                                    }
                                }
                            }
                            if (dimMappings.Count > 0)
                            {
                                dimMappings.Sort(delegate (MappingBranchPath x, MappingBranchPath y)
                                {
                                    return x.BranchPath.CompareTo(y.BranchPath);
                                });
                                var deepest = dimMappings[dimMappings.Count - 1];
                                var deepestDimension = (RegisterDimension)r.Cfg.DicNodes[deepest.RegPropGuid];
                                foreach (var pm in propMappings)
                                {
                                    if (r.UseMoneyAccumulator && r.PropertyMoneyAccumulatorGuid == pm.RegPropGuid)
                                    {
                                        if (deepest.BranchPath != pm.BranchPath)
                                        {
                                            var vf = new ValidationFailure(cntx.PropertyPath,
                                                $"Register '{r.Name}'. Accumulator property '{r.PropertyMoneyAccumulatorName}' not mapped on a same record as a deepest dimension '{deepestDimension.Name}' of '{doc.Name}' document.");
                                            vf.Severity = Severity.Error;
                                            cntx.AddFailure(vf);
                                        }
                                    }
                                    if (r.UseQtyAccumulator && r.PropertyQtyAccumulatorGuid == pm.RegPropGuid)
                                    {
                                        if (deepest.BranchPath != pm.BranchPath)
                                        {
                                            var vf = new ValidationFailure(cntx.PropertyPath,
                                                $"Register '{r.Name}'. Accumulator property '{r.PropertyQtyAccumulatorName}' not mapped on a same record as a deepest dimension '{deepestDimension.Name}' of '{doc.Name}' document.");
                                            vf.Severity = Severity.Error;
                                            cntx.AddFailure(vf);
                                        }
                                    }
                                }
                            }
                        }
                    }




                    //foreach (var rd in r.GroupRegisterDimensions.ListDimensions)
                    //{
                    //    if (string.IsNullOrEmpty(rd.DimensionCatalogGuid))
                    //        continue;
                    //    var cat = (Catalog)rd.Cfg.DicNodes[rd.DimensionCatalogGuid];
                    //    int level = 0;
                    //    uint pos = 0;
                    //    int found = 0;
                    //    found += this.TryFindPropertyByCatalogGuid(doc.GroupProperties, doc.GroupDetails, rd, level, 0);
                    //    // Chack if dimension catalog reference can be found
                    //    if (found == 0)
                    //    {
                    //        var vf = new ValidationFailure(nameof(r.ListDocGuids),
                    //            $"Register '{r.Name}'. Document '{doc.Name}' doesn't have property of type catalog '{cat.Name}' which is required by dimension {rd.Name}.");
                    //        vf.Severity = Severity.Error;
                    //        cntx.AddFailure(vf);
                    //    }
                    //    else if (found > 1)
                    //    {
                    //        // check if explicitly mapped
                    //        bool isExplicitlyMapped = false;
                    //        foreach (var dm in r.ListDocMappings)
                    //        {
                    //            Debug.Assert(!string.IsNullOrWhiteSpace(dm.DocGuid));
                    //            if (dm.DocGuid != doc.Guid)
                    //                continue;
                    //            foreach (var m in dm.ListMappings)
                    //            {
                    //                if (string.IsNullOrEmpty(m.DocPropGuid))
                    //                    continue;
                    //                var p = (IProperty)rd.Cfg.DicNodes[m.DocPropGuid];
                    //                if (string.IsNullOrEmpty(p.DataType.ObjectGuid))
                    //                    continue;
                    //                if (p.DataType.ObjectGuid == rd.DimensionCatalogGuid)
                    //                {
                    //                    isExplicitlyMapped = true;
                    //                    break;
                    //                }
                    //            }
                    //            if (isExplicitlyMapped)
                    //                break;
                    //        }
                    //        if (!isExplicitlyMapped)
                    //        {
                    //            var vf = new ValidationFailure(nameof(r.ListDocGuids),
                    //                $"Register '{r.Name}'. Document '{doc.Name}' has more than one property of type catalog '{cat.Name}' which is required by dimension {rd.Name}. Need manual mapping.");
                    //            vf.Severity = Severity.Error;
                    //            cntx.AddFailure(vf);
                    //        }
                    //    }
                    //}

                    //// Check if all catalog references are on an one branch (or on a same node)
                    //Dictionary<uint, List<IProperty>> deepestNode = new();
                    //var deepestLevel = -1;
                    //foreach (var nrd in this.foundDic)
                    //{
                    //    foreach (var lev in nrd.Value.Keys)
                    //    {
                    //        if (lev > deepestLevel)
                    //        {
                    //            deepestLevel = lev;
                    //            deepestNode = nrd.Value[lev];
                    //        }
                    //    }
                    //}
                    //if (deepestLevel != -1)
                    //{
                    //    Debug.Assert(deepestNode.Keys.Count == 1);
                    //    deepestNode[deepestNode.Keys[0]]



                    //        //var vf = new ValidationFailure(nameof(r.ListDocGuids),
                    //        //    $"Document '{doc.Name}' doesn't have property of type catalog '{cat.Name}' which is required by dimension {rd.Name}.");
                    //        //vf.Severity = Severity.Error;
                    //        //cntx.AddFailure(vf);


                    //    // Chack if all accumulation properties can be found on deepest node

                    //}
                    //foundDic.Clear();
                }
            });
        }
        // IRegisterDimension, tree level (0 for root), detail position (0 for root), property
        //private Dictionary<RegisterDimension, Dictionary<int, Dictionary<uint, List<Property>>>> foundDic = new();
        //private int TryFindPropertyByCatalogGuid(GroupListProperties gp, GroupListDetails? gd, RegisterDimension rd, int level, uint posDetail)
        //{
        //    int found = 0;

        //    foreach (var p in gp.ListProperties)
        //    {
        //        if (p.DataType.ObjectGuid == rd.DimensionCatalogGuid)
        //        {
        //            found++;
        //            if (!foundDic.TryGetValue(rd, out var d_rd)) { d_rd = new(); foundDic[rd] = d_rd; }
        //            if (!d_rd.TryGetValue(level, out var d_rd_level)) { d_rd_level = new(); d_rd[level] = d_rd_level; }
        //            if (!d_rd_level.TryGetValue(posDetail, out var d_rd_level_pos)) { d_rd_level_pos = new(); d_rd_level[posDetail] = d_rd_level_pos; }
        //            d_rd_level_pos.Add(p);
        //        }
        //    }

        //    if (gd != null)
        //    {
        //        level++;
        //        foreach (var t in gd.ListDetails)
        //        {
        //            found += this.TryFindPropertyByCatalogGuid(t.GroupProperties, t.GroupDetails, rd, level, t.Position);
        //            //var pos = t.Position;
        //            //foreach (var p in t.GroupProperties.ListProperties)
        //            //{
        //            //    if (p.DataType.ObjectGuid == rd.DimensionCatalogGuid)
        //            //    {
        //            //        found++;
        //            //        if (!foundDic.TryGetValue(rd, out var d_rd)) { d_rd = new(); foundDic[rd] = d_rd; }
        //            //        if (!d_rd.TryGetValue(level, out var d_rd_level)) { d_rd_level = new(); d_rd[level] = d_rd_level; }
        //            //        if (!d_rd_level.TryGetValue(pos, out var d_rd_level_pos)) { d_rd_level_pos = new(); d_rd_level[pos] = d_rd_level_pos; }
        //            //        d_rd_level_pos.Add(p);
        //            //    }
        //            //}
        //            //found += this.TryFindPropertyByCatalogGuid(t.GroupProperties, t.GroupDetails, rd, level);
        //        }
        //    }
        //    return found;
        //}
        private StringBuilder MappingPath(ITreeConfigNode? node, StringBuilder? sb = null)
        {
            Debug.Assert(node != null);
            if (sb == null)
                sb = new StringBuilder();
            if (node is Document)
            {
                sb.Append('0');
            }
            else
            {
                if (node is Detail dt)
                {
                    MappingPath(dt.ParentGroupListDetails.Parent, sb);
                    sb.Append(',');
                    sb.Append(dt.Position);
                }
                else if (node is Property p)
                {
                    if (p.Parent is DocumentTimeline dtl)
                    {
                        //MappingPath(dtl, sb);
                    }
                    else
                    {
                        MappingPath(p.ParentGroupListProperties.Parent, sb);
                    }
                }
            }
            return sb;
        }
        private class MappingBranchPath // : IComparable // IComparer<MappingBranchPath>
        {
            public string BranchPath { get; set; } = string.Empty;
            public string RegPropGuid { get; set; } = string.Empty;

            //public int CompareTo(object? obj)
            //{
            //    throw new NotImplementedException();
            //}
            //public int Compare(MappingBranchPath? x, MappingBranchPath? y)
            //{
            //    return x.BranchPath.CompareTo(y.BranchPath);
            //}
        }
    }
}
