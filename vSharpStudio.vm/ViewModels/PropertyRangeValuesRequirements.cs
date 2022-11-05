using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class PropertyRangeValuesRequirements : IPropertyRangeValuesRequirements
    {
        public static PropertyRangeValuesRequirements GetRangeValidation(Property p)
        {
            PropertyRangeValuesRequirements res = new PropertyRangeValuesRequirements();
            res.ValidateRangeValuesRequirements(p);
            p.RangeValuesRequirements = res;
            return res;
        }
        public void ValidateRangeValuesRequirements(Property p)
        {
            ReadOnlySpan<char> req = p.RangeValuesRequirementStr;
            req = req.Trim();
            if (req.Length == 0)
                return;
            if (p.DataTypeEnum == EnumDataType.STRING)
            {
                if (req[0] != '\"')
                {
                    this.AddError($"Can't parse string value from \"{req.ToString()}\". Expected '\"' symbol at the begining of string");
                    return;
                }
                int posStart = 0;
                int posEnd = 0;
                int pos = 1;
                while (pos < req.Length)
                {
                    var r = req.Slice(pos);
                    var indx = r.IndexOf('\"');
                    if (indx == -1)
                    {
                        this.AddError($"Can't parse string value from \"{req.ToString()}\". Expected '\"' symbol at the end of string");
                        return;
                    }
                    else
                    {
                        pos = pos + indx;
                        if (req[pos - 1] != '\\')
                        {
                            posEnd = pos;
                            var b = req.Slice(posStart, posEnd - posStart + 1);
                            this.AddValue(b.ToString());
                            pos = posEnd + 1;
                            r = req.Slice(pos);
                            var l = r.Length;
                            r = r.TrimStart();
                            pos = pos + l - r.Length;
                            if (r.Length > 0)
                            {
                                if (r[0] != ';')
                                {
                                    this.AddError($"Can't parse string value from \"{req.ToString()}\". Expected ';' symbol after \"{b.ToString()}\"");
                                    return;
                                }
                                pos++;
                                r = req.Slice(pos);
                                l = r.Length;
                                r = r.TrimStart();
                                pos = pos + l - r.Length;
                                if (r[0] != '\"')
                                {
                                    this.AddError($"Can't parse string value from \"{req.ToString()}\". Expected '\"' symbol after \"{req.Slice(0, pos).ToString()}\"");
                                    return;
                                }
                                posStart = pos;
                                pos++;
                            }
                        }
                    }
                }
            }
            else
            {
                int pos = -1;
                ReadOnlySpan<char> b;
                do
                {
                    req = req.Slice(pos + 1);
                    pos = req.IndexOf(';');
                    if (pos == -1)
                        b = req;
                    else
                        b = req.Slice(0, pos);
                    if (!ValidateRange(p, b.Trim()))
                        break;
                } while (pos != -1);
            }
        }
        private bool ValidateRange(Property p, ReadOnlySpan<char> range)
        {
            if (p.DataTypeEnum == EnumDataType.STRING)
            {
                var a = ValidateBoundary(p, range);
                if (!a.Item1)
                    return false;
                this.AddValue(range.ToString());
            }
            else
            {
                var pos1 = range.IndexOf('#');
                if (pos1 == -1) // no range, one value
                {
                    var a = ValidateBoundary(p, range);
                    if (!a.Item1)
                        return false;
                    this.AddValue(range.ToString());
                }
                else
                {
                    var bound = new ValidationBoundary();
                    if (pos1 == 0) // range with left infinite bound
                    {
                        var r1 = range.Slice(pos1 + 1);
                        var b = ValidateBoundary(p, r1.Trim());
                        if (!b.Item1)
                            return false;
                        bound.BoundaryMax = r1.ToString();
                        this.AddBoundary(bound);
                    }
                    else
                    {
                        var r1 = range.Slice(0, pos1);
                        var pos2 = range.Slice(pos1 + 1).IndexOf('#');
                        if (pos2 == -1) // normal range with two boundaries
                        {
                            var r = r1.Trim();
                            var a = ValidateBoundary(p, r);
                            if (!a.Item1)
                                return false;
                            bound.BoundaryMin = r.ToString();
                            var r2 = range.Slice(pos1 + 1);
                            r = r2.Trim();
                            var b = ValidateBoundary(p, r);
                            if (!b.Item1)
                                return false;
                            if (r.Length > 0)
                                bound.BoundaryMax = r.ToString();
                            if (a.Item2 != null && b.Item2 != null && a.Item2.CompareTo(b.Item2) > 0)
                            {
                                this.AddError($"Left limit of range is greater than right limit of range \"{range.ToString()}\"");
                                return false;
                            }
                            this.AddBoundary(bound);
                        }
                        else // wrong range
                        {
                            this.AddError($"Can't parse data range \"{range.ToString()}\". Limit separator '#' used more than ones. Sample of usage: 1;3-4;6");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private Tuple<bool, IComparable?> ValidateBoundary(Property p, ReadOnlySpan<char> boundary)
        {
            if (boundary.Length == 0)
                return new Tuple<bool, IComparable?>(true, null);
            if (p.DataTypeEnum == EnumDataType.NUMERICAL)
            {
                var res = CanParse(boundary.ToString(), p);
                if (!string.IsNullOrWhiteSpace(res.Item1))
                {
                    this.AddError(res.Item1);
                    return new Tuple<bool, IComparable?>(false, null);
                }
                //this.AddValue(boundary.ToString());
                return new Tuple<bool, IComparable?>(true, res.Item2);
            }
            else if (p.DataTypeEnum == EnumDataType.STRING)
            {
                if (boundary[0] != '\"')
                {
                    this.AddError($"Can't parse string value from \"{boundary.ToString()}\". Expected '\"' symbol at the begining of string");
                    return new Tuple<bool, IComparable?>(false, null);
                }
                if (boundary[boundary.Length - 1] != '\"')
                {
                    this.AddError($"Can't parse string value from \"{boundary.ToString()}\". Expected '\"' symbol at the end of string");
                    return new Tuple<bool, IComparable?>(false, null);
                }
                var r = boundary.Slice(1, boundary.Length - 2);
                var indx = r.IndexOf('\"');
                while (indx != -1)
                {
                    if (indx == 0 || r[indx - 1] != '\\')
                    {
                        this.AddError($"Can't parse string value from \"{boundary.ToString()}\". Unexpected '\"' symbol inside of string");
                        return new Tuple<bool, IComparable?>(false, null);
                    }
                    r = r.Slice(indx + 1);
                    indx = r.IndexOf('\"');
                }
                //this.AddValue(boundary.ToString());
                return new Tuple<bool, IComparable?>(true, null);
            }
            else if (//p.DataTypeEnum == EnumDataType.DATETIME || 
                p.DataTypeEnum == EnumDataType.DATETIMELOCAL || 
                p.DataTypeEnum == EnumDataType.DATETIMEUTC || 
                //p.DataTypeEnum == EnumDataType.DATETIMEZ || 
                p.DataTypeEnum == EnumDataType.DATE)
            {
                DateTime res;
                string err = "";
                var str = boundary.ToString();
                if (!DateTime.TryParse(str, out res))
                {
                    try
                    {
                        DateTime.Parse(str);
                    }
                    catch (Exception ex)
                    {
                        err = ex.Message;
                    }
                    this.AddError($"Can't parse \"{str}\" value by DateTime.TryParse(). Error: {err}");
                    return new Tuple<bool, IComparable?>(false, null);
                }
                //this.AddValue(boundary.ToString());
                return new Tuple<bool, IComparable?>(true, res);
            }
            else if (p.DataTypeEnum == EnumDataType.CHAR)
            {
                if (boundary.Length != 3 || boundary[0] != '\'' || boundary[2] != '\'')
                {
                    this.AddError($"Can't parse char value from \"{boundary.ToString()}\". Expected value like \"'a'\"");
                    return new Tuple<bool, IComparable?>(false, null);
                }
                //this.AddValue(boundary.ToString());
                return new Tuple<bool, IComparable?>(true, boundary[1]);
            }
            return new Tuple<bool, IComparable?>(true, null);
        }
        private Tuple<string?, IComparable> CanParse(string x, Property p)
        {
            var dt = p.DataType;
            if (p.Accuracy == 0)
            {
                if (p.IsPositive)
                {
                    if (dt.MaxNumericalValue <= byte.MaxValue)
                    {
                        byte res;
                        if (!byte.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by byte.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.MaxNumericalValue <= ushort.MaxValue)
                    {
                        ushort res;
                        if (!ushort.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by ushort.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.MaxNumericalValue <= uint.MaxValue)
                    {
                        uint res;
                        if (!uint.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by uint.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.MaxNumericalValue <= ulong.MaxValue) // long, not ulong
                    {
                        ulong res;
                        if (!ulong.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by ulong.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.Length <= 28)
                    {
                        decimal res;
                        if (!decimal.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by decimal.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    throw new Exception("Not supported operation");
                    // return "BigInteger" + sn;
                }
                else
                {
                    if (dt.MaxNumericalValue <= sbyte.MaxValue)
                    {
                        sbyte res;
                        if (!sbyte.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by sbyte.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.MaxNumericalValue <= short.MaxValue)
                    {
                        short res;
                        if (!short.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by short.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.MaxNumericalValue <= int.MaxValue)
                    {
                        int res;
                        if (!int.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by int.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.MaxNumericalValue <= long.MaxValue)
                    {
                        long res;
                        if (!long.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by long.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    else if (dt.Length <= 28)
                    {
                        decimal res;
                        if (!decimal.TryParse(x, out res))
                            return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by decimal.TryParse()", res);
                        return new Tuple<string?, IComparable>(null, res);
                    }
                    throw new Exception("Not supported operation");
                    // return "BigInteger" + sn;
                }
            }
            else
            {
                // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                if (dt.Length == 0)
                {
                    //BigDecimal res;
                    //if (!BigDecimal.TryParse(x, out res))
                    //    return $"Can't parse value by BigDecimal.TryParse()";
                }
                else if (dt.Length <= 6)
                {
                    float res;
                    if (!float.TryParse(x, out res))
                        return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by float.TryParse()", res);
                    return new Tuple<string?, IComparable>(null, res);
                }
                else if (dt.Length <= 15)
                {
                    double res;
                    if (!double.TryParse(x, out res))
                        return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by double.TryParse()", res);
                    return new Tuple<string?, IComparable>(null, res);
                }
                else if (dt.Length < 29)
                {
                    decimal res;
                    if (!decimal.TryParse(x, out res))
                        return new Tuple<string?, IComparable>($"Can't parse value \"{x}\" by decimal.TryParse()", res);
                    return new Tuple<string?, IComparable>(null, res);
                }
                throw new Exception("Not supported operation");
                // return "BigDecimal";
            }
        }
        public PropertyRangeValuesRequirements()
        {
            this._ListBoundaries = new List<ValidationBoundary>();
            this._ListValues = new List<string>();
            this._ListErrors = new List<string>();
        }
        public bool IsHasRequirements { get { return _ListBoundaries.Count > 0 || _ListValues.Count > 0; } }
        public IReadOnlyList<IValidationBoundary> ListBoundaries { get { return _ListBoundaries; } }
        private List<ValidationBoundary> _ListBoundaries;
        public IReadOnlyList<string> ListValues { get { return _ListValues; } }
        private List<string> _ListValues;
        public bool IsHasErrors { get { return _ListErrors.Count > 0; } }
        public IReadOnlyList<string> ListErrors { get { return _ListErrors; } }
        private List<string> _ListErrors;
        public void AddBoundary(ValidationBoundary boundary)
        {
            _ListBoundaries.Add(boundary);
        }
        public void AddValue(string val)
        {
            _ListValues.Add(val);
        }
        public void AddError(string error)
        {
            _ListErrors.Add(error);
        }
    }
    public class ValidationBoundary : IValidationBoundary
    {
        public string? BoundaryMin { get; set; }
        public string? BoundaryMax { get; set; }
    }
}
