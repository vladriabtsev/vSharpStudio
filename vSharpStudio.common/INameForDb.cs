using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public interface INameForDb
    {
        /// <summary>
        /// 1. Model.IsCompositNames=false and Model.isUseGroupPrefix=false
        ///     Model objects names are used for DB tables names
        ///     Pros: Short names
        ///     Cons: All objects names, which translated in DB tables names, has to be different
        ///     
        /// 2. Model.IsCompositNames=true
        ///     For properties tabs names will have their parents names as prefix
        ///     Pros: Same name can be used in different groups of objects. Db names for subtables will be shown together with their parents
        ///     Cons: Db table name can be very long lenth
        ///     
        /// 3. Model.isUseGroupPrefix=true
        ///     Objects DB names will contains prefix, specified in group of objects. ('Doc' is default prefix for documents for example)
        ///     Pros: Same name can be used in different groups of objects. Db names for subtables will be shown together with their parents
        ///     Cons: Db table name can be very long lenth
        ///     
        /// </summary>
        string NameForDb { get; }
    }
}
