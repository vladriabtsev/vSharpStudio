using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
            this.RuleFor(x => x.TableTurnoverPropertyMoneyAccumulatorAccuracy).Custom((acc, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseMoneyAccumulator)
                    return;
                if (acc >= p.TableTurnoverPropertyMoneyAccumulatorLength)
                {
                    var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyMoneyAccumulatorAccuracy),
                        $"Value greater or equal than {nameof(p.TableTurnoverPropertyMoneyAccumulatorLength)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.TableTurnoverPropertyMoneyAccumulatorLength).Custom((len, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseMoneyAccumulator)
                    return;
                if (len <= p.TableTurnoverPropertyMoneyAccumulatorAccuracy)
                {
                    var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyMoneyAccumulatorLength),
                        $"Value less or equal than {nameof(p.TableTurnoverPropertyMoneyAccumulatorAccuracy)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (len > 28)
                {
                    var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyMoneyAccumulatorLength),
                        $"Value greater than 28 is not supported");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.TableTurnoverPropertyQtyAccumulatorAccuracy).Custom((acc, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseQtyAccumulator)
                    return;
                if (acc >= p.TableTurnoverPropertyQtyAccumulatorLength)
                {
                    var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyQtyAccumulatorAccuracy),
                        $"Value greater or equal than {nameof(p.TableTurnoverPropertyQtyAccumulatorLength)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.TableTurnoverPropertyQtyAccumulatorLength).Custom((len, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseQtyAccumulator)
                    return;
                if (len <= p.TableTurnoverPropertyQtyAccumulatorAccuracy)
                {
                    var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyQtyAccumulatorLength),
                        $"Value less or equal than {nameof(p.TableTurnoverPropertyQtyAccumulatorAccuracy)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (len > 28)
                {
                    var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyQtyAccumulatorLength),
                        $"Value greater than 28 is not supported");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.UseQtyAccumulator).Custom((use, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!use && !p.UseMoneyAccumulator)
                {
                    var vf = new ValidationFailure(nameof(p.UseQtyAccumulator),
                        $"At least one accumulator type has to be selected");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.UseMoneyAccumulator).Custom((use, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!use && !p.UseQtyAccumulator)
                {
                    var vf = new ValidationFailure(nameof(p.UseMoneyAccumulator),
                        $"At least one accumulator type has to be selected");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.GroupRegisterDimensions.ListDimensions).Must((lst) =>
            {
                if (lst.Count == 0)
                    return false;
                return true;
            }).WithMessage("Register dimentions are not selected");
            this.RuleFor(x => x.ListDocGuids).Must((lst) =>
            {
                if (lst.Count == 0)
                    return false;
                return true;
            }).WithMessage("List of Document types for Register is empty"); //.WithSeverity(Severity.Warning);
            this.RuleFor(x => x.ListDocGuids).Custom((lst, cntx) =>
            {
                var r = (Register)cntx.InstanceToValidate;
                foreach (var t in lst)
                {
                    var doc = (IDocument)r.Cfg.DicNodes[t];
                    this.foundDic.Clear();
                    foreach (var rd in r.GroupRegisterDimensions.ListDimensions)
                    {
                        if (string.IsNullOrEmpty(rd.DimensionCatalogGuid))
                            continue;
                        var cat = (ICatalog)rd.Cfg.DicNodes[rd.DimensionCatalogGuid];
                        int level = 0;
                        uint pos = 0;
                        int found = 0;
                        foreach (var p in doc.GetProperties())
                        {
                            if (p.DataType.ObjectGuid == rd.DimensionCatalogGuid)
                            {
                                found++;
                                if (!foundDic.TryGetValue(rd, out var d_rd)) { d_rd = new(); foundDic[rd] = d_rd; }
                                if (!d_rd.TryGetValue(level, out var d_rd_level)) { d_rd_level = new(); d_rd[level] = d_rd_level; }
                                if (!d_rd_level.TryGetValue(pos, out var d_rd_level_pos)) { d_rd_level_pos = new(); d_rd_level[pos] = d_rd_level_pos; }
                                d_rd_level_pos.Add(p);
                            }
                        }
                        found += this.TryFindPropertyByCatalogGuid(doc.GroupDetails, rd, level);
                        // Chack if dimension catalog reference can be found
                        if (found == 0)
                        {
                            var vf = new ValidationFailure(nameof(r.ListDocGuids),
                                $"Document '{doc.Name}' doesn't have property of type catalog '{cat.Name}' which is required by dimension {rd.Name}.");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        else if (found > 1)
                        {
                            // check if explicitly mapped
                            bool isExplicitlyMapped = false;
                            foreach (var dm in r.ListDocMappings)
                            {
                                if (dm.DocGuid != doc.Guid)
                                    continue;
                                foreach (var m in dm.ListMapings)
                                {
                                    if (string.IsNullOrEmpty(m.DocPropGuid))
                                        continue;
                                    var p = (IProperty)rd.Cfg.DicNodes[m.DocPropGuid];
                                    if (string.IsNullOrEmpty(p.DataType.ObjectGuid))
                                        continue;
                                    if (p.DataType.ObjectGuid == rd.DimensionCatalogGuid)
                                    {
                                        isExplicitlyMapped = true;
                                        break;
                                    }
                                }
                                if (isExplicitlyMapped)
                                    break;
                            }
                            if (!isExplicitlyMapped)
                            {
                                var vf = new ValidationFailure(nameof(r.ListDocGuids),
                                    $"Document '{doc.Name}' has more than one property of type catalog '{cat.Name}' which is required by dimension {rd.Name}. Need manual mapping.");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
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
                    foundDic.Clear();
                }
            });
        }
        // IRegisterDimension, tree level (0 for root), detail position (0 for root), property
        private Dictionary<IRegisterDimension, Dictionary<int, Dictionary<uint, List<IProperty>>>> foundDic = new();
        private int TryFindPropertyByCatalogGuid(IGroupListDetails gd, IRegisterDimension rd, int level)
        {
            level++;
            int found = 0;
            foreach (var t in gd.ListDetails)
            {
                var pos = t.Position;
                foreach (var p in t.GroupProperties.ListProperties)
                {
                    if (p.DataType.ObjectGuid == rd.DimensionCatalogGuid)
                    {
                        found++;
                        if (!foundDic.TryGetValue(rd, out var d_rd)) { d_rd = new(); foundDic[rd] = d_rd; }
                        if (!d_rd.TryGetValue(level, out var d_rd_level)) { d_rd_level = new(); d_rd[level] = d_rd_level; }
                        if (!d_rd_level.TryGetValue(pos, out var d_rd_level_pos)) { d_rd_level_pos = new(); d_rd_level[pos] = d_rd_level_pos; }
                        d_rd_level_pos.Add(p);
                    }
                }
                found += this.TryFindPropertyByCatalogGuid(t.GroupDetails, rd, level);
            }
            return found;
        }
    }
}
