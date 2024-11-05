using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public  interface IDbNamesValidator
    {
        /// <summary>
        /// Validate name for DB table name
        /// </summary>
        /// <param name="name">Name for DB table name</param>
        /// <returns>Return null if name is valid DB table name.</returns>
        string? TableNameValidation(string name);
        /// <summary>
        /// Validate name for DB field name
        /// </summary>
        /// <param name="name">Name for DB field name</param>
        /// <returns>Return null if name is valid DB field name.</returns>
        string? FieldNameValidation(string name);
    }
}
