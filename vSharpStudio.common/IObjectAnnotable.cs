using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    //
    // Summary:
    //     An arbitrary piece of metadata that can be stored on an object that implements
    //     Microsoft.EntityFrameworkCore.Infrastructure.IAnnotatable.
    //     This interface is typically used by database providers (and other extensions).
    //     It is generally not used in application code.
    public class Annotation
    {
        public Annotation(string name, object val)
        {
            this.Name = name;
            this.Value = val;
        }
        //
        // Summary:
        //     Gets the key of this annotation.
        public string Name { get; set; }
        //
        // Summary:
        //     Gets the value assigned to this annotation.
        public object Value { get; set; }
    }
    //
    // Summary:
    //     A class that exposes annotations that can be modified. Annotations allow for
    //     arbitrary metadata to be stored on an object.
    //     This interface is typically used by database providers (and other extensions).
    //     It is generally not used in application code.
    //[DefaultMember("Item")]
    public interface IObjectAnnotatable
    {
        //
        // Summary:
        //     Gets or sets the value of the annotation with the given name.
        //
        // Parameters:
        //   name:
        //     The name of the annotation.
        //
        // Returns:
        //     The value of the existing annotation if an annotation with the specified name
        //     already exists. Otherwise, null.
        object this[string name] { get; set; }

        //
        // Summary:
        //     Adds an annotation to this object. Throws if an annotation with the specified
        //     name already exists.
        //
        // Parameters:
        //   name:
        //     The name of the annotation to be added.
        //
        //   value:
        //     The value to be stored in the annotation.
        //
        // Returns:
        //     The newly added annotation.
        Annotation AddAnnotation(string name, object value);
        //
        // Summary:
        //     Removes the given annotation from this object.
        //
        // Parameters:
        //   name:
        //     The name of the annotation to remove.
        //
        // Returns:
        //     The annotation that was removed.
        Annotation RemoveAnnotation(string name);
        //
        // Summary:
        //     Sets the annotation stored under the given key. Overwrites the existing annotation
        //     if an annotation with the specified name already exists.
        //
        // Parameters:
        //   name:
        //     The name of the annotation to be added.
        //
        //   value:
        //     The value to be stored in the annotation.
        void SetAnnotation(string name, object value);




        //
        // Summary:
        //     Gets the annotation with the given name, returning null if it does not exist.
        //
        // Parameters:
        //   name:
        //     The name of the annotation to find.
        //
        // Returns:
        //     The existing annotation if an annotation with the specified name already exists.
        //     Otherwise, null.
        Annotation FindAnnotation(string name);
        //
        // Summary:
        //     Gets all annotations on the current object.
        IEnumerable<Annotation> GetAnnotations();

    }
}
