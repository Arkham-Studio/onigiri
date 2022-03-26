using System;

namespace Arkham.Onigiri.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class EditorIconAttribute : Attribute
    {

        public EditorIconAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

}